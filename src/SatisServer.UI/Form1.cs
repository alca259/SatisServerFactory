namespace SatisServer.UI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)?.Apply(this);
    }
}
