using Autofac;
using IntegrationAi.ViewModels.MainWindow;
using IntegrationAi.ViewModels.Windows;
using IntegrationAi.Views.Factories;
using IntegrationAi.Views.MainWindow;

namespace IntegrationAi.Bootstrapper.Factories;

public class WindowFactory : IWindowFactory
{
    private readonly IComponentContext _componentContext;

    private readonly Dictionary<Type, Type> _map = new()
    {
        { typeof(IMainWindowViewModel), typeof(IMainWindow) }
    };

    public WindowFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public IWindow Create<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
    {
        if (!_map.TryGetValue(typeof(TWindowViewModel), out var windowType))
            throw new InvalidOperationException($"There is now window register for {typeof(TWindowViewModel)}");
        var instance = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));
        return (IWindow)instance;
    }
}