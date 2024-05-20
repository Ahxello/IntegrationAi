using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntegrationAi.ViewModels;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void InvokePropertyChanged([CallerMemberName] string propertName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertName));
    }
}