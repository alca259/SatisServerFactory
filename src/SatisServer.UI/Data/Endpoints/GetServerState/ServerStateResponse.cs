using SatisServer.UI.Data.i18n;

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

    /// <summary>Get the total duration of this save</summary>
    public string GetSessionDuration()
    {
        var time = TimeSpan.FromSeconds(ServerGameState.TotalGameDuration);
        return $"{time.Days} days, {time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds";
    }

    /// <summary>Get the current average tick rate</summary>
    public string GetTickRate()
    {
        return $"{(int)(1000 / ServerGameState.AverageTickRate)} ms";
    }

    /// <summary>Get the current game phase</summary>
    public string? GetGamePhase()
    {
        var gamePhase = GamePhases.FirstOrDefault(x => x.Key == ServerGameState.GamePhase);
        if (gamePhase == null) return ServerGameState.GamePhase;

        var nextGamePhase = GamePhases.FirstOrDefault(x => x.Number == gamePhase.Number + 1);
        if (nextGamePhase == null) return $"{gamePhase.Description} (Phase: {gamePhase.Number})";

        return $"{gamePhase.Description} (Phase: {gamePhase.Number}) - In progress: {nextGamePhase.Description} (Phase: {nextGamePhase.Number})";
    }

    /// <summary>Get the current active schematic</summary>
    public string? GetActiveSchematic()
    {
        var activeSchematic = I18nResources.GetAllSchematics().Find(x => ServerGameState.ActiveSchematic.ContainsIgnoreCase(x.FullName));
        return activeSchematic is not null ? $"{activeSchematic.Description} (Tier {activeSchematic.TechTier})" : ServerGameState.ActiveSchematic;
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

    #region Data
    private sealed record GamePhaseDescriptions(string Key, int Number, string Description);

    private static IReadOnlyList<GamePhaseDescriptions> GamePhases =>
    [
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_0.GP_Project_Assembly_Phase_0'", 0, "Build HUB"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_1.GP_Project_Assembly_Phase_1'", 1, "Distribution Platform"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_2.GP_Project_Assembly_Phase_2'", 2, "Construction Dock"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_3.GP_Project_Assembly_Phase_3'", 3, "Main Body"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_4.GP_Project_Assembly_Phase_4'", 4, "Propulsion Systems"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_5.GP_Project_Assembly_Phase_5'", 5, "Assembly"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_6.GP_Project_Assembly_Phase_6'", 6, "Launch"),
        new("/Script/FactoryGame.FGGamePhase'/Game/FactoryGame/GamePhases/GP_Project_Assembly_Phase_7.GP_Project_Assembly_Phase_7'", 7, "Completed")
    ];

    private static CommunityResourceI18N? _communityResourceI18N;
    private static CommunityResourceI18N I18nResources => _communityResourceI18N ??= CommunityResourceI18N.FromLanguage("en-US");
    #endregion
}
