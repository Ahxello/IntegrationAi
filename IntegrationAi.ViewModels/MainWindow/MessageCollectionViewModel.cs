using AiTestLibrary.Classes;
using AiTestLibrary.Interfaces;
using IntegrationAi.ViewModels.Messages;
using System.Linq;
using IntegrationAi.Domain.Messages;

namespace IntegrationAi.ViewModels.MainWindow;

public class MessageCollectionViewModel : IMessageCollectionViewModel
{
    private readonly IYandexGpt _yandexGpt;
    private readonly IResponseParser _responseParser;

    public MessageCollectionViewModel(IYandexGpt yandexGpt, IResponseParser responseParser)
    {
        _yandexGpt = yandexGpt;
        _responseParser = responseParser;
    }

    public IEnumerable<MessageCollectionItemViewModel> Items { get; private set; } =
        Enumerable.Empty<MessageCollectionItemViewModel>();

    public async Task InitializeAsync()
    {
        var iamtoken = "t1.9euelZqezpSRyZaYl4yXi8mZkJyLze3rnpWam46WzZKSlMaVjsycjZCZksjl8_ddQk1N-e8iWTgd_t3z9x1xSk357yJZOB3-zef1656VmpGansaXnpCZysqbyMebyZrO7_zF656VmpGansaXnpCZysqbyMebyZrO.AZeQC9qJt5SK2r-ktnz8Zf-ce0VbmAdQn6K7sKMc37NhJC0yj5WpbqyqCurHoHsDlHbClVSjr_nqfL8W42hkBQ"; var foledrId = "b1gphb1c693npe94nmrv";
        var messageCollection =
            await _yandexGpt.Request("Привет", iamtoken, foledrId );
        var msg = _responseParser.GetMessageAsync(messageCollection);
        var messageViewModel = new MessageCollectionItemViewModel(msg.Result);
        Items = new List<MessageCollectionItemViewModel> { messageViewModel };

    }
}