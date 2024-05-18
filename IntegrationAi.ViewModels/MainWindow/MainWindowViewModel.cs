using System.Windows.Input;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IMainWindowSettingsWrapper _mainWindowSettingsWrapper;
    private readonly IWindowManager _windowManager;
    private readonly Command _closeMainWindowCommand;

    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper, 
        IWindowManager windowManager)
    {
        _mainWindowSettingsWrapper = mainWindowSettingsWrapper;
        _windowManager = windowManager;

        _closeMainWindowCommand = new Command(CloseMainWindow);
    }

    public double Left
    {
        get => _mainWindowSettingsWrapper.Left;
        set => _mainWindowSettingsWrapper.Left = value;
    }

    public double Top
    {
        get => _mainWindowSettingsWrapper.Top;
        set => _mainWindowSettingsWrapper.Top = value;
    }

    public double Width
    {
        get => _mainWindowSettingsWrapper.Width;
        set => _mainWindowSettingsWrapper.Width = value;
    }

    public double Height
    {
        get => _mainWindowSettingsWrapper.Height;
        set => _mainWindowSettingsWrapper.Height = value;
    }

    public bool isMaximized
    {
        get => _mainWindowSettingsWrapper.isMaximized;
        set => _mainWindowSettingsWrapper.isMaximized = value;
    }

    public string Title => "IntegrationAI";

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    
    private void CloseMainWindow()
    {
        _windowManager.Close(this);
    }
}