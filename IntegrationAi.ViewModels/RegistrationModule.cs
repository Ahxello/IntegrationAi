using Autofac;
using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<MainWindowViewModel>()
            .As<IMainWindowViewModel>().InstancePerDependency();

    }
}