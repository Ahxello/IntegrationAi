using IntegrationAi.Domain.Settings;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IMainWindowSettingsWrapper _mainWindowSettingsWrapper;

    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper)
    {
        _mainWindowSettingsWrapper = mainWindowSettingsWrapper;
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
}