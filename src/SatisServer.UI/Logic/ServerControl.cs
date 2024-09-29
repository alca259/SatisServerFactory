using SatisServer.UI.Data;
using System.Net.Http.Json;
using System.Text;

namespace SatisServer.UI.Logic;

internal static class ServerControl
{
    private static ServerGameState _lastServerState = new();
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

    // Get players online
    public static ValueTask<int> GetPlayersOnline() => throw new NotImplementedException();

    /// <summary>Retrieves the current state of the Dedicated Server. Does not require any input parameters.</summary>
    public static async ValueTask<string> GetServerStatus()
    {
        var status = await QueryServerState();
        return $"Server is {(status.IsGameRunning ? "running" : "stopped")}";
    }

    // Get server uptime
    public static ValueTask<string> GetServerUptime() => throw new NotImplementedException();

    // Get last world save
    public static ValueTask<string> GetLastWorldSave() => throw new NotImplementedException();


    /// <summary>Respuesta del HealthCheck</summary>
    /// <param name="Health">"healthy" if tick rate is above ten ticks per second, "slow" otherwise</param>
    /// <param name="ServerCustomData">Custom Data passed from the Dedicated Server to the Game Client or Third Party service. Vanilla Dedicated Server returns empty string</param>
    internal record HealthCheckResponse(string health, string serverCustomData);
    /// <summary>Performs a health check on the Dedicated Server API. Allows passing additional data between Modded Dedicated Server and Modded Game Client. This function requires no Authentication.</summary>
    public static async ValueTask<bool> HealthCheck()
    {
        var response = await QueryApi(new("HealthCheck", new { ClientCustomData = "" }));
        if (response == null || !response.IsSuccessStatusCode) return false;

        var objectResponse = await response.Content.ReadFromJsonAsync<HealthCheckResponse>();
        return objectResponse?.health == "healthy" || objectResponse?.health == "slow";
    }

    internal record QueryServerStateResponse(ServerGameState serverGameState);
    public static async ValueTask<ServerGameState> QueryServerState()
    {
        var response = await QueryApi(new("QueryServerState"));
        if (response == null || !response.IsSuccessStatusCode) return new();

        var objectResponse = await response.Content.ReadFromJsonAsync<QueryServerStateResponse>();
        if (objectResponse?.serverGameState == null) return _lastServerState;

        _lastServerState = objectResponse.serverGameState;
        return _lastServerState;
    }

    #region Helpers
    private sealed record QueryRequestData(string functionName, object? data = null);

    private static async ValueTask<HttpResponseMessage?> QueryApi(QueryRequestData request)
    {
        var fullUri = $"https://{SatisConfig.Instance.ServerIpAddress}:{SatisConfig.Instance.ServerPort}/api/v1";

        try
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };

            using var client = new HttpClient(handler);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsePOST = await client.PostAsync(fullUri, content);
            return responsePOST;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

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
