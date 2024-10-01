namespace SatisServer.UI;

internal static class Extensions
{
    public static int ConvertToInt(this string? value, int defaultValue = 0) => int.TryParse(value, out var result) ? result : defaultValue;

    public static void ScrollToEnd(this TextBox textBox)
    {
        if (!textBox.Multiline) return;
        textBox.SelectionStart = textBox.Text.Length;
        textBox.ScrollToCaret();
    }
}
