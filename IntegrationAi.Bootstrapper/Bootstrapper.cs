using System.Net.Http.Headers;
using System.Windows;
using IntegrationAi.Views;

namespace IntegrationAi.Bootstrapper;

public class Bootstrapper : IDisposable
{
    public Window Run()
    {
        var mainWindow = new MainWindow();
         mainWindow.Show();
         return mainWindow;
    }

    public void Dispose()
    {

    }
}