﻿using Autofac;
using IntegrationAi.Bootstrapper.Factories;
using IntegrationAi.Views.Factories;

namespace IntegrationAi.Bootstrapper;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<WindowFactory>()
            .As<IWindowFactory>().SingleInstance();
    }
}