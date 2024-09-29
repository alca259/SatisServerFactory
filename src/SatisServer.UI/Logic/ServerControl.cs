using SatisServer.UI.Data;

namespace SatisServer.UI.Logic;

internal static class ServerControl
{
    private static readonly string[] _processNames = ["FactoryServer", "UnrealServer-Win64-Shipping", "FactoryServer-Win64-Shipping-Cmd"];

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

    public static void RestartServer()
    {
        StopServer();
        StartServer();
    }

    // Checks if server is running
    public static bool IsServerRunning()
    {
        var processes = FindProcesses();
        return processes.Length > 0;
    }

    #region Helpers
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
