using SatisServer.UI.Data;

namespace SatisServer.UI.Logic;

/// <summary>Controls the dedicated server.</summary>
internal static class ServerControl
{
    private static readonly string[] _processNames = ["FactoryServer", "UnrealServer-Win64-Shipping", "FactoryServer-Win64-Shipping-Cmd"];

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
            return;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = serverExe,
            Arguments = $"-ServerQueryPort={SatisConfig.Instance.ServerPort} -BeaconPort={SatisConfig.Instance.ServerPort} -Port={SatisConfig.Instance.ServerPort} -log -unattended",
            WorkingDirectory = SatisConfig.Instance.RootPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        var process = new Process { StartInfo = startInfo };
        process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
        process.Start();
        process.BeginOutputReadLine();

        Console.WriteLine("Server started");
    }

    /// <summary>Stops the server.</summary>
    /// <remarks>Does nothing if the server is not running.</remarks>
    public static void StopServer()
    {
        if (!IsServerRunning())
        {
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

        Console.WriteLine("Server stopped");
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
        return processes.Length > 0;
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
    #endregion
}
