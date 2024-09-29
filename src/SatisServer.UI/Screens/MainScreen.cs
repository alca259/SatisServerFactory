
namespace SatisServer.UI.Screens;

public partial class MainScreen : Form
{
    public MainScreen()
    {
        Load += MainForm_Load;
        Hide();
        InitializeComponent();
        ShowSplashScreen();
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)?.Apply(this);
    }

    private void ShowSplashScreen()
    {
        Form splashScreen = new SplashScreen();
        splashScreen.ShowDialog();
        Show();
    }
}