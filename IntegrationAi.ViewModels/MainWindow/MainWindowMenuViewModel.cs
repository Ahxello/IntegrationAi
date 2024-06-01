using System.Windows;
using System.Windows.Input;
using IntegrationAi.Domain.Factories;
using IntegrationAi.ViewModels.Commands;
using IntegrationAi.ViewModels.Dialogs;
using IntegrationAi.ViewModels.Messages;
using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.ViewModels.MainWindow;

public class MainWindowMenuViewModel : IMainWindowMenuViewModel
{
    private readonly AsyncCommand _addPropetiesForMessageCollectionAsyncCommand;
    private readonly AsyncCommand _addRelatedEntitiesForMessageCollectionAsyncCommand;
    private readonly IFactory<IInputWindowViewModel> _inputWindowViewModelFactory;
    private readonly AsyncCommand _loadFileCommand;
    private readonly IFactory<IMessageCollectionViewModel> _messageCollectionViewModelFactory;
    private readonly Command _openInputDialogCommand;
    private readonly IWindowManager _windowManager;
    private IInputWindowViewModel? _inputWindowViewModel;
    private List<string> localContentList = new();

    public MainWindowMenuViewModel(IFactory<IInputWindowViewModel> inputWindowViewModelFactory,
        IWindowManager windowManager,
        IFactory<IMessageCollectionViewModel> messageCollectionViewModelFactory)
    {
        _windowManager = windowManager;

        _messageCollectionViewModelFactory = messageCollectionViewModelFactory;

        _inputWindowViewModelFactory = inputWindowViewModelFactory;

        _openInputDialogCommand = new Command(OpenInputDialog);

        _loadFileCommand = new AsyncCommand(LoadFile);

        _addPropetiesForMessageCollectionAsyncCommand = new AsyncCommand(AddPropetiesForMessageCollectionAsync);

        _addRelatedEntitiesForMessageCollectionAsyncCommand =
            new AsyncCommand(AddRelatedEntitiesForMessageCollectionAsync);
    }

    public ICommand AddRelatedEntitiesForMessageCollectionAsyncCommand =>
        _addRelatedEntitiesForMessageCollectionAsyncCommand;

    public ICommand LoadFileCommand => _loadFileCommand;
    public ICommand AddPropetiesForMessageCollectionAsyncCommand => _addPropetiesForMessageCollectionAsyncCommand;
    public ICommand OpenInputDialogCommand => _openInputDialogCommand;

    public event Action<IMainWindowContentViewModel>? ContentViewModelChanged;

    public void CloseInputWindow()
    {
        if (_inputWindowViewModel != null) _windowManager.Close(_inputWindowViewModel);
    }

    private void OpenInputDialog()
    {
        if (_inputWindowViewModel == null)
        {
            _inputWindowViewModel = _inputWindowViewModelFactory.Create();
            var inputWindow = _windowManager.Show(_inputWindowViewModel);
            inputWindow.Closed += OnInputWindowClosed;
        }
        else
        {
            _windowManager.Show(_inputWindowViewModel);
        }
    }

    private void OnInputWindowClosed(object sender, EventArgs e)
    {
        if (sender is IWindow window)
        {
            window.Closed -= OnInputWindowClosed;

            _inputWindowViewModel = null;
        }
    }

    private async Task AddPropetiesForMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.AddPropeties(localContentList);
        ContentViewModelChanged?.Invoke(messageCollectionViewModel);
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = [message.Message];
    }

    private async Task AddRelatedEntitiesForMessageCollectionAsync()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();

        await messageCollectionViewModel.AddRelatedEntites(localContentList, "10");

        ContentViewModelChanged?.Invoke(messageCollectionViewModel);

        foreach (var message in messageCollectionViewModel.Items)

            localContentList = [message.Message];
    }


    private async Task AiFileProcessing()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.InitializeAsync(localContentList);
        ContentViewModelChanged?.Invoke(messageCollectionViewModel);
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = [message.Message];
    }

    private async Task LoadFile()
    {
        var messageCollectionViewModel = _messageCollectionViewModelFactory.Create();
        await messageCollectionViewModel.OpenFileDialog();
        ContentViewModelChanged?.Invoke(messageCollectionViewModel);
        foreach (var message in messageCollectionViewModel.Items)
            localContentList = [message.Message];
        var result = MessageBox.Show("Хотите чтобы ИИ обработал данные файла?", "Повторить?", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes) await AiFileProcessing();
    }
}