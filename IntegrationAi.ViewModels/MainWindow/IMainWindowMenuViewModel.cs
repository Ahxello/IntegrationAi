using System.Windows.Input;

namespace IntegrationAi.ViewModels.MainWindow;

public interface IMainWindowMenuViewModel
{
    ICommand AddRelatedEntitiesForMessageCollectionAsyncCommand { get; }
    ICommand OpenInputDialogCommand { get; }
    ICommand LoadFileCommand { get; }
    ICommand AddPropetiesForMessageCollectionAsyncCommand { get; }
    void CloseInputWindow();

    event Action<IMainWindowContentViewModel>? ContentViewModelChanged;
}