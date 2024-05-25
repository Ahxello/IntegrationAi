using System.Windows.Input;

namespace IntegrationAi.ViewModels.MainWindow;

public interface IMainWindowMenuViewModel
{
    ICommand CloseMainWindowCommand { get; }

    event Action? MainWindowClosingRequested;
}