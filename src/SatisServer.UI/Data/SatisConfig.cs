namespace SatisServer.UI.Data;

/// <summary>Configuration object for the Satis Server</summary>
public sealed class SatisConfig
{
    /// <summary>Path to the root directory of the Dedicated Server</summary>
    public string? RootPath { get; set; }
    /// <summary>Path to the log directory of the Dedicated Server</summary>
    public string? LogDirectory { get; set; }
    /// <summary>Path to the save directory of the Dedicated Server</summary>
    public string? SaveDirectory { get; set; }
    /// <summary>IP address the Dedicated Server will be hosted on</summary>
    public string ServerIpAddress { get; set; } = "127.0.0.1";
    /// <summary>Port the Dedicated Server will be hosted on</summary>
    public int ServerPort { get; set; } = 7777;
    /// <summary>Password required to access the Admin Console</summary>
    public string? AdminPassword { get; set; }
    /// <summary>True if the Dedicated Server should not display a console window</summary>
    public bool NoVisibleConsole { get; set; } = true;
    /// <summary>True if the Dedicated Server should use experimental branch instead of stable version</summary>
    public bool UseExperimental { get; set; } = false;
    /// <summary>True if the Dedicated Server should disable seasonal events. Like Ficsmas</summary>
    public bool DisableEventsSeasonal { get; set; } = false;

    private static SatisConfig? _instance;
    /// <summary>Singleton instance of the SatisConfig object</summary>
    public static SatisConfig Instance => _instance ??= new SatisConfig();

    /// <summary>Checks if the RootPath is valid</summary>
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

    /// <summary>Gets the base URL API for the Dedicated Server</summary>
    public string GetBaseUrl()
    {
        return $"https://{ServerIpAddress}:{ServerPort}/api/v1/";
    }

    /// <summary>Copy the values of a SatisConfig object to a new instance</summary>
    public void CopyValues(SatisConfig? config)
    {
        if (config == null) return;

        RootPath = config.RootPath;
        LogDirectory = config.LogDirectory;
        SaveDirectory = config.SaveDirectory;
        ServerIpAddress = config.ServerIpAddress;
        ServerPort = config.ServerPort;
        AdminPassword = config.AdminPassword;
        NoVisibleConsole = config.NoVisibleConsole;
        UseExperimental = config.UseExperimental;
        DisableEventsSeasonal = config.DisableEventsSeasonal;
    }
}