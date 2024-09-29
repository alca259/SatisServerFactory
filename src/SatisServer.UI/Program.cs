namespace SatisServer.UI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        StylableWinFormsControlsSettings.DEFAULT.ErrorHandling = ErrorHandling.Continue;

        ThemeRegistryHolder.ThemeRegistry = ThemeRegistryHolder
            .GetBuilder()
            .WithCurrentThemeSelector((selector)
                => ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)).Build();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        Application.Run(new MainForm());
    }
}