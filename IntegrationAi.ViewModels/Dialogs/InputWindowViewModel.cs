using System.ComponentModel;
using System.Windows.Input;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.Dialogs;

public class InputWindowViewModel : WindowViewModel<IInputWindowSettingsWrapper>, IInputWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private string _userInput;
    private readonly Command _inputSubmittedCommand;

    public InputWindowViewModel(IInputWindowSettingsWrapper windowSettingsWrapper,
        IWindowManager windowManager) : base(windowSettingsWrapper)
    {
        _windowManager = windowManager;
        _inputSubmittedCommand = new Command(OnInputSubmitted);
    }

    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            OnPropertyChanged(nameof(UserInput));
        }
    }

    public ICommand InputSubmittedCommand => _inputSubmittedCommand;

    public event Action<string> InputSubmitted;


    private void OnInputSubmitted()
    {
        InputSubmitted?.Invoke(UserInput);
    }
    public override void WindowClosing()
    {
        _windowManager.Close(this);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}