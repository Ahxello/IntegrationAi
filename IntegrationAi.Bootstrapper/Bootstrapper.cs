using System.Windows;
using Autofac;
using IntegrationAi.ViewModels;
using IntegrationAi.Views.MainWindow;

namespace IntegrationAi.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;

    public Bootstrapper()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder
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
        var mainWindow = _container.Resolve<IMainWindow>();
        if (mainWindow is not Window window) throw new NotImplementedException();
        window.Show();
        return window;
    }
}