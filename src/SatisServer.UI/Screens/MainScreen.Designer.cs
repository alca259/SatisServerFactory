namespace SatisServer.UI.Screens;

partial class MainScreen
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
        stylableTabControl1 = new StylableTabControl();
        tabPage1 = new TabPage();
        groupBox1 = new GroupBox();
        ControlInfoPlayers = new StylableLabel();
        ControlInfoStatus = new StylableLabel();
        stylableLabel12 = new StylableLabel();
        stylableLabel13 = new StylableLabel();
        panel2 = new Panel();
        panel1 = new Panel();
        stylableGroupBox2 = new StylableGroupBox();
        ControlButtonRestart = new StylableButton();
        ControlButtonStop = new StylableButton();
        ControlButtonStart = new StylableButton();
        stylableGroupBox1 = new StylableGroupBox();
        ControlDisableEventsSeasonal = new StylableCheckBox();
        ControlServerPort = new StylableTextBox();
        stylableLabel3 = new StylableLabel();
        ControlUseExperimental = new StylableCheckBox();
        ControlNoVisibleConsole = new StylableCheckBox();
        tabPage2 = new TabPage();
        tableLayoutPanel2 = new TableLayoutPanel();
        StatusInfoUptime = new StylableLabel();
        stylableLabel10 = new StylableLabel();
        StatusInfoLastWorldSave = new StylableLabel();
        stylableLabel8 = new StylableLabel();
        StatusInfoPlayers = new StylableLabel();
        StatusInfoCurrent = new StylableLabel();
        stylableLabel4 = new StylableLabel();
        stylableLabel5 = new StylableLabel();
        tabPage3 = new TabPage();
        LogInfoShow = new StylableTextBox();
        LogComboFilter = new StylableComboBox();
        stylableLabel14 = new StylableLabel();
        tabPage4 = new TabPage();
        groupBox2 = new GroupBox();
        panel5 = new Panel();
        SettingsFolderLogsButtonOpen = new StylableButton();
        SettingsFolderLogsInfo = new StylableLabel();
        stylableLabel20 = new StylableLabel();
        panel4 = new Panel();
        SettingsFolderSavesButtonOpen = new StylableButton();
        SettingsFolderSavesInfo = new StylableLabel();
        stylableLabel18 = new StylableLabel();
        panel3 = new Panel();
        SettingsFolderSSButtonOpen = new StylableButton();
        SettingsFolderSSInfo = new StylableLabel();
        stylableLabel15 = new StylableLabel();
        SettingsFolderSSButtonSet = new StylableButton();
        folderBrowserDialog1 = new FolderBrowserDialog();
        stylableTabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        groupBox1.SuspendLayout();
        panel1.SuspendLayout();
        stylableGroupBox2.SuspendLayout();
        stylableGroupBox1.SuspendLayout();
        tabPage2.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tabPage3.SuspendLayout();
        tabPage4.SuspendLayout();
        groupBox2.SuspendLayout();
        panel5.SuspendLayout();
        panel4.SuspendLayout();
        panel3.SuspendLayout();
        SuspendLayout();
        // 
        // stylableTabControl1
        // 
        stylableTabControl1.ActiveTabBackgroundColor = SystemColors.Control;
        stylableTabControl1.ActiveTabForegroundColor = SystemColors.ControlText;
        stylableTabControl1.BackgroundColor = SystemColors.Control;
        stylableTabControl1.BorderColor = SystemColors.ControlDark;
        stylableTabControl1.Controls.Add(tabPage1);
        stylableTabControl1.Controls.Add(tabPage2);
        stylableTabControl1.Controls.Add(tabPage3);
        stylableTabControl1.Controls.Add(tabPage4);
        stylableTabControl1.Dock = DockStyle.Fill;
        stylableTabControl1.Location = new Point(0, 0);
        stylableTabControl1.Name = "stylableTabControl1";
        stylableTabControl1.SelectedIndex = 0;
        stylableTabControl1.Size = new Size(800, 450);
        stylableTabControl1.TabIndex = 0;
        stylableTabControl1.UseRoundedCorners = false;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(groupBox1);
        tabPage1.Controls.Add(panel2);
        tabPage1.Controls.Add(panel1);
        tabPage1.Location = new Point(4, 25);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(792, 421);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Server controls";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(ControlInfoPlayers);
        groupBox1.Controls.Add(ControlInfoStatus);
        groupBox1.Controls.Add(stylableLabel12);
        groupBox1.Controls.Add(stylableLabel13);
        groupBox1.Location = new Point(11, 9);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(770, 72);
        groupBox1.TabIndex = 3;
        groupBox1.TabStop = false;
        groupBox1.Text = "Current";
        // 
        // ControlInfoPlayers
        // 
        ControlInfoPlayers.AutoSize = true;
        ControlInfoPlayers.DisabledForeColor = Color.Empty;
        ControlInfoPlayers.Location = new Point(90, 44);
        ControlInfoPlayers.Name = "ControlInfoPlayers";
        ControlInfoPlayers.Size = new Size(16, 15);
        ControlInfoPlayers.TabIndex = 4;
        ControlInfoPlayers.Text = "...";
        // 
        // ControlInfoStatus
        // 
        ControlInfoStatus.AutoSize = true;
        ControlInfoStatus.DisabledForeColor = Color.Empty;
        ControlInfoStatus.Location = new Point(90, 19);
        ControlInfoStatus.Name = "ControlInfoStatus";
        ControlInfoStatus.Size = new Size(16, 15);
        ControlInfoStatus.TabIndex = 3;
        ControlInfoStatus.Text = "...";
        // 
        // stylableLabel12
        // 
        stylableLabel12.AutoSize = true;
        stylableLabel12.DisabledForeColor = Color.Empty;
        stylableLabel12.Location = new Point(8, 19);
        stylableLabel12.Name = "stylableLabel12";
        stylableLabel12.Size = new Size(39, 15);
        stylableLabel12.TabIndex = 1;
        stylableLabel12.Text = "Status";
        stylableLabel12.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // stylableLabel13
        // 
        stylableLabel13.AutoSize = true;
        stylableLabel13.DisabledForeColor = Color.Empty;
        stylableLabel13.Location = new Point(8, 44);
        stylableLabel13.Name = "stylableLabel13";
        stylableLabel13.Size = new Size(44, 15);
        stylableLabel13.TabIndex = 2;
        stylableLabel13.Text = "Players";
        stylableLabel13.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel2
        // 
        panel2.Location = new Point(8, 6);
        panel2.Name = "panel2";
        panel2.Size = new Size(776, 75);
        panel2.TabIndex = 2;
        // 
        // panel1
        // 
        panel1.Controls.Add(stylableGroupBox2);
        panel1.Controls.Add(stylableGroupBox1);
        panel1.Location = new Point(8, 87);
        panel1.Name = "panel1";
        panel1.Size = new Size(776, 326);
        panel1.TabIndex = 1;
        // 
        // stylableGroupBox2
        // 
        stylableGroupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        stylableGroupBox2.Controls.Add(ControlButtonRestart);
        stylableGroupBox2.Controls.Add(ControlButtonStop);
        stylableGroupBox2.Controls.Add(ControlButtonStart);
        stylableGroupBox2.DisabledForeColor = SystemColors.GrayText;
        stylableGroupBox2.EnabledForeColor = SystemColors.ControlText;
        stylableGroupBox2.Location = new Point(3, 281);
        stylableGroupBox2.Name = "stylableGroupBox2";
        stylableGroupBox2.Size = new Size(770, 45);
        stylableGroupBox2.TabIndex = 1;
        stylableGroupBox2.TabStop = false;
        stylableGroupBox2.Text = "Control";
        // 
        // ControlButtonRestart
        // 
        ControlButtonRestart.BorderColor = Color.Black;
        ControlButtonRestart.DisabledBackColor = Color.Gray;
        ControlButtonRestart.DisabledForeColor = Color.Black;
        ControlButtonRestart.EnabledBackColor = Color.White;
        ControlButtonRestart.EnabledForeColor = Color.Black;
        ControlButtonRestart.EnabledHoverColor = Color.LightGray;
        ControlButtonRestart.Location = new Point(168, 16);
        ControlButtonRestart.Name = "ControlButtonRestart";
        ControlButtonRestart.Size = new Size(75, 23);
        ControlButtonRestart.TabIndex = 2;
        ControlButtonRestart.Text = "Restart";
        ControlButtonRestart.UseVisualStyleBackColor = true;
        // 
        // ControlButtonStop
        // 
        ControlButtonStop.BorderColor = Color.Black;
        ControlButtonStop.DisabledBackColor = Color.Gray;
        ControlButtonStop.DisabledForeColor = Color.Black;
        ControlButtonStop.EnabledBackColor = Color.White;
        ControlButtonStop.EnabledForeColor = Color.Black;
        ControlButtonStop.EnabledHoverColor = Color.LightGray;
        ControlButtonStop.Location = new Point(87, 16);
        ControlButtonStop.Name = "ControlButtonStop";
        ControlButtonStop.Size = new Size(75, 23);
        ControlButtonStop.TabIndex = 1;
        ControlButtonStop.Text = "Stop";
        ControlButtonStop.UseVisualStyleBackColor = true;
        // 
        // ControlButtonStart
        // 
        ControlButtonStart.BorderColor = Color.Black;
        ControlButtonStart.DisabledBackColor = Color.Gray;
        ControlButtonStart.DisabledForeColor = Color.Black;
        ControlButtonStart.EnabledBackColor = Color.White;
        ControlButtonStart.EnabledForeColor = Color.Black;
        ControlButtonStart.EnabledHoverColor = Color.LightGray;
        ControlButtonStart.Location = new Point(6, 16);
        ControlButtonStart.Name = "ControlButtonStart";
        ControlButtonStart.Size = new Size(75, 23);
        ControlButtonStart.TabIndex = 0;
        ControlButtonStart.Text = "Start";
        ControlButtonStart.UseVisualStyleBackColor = true;
        // 
        // stylableGroupBox1
        // 
        stylableGroupBox1.Controls.Add(ControlDisableEventsSeasonal);
        stylableGroupBox1.Controls.Add(ControlServerPort);
        stylableGroupBox1.Controls.Add(stylableLabel3);
        stylableGroupBox1.Controls.Add(ControlUseExperimental);
        stylableGroupBox1.Controls.Add(ControlNoVisibleConsole);
        stylableGroupBox1.DisabledForeColor = SystemColors.GrayText;
        stylableGroupBox1.EnabledForeColor = SystemColors.ControlText;
        stylableGroupBox1.Location = new Point(3, 3);
        stylableGroupBox1.Name = "stylableGroupBox1";
        stylableGroupBox1.Size = new Size(770, 126);
        stylableGroupBox1.TabIndex = 0;
        stylableGroupBox1.TabStop = false;
        stylableGroupBox1.Text = "Options";
        // 
        // ControlDisableEventsSeasonal
        // 
        ControlDisableEventsSeasonal.DisabledForeColor = Color.Empty;
        ControlDisableEventsSeasonal.Location = new Point(6, 82);
        ControlDisableEventsSeasonal.Name = "ControlDisableEventsSeasonal";
        ControlDisableEventsSeasonal.Size = new Size(176, 24);
        ControlDisableEventsSeasonal.TabIndex = 4;
        ControlDisableEventsSeasonal.Text = "Disable seasonal events";
        ControlDisableEventsSeasonal.UseVisualStyleBackColor = true;
        // 
        // ControlServerPort
        // 
        ControlServerPort.BorderColor = Color.Blue;
        ControlServerPort.BorderStyle = BorderStyle.FixedSingle;
        ControlServerPort.DelayedTextChangedTimeout = 900;
        ControlServerPort.IsDelayActive = true;
        ControlServerPort.Location = new Point(300, 23);
        ControlServerPort.MaxLength = 5;
        ControlServerPort.Name = "ControlServerPort";
        ControlServerPort.PlaceholderForeColor = Color.Gray;
        ControlServerPort.PlaceholderText = "7777";
        ControlServerPort.Size = new Size(100, 23);
        ControlServerPort.TabIndex = 3;
        ControlServerPort.Text = "7777";
        // 
        // stylableLabel3
        // 
        stylableLabel3.AutoSize = true;
        stylableLabel3.DisabledForeColor = Color.Empty;
        stylableLabel3.Location = new Point(230, 26);
        stylableLabel3.Name = "stylableLabel3";
        stylableLabel3.Size = new Size(64, 15);
        stylableLabel3.TabIndex = 2;
        stylableLabel3.Text = "Server port";
        // 
        // ControlUseExperimental
        // 
        ControlUseExperimental.DisabledForeColor = Color.Empty;
        ControlUseExperimental.Location = new Point(6, 52);
        ControlUseExperimental.Name = "ControlUseExperimental";
        ControlUseExperimental.Size = new Size(176, 24);
        ControlUseExperimental.TabIndex = 1;
        ControlUseExperimental.Text = "Use experimental version";
        ControlUseExperimental.UseVisualStyleBackColor = true;
        // 
        // ControlNoVisibleConsole
        // 
        ControlNoVisibleConsole.Checked = true;
        ControlNoVisibleConsole.CheckState = CheckState.Checked;
        ControlNoVisibleConsole.DisabledForeColor = Color.Empty;
        ControlNoVisibleConsole.Location = new Point(6, 22);
        ControlNoVisibleConsole.Name = "ControlNoVisibleConsole";
        ControlNoVisibleConsole.Size = new Size(176, 24);
        ControlNoVisibleConsole.TabIndex = 0;
        ControlNoVisibleConsole.Text = "No visible console";
        ControlNoVisibleConsole.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(tableLayoutPanel2);
        tabPage2.Location = new Point(4, 25);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(192, 71);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Stats";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.54902F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.45098F));
        tableLayoutPanel2.Controls.Add(StatusInfoUptime, 1, 3);
        tableLayoutPanel2.Controls.Add(stylableLabel10, 0, 3);
        tableLayoutPanel2.Controls.Add(StatusInfoLastWorldSave, 1, 2);
        tableLayoutPanel2.Controls.Add(stylableLabel8, 0, 2);
        tableLayoutPanel2.Controls.Add(StatusInfoPlayers, 1, 1);
        tableLayoutPanel2.Controls.Add(StatusInfoCurrent, 1, 0);
        tableLayoutPanel2.Controls.Add(stylableLabel4, 0, 0);
        tableLayoutPanel2.Controls.Add(stylableLabel5, 0, 1);
        tableLayoutPanel2.Location = new Point(6, 6);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 4;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableLayoutPanel2.Size = new Size(776, 104);
        tableLayoutPanel2.TabIndex = 1;
        // 
        // StatusInfoUptime
        // 
        StatusInfoUptime.DisabledForeColor = Color.Empty;
        StatusInfoUptime.Dock = DockStyle.Fill;
        StatusInfoUptime.Location = new Point(102, 76);
        StatusInfoUptime.Name = "StatusInfoUptime";
        StatusInfoUptime.Size = new Size(670, 27);
        StatusInfoUptime.TabIndex = 7;
        StatusInfoUptime.Text = "...";
        StatusInfoUptime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // stylableLabel10
        // 
        stylableLabel10.DisabledForeColor = Color.Empty;
        stylableLabel10.Dock = DockStyle.Fill;
        stylableLabel10.Location = new Point(4, 76);
        stylableLabel10.Name = "stylableLabel10";
        stylableLabel10.Size = new Size(91, 27);
        stylableLabel10.TabIndex = 6;
        stylableLabel10.Text = "Uptime";
        stylableLabel10.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // StatusInfoLastWorldSave
        // 
        StatusInfoLastWorldSave.DisabledForeColor = Color.Empty;
        StatusInfoLastWorldSave.Dock = DockStyle.Fill;
        StatusInfoLastWorldSave.Location = new Point(102, 51);
        StatusInfoLastWorldSave.Name = "StatusInfoLastWorldSave";
        StatusInfoLastWorldSave.Size = new Size(670, 24);
        StatusInfoLastWorldSave.TabIndex = 5;
        StatusInfoLastWorldSave.Text = "...";
        StatusInfoLastWorldSave.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // stylableLabel8
        // 
        stylableLabel8.DisabledForeColor = Color.Empty;
        stylableLabel8.Dock = DockStyle.Fill;
        stylableLabel8.Location = new Point(4, 51);
        stylableLabel8.Name = "stylableLabel8";
        stylableLabel8.Size = new Size(91, 24);
        stylableLabel8.TabIndex = 4;
        stylableLabel8.Text = "Last world save";
        stylableLabel8.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // StatusInfoPlayers
        // 
        StatusInfoPlayers.DisabledForeColor = Color.Empty;
        StatusInfoPlayers.Dock = DockStyle.Fill;
        StatusInfoPlayers.Location = new Point(102, 26);
        StatusInfoPlayers.Name = "StatusInfoPlayers";
        StatusInfoPlayers.Size = new Size(670, 24);
        StatusInfoPlayers.TabIndex = 3;
        StatusInfoPlayers.Text = "...";
        StatusInfoPlayers.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // StatusInfoCurrent
        // 
        StatusInfoCurrent.DisabledForeColor = Color.Empty;
        StatusInfoCurrent.Dock = DockStyle.Fill;
        StatusInfoCurrent.Location = new Point(102, 1);
        StatusInfoCurrent.Name = "StatusInfoCurrent";
        StatusInfoCurrent.Size = new Size(670, 24);
        StatusInfoCurrent.TabIndex = 2;
        StatusInfoCurrent.Text = "...";
        StatusInfoCurrent.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // stylableLabel4
        // 
        stylableLabel4.DisabledForeColor = Color.Empty;
        stylableLabel4.Dock = DockStyle.Fill;
        stylableLabel4.Location = new Point(4, 1);
        stylableLabel4.Name = "stylableLabel4";
        stylableLabel4.Size = new Size(91, 24);
        stylableLabel4.TabIndex = 0;
        stylableLabel4.Text = "Status";
        stylableLabel4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // stylableLabel5
        // 
        stylableLabel5.DisabledForeColor = Color.Empty;
        stylableLabel5.Dock = DockStyle.Fill;
        stylableLabel5.Location = new Point(4, 26);
        stylableLabel5.Name = "stylableLabel5";
        stylableLabel5.Size = new Size(91, 24);
        stylableLabel5.TabIndex = 1;
        stylableLabel5.Text = "Players";
        stylableLabel5.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tabPage3
        // 
        tabPage3.Controls.Add(LogInfoShow);
        tabPage3.Controls.Add(LogComboFilter);
        tabPage3.Controls.Add(stylableLabel14);
        tabPage3.Location = new Point(4, 25);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new Padding(3);
        tabPage3.Size = new Size(192, 71);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Log";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // LogInfoShow
        // 
        LogInfoShow.BorderColor = Color.Blue;
        LogInfoShow.BorderStyle = BorderStyle.FixedSingle;
        LogInfoShow.DelayedTextChangedTimeout = 900;
        LogInfoShow.IsDelayActive = true;
        LogInfoShow.Location = new Point(14, 46);
        LogInfoShow.Multiline = true;
        LogInfoShow.Name = "LogInfoShow";
        LogInfoShow.PlaceholderForeColor = Color.Gray;
        LogInfoShow.ReadOnly = true;
        LogInfoShow.ScrollBars = ScrollBars.Both;
        LogInfoShow.Size = new Size(770, 367);
        LogInfoShow.TabIndex = 2;
        LogInfoShow.TabStop = false;
        // 
        // LogComboFilter
        // 
        LogComboFilter.BorderColor = SystemColors.ControlDark;
        LogComboFilter.DrawMode = DrawMode.OwnerDrawFixed;
        LogComboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        LogComboFilter.FormattingEnabled = true;
        LogComboFilter.ItemHoverColor = SystemColors.Highlight;
        LogComboFilter.Items.AddRange(new object[] { "Debug", "Information", "Warning", "Only errors" });
        LogComboFilter.Location = new Point(53, 16);
        LogComboFilter.Name = "LogComboFilter";
        LogComboFilter.Size = new Size(121, 24);
        LogComboFilter.TabIndex = 1;
        // 
        // stylableLabel14
        // 
        stylableLabel14.AutoSize = true;
        stylableLabel14.DisabledForeColor = Color.Empty;
        stylableLabel14.Location = new Point(14, 19);
        stylableLabel14.Name = "stylableLabel14";
        stylableLabel14.Size = new Size(33, 15);
        stylableLabel14.TabIndex = 0;
        stylableLabel14.Text = "Filter";
        // 
        // tabPage4
        // 
        tabPage4.Controls.Add(groupBox2);
        tabPage4.Location = new Point(4, 25);
        tabPage4.Name = "tabPage4";
        tabPage4.Padding = new Padding(3);
        tabPage4.Size = new Size(192, 71);
        tabPage4.TabIndex = 3;
        tabPage4.Text = "Settings";
        tabPage4.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(panel5);
        groupBox2.Controls.Add(panel4);
        groupBox2.Controls.Add(panel3);
        groupBox2.Location = new Point(6, 6);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(778, 158);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Folders";
        // 
        // panel5
        // 
        panel5.Controls.Add(SettingsFolderLogsButtonOpen);
        panel5.Controls.Add(SettingsFolderLogsInfo);
        panel5.Controls.Add(stylableLabel20);
        panel5.Location = new Point(6, 110);
        panel5.Name = "panel5";
        panel5.Size = new Size(766, 38);
        panel5.TabIndex = 5;
        // 
        // SettingsFolderLogsButtonOpen
        // 
        SettingsFolderLogsButtonOpen.BorderColor = Color.Black;
        SettingsFolderLogsButtonOpen.DisabledBackColor = Color.Gray;
        SettingsFolderLogsButtonOpen.DisabledForeColor = Color.Black;
        SettingsFolderLogsButtonOpen.EnabledBackColor = Color.White;
        SettingsFolderLogsButtonOpen.EnabledForeColor = Color.Black;
        SettingsFolderLogsButtonOpen.EnabledHoverColor = Color.LightGray;
        SettingsFolderLogsButtonOpen.Location = new Point(111, 7);
        SettingsFolderLogsButtonOpen.Name = "SettingsFolderLogsButtonOpen";
        SettingsFolderLogsButtonOpen.Size = new Size(37, 23);
        SettingsFolderLogsButtonOpen.TabIndex = 3;
        SettingsFolderLogsButtonOpen.Text = "Open";
        SettingsFolderLogsButtonOpen.UseVisualStyleBackColor = true;
        // 
        // SettingsFolderLogsInfo
        // 
        SettingsFolderLogsInfo.AutoSize = true;
        SettingsFolderLogsInfo.DisabledForeColor = Color.Empty;
        SettingsFolderLogsInfo.Location = new Point(208, 11);
        SettingsFolderLogsInfo.Name = "SettingsFolderLogsInfo";
        SettingsFolderLogsInfo.Size = new Size(16, 15);
        SettingsFolderLogsInfo.TabIndex = 2;
        SettingsFolderLogsInfo.Text = "...";
        // 
        // stylableLabel20
        // 
        stylableLabel20.AutoSize = true;
        stylableLabel20.DisabledForeColor = Color.Empty;
        stylableLabel20.Location = new Point(3, 11);
        stylableLabel20.Name = "stylableLabel20";
        stylableLabel20.Size = new Size(32, 15);
        stylableLabel20.TabIndex = 0;
        stylableLabel20.Text = "Logs";
        // 
        // panel4
        // 
        panel4.Controls.Add(SettingsFolderSavesButtonOpen);
        panel4.Controls.Add(SettingsFolderSavesInfo);
        panel4.Controls.Add(stylableLabel18);
        panel4.Location = new Point(6, 66);
        panel4.Name = "panel4";
        panel4.Size = new Size(766, 38);
        panel4.TabIndex = 4;
        // 
        // SettingsFolderSavesButtonOpen
        // 
        SettingsFolderSavesButtonOpen.BorderColor = Color.Black;
        SettingsFolderSavesButtonOpen.DisabledBackColor = Color.Gray;
        SettingsFolderSavesButtonOpen.DisabledForeColor = Color.Black;
        SettingsFolderSavesButtonOpen.EnabledBackColor = Color.White;
        SettingsFolderSavesButtonOpen.EnabledForeColor = Color.Black;
        SettingsFolderSavesButtonOpen.EnabledHoverColor = Color.LightGray;
        SettingsFolderSavesButtonOpen.Location = new Point(111, 7);
        SettingsFolderSavesButtonOpen.Name = "SettingsFolderSavesButtonOpen";
        SettingsFolderSavesButtonOpen.Size = new Size(37, 23);
        SettingsFolderSavesButtonOpen.TabIndex = 3;
        SettingsFolderSavesButtonOpen.Text = "Open";
        SettingsFolderSavesButtonOpen.UseVisualStyleBackColor = true;
        // 
        // SettingsFolderSavesInfo
        // 
        SettingsFolderSavesInfo.AutoSize = true;
        SettingsFolderSavesInfo.DisabledForeColor = Color.Empty;
        SettingsFolderSavesInfo.Location = new Point(208, 11);
        SettingsFolderSavesInfo.Name = "SettingsFolderSavesInfo";
        SettingsFolderSavesInfo.Size = new Size(16, 15);
        SettingsFolderSavesInfo.TabIndex = 2;
        SettingsFolderSavesInfo.Text = "...";
        // 
        // stylableLabel18
        // 
        stylableLabel18.AutoSize = true;
        stylableLabel18.DisabledForeColor = Color.Empty;
        stylableLabel18.Location = new Point(3, 11);
        stylableLabel18.Name = "stylableLabel18";
        stylableLabel18.Size = new Size(36, 15);
        stylableLabel18.TabIndex = 0;
        stylableLabel18.Text = "Saves";
        // 
        // panel3
        // 
        panel3.Controls.Add(SettingsFolderSSButtonOpen);
        panel3.Controls.Add(SettingsFolderSSInfo);
        panel3.Controls.Add(stylableLabel15);
        panel3.Controls.Add(SettingsFolderSSButtonSet);
        panel3.Location = new Point(6, 22);
        panel3.Name = "panel3";
        panel3.Size = new Size(766, 38);
        panel3.TabIndex = 3;
        // 
        // SettingsFolderSSButtonOpen
        // 
        SettingsFolderSSButtonOpen.BorderColor = Color.Black;
        SettingsFolderSSButtonOpen.DisabledBackColor = Color.Gray;
        SettingsFolderSSButtonOpen.DisabledForeColor = Color.Black;
        SettingsFolderSSButtonOpen.EnabledBackColor = Color.White;
        SettingsFolderSSButtonOpen.EnabledForeColor = Color.Black;
        SettingsFolderSSButtonOpen.EnabledHoverColor = Color.LightGray;
        SettingsFolderSSButtonOpen.Location = new Point(154, 7);
        SettingsFolderSSButtonOpen.Name = "SettingsFolderSSButtonOpen";
        SettingsFolderSSButtonOpen.Size = new Size(37, 23);
        SettingsFolderSSButtonOpen.TabIndex = 3;
        SettingsFolderSSButtonOpen.Text = "Open";
        SettingsFolderSSButtonOpen.UseVisualStyleBackColor = true;
        // 
        // SettingsFolderSSInfo
        // 
        SettingsFolderSSInfo.AutoSize = true;
        SettingsFolderSSInfo.DisabledForeColor = Color.Empty;
        SettingsFolderSSInfo.Location = new Point(208, 11);
        SettingsFolderSSInfo.Name = "SettingsFolderSSInfo";
        SettingsFolderSSInfo.Size = new Size(16, 15);
        SettingsFolderSSInfo.TabIndex = 2;
        SettingsFolderSSInfo.Text = "...";
        // 
        // stylableLabel15
        // 
        stylableLabel15.AutoSize = true;
        stylableLabel15.DisabledForeColor = Color.Empty;
        stylableLabel15.Location = new Point(3, 11);
        stylableLabel15.Name = "stylableLabel15";
        stylableLabel15.Size = new Size(102, 15);
        stylableLabel15.TabIndex = 0;
        stylableLabel15.Text = "Satisfactory server";
        // 
        // SettingsFolderSSButtonSet
        // 
        SettingsFolderSSButtonSet.BorderColor = Color.Black;
        SettingsFolderSSButtonSet.DisabledBackColor = Color.Gray;
        SettingsFolderSSButtonSet.DisabledForeColor = Color.Black;
        SettingsFolderSSButtonSet.EnabledBackColor = Color.White;
        SettingsFolderSSButtonSet.EnabledForeColor = Color.Black;
        SettingsFolderSSButtonSet.EnabledHoverColor = Color.LightGray;
        SettingsFolderSSButtonSet.Location = new Point(111, 7);
        SettingsFolderSSButtonSet.Name = "SettingsFolderSSButtonSet";
        SettingsFolderSSButtonSet.Size = new Size(37, 23);
        SettingsFolderSSButtonSet.TabIndex = 1;
        SettingsFolderSSButtonSet.Text = "Set";
        SettingsFolderSSButtonSet.UseVisualStyleBackColor = true;
        // 
        // folderBrowserDialog1
        // 
        folderBrowserDialog1.AddToRecent = false;
        folderBrowserDialog1.Description = "Select folder root of SatisfactoryServerDedicated";
        folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
        folderBrowserDialog1.ShowNewFolderButton = false;
        folderBrowserDialog1.UseDescriptionForTitle = true;
        // 
        // MainScreen
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(stylableTabControl1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainScreen";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Satisfactory Dedicated Server UI";
        stylableTabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        panel1.ResumeLayout(false);
        stylableGroupBox2.ResumeLayout(false);
        stylableGroupBox1.ResumeLayout(false);
        stylableGroupBox1.PerformLayout();
        tabPage2.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tabPage3.ResumeLayout(false);
        tabPage3.PerformLayout();
        tabPage4.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        panel5.ResumeLayout(false);
        panel5.PerformLayout();
        panel4.ResumeLayout(false);
        panel4.PerformLayout();
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private StylableTabControl stylableTabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private TabPage tabPage4;
    private Panel panel1;
    private StylableGroupBox stylableGroupBox1;
    private StylableCheckBox ControlNoVisibleConsole;
    private StylableCheckBox ControlUseExperimental;
    private StylableTextBox ControlServerPort;
    private StylableLabel stylableLabel3;
    private StylableCheckBox ControlDisableEventsSeasonal;
    private StylableGroupBox stylableGroupBox2;
    private StylableButton ControlButtonRestart;
    private StylableButton ControlButtonStop;
    private StylableButton ControlButtonStart;
    private TableLayoutPanel tableLayoutPanel2;
    private StylableLabel StatusInfoUptime;
    private StylableLabel stylableLabel10;
    private StylableLabel StatusInfoLastWorldSave;
    private StylableLabel stylableLabel8;
    private StylableLabel StatusInfoPlayers;
    private StylableLabel StatusInfoCurrent;
    private StylableLabel stylableLabel4;
    private StylableLabel stylableLabel5;
    private Panel panel2;
    private StylableLabel stylableLabel12;
    private StylableLabel stylableLabel13;
    private GroupBox groupBox1;
    private StylableLabel ControlInfoPlayers;
    private StylableLabel ControlInfoStatus;
    private StylableComboBox LogComboFilter;
    private StylableLabel stylableLabel14;
    private StylableTextBox LogInfoShow;
    private GroupBox groupBox2;
    private StylableLabel stylableLabel15;
    private StylableLabel SettingsFolderSSInfo;
    private StylableButton SettingsFolderSSButtonSet;
    private Panel panel3;
    private StylableButton SettingsFolderSSButtonOpen;
    private Panel panel5;
    private StylableButton SettingsFolderLogsButtonOpen;
    private StylableLabel SettingsFolderLogsInfo;
    private StylableLabel stylableLabel20;
    private Panel panel4;
    private StylableButton SettingsFolderSavesButtonOpen;
    private StylableLabel SettingsFolderSavesInfo;
    private StylableLabel stylableLabel18;
    private FolderBrowserDialog folderBrowserDialog1;
}
