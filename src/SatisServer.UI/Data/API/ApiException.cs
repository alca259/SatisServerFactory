namespace SatisServer.UI.Data.API;

/// <summary>Exception thrown when an API call fails.</summary>
public sealed class ApiException : Exception
{
    /// <summary>The error code returned by the API.</summary>
    public string? ErrorCode { get; }
    /// <summary>Additional data returned by the API.</summary>
    public object? ErrorData { get; }

    /// <summary>Constructor</summary>
    /// <param name="errorCode">The error code returned by the API.</param>
    /// <param name="message">The error message.</param>
    /// <param name="errorData">Additional data returned by the API.</param>
    public ApiException(string? errorCode, string? message, object? errorData = null) : base(message)
    {
        ErrorCode = errorCode;
        ErrorData = errorData;
    }
}