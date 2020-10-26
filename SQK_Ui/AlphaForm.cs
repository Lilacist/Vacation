/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

public partial class AlphaForm : Form
{
    public Bitmap 窗体图像; /*窗体图像*/
    double _transparency;

    public AlphaForm()
    {
        if (!DesignMode)
        {
            m_layeredWnd = new LayeredWindow();
        }
        m_sizeMode = SizeModes.None;
        m_Animation = Animations.None;
        m_animationspeed = 0;
        m_background = null;
        m_backgroundEx = null;
        m_backgroundFull = null;
        m_renderCtrlBG = false;
        m_enhanced = false;
        m_isDown.Left = false;
        m_isDown.Right = false;
        m_isDown.Middle = false;
        m_isDown.XBtn = false;
        m_moving = false;
        m_hiddenControls = new List<Control>();
        m_controlDict = new Dictionary<Control, bool>();
        m_initialised = false;
        SetStyle(ControlStyles.DoubleBuffer, true);
        Load += new EventHandler(AlphaForm_Load);
        Shown += new EventHandler(AlphaForm_Shown);
    }
   
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point pt);

    private void AlphaForm_Load(object sender, EventArgs e)
    {
        _transparency = Opacity;
        SetOpacity(0);
        BlendedBackground = 窗体图像;
    }
    
    private void AlphaForm_Shown(object sender, EventArgs e)
    {
        if (Animation == Animations.rotation)
        {
            AnimationForm.rotationForm(this, this, 0, 1, Location.X, Location.Y, Width, Height, _transparency, AnimationSpeed, 0);
        }
        else
         if (Animation == Animations.Flip)
        {
            AnimationForm.rotationForm(this, this, 0, 1, Location.X, Location.Y, Width, Height, _transparency, AnimationSpeed, 1);
        }
        /*
        else
        if (Animation == Animations.stereomutation)
        {
            AnimationForm.stereomutationForm(this, this, 0, 1, Location.X, Location.Y, Width, Height, _transparency, AnimationSpeed, 0);
        }
        */
        else
        if (Animation == Animations.Fading)
        {
            AnimationForm.fadingForm(this, 窗体图像, _transparency, AnimationSpeed);
        }
        else
        {
            SetOpacity(_transparency);
        }
    }

    #region Properties
    public enum SizeModes
    {
        None,
        Stretch,
        Clip
    }
    
    public enum Animations
    {
        None,
        Fading,
        Flip,
        rotation
    }
    
    [Category("AlphaForm")]
    public Bitmap BlendedBackground
    {
        get { return m_background; }
        set
        {
            if (m_background != value)
            {
                m_background = value;
                UpdateLayeredBackground();
            }
        }
    }

    [Category("AlphaForm")]
    public int AnimationSpeed
    {
        get { return m_animationspeed; }
        set { m_animationspeed = value; }
    }

    [Category("AlphaForm")]
    public bool DrawControlBackgrounds
    {
        get { return m_renderCtrlBG; }
        set
        {
            if (m_renderCtrlBG != value)
            {
                m_renderCtrlBG = value;
                UpdateLayeredBackground();
            }
        }
    }

    [Category("AlphaForm")]
    public bool EnhancedRendering
    {
        get { return m_enhanced; }
        set { m_enhanced = value; }
    }

    [Category("AlphaForm")]
    public SizeModes SizeMode
    {
        get { return m_sizeMode; }
        set
        {
            m_sizeMode = value;
            UpdateLayeredBackground();
        }
    }

    [Category("AlphaForm")]
    public Animations Animation
    {
        get { return m_Animation; }
        set { m_Animation = value; }
    }

    public void SetOpacity(double Opacity)
    {
        this.Opacity = Opacity;
        if (m_background != null)
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;
            if (m_sizeMode == SizeModes.None)
            {
                width = m_background.Width;
                height = m_background.Height;
            }

            byte _opacity = (byte)(this.Opacity * 255);
            if (m_useBackgroundEx)
            {
                try
                {
                    m_layeredWnd.UpdateWindow(m_backgroundEx, _opacity, width, height, m_layeredWnd.LayeredPos);
                }
                catch { }
            }
            else
            {
                try
                {
                    m_layeredWnd.UpdateWindow(m_background, _opacity, width, height, m_layeredWnd.LayeredPos);
                }
                catch { }
            }
        }
    }

    public void UpdateLayeredBackground()
    {
        updateLayeredBackground(ClientSize.Width, ClientSize.Height);
    }

    public void DrawControlBackground(Control ctrl, bool drawBack)
    {
        if (m_controlDict.ContainsKey(ctrl))
            m_controlDict[ctrl] = drawBack;
    }
    #endregion

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        BackColor = Color.Fuchsia;
        TransparencyKey = Color.Fuchsia;
        AllowTransparency = true;

        Point screen = new Point(0, 0);
        screen = PointToScreen(screen);
        m_offX = screen.X - Location.X;
        m_offY = screen.Y - Location.Y;

        if (!DesignMode)
        {
            Point formLoc = Location;
            formLoc.X += m_offX;
            formLoc.Y += m_offY;
            m_layeredWnd.Text = "AlphaForm";
            m_initialised = true;
            updateLayeredBackground(ClientSize.Width, ClientSize.Height, formLoc, true);
            m_layeredWnd.Show();
            m_layeredWnd.Enabled = false;
            m_customLayeredWindowProc = new Win32.Win32WndProc(LayeredWindowWndProc);
            m_layeredWindowProc = Win32.SetWindowLong(m_layeredWnd.Handle, (uint)Win32.Message.GWL_WNDPROC, m_customLayeredWindowProc);
        }
    }

    protected override void OnClosed (EventArgs e)
    {
        m_layeredWnd.Close();
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        base.OnPaintBackground(e);

        if (m_background != null)
        {
            if (DesignMode)
            {
                e.Graphics.DrawImage(m_background, 0, 0, m_background.Width, m_background.Height);
            }
            else if (!m_moving && m_renderCtrlBG)
            {
                foreach (KeyValuePair<Control, bool> kvp in m_controlDict)
                {
                    Control ctrl = kvp.Key;
                    bool drawBack = kvp.Value;
                    if (drawBack && ctrl.BackColor == Color.Transparent)
                    {
                        Rectangle rect = ctrl.ClientRectangle;
                        rect.X = ctrl.Left;
                        rect.Y = ctrl.Top;

                        if (m_useBackgroundEx)
                            e.Graphics.DrawImage(m_backgroundFull, rect, rect, GraphicsUnit.Pixel);
                        else
                            e.Graphics.DrawImage(m_background, rect, rect, GraphicsUnit.Pixel);
                    }
                }
            }
        }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        if (!m_controlDict.ContainsKey(e.Control))
            m_controlDict.Add(e.Control, true);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
        base.OnControlRemoved(e);
        if (m_controlDict.ContainsKey(e.Control))
            m_controlDict.Remove(e.Control);
    }

    private void updateLayeredBackground(int width, int height, Point pos)
    {
        updateLayeredBackground(width, height, pos, true);
    }

    private void updateLayeredBackground(int width, int height)
    {
        updateLayeredBackground(width, height, m_layeredWnd.LayeredPos, true);
    }

    private void updateLayeredBackground(int width, int height, Point pos, bool Render)
    {
        m_useBackgroundEx = false;
        if (DesignMode || m_background == null || !m_initialised)
            return;

        switch (m_sizeMode)
        {
            case SizeModes.Stretch:
                m_useBackgroundEx = true;
                break;
            case SizeModes.Clip:
                break;
            case SizeModes.None:
                width = m_background.Width;
                height = m_background.Height;
                break;
        }

        if ((m_renderCtrlBG || m_useBackgroundEx) && Render)
        {
            if (m_backgroundEx != null)
            {
                m_backgroundEx.Dispose();
                m_backgroundEx = null;
            }
            if (m_backgroundFull != null)
            {
                m_backgroundFull.Dispose();
                m_backgroundFull = null;
            }

            if (m_sizeMode == SizeModes.Clip)
                m_backgroundEx = new Bitmap(m_background);
            else
                m_backgroundEx = new Bitmap(m_background, width, height);

            m_backgroundFull = new Bitmap(m_backgroundEx);
        }

        if (m_renderCtrlBG)
        {
            if (Render)
            {
                Graphics g = Graphics.FromImage(m_backgroundEx);
                foreach (KeyValuePair<Control, bool> kvp in m_controlDict)
                {
                    Control ctrl = kvp.Key;
                    bool drawBack = kvp.Value;
                    if (drawBack && ctrl.BackColor == Color.Transparent)
                    {
                        Rectangle rect = ctrl.ClientRectangle;
                        rect.X = ctrl.Left;
                        rect.Y = ctrl.Top;
                        g.FillRectangle(Brushes.Fuchsia, rect);
                    }
                }
                g.Dispose();
                m_backgroundEx.MakeTransparent(Color.Fuchsia);
            }
            m_useBackgroundEx = true;
        }

        byte _opacity = (byte)(Opacity * 255);
        if (m_useBackgroundEx)
        {
            try
            {
                m_layeredWnd.UpdateWindow(m_backgroundEx, _opacity, width, height, pos);
            }
            catch { }
        }
        else
        {
            try
            {
                m_layeredWnd.UpdateWindow(m_background, _opacity, width, height, pos);
            }
            catch { }
        }
    }

    private void updateLayeredSize(int width, int height)
    {
        updateLayeredSize(width, height, false);
    }

    private void updateLayeredSize(int width, int height, bool forceUpdate)
    {
        if (!m_initialised)
            return;

        if (!forceUpdate && (width == m_layeredWnd.LayeredSize.Width && height == m_layeredWnd.LayeredSize.Height))
            return;

        switch (m_sizeMode)
        {
            case SizeModes.None:
                break;

            case SizeModes.Stretch:
                {
                    updateLayeredBackground(width, height);
                    Invalidate(false);
                }
                break;

            case SizeModes.Clip:
                {
                    byte _opacity = (byte)(Opacity * 255);
                    if (m_useBackgroundEx)
                    {
                        try
                        {
                            m_layeredWnd.UpdateWindow(m_backgroundEx, _opacity, width, height, m_layeredWnd.LayeredPos);
                        } catch { }
                    }
                    else
                    {
                        try
                        {
                            m_layeredWnd.UpdateWindow(m_background, _opacity, width, height, m_layeredWnd.LayeredPos);
                        } catch { }
                    }
                }
                break;
        }
    }

    private Bitmap m_background;
    private Bitmap m_backgroundEx;
    private Bitmap m_backgroundFull;
    private bool m_useBackgroundEx;
    private LayeredWindow m_layeredWnd;
    private int m_offX;
    private int m_offY;
    private bool m_renderCtrlBG;
    private bool m_enhanced;
    private SizeModes m_sizeMode;
    private Animations m_Animation;
    private int m_animationspeed;
    private List<Control> m_hiddenControls;
    private Dictionary<Control, bool> m_controlDict;
    private bool m_moving;
    private bool m_initialised;
    private Win32.Win32WndProc m_customLayeredWindowProc;
    private IntPtr m_layeredWindowProc;
    private Point m_lockedPoint = new Point();
    private DateTime m_clickTime = DateTime.Now;
    private Win32.Message m_lastClickMsg = 0;
    private HeldButtons m_isDown;
    private delegate void delMouseEvent(MouseEventArgs e);
    private delegate void delStdEvent(EventArgs e);

    struct HeldButtons
    {
        public bool Left;
        public bool Middle;
        public bool Right;
        public bool XBtn;
    };

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.DoubleBuffered = true;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "AlphaForm";
        this.ResumeLayout(false);
    }
}

