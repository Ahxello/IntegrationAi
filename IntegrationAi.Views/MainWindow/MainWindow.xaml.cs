using System.Windows;
using IntegrationAi.ViewModels.MainWindow;

namespace IntegrationAi.Views.MainWindow
{
    public partial class MainWindow :IMainWindow
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }
    }
}