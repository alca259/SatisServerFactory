using SatisServer.UI.Data;
using SatisServer.UI.Logic;

namespace SatisServer.UI.Screens;

public partial class MainScreen : Form
{
    private const string EmptyDir = "...";
    private readonly System.Windows.Forms.Timer _timer;

    public MainScreen()
    {
        Load += MainForm_Load;
        Hide();
        InitializeComponent();
        ShowSplashScreen();
        LoadConfig();
        SetEvents();
        SetUI();

        _timer = new()
        {
            Interval = 5000
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }



    #region Event Handlers
    private void SetEvents()
    {
        ControlButtonStart.Click += ControlButtonStart_Click;
        ControlButtonStop.Click += ControlButtonStop_Click;
        ControlButtonRestart.Click += ControlButtonRestart_Click;
        ControlNoVisibleConsole.CheckedChanged += (sender, e) => SetConfig();
        ControlUseExperimental.CheckedChanged += (sender, e) => SetConfig();
        ControlDisableEventsSeasonal.CheckedChanged += (sender, e) => SetConfig();
        ControlServerPort.TextChanged += (sender, e) => SetConfig();

        //LogComboFilter
        //LogInfoShow

        SettingsFolderSSButtonSet.Click += SettingsFolderSSButtonSet_Click;
        SettingsFolderSSButtonOpen.Click += (sender, e) => OpenPath(SettingsFolderSSInfo.Text);
        SettingsFolderLogsButtonOpen.Click += (sender, e) => OpenPath(SettingsFolderLogsInfo.Text);
        SettingsFolderSavesButtonOpen.Click += (sender, e) => OpenPath(SettingsFolderSavesInfo.Text);
    }

    private void SetUI()
    {
        bool isRunning = ServerControl.IsServerRunning();
        ControlButtonStart.Enabled = !isRunning;
        ControlButtonStop.Enabled = isRunning;
        ControlButtonRestart.Enabled = isRunning;
        ControlNoVisibleConsole.Enabled = !isRunning;
        ControlUseExperimental.Enabled = !isRunning;
        ControlDisableEventsSeasonal.Enabled = !isRunning;
        ControlServerPort.Enabled = !isRunning;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (!ServerControl.IsServerRunning()) return;

        Task.Factory.StartNew(async () =>
        {
            var hc = await ServerControl.HealthCheck();
            if (!hc)
            {
                Invoke(new Action(() =>
                {
                    ControlInfoStatus.Text = "Starting - No online";
                    ControlInfoPlayers.Text = "0 / 0";
                }));
                return;
            }

            var state = await ServerControl.QueryServerState();

            Invoke(new Action(() =>
            {
                ControlInfoStatus.Text = state.IsGameRunning ? "Started - Game running" : "Started - No session loaded";
                ControlInfoPlayers.Text = $"{state.NumConnectedPlayers} / {state.PlayerLimit}";

                //StatusInfoLastWorldSave

                StatusInfoCurrent.Text = ControlInfoStatus.Text;
                StatusInfoPlayers.Text = ControlInfoPlayers.Text;
                StatusInfoUptime.Text = $"{state.TotalGameDuration} secs";
            }));
        });
    }
    private void ControlButtonStart_Click(object? sender, EventArgs e) => ServerControl.StartServer();
    private void ControlButtonStop_Click(object? sender, EventArgs e) => ServerControl.StopServer();
    private void ControlButtonRestart_Click(object? sender, EventArgs e) => ServerControl.RestartServer();
    private void SettingsFolderSSButtonSet_Click(object? sender, EventArgs e)
    {
        var result = folderBrowserDialog1.ShowDialog();
        if (result != DialogResult.OK)
        {
            return;
        }

        SettingsFolderSSInfo.Text = folderBrowserDialog1.SelectedPath;
        SetConfig();
        SetFolderSaveInfo();
        SetFolderLogsInfo();
    }


    private void MainForm_Load(object? sender, EventArgs e) => ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)?.Apply(this);
    #endregion


    #region Helpers
    private void ShowSplashScreen()
    {
        Form splashScreen = new SplashScreen();
        splashScreen.ShowDialog();
        Show();
    }

    private static void OpenPath(string path)
    {
        if (Directory.Exists(path))
        {
            Process p = new();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = path;
            p.Start();
        }
        else
        {
            MessageBox.Show("Could not find path", "Folder not found!");
        }
    }

    private void SetConfig()
    {
        SatisConfig.Instance.RootPath = SettingsFolderSSInfo.Text;
        SatisConfig.Instance.DisableEventsSeasonal = ControlDisableEventsSeasonal.Checked;
        SatisConfig.Instance.NoVisibleConsole = ControlNoVisibleConsole.Checked;
        SatisConfig.Instance.UseExperimental = ControlUseExperimental.Checked;
        SatisConfig.Instance.ServerPort = ControlServerPort.Text.ConvertToInt(7777);

        string satisConfig = JsonConvert.SerializeObject(SatisConfig.Instance, Formatting.Indented);
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        File.WriteAllText(Path.Combine(currentPath, "satisconfig.json"), satisConfig);
    }

    private void LoadConfig()
    {
        var instanceConfig = SatisConfig.Instance;

        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string satisConfigPath = Path.Combine(currentPath, "satisconfig.json");
        if (!File.Exists(satisConfigPath))
        {
            return;
        }

        string satisConfig = File.ReadAllText(satisConfigPath);
        SatisConfig config = JsonConvert.DeserializeObject<SatisConfig>(satisConfig) ?? new();
        instanceConfig.NoVisibleConsole = config.NoVisibleConsole;
        instanceConfig.UseExperimental = config.UseExperimental;
        instanceConfig.DisableEventsSeasonal = config.DisableEventsSeasonal;
        instanceConfig.ServerPort = config.ServerPort;
        instanceConfig.RootPath = config.RootPath;

        SettingsFolderSSInfo.Text = config.RootPath;
        ControlDisableEventsSeasonal.Checked = config.DisableEventsSeasonal;
        ControlNoVisibleConsole.Checked = config.NoVisibleConsole;
        ControlUseExperimental.Checked = config.UseExperimental;
        ControlServerPort.Text = config.ServerPort.ToString();
        SetFolderSaveInfo();
        SetFolderLogsInfo();
    }

    private void SetFolderSaveInfo()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var savesPath = Path.Combine(appDataFolder, "FactoryGame", "Saved", "SaveGames", "server");
        SettingsFolderSavesInfo.Text = Directory.Exists(savesPath) ? savesPath : EmptyDir;
    }

    private void SetFolderLogsInfo()
    {
        if (SatisConfig.Instance.RootPath == null)
        {
            SettingsFolderLogsInfo.Text = EmptyDir;
            return;
        }

        var logsPath = Path.Combine(SatisConfig.Instance.RootPath, "FactoryGame", "Saved", "Logs");
        SettingsFolderLogsInfo.Text = Directory.Exists(logsPath) ? logsPath : EmptyDir;
    }
    #endregion
}
