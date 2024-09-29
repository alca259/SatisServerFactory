namespace SatisServer.UI.Data.API;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}