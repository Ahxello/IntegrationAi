using Autofac;
using IntegrationAi.Views.MainWindow;

namespace IntegrationAi.Views;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<MainWindow.MainWindow>()
            .As<IMainWindow>().InstancePerDependency();
    }
}