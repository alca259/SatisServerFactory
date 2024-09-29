namespace SatisServer.UI.Data.Endpoints.GetServerOptions;

public class ServerOptionsResponse
{
    public Dictionary<string, string> ServerOptions { get; set; }
    public Dictionary<string, string> PendingServerOptions { get; set; }
}
