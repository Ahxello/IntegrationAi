using System.Windows;
using Autofac;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Infrastructure.Common;
using IntegrationAi.Infrastructure.Settings;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;

    public Bootstrapper()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder
            .RegisterModule<Infrastructure.RegistrationModule>()
            .RegisterModule<ViewModels.RegistrationModule>()
            .RegisterModule<Views.RegistrationModule>()
            .RegisterModule<RegistrationModule>();
        _container = containerBuilder.Build();
    }
    public Window Run()
    {
        InitializeDependencies();

        var mainWindowViewModelFactory = _container.Resolve<IFactory<IMainWindowViewModel>>();

        var mainWindowViewModel = mainWindowViewModelFactory.Create();

        var windowManager = _container.Resolve<IWindowManager>();

        var mainWindow = windowManager.Show(mainWindowViewModel);

        if (mainWindow is not Window window) throw new NotImplementedException();


        return window;
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IPathServiceInitializer>().Initialize();
        var windowSettingsWrapperInitializers =
            _container.Resolve<IEnumerable<IWindowSettingsWrapperInitializer>>();

        foreach (var windowSettingsWrapperInitializer in windowSettingsWrapperInitializers)
            windowSettingsWrapperInitializer.Initialize();
    }

    public void Dispose()
    {
        _container.Dispose();
    }

}