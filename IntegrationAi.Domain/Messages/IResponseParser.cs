namespace IntegrationAi.Domain.Messages;

public interface IResponseParser
{
    Task<string> GetMessageAsync(string json);
}