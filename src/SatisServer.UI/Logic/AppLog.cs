namespace SatisServer.UI.Logic;

/// <summary>Logs messages to the application log.</summary>
internal static class AppLog
{
    private static readonly string _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "satisserver.log");

    internal enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 99,
        Fatal = 100
    }

    /// <summary>Logs a message to the application log.</summary>
    /// <param name="message">The message to log.</param>
    /// <param name="level">The log level of the message.</param>
    public static void Write(string? message, LogLevel level = LogLevel.Info)
    {
        if (message == null) return;
        
        var dir = Path.GetDirectoryName(_logPath)!;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.AppendAllText(_logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][{level.ToString().ToUpper()}] {message}{Environment.NewLine}");
    }
}
