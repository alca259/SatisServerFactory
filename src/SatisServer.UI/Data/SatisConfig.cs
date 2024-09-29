namespace SatisServer.UI.Data;

public sealed class SatisConfig
{
    public string? RootPath { get; set; }
    public string ServerIpAddress { get; set; } = "127.0.0.1";
    public int ServerPort { get; set; } = 7777;
    public bool NoVisibleConsole { get; set; } = true;
    public bool UseExperimental { get; set; } = false;
    public bool DisableEventsSeasonal { get; set; } = false;

    private static SatisConfig? _instance;
    public static SatisConfig Instance => _instance ??= new SatisConfig();

    public bool IsRootPathValid()
    {
        if (string.IsNullOrWhiteSpace(RootPath))
        {
            return false;
        }

        if (!Directory.Exists(RootPath))
        {
            return false;
        }

        return true;
    }
}