namespace SatisServer.UI;

public partial class MainForm : Form
{
    public MainForm()
    {
        Hide();
        InitializeComponent();
        MainForm_Load();
    }

    private void MainForm_Load()
    {
        ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)?.Apply(this);

        Form splashScreen = new SplashScreen();
        splashScreen.ShowDialog();
        Show();
    }
}