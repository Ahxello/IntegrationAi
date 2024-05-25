using System.Windows.Input;
using IntegrationAi.ViewModels.Commands;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowMenuViewModel : IMainWindowMenuViewModel
{
    public Command _closeMainWindowCommand;
    public MainWindowMenuViewModel()
    {
        _closeMainWindowCommand = new Command(CloseMainWindow);
    }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;

    private void CloseMainWindow()
    {
        MainWindowClosingRequested?.Invoke();
    }

    public event Action? MainWindowClosingRequested;
}