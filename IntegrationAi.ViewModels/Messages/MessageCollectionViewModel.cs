using System.IO;
using System.Windows;
using System.Windows.Interop;
using AiTestLibrary.Interfaces;
using GemBox.Document;
using IntegrationAi.Domain.Messages;
using Microsoft.Win32;

namespace IntegrationAi.ViewModels.Messages;

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
        var iamtoken = "t1.9euelZqQnpidko2Qio6WkM2VjZSJku3rnpWam46WzZKSlMaVjsycjZCZksjl8_cyCUhN-e96J3Jb_N3z93I3RU3573onclv8zef1656VmpfNmoycz4_LlZCPjZiSzY_N7_zF656VmpfNmoycz4_LlZCPjZiSzY_N.grTsh2yJ_td6G6137toYsInAeaCdPyfOVIx2RyHDj5rF1QgKk9uVCj998JP2Dt8URcWct9THzb0IV88nVq_sBA";
        var foledrId = "b1gphb1c693npe94nmrv";
        var messageCollection =
            await _yandexGpt.Request("Привет", iamtoken, foledrId );
        var msg = _responseParser.GetMessageAsync(messageCollection);
        var messageViewModel = new MessageCollectionItemViewModel(msg.Result);
        Items = new List<MessageCollectionItemViewModel> { messageViewModel };

    }

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