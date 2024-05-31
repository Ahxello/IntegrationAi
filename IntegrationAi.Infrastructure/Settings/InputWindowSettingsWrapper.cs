using IntegrationAi.Domain.Settings;
using IntegrationAi.Infrastructure.Common;

namespace IntegrationAi.Infrastructure.Settings;

internal class InputWindowSettingsWrapper : WindowSettingsWrapper<InputWindowSettings>, IInputWindowSettingsWrapper
{
    public InputWindowSettingsWrapper(IPathService pathService) : base(pathService)
    {
    }

    protected override string SettingsName => "InputWindowSettings";
}