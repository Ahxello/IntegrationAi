using IntegrationAi.ViewModels.Dialogs;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.Views.MainWindow;

namespace IntegrationAi.Views.InputWindow
{

    public partial class InputDialogWindow : IInputDialogWindow
    {
        public InputDialogWindow(IInputWindowViewModel inputWindowViewModel)
        {
            InitializeComponent();

            DataContext = inputWindowViewModel;
        }
    }
}
