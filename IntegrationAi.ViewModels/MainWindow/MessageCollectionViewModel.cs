using AiTestLibrary.Interfaces;
using IntegrationAi.ViewModels.Messages;

namespace IntegrationAi.ViewModels.MainWindow;

public class MessageCollectionViewModel : IMessageCollectionViewModel
{
    private readonly IGigaChatGpt _gigaChatGpt;

    public MessageCollectionViewModel(IGigaChatGpt gigaChatGpt)
    {
        _gigaChatGpt = gigaChatGpt;
    }

    public IEnumerable<MessageCollectionItemViewModel> Items { get; private set; } =
        Enumerable.Empty<MessageCollectionItemViewModel>();

    public async Task InitializeAsync()
    {
        var messageCollection =
            await _gigaChatGpt.Request("Привет", "d1e2fd22-0042-4c70-bd32-b23182f4757d");
        Items = messageCollection.Select(response
            => new MessageCollectionItemViewModel(response.ToString()));
    }
}