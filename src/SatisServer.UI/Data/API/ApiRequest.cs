namespace SatisServer.UI.Data.API;

public class ApiRequest
{
    public string Function { get; set; } = string.Empty;
    public object? Data { get; set; }
}