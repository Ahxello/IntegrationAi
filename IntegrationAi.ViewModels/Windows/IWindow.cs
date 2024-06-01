using System.ComponentModel;

namespace IntegrationAi.ViewModels.Windows;

public interface IWindow
{
    void Show();
    void Close();

    bool Activate();

    event CancelEventHandler Closing;

    event EventHandler Closed;
}