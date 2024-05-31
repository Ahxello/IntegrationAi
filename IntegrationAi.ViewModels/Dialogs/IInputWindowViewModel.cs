using IntegrationAi.ViewModels.Windows;
using System.Windows.Input;

namespace IntegrationAi.ViewModels.Dialogs;

public interface IInputWindowViewModel : IWindowViewModel
{
    ICommand InputSubmittedCommand { get; }
    string UserInput { get; set; }
}