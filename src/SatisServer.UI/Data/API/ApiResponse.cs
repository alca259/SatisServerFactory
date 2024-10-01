namespace SatisServer.UI.Data.API;

/// <summary>Generic API response object</summary>
public sealed class ApiResponse<T>
{
    /// <summary>Response data</summary>
    public T? Data { get; set; }
    /// <summary>Response error code</summary>
    public string? ErrorCode { get; set; }
    /// <summary>Response error message</summary>
    public string? ErrorMessage { get; set; }
}