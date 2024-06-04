using IntegrationAi.ViewModels.Dialogs;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.Views.MainWindow;
using System.Windows;

namespace IntegrationAi.Views.InputWindow
{

    public partial class InputDialogWindow : IInputDialogWindow
    {
        public string UserInput => ItemNameTextBox.Text;
        public InputDialogWindow(IInputWindowViewModel inputWindowViewModel)
        {
            InitializeComponent();

            DataContext = inputWindowViewModel;
        }
    }
}
