namespace SatisServer.UI;

partial class SplashScreen
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
        pictureBox1 = new PictureBox();
        progressBar1 = new ProgressBar();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(0, 0);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(1080, 608);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // progressBar1
        // 
        progressBar1.BackColor = Color.Black;
        progressBar1.ForeColor = Color.DarkOrange;
        progressBar1.Location = new Point(12, 586);
        progressBar1.MarqueeAnimationSpeed = 1;
        progressBar1.Maximum = 10;
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(1056, 10);
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.TabIndex = 1;
        // 
        // SplashScreen
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CausesValidation = false;
        ClientSize = new Size(1080, 608);
        ControlBox = false;
        Controls.Add(progressBar1);
        Controls.Add(pictureBox1);
        FormBorderStyle = FormBorderStyle.None;
        MaximumSize = new Size(1080, 608);
        MinimumSize = new Size(1080, 608);
        Name = "SplashScreen";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SplashScreen";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private ProgressBar progressBar1;
}