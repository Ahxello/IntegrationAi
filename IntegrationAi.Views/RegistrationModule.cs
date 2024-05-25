using Autofac;
using IntegrationAi.ViewModels.Services;
using IntegrationAi.ViewModels.Windows;
using IntegrationAi.Views.MainWindow;
using IntegrationAi.Views.Windows;

namespace IntegrationAi.Views;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<MainWindow.MainWindow>()
            .As<IMainWindow>().InstancePerDependency();
        builder.RegisterType<WindowManager>()
            .As<IWindowManager>().SingleInstance();
        builder.RegisterType<DialogService>()
            .As<IDialogService>().SingleInstance();
    }
}