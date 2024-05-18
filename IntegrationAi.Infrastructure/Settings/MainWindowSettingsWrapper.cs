using IntegrationAi.Domain.Settings;
using IntegrationAi.Infrastructure.Common;
using Newtonsoft.Json;

namespace IntegrationAi.Infrastructure.Settings;

internal class MainWindowSettingsWrapper : WindowSettingsWrapper<MainWindowSettings>, IMainWindowSettingsWrapper
{
    public MainWindowSettingsWrapper(IPathService pathService) : base(pathService)
    {
        
    }

    protected override string SettingsName => "MainWindowSettings";
}