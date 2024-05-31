using System.ComponentModel;
using System.Windows.Input;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.ViewModels.Dialogs;

public class InputWindowViewModel : WindowViewModel<IInputWindowSettingsWrapper>, IInputWindowViewModel
{
        public InputWindowViewModel(IInputWindowSettingsWrapper windowSettingsWrapper) : base(windowSettingsWrapper)
    {
        InputSubmittedCommand = new Command(OnInputSubmitted);
    }
    private string _userInput;
    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            OnPropertyChanged(nameof(UserInput));
        }
    }

    public ICommand InputSubmittedCommand { get; }

    public event Action<string> InputSubmitted;


    private void OnInputSubmitted()
    {
        InputSubmitted?.Invoke(UserInput);
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}