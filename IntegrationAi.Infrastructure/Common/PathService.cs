using IntegrationAi.Domain.Settings;

namespace IntegrationAi.Infrastructure.Common;

internal class PathService : IPathService, IPathServiceInitializer
{
    private string _applicationFolder;
    private bool _initialized;

    public string ApplicationFolder
    {
        get
        {
            EnsureInitialized();

            return _applicationFolder;
        }
        private set => _applicationFolder = value;
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");
        _initialized = true;

        var localApplicetionData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        const string company = "Code Cruiser";
        const string applicationName = "IntegrationAi";
        ApplicationFolder = Path.Combine(localApplicetionData, company, applicationName);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IMainWindowSettingsWrapper)} is not initialized");
    }
}