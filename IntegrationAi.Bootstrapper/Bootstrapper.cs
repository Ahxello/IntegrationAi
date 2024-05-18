using System.Windows;
using Autofac;
using IntegrationAi.Infrastructure.Settings;
using IntegrationAi.ViewModels;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.ViewModels.Windows;
using IntegrationAi.Views.MainWindow;

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

    public void Dispose()
    {
        _container.Dispose();
    }

    public Window Run()
    {
        InitializeDependencies();

        var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();

        var windowManager = _container.Resolve<IWindowManager>();

        var mainWindow = windowManager.Show(mainWindowViewModel);

        if (mainWindow is not Window window) throw new NotImplementedException();


        return window;
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IMainWindowSettingsWrapperInitializer>().Initialize();
    }
}