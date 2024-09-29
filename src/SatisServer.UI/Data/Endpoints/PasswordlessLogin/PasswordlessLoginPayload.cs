using SatisServer.UI.Data.Enums;

namespace SatisServer.UI.Data.Endpoints.PasswordlessLogin;

public class PasswordlessLoginPayload
{
    private PrivilegeLevel _minimumPrivilegeLevel;

    public string MinimumPrivilegeLevel
    {
        get => _minimumPrivilegeLevel.ToString();
        set
        {
            if (Enum.TryParse(value, out PrivilegeLevel parsedValue))
            {
                _minimumPrivilegeLevel = parsedValue;
            }
            else
            {
                throw new ArgumentException("Invalid PrivilegeLevel value", nameof(value));
            }
        }
    }
}
