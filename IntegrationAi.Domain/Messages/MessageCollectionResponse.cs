namespace IntegrationAi.Domain.Messages;

public class MessageCollectionResponse
{
    public required IEnumerable<MessageResponse> Items { get; init; }
}