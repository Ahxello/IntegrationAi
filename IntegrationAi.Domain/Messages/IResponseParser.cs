namespace IntegrationAi.Domain.Messages;

public interface IResponseParser
{
    string[] GetMessageAsync(string json);
}