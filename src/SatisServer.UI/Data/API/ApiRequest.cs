namespace SatisServer.UI.Data.API;

/// <summary>Request object for the API</summary>
public sealed class ApiRequest
{
    /// <summary>Function to call on the server</summary>
    public string Function { get; set; } = string.Empty;
    /// <summary>Optional data to send to the server</summary>
    public object? Data { get; set; }
}