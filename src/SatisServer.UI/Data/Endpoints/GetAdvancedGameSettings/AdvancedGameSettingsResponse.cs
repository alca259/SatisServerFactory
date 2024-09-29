namespace SatisServer.UI.Data.Endpoints.GetAdvancedGameSettings;

public class AdvancedGameSettingsResponse
{
    public bool CreativeModeEnabled { get; set; }
    public Dictionary<string, string> AdvancedGameSettings { get; set; }
}
