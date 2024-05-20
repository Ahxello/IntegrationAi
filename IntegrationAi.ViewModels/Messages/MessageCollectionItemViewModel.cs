namespace IntegrationAi.ViewModels.Messages;

public class MessageCollectionItemViewModel
{
    public MessageCollectionItemViewModel(string message)
    {
        Message = message;
    }
    public string Message { get; init; }
}