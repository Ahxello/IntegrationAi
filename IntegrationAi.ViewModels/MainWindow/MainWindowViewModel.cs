using System.Windows;
using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Messages;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowSettingsWrapper>, IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private IMainWindowContentViewModel _contentViewModel;

    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper,
        IWindowManager windowManager,
        IFactory<IMainWindowMenuViewModel> mainWindowMenuViewModelFactory
    )
        : base(mainWindowSettingsWrapper)
    {
        _windowManager = windowManager;

        MenuViewModel = mainWindowMenuViewModelFactory.Create();

        MenuViewModel.ContentViewModelChanged += OnContentViewModelChanged;
    }


    public IMainWindowMenuViewModel MenuViewModel { get; }

    public string Title => "IntegrationAI";

    public IMainWindowContentViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            _contentViewModel = value;
            InvokePropertyChanged();
        }
    }

    public override void WindowClosing()
    {
        MenuViewModel.CloseInputWindow();
    }

    private void OnContentViewModelChanged(IMainWindowContentViewModel contentViewModel)
    {
        ContentViewModel = contentViewModel;
    }
    public void Dispose()
    {
        MenuViewModel.ContentViewModelChanged -= OnContentViewModelChanged;
    }
}