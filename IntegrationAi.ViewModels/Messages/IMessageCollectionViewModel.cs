using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.ViewModels.Messages;

public interface IMessageCollectionViewModel : IMainWindowContentViewModel
{
    Task OpenFileDialog();
    Task InitializeAsync(List<string> items);
    Task AddPropeties(List<string> items);
    Task AddMessage(List<string> items);

    Task AddRelatedEntites(List<string> items, string userInput);

    IEnumerable<MessageCollectionItemViewModel> Items { get; }

}