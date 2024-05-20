using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowSettingsWrapper>, IMainWindowViewModel
{
    private readonly Command _closeMainWindowCommand;
    private readonly IFactory<IMessageCollectionViewModel> _messageCollectionViewModelFactory;
    private readonly AsyncCommand _openMessageCollectionCommand;
    private readonly IWindowManager _windowManager;
    private IMainWindowContentViewModel _contentViewModel;

    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper,
        IWindowManager windowManager,
        IFactory<IMessageCollectionViewModel> messageCollectionViewModelFactory) : base(mainWindowSettingsWrapper)
    {
        _windowManager = windowManager;

        _messageCollectionViewModelFactory = messageCollectionViewModelFactory;


        _closeMainWindowCommand = new Command(CloseMainWindow);

        _openMessageCollectionCommand = new AsyncCommand(OpenMessageCollectionAsync);
    }


    public string Title => "IntegrationAI";

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;

    public ICommand OpenMessageCollectionCommand => _openMessageCollectionCommand;

    public IMainWindowContentViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            _contentViewModel = value;
            InvokePropertyChanged();
        }
    }

    private async Task OpenMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.InitializeAsync();
        ContentViewModel = messageCollectionViewModel;
    }

    private void CloseMainWindow()
    {
        _windowManager.Close(this);
    }
}