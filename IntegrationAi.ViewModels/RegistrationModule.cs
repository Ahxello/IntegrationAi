using Autofac;
using IntegrationAi.ViewModels.Extensions;
using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterViewModel<MainWindowViewModel, IMainWindowViewModel>();
        builder.RegisterViewModel<MessageCollectionViewModel, IMessageCollectionViewModel>();
    }
}