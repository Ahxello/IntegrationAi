using System.ComponentModel;
using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.Dialogs;

public class InputWindowViewModel : WindowViewModel<IInputWindowSettingsWrapper>, IInputWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private readonly IFactory<IMainWindowMenuViewModel> _mainWindowMenuViewModelFactory;
    private string _userInput;
    private readonly Command _confirmCommand;
    private readonly Command _cancelCommand;

    public InputWindowViewModel(IInputWindowSettingsWrapper windowSettingsWrapper,
        IWindowManager windowManager) : base(windowSettingsWrapper)
    {

        _windowManager = windowManager;
        _confirmCommand = new Command(Confirm);
        _cancelCommand = new Command(Cancel);
    }

    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            InvokePropertyChanged(nameof(UserInput));
        }
    }

    public ICommand ConfirmCommand => _confirmCommand;
    public ICommand CancelCommand => _cancelCommand;

    public event EventHandler RequestClose;

    private void Confirm()
    {
        RequestClose?.Invoke(this, EventArgs.Empty);
    }
    private void Cancel()
    {
        OnRequestClose();
    }
    protected virtual void OnRequestClose()
    {
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

}