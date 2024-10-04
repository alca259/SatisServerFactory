using SatisServer.UI.Data;

namespace SatisServer.UI.Logic;

/// <summary>Controls the dedicated server.</summary>
internal static class ServerControl
{
    private static readonly string[] _processNames = ["FactoryServer", "UnrealServer-Win64-Shipping", "FactoryServer-Win64-Shipping-Cmd"];
    private static readonly List<nint> _hidedWindows = [];

    internal static event EventHandler? OnServerStarted;
    internal static event EventHandler? OnServerStopped;
    internal static event EventHandler? OnDetectServerOnline;

    /// <summary>Starts the server.</summary>
    /// <remarks>Does nothing if the server is already running.</remarks>
    public static void StartServer()
    {
        if (!SatisConfig.Instance.IsRootPathValid())
        {
            return;
        }

        var serverExe = Path.Combine(SatisConfig.Instance.RootPath!, "FactoryServer.exe");
        if (!File.Exists(serverExe))
        {
            return;
        }

        if (IsServerRunning())
        {
            OnServerStarted?.Invoke(null, EventArgs.Empty);
            return;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = serverExe,
            Arguments = $"-ServerQueryPort={SatisConfig.Instance.ServerPort} -BeaconPort={SatisConfig.Instance.ServerPort} -Port={SatisConfig.Instance.ServerPort} -log -unattended",
            WorkingDirectory = SatisConfig.Instance.RootPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = false,
            RedirectStandardError = false
        };

        var process = new Process { StartInfo = startInfo };
        process.Start();

        AppLog.Write("Server started");
        OnServerStarted?.Invoke(null, EventArgs.Empty);

        if (!SatisConfig.Instance.NoVisibleConsole) return;
        
        var processes = FindProcesses()
            .Where(w => w.MainWindowHandle == IntPtr.Zero)
            .Where(w => w.Id != process.Id)
            .ToArray();

        while (processes.Length > 0)
        {
            AppLog.Write("Waiting for server console to hide", AppLog.LogLevel.Debug);
            Thread.Sleep(1000);
            processes = FindProcesses()
                .Where(w => w.MainWindowHandle == IntPtr.Zero)
                .Where(w => w.Id != process.Id)
                .ToArray();
        }

        HideServerConsole();
    }

    public static void ShowServerConsole()
    {
        var consoleProcess = FindProcesses();
        foreach (var prWindow in consoleProcess)
        {
            CommandLineHelper.ShowWindowHandle(prWindow.MainWindowHandle);
        }

        foreach (var handle in _hidedWindows)
        {
            CommandLineHelper.ShowWindowHandle(handle);
        }
    }

    public static void HideServerConsole()
    {
        var consoleProcess = FindProcesses();
        foreach (var prWindow in consoleProcess)
        {
            if (!_hidedWindows.Contains(prWindow.MainWindowHandle))
            {
                _hidedWindows.Add(prWindow.MainWindowHandle);
            }
            CommandLineHelper.HideWindowHandle(prWindow.MainWindowHandle);
        }
    }

    /// <summary>Stops the server.</summary>
    /// <remarks>Does nothing if the server is not running.</remarks>
    public static void StopServer()
    {
        if (!IsServerRunning())
        {
            OnServerStopped?.Invoke(null, EventArgs.Empty);
            return;
        }

        const int retries = 20;
        const int waitTime = 1000;

        Process[] processes;
        int retryCount = 0;

        // Close server
        while (IsServerRunning() && retryCount < retries)
        {
            processes = FindProcesses();
            foreach (var process in processes)
            {
                try
                {
                    process.CloseMainWindow();
                    process.Close();
                }
                catch (Exception)
                {
                    // Ignore
                }
            }

            Thread.Sleep(waitTime);
            retryCount++;
        }

        // Wait for server to stop
        if (IsServerRunning())
        {
            processes = FindProcesses();
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        AppLog.Write("Server stopped");
        OnServerStopped?.Invoke(null, EventArgs.Empty);
    }

    /// <summary>Restarts the server.</summary>
    public static void RestartServer()
    {
        StopServer();
        StartServer();
    }

    /// <summary>Checks if the server is running.</summary>
    public static bool IsServerRunning()
    {
        var processes = FindProcesses();
        bool isRunning = processes.Length > 0;
        if (isRunning)
        {
            OnDetectServerOnline?.Invoke(null, EventArgs.Empty);
        }

        return isRunning;
    }

    /// <summary>Gets the server RAM usage.</summary>
    public static void GetRamUsage(out double ramUsage)
    {
        ramUsage = 0;

        if (!IsServerRunning())
            return;

        var process = FindProcesses().OrderByDescending(o => o.PrivateMemorySize64).FirstOrDefault();
        if (process == null)
            return;

        ramUsage = process.WorkingSet64 / 1024.0 / 1024.0;
    }

    private static DateTime? _lastTime;
    private static TimeSpan _lastTotalProcessorTime;
    /// <summary>Gets the server CPU percentage.</summary>
    public static void GetCpuPercentage(out double cpuUsage)
    {
        cpuUsage = 0;

        if (!IsServerRunning())
            return;

        var process = FindProcesses().OrderByDescending(o => o.TotalProcessorTime).FirstOrDefault();
        if (process == null)
            return;

        if (_lastTime == null)
        {
            _lastTime = DateTime.UtcNow;
            _lastTotalProcessorTime = process.TotalProcessorTime;
        }
        else
        {
            var curTime = DateTime.UtcNow;
            var curTotalProcessorTime = process.TotalProcessorTime;

            double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - _lastTotalProcessorTime.TotalMilliseconds)
                / curTime.Subtract(_lastTime.Value).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
            cpuUsage = CPUUsage * 100;

            _lastTime = curTime;
            _lastTotalProcessorTime = curTotalProcessorTime;
        }
    }

    #region Helpers
    /// <summary>Finds the server processes.</summary>
    private static Process[] FindProcesses()
    {
        List<Process> result = [];
        foreach (var processName in _processNames)
        {
            Process.GetProcessesByName(processName).ToList().ForEach(p => result.Add(p));
        }

        return [.. result];
    }

    internal static string GetServerOnlineDuration()
    {
        if (!IsServerRunning())
            return "0 seconds";

        var process = FindProcesses().FirstOrDefault();
        if (process == null)
            return "0 seconds";

        var time = DateTime.UtcNow - process.StartTime.ToUniversalTime();
        return $"{time.Days} days, {time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds";
    }
    #endregion
}
