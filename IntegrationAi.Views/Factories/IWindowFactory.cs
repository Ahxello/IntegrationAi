using IntegrationAi.ViewModels.Windows;

namespace IntegrationAi.Views.Factories;

public interface IWindowFactory
{
    IWindow Create<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel;
}