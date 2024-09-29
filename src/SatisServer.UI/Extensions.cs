namespace SatisServer.UI;

internal static class Extensions
{
    public static int ConvertToInt(this string? value, int defaultValue = 0) => int.TryParse(value, out var result) ? result : defaultValue;
}
