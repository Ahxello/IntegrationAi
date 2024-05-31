using System.Windows.Input;
using IntegrationAi.ViewModels.Commands;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowMenuViewModel : IMainWindowMenuViewModel
{
    public Command _closeMainWindowCommand;
    public MainWindowMenuViewModel()
    {
        
    }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    public event Action? ContentViewModelChanged;



    public event Action<IMainWindowContentViewModel>? MainWindowClosingRequested;
}