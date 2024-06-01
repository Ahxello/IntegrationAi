using IntegrationAi.ViewModels.Windows;
using System.Windows.Input;

namespace IntegrationAi.ViewModels.Dialogs;

public interface IInputWindowViewModel : IWindowViewModel
{
    ICommand ConfirmCommand { get; }
    ICommand CancelCommand { get; }
    string UserInput { get; set; }
    bool? DialogResult { get; set; }

}