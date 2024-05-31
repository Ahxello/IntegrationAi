using System.IO;
using GemBox.Document;
using Microsoft.Win32;

namespace IntegrationAi.Domain.Services;

public class DialogService : IDialogService
{
    public DialogService()
    {
        
    }

    public string OpenFileDialog()
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
                var document = DocumentModel.Load(filePath);
                return document.Content.ToString();
            }

            else if (fileExtension == ".txt")
                return File.ReadAllText(filePath);
        }

        return "Does not exist";
    }
}