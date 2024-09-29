namespace SatisServer.UI.Data;
internal sealed class ServerGameState
{
    /// <summary>Name of the currently loaded game session</summary>
    public string? ActiveSessionName { get; set; }
    /// <summary>Number of the players currently connected to the Dedicated Server</summary>
    public int NumConnectedPlayers { get; set; }
    /// <summary>Maximum number of players that can be connected to the Dedicated Server</summary>
    public int PlayerLimit { get; set; }
    /// <summary>Maximum Tech Tier of all Schematics currently unlocked</summary>
    public int TechTier { get; set; }
    /// <summary>Schematic currently set as Active Milestone</summary>
    public string? ActiveSchematic { get; set; }
    /// <summary>Current game phase. None if no game is running</summary>
    public string? GamePhase { get; set; }
    /// <summary>True if the save is currently loaded, false if the server is waiting for the session to be created</summary>
    public bool IsGameRunning { get; set; }
    /// <summary>Total time the current save has been loaded, in seconds</summary>
    public int TotalGameDuration { get; set; }
    /// <summary>True if the game is paused. If the game is paused, total game duration does not increase</summary>
    public bool IsGamePaused { get; set; }
    /// <summary>Average tick rate of the server, in ticks per second</summary>
    public float AverageTickRate { get; set; }
    /// <summary>Name of the session that will be loaded when the server starts automatically</summary>
    public string? AutoLoadSessionName { get; set; }
}
