using System.Runtime.Serialization;

namespace IntegrationAi.Infrastructure.Settings;

[DataContract]
internal class MainWindowSettings
{
    public MainWindowSettings()
    {
        Left = 100;
        Top = 100;
        Width = 600;
        Height = 400;
        isMaximized = true;
    }
    [DataMember(Name="left")]
    public double Left { get; set; }
    [DataMember(Name = "top")]
    public double Top { get; set; }
    [DataMember(Name = "width")]
    public double Width { get; set; }
    [DataMember(Name = "height")]
    public double Height { get; set; }
    [DataMember(Name = "isMaximized")]
    public bool isMaximized { get; set; }
}