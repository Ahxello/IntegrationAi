using Autofac;
using IntegrationAi.Domain.Settings;
using IntegrationAi.Infrastructure.Settings;

namespace IntegrationAi.Infrastructure;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowSettingsWrapper>()
            .As<IMainWindowSettingsWrapper>()
            .As<IMainWindowSettingsWrapperInitializer>()
            .SingleInstance();
    }
}