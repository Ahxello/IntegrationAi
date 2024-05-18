using System.Windows.Input;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowSettingsWrapper>, IMainWindowViewModel
{
    private readonly Command _closeMainWindowCommand;
    private readonly IWindowManager _windowManager;

    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper,
        IWindowManager windowManager) : base(mainWindowSettingsWrapper)
    {
        _windowManager = windowManager;

        _closeMainWindowCommand = new Command(CloseMainWindow);
    }


    public string Title => "IntegrationAI";

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;

    private void CloseMainWindow()
    {
        _windowManager.Close(this);
    }
}