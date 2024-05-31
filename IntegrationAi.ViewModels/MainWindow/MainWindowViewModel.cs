using System.Windows;
using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.Domain.Services;
using IntegrationAi.Domain.Settings;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Dialogs;
using IntegrationAi.ViewModels.Messages;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowSettingsWrapper>, IMainWindowViewModel
{
    private readonly AsyncCommand _addPropetiesForMessageCollectionAsyncCommand;
    private readonly IDialogService _dialogService;
    private readonly IInputWindowViewModel _inputWindowViewModel;
    private readonly AsyncCommand _loadFileCommand;
    private readonly IFactory<IMessageCollectionViewModel> _messageCollectionViewModelFactory;
    private readonly Command _openInputDialogCommand;
    private readonly IWindowManager _windowManager;
    private IMainWindowContentViewModel _contentViewModel;

    private List<string> localContentList = new();


    public MainWindowViewModel(IMainWindowSettingsWrapper mainWindowSettingsWrapper,
        IWindowManager windowManager,
        IFactory<IMessageCollectionViewModel> messageCollectionViewModelFactory,
        IFactory<IMainWindowMenuViewModel> mainWindowMenuViewModelFactory,
        IInputWindowViewModel inputWindowViewModel,
        IDialogService dialogService
        )
        : base(mainWindowSettingsWrapper)
    {
        _windowManager = windowManager;

        _messageCollectionViewModelFactory = messageCollectionViewModelFactory;

        _inputWindowViewModel = inputWindowViewModel;

        _dialogService = dialogService;

        _loadFileCommand = new AsyncCommand(LoadFile);

        _addPropetiesForMessageCollectionAsyncCommand = new AsyncCommand(AddPropetiesForMessageCollectionAsync);

        _openInputDialogCommand = new Command(OpenInputDialog);


        MenuViewModel = mainWindowMenuViewModelFactory.Create();
    }

    public ICommand OpenInputDialogCommand => _openInputDialogCommand;

    public IMainWindowMenuViewModel MenuViewModel { get; }

    public string Title => "IntegrationAI";


    public ICommand AddPropetiesForMessageCollectionAsyncCommand => _addPropetiesForMessageCollectionAsyncCommand;

    public IMainWindowContentViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            _contentViewModel = value;
            InvokePropertyChanged();
        }
    }

    public ICommand LoadFileCommand => _loadFileCommand;

    private void OpenInputDialog()
    {
        _windowManager.Show(_inputWindowViewModel);
    }


    private async Task AddPropetiesForMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.AddPropeties(localContentList);
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = new List<string> { message.Message };
    }

    private async Task AddRelatedEntitiesForMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();

        await messageCollectionViewModel.AddRelatedEntites(localContentList, "hello");
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = new List<string> { message.Message };
    }


    private async Task AiFileProcessing()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.InitializeAsync(localContentList);
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = new List<string> { message.Message };
    }

    private async Task LoadFile()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.OpenFileDialog();
        ContentViewModel = messageCollectionViewModel;
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = new List<string> { message.Message };
        var result = MessageBox.Show("Хотите чтобы ИИ обработал данные файла?", "Повторить?", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes) await AiFileProcessing();
    }

    private void OnMainWindowClosingRequested()
    {
        _windowManager.Close(this);
    }

    private void OnContentViewModelChanged(IMainWindowContentViewModel contentViewModel)
    {
        ContentViewModel = contentViewModel;
    }

    public override void WindowClosing()
    {
        _windowManager.Close(_inputWindowViewModel);
    }

    public void Dispose()
    {
        MenuViewModel.ContentViewModelChanged -= OnMainWindowClosingRequested;
    }
}