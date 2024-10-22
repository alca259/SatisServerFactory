using System.ComponentModel;
using System.Drawing.Design;
using StylableWinFormsControls.LayoutInternals;
using StylableWinFormsControls.Native;

namespace StylableWinFormsControls;

/// <summary>
/// Manages a related set of tab pages.
/// </summary>
/// <remarks>
/// Source: https://www.codeproject.com/Articles/12185/A-NET-Flat-TabControl-CustomDraw
/// Modified by the MFBot team
/// Note that this has neither multiline support nor left/right alignment support
/// </remarks>
public class StylableTabControl : TabControl
{
    private const int N_MARGIN = 5;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private Container? _components;

    private NativeSubClass? _scUpDown;

    /// <summary>
    /// Gets a value indicating whether an UpDown control is required
    /// </summary>
    private bool _bUpDown;

    private ImageList? _upDownImages;

    private Brush _backgroundColorBrush = new SolidBrush(DefaultBackColor);
    private Pen _borderColorPen = new(SystemColors.ControlDark);

    /// <summary>
    /// Background color of the entire tab control
    /// </summary>
    public Color BackgroundColor
    {
        get => _backgroundColorBrush is SolidBrush solidBrush ? solidBrush.Color : default;
        set
        {
            _backgroundColorBrush?.Dispose();
            _backgroundColorBrush = new SolidBrush(value);
        }
    }

    /// <summary>
    /// Background color of the currently active tab rectangle
    /// </summary>
    public Color ActiveTabBackgroundColor { get; set; } = DefaultBackColor;

    /// <summary>
    /// Foreground color of the currently active tab rectangle
    /// </summary>
    public Color ActiveTabForegroundColor { get; set; } = DefaultForeColor;

    /// <summary>
    /// Border color of border in the tab control and around the tabs
    /// </summary>
    public Color BorderColor
    {
        get => _borderColorPen.Color;
        set
        {
            _borderColorPen?.Dispose();
            _borderColorPen = new Pen(value);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tab page controls have a corner radius or not.
    /// </summary>
    public bool UseRoundedCorners { get; set; }

    /// <summary>
    /// Gets or sets the currently used images for the UpDown control.
    /// [0] = Left, [1] = Right, [2] = Left (disabled), [3] = Right (disabled)
    /// </summary>
    /// <remarks>
    /// Can be null if the system control is to be used.<br/>
    /// Transparency color is white.
    /// </remarks>
    public IEnumerable<Bitmap> UpDownImages
    {
        get => _upDownImages is null
                ? Array.Empty<Bitmap>()
                : _upDownImages.Images.Cast<Bitmap>().ToArray();
        set
        {
            _upDownImages = new ImageList();

            if (value is null)
            {
                return;
            }

            foreach (Bitmap upDownImage in value)
            {
                if (upDownImage is not null)
                {
                    upDownImage.MakeTransparent(Color.White);
                    _upDownImages.Images.AddStrip(upDownImage);
                }
            }
        }
    }

    [Editor(typeof(TabpageExCollectionEditor), typeof(UITypeEditor))]
    public new TabPageCollection TabPages => base.TabPages;

    public new TabAlignment Alignment
    {
        get => base.Alignment;
        set
        {
            TabAlignment ta = value;
            if (ta is not TabAlignment.Top and not TabAlignment.Bottom)
            {
                ta = TabAlignment.Top;
            }

            base.Alignment = ta;
        }
    }

    [Browsable(false)]
    public new bool Multiline
    {
        get => base.Multiline;
        set => base.Multiline = false;
    }

    public StylableTabControl()
    {
        // This call is required by the Windows.Forms Form Designer.
        initializeComponent();

        // double buffering
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        _bUpDown = false;

        ControlAdded += stylableTabControl_ControlAdded;
        ControlRemoved += stylableTabControl_ControlRemoved;
        SelectedIndexChanged += stylableTabControl_SelectedIndexChanged;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _components?.Dispose();
            _upDownImages?.Dispose();
            _borderColorPen?.Dispose();
            _backgroundColorBrush?.Dispose();
        }
        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        base.OnPaint(e);

        DrawControl(e.Graphics);
    }

    internal void DrawControl(Graphics g)
    {
        if (!Visible)
        {
            return;
        }

        Rectangle tabControlArea = ClientRectangle;
        Rectangle tabArea = DisplayRectangle;

        #region fill client area

        g.FillRectangle(_backgroundColorBrush, tabControlArea);

        #endregion fill client area

        #region draw border

        int nDelta = SystemInformation.Border3DSize.Width;

        tabArea.Inflate(nDelta, nDelta);
        g.DrawRectangle(_borderColorPen, tabArea);

        #endregion draw border

        #region clip region for drawing tabs

        Region rsaved = g.Clip;
        Rectangle rreg;

        int nWidth = tabArea.Width + N_MARGIN;
        if (_bUpDown && _scUpDown is not null)
        {
            // exclude updown control for painting
            if (NativeMethods.IsWindowVisible(_scUpDown.Handle))
            {
                Rectangle rUpdown = new();
                NativeMethods.GetWindowRect(_scUpDown.Handle, ref rUpdown);
                Rectangle rUpdown2 = RectangleToClient(rUpdown);

                nWidth = rUpdown2.X;
            }
        }

        rreg = new Rectangle(tabArea.Left, tabControlArea.Top, nWidth - N_MARGIN, tabControlArea.Height);

        g.SetClip(rreg);

        // draw tabs
        for (int i = 0; i < TabCount; i++)
        {
            DrawTab(g, TabPages[i], i);
        }

        g.Clip = rsaved;

        #endregion clip region for drawing tabs

        #region draw background to cover flat border areas

        if (SelectedTab is not null)
        {
            TabPage tabPage = SelectedTab;
            Color color = tabPage.BackColor;
            Pen border = new(color);

            tabArea.Offset(1, 1);
            tabArea.Width -= 2;
            tabArea.Height -= 2;

            g.DrawRectangle(border, tabArea);
            tabArea.Width--;
            tabArea.Height--;
            g.DrawRectangle(border, tabArea);

            border.Dispose();
        }

        #endregion draw background to cover flat border areas
    }

    internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
    {
        Rectangle recBounds = GetTabRect(nIndex);
        RectangleF tabTextArea = GetTabRect(nIndex);

        bool bSelected = SelectedIndex == nIndex;

        Point[] pt = new Point[7];

        // Sets the difference in positioning of top/bottom line for the selected vs unselected tabs.
        int posDifference = bSelected ? 0 : 2;
        tabTextArea.Y += posDifference / 2;
        if (Alignment == TabAlignment.Top)
        {
            pt[0] = new Point(recBounds.Left, recBounds.Bottom);
            pt[1] = new Point(recBounds.Left, recBounds.Top + posDifference + (UseRoundedCorners ? 3 : 0));
            pt[2] = new Point(recBounds.Left + (UseRoundedCorners ? 3 : 0), recBounds.Top + posDifference);
            pt[3] = new Point(recBounds.Right - (UseRoundedCorners ? 3 : 0), recBounds.Top + posDifference);
            pt[4] = new Point(recBounds.Right, recBounds.Top + posDifference + (UseRoundedCorners ? 3 : 0));
            pt[5] = new Point(recBounds.Right, recBounds.Bottom);
            pt[6] = new Point(recBounds.Left, recBounds.Bottom);
        }
        else
        {
            pt[0] = new Point(recBounds.Left, recBounds.Top);
            pt[1] = new Point(recBounds.Right, recBounds.Top);
            pt[2] = new Point(recBounds.Right, recBounds.Bottom + posDifference - (UseRoundedCorners ? 3 : 0));
            pt[3] = new Point(recBounds.Right - (UseRoundedCorners ? 3 : 0), recBounds.Bottom + posDifference);
            pt[4] = new Point(recBounds.Left + (UseRoundedCorners ? 3 : 0), recBounds.Bottom + posDifference);
            pt[5] = new Point(recBounds.Left, recBounds.Bottom + posDifference - (UseRoundedCorners ? 3 : 0));
            pt[6] = new Point(recBounds.Left, recBounds.Top);
        }

        // fill this tab with background color
        Color activeBackgroundColor = bSelected ? ActiveTabBackgroundColor : tabPage.BackColor;
        using (Brush backColorBrush = new SolidBrush(activeBackgroundColor))
        {
            g.FillPolygon(backColorBrush, pt);
        }

        // draw border
        g.DrawPolygon(_borderColorPen, pt);

        // clear bottom lines for selected tab page
        if (bSelected)
        {
            using Pen pen = new(activeBackgroundColor);
            switch (Alignment)
            {
                case TabAlignment.Top:
                    g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom, recBounds.Right - 1,
                        recBounds.Bottom);
                    g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom + 1, recBounds.Right - 1,
                        recBounds.Bottom + 1);
                    break;

                case TabAlignment.Bottom:
                    g.DrawLine(pen, recBounds.Left + 1, recBounds.Top, recBounds.Right - 1, recBounds.Top);
                    g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 1, recBounds.Right - 1,
                        recBounds.Top - 1);
                    g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 2, recBounds.Right - 1,
                        recBounds.Top - 2);
                    break;
            }
        }

        // draw tab's icon
        if (tabPage.ImageIndex >= 0 && ImageList is not null)
        {
            const int nLeftMargin = 8;
            const int nRightMargin = 2;

            Image img = ImageList.Images[tabPage.ImageIndex];

            Rectangle rImage = new(recBounds.X + nLeftMargin, recBounds.Y + 1, img.Width, img.Height);

            // adjust rectangles
            float nAdj = nLeftMargin + img.Width + nRightMargin;

            rImage.Y += (recBounds.Height - img.Height) / 2;
            tabTextArea.X += nAdj;
            tabTextArea.Width -= nAdj;

            // draw icon
            g.DrawImage(img, rImage);
        }

        // draw string
        using StringFormat stringFormat = new()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        Color activeForegroundColor = bSelected ? ActiveTabForegroundColor : tabPage.ForeColor;
        using Brush foreColorBrush = new SolidBrush(activeForegroundColor);
        g.DrawString(tabPage.Text, Font, foreColorBrush, tabTextArea, stringFormat);
    }

    /// <summary>
    /// Draws UpDown control for scrolling through the tabs
    /// </summary>
    /// <remarks>Can be left/right images despite the name.</remarks>
    internal void DrawUpDown(Graphics g)
    {
        if (_upDownImages is null || _upDownImages.Images.Count != 4 || _scUpDown is null)
        {
            return;
        }

        #region calc icon positions

        Rectangle tabControlArea = ClientRectangle;

        Rectangle r0 = new();
        if (!NativeMethods.GetClientRectInternal(_scUpDown.Handle, ref r0))
        {
            return;
        }

        using (Brush br = new SolidBrush(SystemColors.Control))
        {
            g.FillRectangle(br, r0);
        }

        Rectangle rBorder = r0;
        rBorder.Inflate(-1, -1);
        g.DrawRectangle(_borderColorPen, rBorder);

        int nMiddle = r0.Width / 2;
        int nTop = (r0.Height - 16) / 2;
        int nLeft = (nMiddle - 16) / 2;

        Rectangle r1 = new(nLeft, nTop, 16, 16);
        Rectangle r2 = new(nMiddle + nLeft, nTop, 16, 16);

        #endregion calc icon positions

        #region actually draw buttons

        Image img = _upDownImages.Images[1];
        if (TabCount > 0)
        {
            Rectangle r3 = GetTabRect(0);
            if (r3.Left < tabControlArea.Left)
            {
                g.DrawImage(img, r1);
            }
            else
            {
                img = _upDownImages.Images[3];
                g.DrawImage(img, r1);
            }
        }

        img = _upDownImages.Images[0];
        if (TabCount > 0)
        {
            Rectangle r3 = GetTabRect(TabCount - 1);
            if (r3.Right > tabControlArea.Width - r0.Width)
            {
                g.DrawImage(img, r2);
            }
            else
            {
                img = _upDownImages.Images[2];
                g.DrawImage(img, r2);
            }
        }

        #endregion actually draw buttons
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();

        findUpDown();
    }

    private void stylableTabControl_ControlAdded(object? sender, ControlEventArgs e)
    {
        findUpDown();
        updateUpDown();
    }

    private void stylableTabControl_ControlRemoved(object? sender, ControlEventArgs e)
    {
        findUpDown();
        updateUpDown();
    }

    private void stylableTabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        updateUpDown();
        Invalidate();   // we need to update border and background colors
    }

    private void findUpDown()
    {
        bool bFound = false;

        // find the UpDown control
        IntPtr pWnd = NativeMethods.GetWindow(Handle, NativeConstants.GW_CHILD);

        while (pWnd != IntPtr.Zero)
        {
            //----------------------------
            // Get the window class name
            char[] className = new char[33];

            int length = NativeMethods.GetClassName(pWnd, className, 32);

            string s = new(className, 0, length);
            //----------------------------

            if (s == "msctls_updown32")
            {
                bFound = true;

                if (!_bUpDown)
                {
                    _scUpDown = new NativeSubClass(pWnd, true);
                    _scUpDown.SubClassedWndProc += scUpDown_SubClassedWndProc;

                    _bUpDown = true;
                }
                break;
            }

            pWnd = NativeMethods.GetWindow(pWnd, NativeConstants.GW_HWNDNEXT);
        }

        if (!bFound && _bUpDown)
        {
            _bUpDown = false;
        }
    }

    private void updateUpDown()
    {
        if (!_bUpDown || _scUpDown is null || NativeMethods.IsWindowVisible(_scUpDown.Handle))
        {
            return;
        }

        Rectangle rect = new();

        if (NativeMethods.GetClientRectInternal(_scUpDown.Handle, ref rect))
        {
            NativeMethods.InvalidateRect(_scUpDown.Handle, ref rect, true);
        }
    }

    private int scUpDown_SubClassedWndProc(ref Message m)
    {
        if (_scUpDown is null)
        {
            return 0;
        }

        switch (m.Msg)
        {
            case NativeConstants.Messages.WM_PAINT:
                //------------------------
                // redraw
                IntPtr hDc = NativeMethods.GetWindowDC(_scUpDown.Handle);
                using (Graphics g = Graphics.FromHdc(hDc))
                {
                    DrawUpDown(g);
                }
                NativeMethods.ReleaseDC(_scUpDown.Handle, hDc);
                //------------------------

                // return 0 (processed)
                m.Result = IntPtr.Zero;

                //------------------------
                // validate current rect
                Rectangle rect = new();

                if (NativeMethods.GetClientRectInternal(_scUpDown.Handle, ref rect))
                {
                    NativeMethods.ValidateRect(_scUpDown.Handle, ref rect);
                }
                //------------------------

                return 1;
        }

        return 0;
    }

    #region Component Designer code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void initializeComponent()
    {
        _components = new Container();
    }

    #endregion Component Designer code
}
