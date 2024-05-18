using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public abstract class WindowViewModel<TWindowSettingsWrapper>
    where TWindowSettingsWrapper : class, IWindowSettingsWrapper
{
    private readonly IWindowSettingsWrapper _windowSettingsWrapper;

    protected WindowViewModel(TWindowSettingsWrapper windowSettingsWrapper)
    {
        _windowSettingsWrapper = windowSettingsWrapper;
    }
    public double Left
    {
        get => _windowSettingsWrapper.Left;
        set => _windowSettingsWrapper.Left = value;
    }

    public double Top
    {
        get => _windowSettingsWrapper.Top;
        set => _windowSettingsWrapper.Top = value;
    }

    public double Width
    {
        get => _windowSettingsWrapper.Width;
        set => _windowSettingsWrapper.Width = value;
    }

    public double Height
    {
        get => _windowSettingsWrapper.Height;
        set => _windowSettingsWrapper.Height = value;
    }

    public bool isMaximized
    {
        get => _windowSettingsWrapper.isMaximized;
        set => _windowSettingsWrapper.isMaximized = value;
    }

}