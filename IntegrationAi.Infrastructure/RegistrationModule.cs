using AiTestLibrary.Classes;
using AiTestLibrary.Interfaces;
using Autofac;
using IntegrationAi.Domain.Messages;
using IntegrationAi.Domain.Settings;
using IntegrationAi.Infrastructure.Common;
using IntegrationAi.Infrastructure.Settings;

namespace IntegrationAi.Infrastructure;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowSettingsWrapper>()
            .As<IMainWindowSettingsWrapper>()
            .As<IWindowSettingsWrapperInitializer>()
            .SingleInstance();
        builder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>().SingleInstance();
        builder.RegisterType<ResponseParser>().As<IResponseParser>().SingleInstance();
    }
}