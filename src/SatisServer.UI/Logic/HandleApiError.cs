using SatisServer.UI.Data.API;

namespace SatisServer.UI.Logic;

/// <summary>Handles API errors.</summary>
public static class HandleApiError
{
    /// <summary>Handles an API error response.</summary>
    public static void HandleError(ApiResponse<object>? response) => throw (response?.ErrorCode) switch
    {
        "save_game_load_failed" => new ApiException(response?.ErrorCode, "Failed to find or load the specified save game file."),
        "invalid_save_game" => new ApiException(response?.ErrorCode, "The save game file is invalid or corrupted."),
        "unsupported_save_game" => new ApiException(response?.ErrorCode, "The save game file version is not supported."),
        "file_save_failed" => new ApiException(response?.ErrorCode, "Failed to save the game file to the server."),
        "file_not_found" => new ApiException(response?.ErrorCode, "The specified save game file was not found on the server."),
        _ => new ApiException(response?.ErrorCode, response?.ErrorMessage, response?.Data),
    };
}