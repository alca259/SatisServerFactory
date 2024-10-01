namespace SatisServer.UI.Data.Endpoints.GetServerState;

/// <summary>Response object for the GetServerState endpoint</summary>
public sealed class ServerStateResponse
{
    /// <summary>Current state of the Dedicated Server</summary>
    public GameState ServerGameState { get; set; } = new();

    /// <summary>Get the current state of the server</summary>
    public string GetServerState()
    {
        if (ServerGameState.IsGameRunning && !string.IsNullOrEmpty(ServerGameState.ActiveSessionName))
        {
            return $"Started - Game running session: {ServerGameState.ActiveSessionName}.";
        }
        
        if (ServerGameState.IsGameRunning)
        {
            return "Started - No session loaded.";
        }

        if (!ServerGameState.IsGameRunning && !string.IsNullOrEmpty(ServerGameState.ActiveSessionName))
        {
            return $"Initializing - Loading session: {ServerGameState.ActiveSessionName}.";
        }

        return "Stopped.";
    }

    /// <summary>Get the number of players currently connected to the server</summary>
    public string GetPlayerCount()
    {
        return $"{ServerGameState.NumConnectedPlayers} / {ServerGameState.PlayerLimit}";
    }

    public string GetSessionDuration()
    {
        var time = TimeSpan.FromSeconds(ServerGameState.TotalGameDuration);
        return $"{time.Days} days, {time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds";
    }

    public string GetTickRate()
    {
        return $"{(int)(1000 / ServerGameState.AverageTickRate)} ms";
    }

    /// <summary>Represents the current state of the Dedicated Server</summary>
    public sealed class GameState
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
}
