using System.Text.Json;
using Newtonsoft.Json.Linq;
namespace IntegrationAi.Domain.Messages;

public class ResponseParser : IResponseParser
{
    public async Task<string> GetMessageAsync(string json)
    {
        try
        {
            JObject parsedJson = JObject.Parse(json);
            string text = parsedJson["result"]["alternatives"][0]["message"]["text"].ToString();
            return text;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Ошибка при парсинге JSON: {ex.Message}");
            return string.Empty;
        }
    }
}