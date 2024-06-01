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
    public IMainWindowMenuViewModel MenuViewModel { get; }
    private readonly Command _confirmCommand;
    private readonly Command _cancelCommand;

    private bool? _dialogResult;

    public InputWindowViewModel(IInputWindowSettingsWrapper windowSettingsWrapper,
        IWindowManager windowManager,
        IFactory<IMainWindowMenuViewModel> mainWindowMenuViewModelFactory) : base(windowSettingsWrapper)
    {

        _windowManager = windowManager;
        _mainWindowMenuViewModelFactory = mainWindowMenuViewModelFactory;
        _confirmCommand = new Command(Confirm);
        _cancelCommand = new Command(Cancel);
    }

    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            InvokePropertyChanged();
        }
    }

    public ICommand ConfirmCommand => _confirmCommand;
    public ICommand CancelCommand => _cancelCommand;

    public event EventHandler RequestClose;
    public bool? DialogResult
    {
        get { return _dialogResult; }
        set
        {
            _dialogResult = value;
            InvokePropertyChanged();
        }
    }

    private void Confirm()
    {
        var menuViewModel = _mainWindowMenuViewModelFactory.Create();
        
    }
    private void Cancel()
    {
        DialogResult = false;
        OnRequestClose();
    }
    protected virtual void OnRequestClose()
    {
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

}