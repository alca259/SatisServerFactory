using Newtonsoft.Json;
using SatisServer.UI.Data;
using SatisServer.UI.Data.Endpoints.HealthCheck;
using SatisServer.UI.Data.Enums;
using SatisServer.UI.Logic;
using System.Text;

namespace SatisServer.UI.Screens;

public partial class MainScreen : Form
{
    private const string EmptyDir = "...";
    private readonly System.Windows.Forms.Timer _timer;

    public MainScreen()
    {
        _timer = new()
        {
            Interval = 5000
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();

        Load += MainForm_Load;
        Hide();
        LoadConfig();
        InitializeComponent();
        ShowSplashScreen();
        SetEvents();
        SetUI();
        SetWatchers();
    }

    #region Watchers
    private FileSystemWatcher? _logWatcher;
    private FileSystemWatcher? _saveWatcher;

    private void SetWatchers()
    {
        if (!string.IsNullOrEmpty(SatisConfig.Instance.LogDirectory))
        {
            _logWatcher ??= new FileSystemWatcher();

            _logWatcher.Path = SatisConfig.Instance.LogDirectory;
            _logWatcher.Filter = "FactoryGame.log";
            _logWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size;
            _logWatcher.EnableRaisingEvents = true;
            _logWatcher.Changed += LogWatcherOnChanged;
        }

        if (!string.IsNullOrEmpty(SatisConfig.Instance.SaveDirectory))
        {
            _saveWatcher ??= new FileSystemWatcher();

            _saveWatcher.Path = SatisConfig.Instance.SaveDirectory;
            _saveWatcher.Filter = "*.sav";
            _saveWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size;
            _saveWatcher.EnableRaisingEvents = true;
            _saveWatcher.Changed += SaveWatcherOnChanged;
        }
    }

    private void SaveWatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        var humanDate = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss tt");
        Invoke(() =>
        {
            StatusInfoLastWorldSave.Text = humanDate;
        });
    }

    private void LogWatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        UpdateServerLog(e.FullPath);
    }

    #endregion

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
        LogComboFilter.SelectedIndexChanged += (sender, e) =>
        {
            if (string.IsNullOrEmpty(SatisConfig.Instance.LogDirectory)) return;
            UpdateServerLog(Path.Combine(SatisConfig.Instance.LogDirectory, "FactoryGame.log"));
        };
        SettingsFolderSSButtonSet.Click += SettingsFolderSSButtonSet_Click;
        SettingsFolderSSButtonOpen.Click += (sender, e) => OpenPath(SatisConfig.Instance.RootPath);
        SettingsFolderLogsButtonOpen.Click += (sender, e) => OpenPath(SatisConfig.Instance.LogDirectory);
        SettingsFolderSavesButtonOpen.Click += (sender, e) => OpenPath(SatisConfig.Instance.SaveDirectory);
    }

    private void SetUI()
    {
        ControlDisableEventsSeasonal.Checked = SatisConfig.Instance.DisableEventsSeasonal;
        ControlNoVisibleConsole.Checked = SatisConfig.Instance.NoVisibleConsole;
        ControlUseExperimental.Checked = SatisConfig.Instance.UseExperimental;
        ControlServerPort.Text = SatisConfig.Instance.ServerPort.ToString();
        SetFolders();
        TriggerButtons();

        LogComboFilter.Items.Clear();
        foreach (var logEl in Enum.GetValues(typeof(FactoryLogPrefix)))
        {
            LogComboFilter.Items.Add(logEl);
        }

        LogComboFilter.SelectedIndex = 0;
    }

    private void TriggerButtons()
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
            var apiClient = DedicatedServerApiClient.Instance;

            // Perform health check
            var healthStatus = await apiClient.HealthCheck(new HealthCheckPayload());

            if (healthStatus == null)
            {
                Invoke(new Action(() =>
                {
                    ControlInfoStatus.Text = "Starting - No online";
                    ControlInfoPlayers.Text = "0 / 0";
                }));
                return;
            }

            // Authenticate
            var authToken = await ApiFunctions.PasswordLogin(apiClient, PrivilegeLevel.Administrator, SatisConfig.Instance.AdminPassword);

            // Set the authentication token for future requests
            apiClient.SetAuthToken(authToken);

            var state = await apiClient.GetServerState();
            Invoke(new Action(() =>
            {
                ControlInfoStatus.Text = state.GetServerState();
                ControlInfoPlayers.Text = state.GetPlayerCount();

                //StatusInfoLastWorldSave.Text = state.GetTickRate()

                StatusInfoCurrent.Text = state.GetServerState();
                StatusInfoPlayers.Text = state.GetPlayerCount();
                StatusInfoUptime.Text = state.GetSessionDuration();
            }));
        });
    }

    private void ControlButtonStart_Click(object? sender, EventArgs e)
    {
        ServerControl.StartServer();
        TriggerButtons();
    }

    private void ControlButtonStop_Click(object? sender, EventArgs e)
    {
        ServerControl.StopServer();

        ControlInfoStatus.Text = EmptyDir;
        ControlInfoPlayers.Text = EmptyDir;
        StatusInfoLastWorldSave.Text = EmptyDir;
        StatusInfoCurrent.Text = EmptyDir;
        StatusInfoPlayers.Text = EmptyDir;
        StatusInfoUptime.Text = EmptyDir;
        TriggerButtons();
    }

    private void ControlButtonRestart_Click(object? sender, EventArgs e)
    {
        TriggerButtons();
        ServerControl.RestartServer();
        TriggerButtons();
    }

    private void SettingsFolderSSButtonSet_Click(object? sender, EventArgs e)
    {
        var result = folderBrowserDialog1.ShowDialog();
        if (result != DialogResult.OK)
        {
            return;
        }

        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        SatisConfig.Instance.RootPath = folderBrowserDialog1.SelectedPath;
        SatisConfig.Instance.LogDirectory = Path.Combine(SatisConfig.Instance.RootPath, "FactoryGame", "Saved", "Logs");
        SatisConfig.Instance.SaveDirectory = Path.Combine(appDataFolder, "FactoryGame", "Saved", "SaveGames", "server");

        SetConfig();
        SetFolders();
        SetWatchers();
    }

    private void MainForm_Load(object? sender, EventArgs e)
        => ThemeRegistryHolder.ThemeRegistry!.GetTheme(ThemeCapabilities.DarkMode)?.Apply(this);
    #endregion

    #region Helpers
    private void ShowSplashScreen()
    {
        Form splashScreen = new SplashScreen();
        splashScreen.ShowDialog();
        Show();
    }

    private static void OpenPath(string? path)
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
        SatisConfig.Instance.DisableEventsSeasonal = ControlDisableEventsSeasonal.Checked;
        SatisConfig.Instance.NoVisibleConsole = ControlNoVisibleConsole.Checked;
        SatisConfig.Instance.UseExperimental = ControlUseExperimental.Checked;
        SatisConfig.Instance.ServerPort = ControlServerPort.Text.ConvertToInt(7777);

        string satisConfig = JsonConvert.SerializeObject(SatisConfig.Instance, Formatting.Indented);
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        File.WriteAllText(Path.Combine(currentPath, "satisconfig.json"), satisConfig);
    }

    private static void LoadConfig()
    {
        var instanceConfig = SatisConfig.Instance;

        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string satisConfigPath = Path.Combine(currentPath, "satisconfig.json");
        if (!File.Exists(satisConfigPath))
        {
            return;
        }

        string satisConfig = File.ReadAllText(satisConfigPath);
        SatisConfig? config = JsonConvert.DeserializeObject<SatisConfig>(satisConfig);
        instanceConfig.CopyValues(config);
    }

    private void SetFolders()
    {
        SettingsFolderSSInfo.Text = SatisConfig.Instance.RootPath;
        SettingsFolderLogsInfo.Text = SatisConfig.Instance.LogDirectory;
        SettingsFolderSavesInfo.Text = SatisConfig.Instance.SaveDirectory;
    }

    private void UpdateServerLog(string fullPath)
    {
        if (!File.Exists(fullPath)) return;
        var logData = "";

        using (StreamReader reader = new(new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
        {
            logData = reader.ReadToEnd();
            logData = FilterFactoryLogs(logData);
        }

        Invoke(() =>
        {
            LogInfoShow.Text = logData;
            LogInfoShow.ScrollToEnd();
        });
    }

    private string FilterFactoryLogs(string logData)
    {
        FactoryLogPrefix factoryFilter = FactoryLogPrefix.All;
        Invoke(() =>
        {
            if (LogComboFilter.SelectedItem is FactoryLogPrefix t)
                factoryFilter = t;
        });


        if (factoryFilter == FactoryLogPrefix.All)
        {
            return logData;
        }

        var lines = logData.Split(Environment.NewLine);
        var filterString = factoryFilter.ToString();
        StringBuilder filteredLogs = new();
        foreach (var line in lines.Where(l => l.Contains(filterString)))
        {
            filteredLogs.AppendLine(line);
        }

        return filteredLogs.ToString().Trim(Environment.NewLine.ToCharArray());
    }
    #endregion
}
