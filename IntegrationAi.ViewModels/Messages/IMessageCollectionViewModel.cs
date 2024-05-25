using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.ViewModels.Messages;

public interface IMessageCollectionViewModel : IMainWindowContentViewModel
{
    Task OpenFileDialog();
    Task InitializeAsync();
}