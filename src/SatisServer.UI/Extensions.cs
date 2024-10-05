using System.Net;

namespace SatisServer.UI;

internal static class Extensions
{
    public static int ConvertToInt(this string? value, int defaultValue = 0) => int.TryParse(value, out var result) ? result : defaultValue;

    public static string ConvertToIpAddress(this string? value, string defaultValue = "127.0.0.1")
    {
        if (string.IsNullOrWhiteSpace(value)) return defaultValue;

        if (IPAddress.TryParse(value, out var ipAddress))
        {
            return ipAddress.ToString();
        }

        return defaultValue;
    }

    public static bool EqualsIgnoreCase(this string? value, string? other) => string.Equals(value, other, StringComparison.InvariantCultureIgnoreCase);
    public static bool ContainsIgnoreCase(this string? value, string? other) => value?.Contains(other ?? string.Empty, StringComparison.InvariantCultureIgnoreCase) ?? false;

    public static void ScrollToEnd(this TextBox textBox)
    {
        if (!textBox.Multiline) return;
        textBox.SelectionStart = textBox.Text.Length;
        textBox.ScrollToCaret();
    }

    public static void AppendText(this RichTextBox textBox, string text, Color color)
    {
        textBox.SelectionStart = textBox.TextLength;
        textBox.SelectionLength = 0;
        textBox.SelectionColor = color;
        textBox.AppendText(text);
        textBox.SelectionColor = textBox.ForeColor;
    }
}
