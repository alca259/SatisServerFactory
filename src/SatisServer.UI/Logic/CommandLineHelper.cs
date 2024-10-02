using System.Runtime.InteropServices;

namespace SatisServer.UI.Logic;

/// <summary>Contains helper methods for command line operations.</summary>
internal static class CommandLineHelper
{
    const int ShowWindowHide = 0;
    const int ShowWindowShow = 5;

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void HideWindowHandle(IntPtr handle)
    {
        ShowWindow(handle, ShowWindowHide);
    }

    public static void ShowWindowHandle(IntPtr handle)
    {
        ShowWindow(handle, ShowWindowShow);
    }
}
