namespace SatisServer.UI.Data.Enums;

/// <summary>Admin privilege levels.</summary>
public enum PrivilegeLevel
{
    NotAuthenticated,
    Client,
    Administrator,
    InitialAdmin,
    APIToken
}
