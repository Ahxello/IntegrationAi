using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Messages;
using IntegrationAi.ViewModels.Services;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowSettingsWrapper>, IMainWindowViewModel
{
    private readonly IFactory<IMessageCollectionViewModel> _messageCollectionViewModelFactory;
    private readonly AsyncCommand _openMessageCollectionCommand;
    private readonly IWindowManager _windowManager;
    private readonly IDialogService _dialogService;
    private IMainWindowContentViewModel _contentViewModel;
    private readonly AsyncCommand _loadFileCommand;


    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper,
        IWindowManager windowManager,
        IFactory<IMessageCollectionViewModel> messageCollectionViewModelFactory,
        IFactory<IMainWindowMenuViewModel> mainWindowMenuViewModelFactory, 
        IDialogService dialogService)
        : base(mainWindowSettingsWrapper)
    {
        _windowManager = windowManager;

        _messageCollectionViewModelFactory = messageCollectionViewModelFactory;

        _dialogService = dialogService;

        _loadFileCommand = new AsyncCommand(LoadFile);

        _openMessageCollectionCommand = new AsyncCommand(OpenMessageCollectionAsync);

        MenuViewModel = mainWindowMenuViewModelFactory.Create();

    }

    public IMainWindowMenuViewModel MenuViewModel { get; }

    public string Title => "IntegrationAI";


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

    private List<string> localContentList = new List<string>();

    private async Task OpenMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.InitializeAsync(localContentList);
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
        {
            localContentList = new List<string>{message.Message};
        }
    }

    public ICommand LoadFileCommand => _loadFileCommand;
    private async Task LoadFile()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.OpenFileDialog();
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
        {
            localContentList = new List<string> { message.Message };
        }
    }

    private void OnMainWindowClosingRequested()
    {
        _windowManager.Close(this);
    }
    private void OnContentViewModelChanged(IMainWindowContentViewModel contentViewModel)
    {
        ContentViewModel = contentViewModel;
    }
    public void Dispose()
    {
        MenuViewModel.ContentViewModelChanged -= OnMainWindowClosingRequested;
    }
}