using System.IO;
using IntegrationAi.Domain.Settings;
using IntegrationAi.Infrastructure.Common;
using Newtonsoft.Json;

namespace IntegrationAi.Infrastructure.Settings;

internal abstract class WindowSettingsWrapper<TSettings> : IWindowSettingsWrapper, IWindowSettingsWrapperInitializer,
    IDisposable
    where TSettings : WindowSettings, new()
{
    private readonly IPathService _pathService;
    private bool _initialized;
    private string _settingFilePath;
    private WindowSettings _windowSettings;

    protected WindowSettingsWrapper(IPathService pathService)
    {
        _pathService = pathService;

        _windowSettings = new TSettings();
    }

    protected abstract string SettingsName { get; }

    public void Dispose()
    {
        EnsureInitialized();
        var serializedSettings = JsonConvert.SerializeObject(_windowSettings);
        File.WriteAllText(_settingFilePath, serializedSettings);
    }

    public double Left
    {
        get
        {
            EnsureInitialized();
            return _windowSettings.Left;
        }
        set
        {
            EnsureInitialized();
            _windowSettings.Left = value;
        }
    }

    public double Top
    {
        get
        {
            EnsureInitialized();
            return _windowSettings.Top;
        }
        set
        {
            EnsureInitialized();
            _windowSettings.Top = value;
        }
    }

    public double Width
    {
        get
        {
            EnsureInitialized();
            return _windowSettings.Width;
        }
        set
        {
            EnsureInitialized();
            _windowSettings.Width = value;
        }
    }

    public double Height
    {
        get
        {
            EnsureInitialized();
            return _windowSettings.Height;
        }
        set
        {
            EnsureInitialized();
            _windowSettings.Height = value;
        }
    }

    public bool isMaximized
    {
        get
        {
            EnsureInitialized();
            return _windowSettings.isMaximized;
        }
        set
        {
            EnsureInitialized();
            _windowSettings.isMaximized = value;
        }
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"Wrapper for {nameof(TSettings)} is already initialized");
        _initialized = true;

        const string settingsFolderName = "Settings";

        var settingsPath = Path.Combine(_pathService.ApplicationFolder, settingsFolderName);
        _settingFilePath = Path.Combine(settingsPath, $"{SettingsName}.json");

        Directory.CreateDirectory(settingsPath);
        if (!File.Exists(_settingFilePath))
            return;
        var serializedSettings = File.ReadAllText(_settingFilePath);
        _windowSettings = JsonConvert.DeserializeObject<TSettings>(serializedSettings);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(TSettings)} is not initialized");
    }
}