namespace SatisServer.UI.Screens;

public partial class SplashScreen : Form
{
    private readonly System.Windows.Forms.Timer _timer = new();

    public SplashScreen()
    {
        _timer.Interval = 1000;
        InitializeComponent();
        _timer.Start();
        _timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _timer.Stop();
        _timer.Dispose();
        Close();
    }

}
