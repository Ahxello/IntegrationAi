using IntegrationAi.Domain.Settings;
using Newtonsoft.Json;

namespace IntegrationAi.Infrastructure.Settings;

internal class MainWindowSettingsWrapper : IMainWindowSettingsWrapperInitializer,
    IMainWindowSettingsWrapper, IDisposable
{
    private MainWindowSettings _mainWindowSettings;
    private bool _initialized;
    private string _settingFilePath;

    public MainWindowSettingsWrapper()
    {
        _mainWindowSettings = new MainWindowSettings();
    }

    public void Dispose()
    {
        EnsureInitialized();
        var serializedSettings = JsonConvert.SerializeObject(_mainWindowSettings);
        File.WriteAllText(_settingFilePath, serializedSettings);
    }

    public double Left
    {
        get
        {
            EnsureInitialized();
            return _mainWindowSettings.Left;
        }
        set
        {
            EnsureInitialized();
            _mainWindowSettings.Left = value;
        }
    }

    public double Top
    {
        get
        {
            EnsureInitialized();
            return _mainWindowSettings.Top;
        }
        set
        {
            EnsureInitialized();
            _mainWindowSettings.Top = value;
        }
    }

    public double Width
    {
        get
        {
            EnsureInitialized();
            return _mainWindowSettings.Width;
        }
        set
        {
            EnsureInitialized();
            _mainWindowSettings.Width = value;
        }
    }

    public double Height
    {
        get
        {
            EnsureInitialized();
            return _mainWindowSettings.Height;
        }
        set
        {
            EnsureInitialized();
            _mainWindowSettings.Height = value;
        }
    }

    public bool isMaximized
    {
        get
        {
            EnsureInitialized();
            return _mainWindowSettings.isMaximized;
        }
        set
        {
            EnsureInitialized();
            _mainWindowSettings.isMaximized = value;
        }
    }

    public void Initialize()
    {
        if(_initialized)
            throw new InvalidOperationException($"{nameof(IMainWindowSettingsWrapper)} is already initialized");
        _initialized = true;
        var localApplicetionData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        const string company = "Code Cruiser";
        const string applicationName = "IntegrationAi";
        const string settingsFolderName = "Settings";

        var settingsPath = Path.Combine(localApplicetionData, company, applicationName, settingsFolderName);
        _settingFilePath = Path.Combine(settingsPath, "MainWindowSettings.json");

        Directory.CreateDirectory(settingsPath);
        if(!File.Exists(_settingFilePath))
            return;
        var serializedSettings = File.ReadAllText(_settingFilePath);
        _mainWindowSettings = JsonConvert.DeserializeObject<MainWindowSettings>(serializedSettings);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IMainWindowSettingsWrapper)} is not initialized");
    }
}