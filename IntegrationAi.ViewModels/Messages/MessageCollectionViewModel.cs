using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using AiTestLibrary.Interfaces;
using GemBox.Document;
using IntegrationAi.Domain.Messages;
using IntegrationAi.ViewModels.Windows;
using Microsoft.Win32;
using TextBox = System.Windows.Controls.TextBox;

namespace IntegrationAi.ViewModels.Messages;

public class MessageCollectionViewModel : IMessageCollectionViewModel
{
    private readonly IYandexGpt _yandexGpt;
    private readonly IResponseParser _responseParser;
    public MessageCollectionViewModel(IYandexGpt yandexGpt, IResponseParser responseParser, IEnumerable<MessageCollectionItemViewModel> items)
    {
        _yandexGpt = yandexGpt;
        _responseParser = responseParser;
    }

    private string iamtoken =
        "t1.9euelZqalc2ans7Gy8-ZnZjMzJvMyO3rnpWam46WzZKSlMaVjsycjZCZksjl8_cYLxRN-e84S0VL_t3z91hdEU357zhLRUv-zef1656VmpGMiZjHnsuOjZSQjpmYjsyY7_zF656VmpGMiZjHnsuOjZSQjpmYjsyY.KytXT0IQvmq_IiKsHK2uZ0AdqIv7RtYurLhgFbSCzl1xPhu0ocLZuCRPderwHAleadc3xdXuQL7yF50iy_YlCQ";
    public async Task AddPropeties(List<string> items)
    {
        var foledrId = "b1gphb1c693npe94nmrv";

        string result = string.Join(" ", items);

        var messageCollection =
            await _yandexGpt.Request($"{result} Опиши свойства для сущности из списка " +
                                     $"для дальнейшего внесения данных " +
                                     $"в БД с их типами данных"
                                     , iamtoken, foledrId);
        var msg = _responseParser.GetMessageAsync(messageCollection);

        var messageViewModel = new MessageCollectionItemViewModel(msg.Result);

        Items = new List<MessageCollectionItemViewModel> { messageViewModel };
    }

    public async Task AddRelatedEntites(List<string> items, string userInput)
    {
        var foledrId = "b1gphb1c693npe94nmrv";

        string result = string.Join(" ", items);

        var messageCollection =
            await _yandexGpt.Request($"{result} Дай мне {userInput} сущностей, расширяющих мой список"
                , iamtoken, foledrId);

        var msg = _responseParser.GetMessageAsync(messageCollection);

        var messageViewModel = new MessageCollectionItemViewModel(msg.Result);

        Items = new List<MessageCollectionItemViewModel> { messageViewModel };
    }


    public async Task InitializeAsync(List<string> items)
    {
        var foledrId = "b1gphb1c693npe94nmrv";
        
        string result = string.Join(" ", items);

        var messageCollection =
            await _yandexGpt.Request($"{result} Выбери из списка сущности и выведи их без описания.", iamtoken, foledrId );
        
        var msg = _responseParser.GetMessageAsync(messageCollection);
        
        var messageViewModel = new MessageCollectionItemViewModel(msg.Result);
        
        Items = new List<MessageCollectionItemViewModel> { messageViewModel };

    }

    public IEnumerable<MessageCollectionItemViewModel> Items { get; private set; }
        = Enumerable.Empty<MessageCollectionItemViewModel>();

    public async Task OpenFileDialog()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Text Files (*.txt)|*.txt|Word Documents (*.docx)|*.docx|All Files (*.*)|*.*"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var filePath = openFileDialog.FileName;
            var fileExtension = Path.GetExtension(filePath);

            if (fileExtension == ".docx")
            {
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                var document = DocumentModel.Load(filePath);
                var messageViewModel = new MessageCollectionItemViewModel(document.Content.ToString());
                Items = new List<MessageCollectionItemViewModel> { messageViewModel };
            }

            else if (fileExtension == ".txt")
            {
                var messageViewModel = new MessageCollectionItemViewModel(File.ReadAllText(filePath));
                Items = new List<MessageCollectionItemViewModel> { messageViewModel };
                
            }
        }
        
    }
}