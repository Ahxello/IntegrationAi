using IntegrationAi.ViewModels.Windows;
using IntegrationAi.Views.Factories;

namespace IntegrationAi.Views.Windows;

public class WindowManager : IWindowManager
{
    private readonly Dictionary<IWindowViewModel, IWindow> _viewModelToWindowMap = new();
    private readonly IWindowFactory _windowfactory;

    public WindowManager(IWindowFactory windowFactory)
    {
        _windowfactory = windowFactory;
    }

    public IWindow Show<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        var window = _windowfactory.Create(viewModel);

        _viewModelToWindowMap.Add(viewModel, window);

        window.Show();

        return window;
    }

    public void Close<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
        {
            window.Close();

            _viewModelToWindowMap.Remove(viewModel);
        }
    }
}