namespace SatisServer.UI.Data.Enums;

/// <summary>Function names for the API calls.</summary>
public enum ApiCallName
{
    HealthCheck,
    VerifyAuthenticationToken,
    PasswordlessLogin,
    PasswordLogin,
    QueryServerState,
    GetServerOptions,
    GetAdvancedGameSettings,
    ApplyAdvancedGameSettings,
    ClaimServer,
    RenameServer,
    SetClientPassword,
    SetAdminPassword,
    SetAutoLoadSessionName,
    RunCommand,
    Shutdown,
    ApplyServerOptions,
    CreateNewGame,
    SaveGame,
    DeleteSaveFile,
    DeleteSaveSession,
    EnumerateSessions,
    LoadGame,
    UploadSaveGame,
    DownloadSaveGame
}
