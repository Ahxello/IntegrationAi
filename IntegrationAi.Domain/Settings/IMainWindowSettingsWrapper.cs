﻿namespace IntegrationAi.Domain.Settings;

public interface IMainWindowSettingsWrapper
{
    double Left { get; set; }
    double Top { get; set; }
    double Width { get; set; }
    double Height { get; set; }
    bool isMaximized { get; set; }
}