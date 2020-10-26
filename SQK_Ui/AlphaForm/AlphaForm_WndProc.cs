/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public partial class AlphaForm
{
    private bool dblClick(Point pos)
    {
        TimeSpan elapsed = DateTime.Now - m_clickTime;
        Size dist = new Size();
        dist.Width = Math.Abs(m_lockedPoint.X - pos.X);
        dist.Height = Math.Abs(m_lockedPoint.Y - pos.Y);

        if (elapsed.Milliseconds <= SystemInformation.DoubleClickTime
            && dist.Width <= SystemInformation.DoubleClickSize.Width
            && dist.Height <= SystemInformation.DoubleClickSize.Height)
        {
            return true;
        }
        return false;
    }

    protected override void WndProc(ref Message m)
    {
        if (this.DesignMode)
        {
            base.WndProc(ref m);
            return;
        }
        Win32.Message msgId = (Win32.Message)m.Msg;
        bool UseBase = true;
        switch (msgId)
        {
            case Win32.Message.WM_LBUTTONUP:
                {
                    if (Win32.GetCapture() != IntPtr.Zero)
                        Win32.ReleaseCapture();
                }
                break;

            case Win32.Message.WM_ENTERSIZEMOVE:
                {

                }
                break;

            case Win32.Message.WM_EXITSIZEMOVE:
                {
                    m_isDown.Left = false;
                    m_moving = false;

                    if (m_enhanced)
                    {
                        this.SuspendLayout();
                        foreach (Control ctrl in m_hiddenControls)
                            ctrl.Visible = true;
                        m_hiddenControls.Clear();
                        this.ResumeLayout();
                        updateLayeredBackground(this.ClientSize.Width, this.ClientSize.Height, m_layeredWnd.LayeredPos, false);
                    }
                }
                break;

            case Win32.Message.WM_MOUSEMOVE:
                if (Win32.GetCapture() != IntPtr.Zero && m_moving)
                {
                    if (m_enhanced)
                    {
                        IntPtr hdcScreen = Win32.GetWindowDC(m_layeredWnd.Handle);
                        IntPtr windowDC = Win32.GetDC(this.Handle);
                        IntPtr memDC = Win32.CreateCompatibleDC(windowDC);
                        IntPtr BmpMask = Win32.CreateBitmap(this.ClientSize.Width,
                                        this.ClientSize.Height, 1, 1, IntPtr.Zero);

                        Bitmap backImage = m_useBackgroundEx ? m_backgroundFull : m_background;
                        IntPtr BmpBack = backImage.GetHbitmap(Color.FromArgb(0));

                        Win32.SelectObject(memDC, BmpMask);
                        uint oldCol = Win32.SetBkColor(windowDC, 0x00FF00FF);
                        Win32.BitBlt(memDC, 0, 0, this.ClientSize.Width, this.ClientSize.Height, windowDC, 0, 0, Win32.TernaryRasterOperations.SRCCOPY);
                        Win32.SetBkColor(windowDC, oldCol);

                        Win32.SelectObject(memDC, BmpBack);
                        IntPtr brush = Win32.CreateSolidBrush(0x00FFFFFF);
                        Win32.SelectObject(memDC, brush);
                        Win32.MaskBlt(memDC, 0, 0, backImage.Width, backImage.Height, windowDC, 0, 0, BmpMask, 0, 0, 0xCFAA0020);

                        Point zero = new Point(0, 0);
                        Size size = m_layeredWnd.LayeredSize;
                        Point pos = m_layeredWnd.LayeredPos;
                        Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                        blend.AlphaFormat = (byte)Win32.BlendOps.AC_SRC_ALPHA;
                        blend.BlendFlags = (byte)Win32.BlendFlags.None;
                        blend.BlendOp = (byte)Win32.BlendOps.AC_SRC_OVER;
                        blend.SourceConstantAlpha = (byte)(this.Opacity * 255);

                        Win32.UpdateLayeredWindow(m_layeredWnd.Handle, hdcScreen, ref pos, ref size, memDC, ref zero, 0, ref blend, Win32.BlendFlags.ULW_ALPHA);
                        Win32.ReleaseDC(IntPtr.Zero, hdcScreen);
                        Win32.ReleaseDC(this.Handle, windowDC);
                        Win32.DeleteDC(memDC);
                        Win32.DeleteObject(brush);
                        Win32.DeleteObject(BmpMask);
                        Win32.DeleteObject(BmpBack);

                        this.SuspendLayout();
                        foreach (Control ctrl in this.Controls)
                        {
                            if (ctrl.Visible)
                            {
                                m_hiddenControls.Add(ctrl);
                                ctrl.Visible = false;
                            }
                        }
                        this.ResumeLayout();
                    }

                    Win32.ReleaseCapture();
                    Win32.SendMessage(this.Handle, (int)Win32.Message.WM_NCLBUTTONDOWN, (int)Win32.Message.HTCAPTION, 0);
                }
                break;

            case Win32.Message.WM_SIZE:
                {
                    int width = m.LParam.ToInt32() & 0xFFFF;
                    int height = m.LParam.ToInt32() >> 16;
                    this.updateLayeredSize(width, height);
                }
                break;

            case Win32.Message.WM_WINDOWPOSCHANGING:
                {
                    Win32.WINDOWPOS posInfo = (Win32.WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(Win32.WINDOWPOS));
                    Win32.WindowPosFlags move_size = Win32.WindowPosFlags.SWP_NOMOVE | Win32.WindowPosFlags.SWP_NOSIZE;
                    if ((posInfo.flags & move_size) != move_size)
                    {
                        if (posInfo.hwndInsertAfter != this.Handle)
                        {
                            IntPtr hwdp = Win32.BeginDeferWindowPos(2);
                            if (hwdp != IntPtr.Zero)
                                hwdp = Win32.DeferWindowPos(hwdp, m_layeredWnd.Handle, this.Handle, posInfo.x + m_offX, posInfo.y + m_offY,
                                        0, 0, (uint)(posInfo.flags | Win32.WindowPosFlags.SWP_NOSIZE | Win32.WindowPosFlags.SWP_NOZORDER));
                            if (hwdp != IntPtr.Zero)
                                hwdp = Win32.DeferWindowPos(hwdp, this.Handle, this.Handle, posInfo.x, posInfo.y, posInfo.cx, posInfo.cy,
                                        (uint)(posInfo.flags | Win32.WindowPosFlags.SWP_NOZORDER));
                            if (hwdp != IntPtr.Zero)
                                Win32.EndDeferWindowPos(hwdp);

                            m_layeredWnd.LayeredPos = new Point(posInfo.x + m_offX, posInfo.y + m_offY);
                            posInfo.flags |= Win32.WindowPosFlags.SWP_NOMOVE;
                            Marshal.StructureToPtr(posInfo, m.LParam, true);
                        }

                        if ((posInfo.flags & Win32.WindowPosFlags.SWP_NOSIZE) == 0)
                        {
                            int diffX = posInfo.cx - this.Size.Width;
                            int diffY = posInfo.cy - this.Size.Height;
                            if (diffX != 0 || diffY != 0)
                                this.updateLayeredSize(this.ClientSize.Width + diffX, this.ClientSize.Height + diffY);
                        }
                        UseBase = false;
                    }
                }
                break;


            case Win32.Message.WM_ACTIVATE:
                {
                    if (m.WParam != IntPtr.Zero)
                    {
                        IntPtr prevWnd = Win32.GetWindow(m_layeredWnd.Handle, Win32.GetWindow_Cmd.GW_HWNDPREV);
                        while (prevWnd != IntPtr.Zero)
                        {
                            if (Win32.IsWindowVisible(prevWnd))
                                break;

                            prevWnd = Win32.GetWindow(prevWnd, Win32.GetWindow_Cmd.GW_HWNDPREV);
                        }

                        if (prevWnd != this.Handle)
                            Win32.SetWindowPos(m_layeredWnd.Handle, this.Handle, 0, 0, 0, 0, (uint)(Win32.WindowPosFlags.SWP_NOSENDCHANGING | Win32.WindowPosFlags.SWP_NOACTIVATE | Win32.WindowPosFlags.SWP_NOSIZE | Win32.WindowPosFlags.SWP_NOMOVE));
                    }
                }
                break;
        }

        if (UseBase)
            base.WndProc(ref m);
    }

    private int LayeredWindowWndProc(IntPtr hWnd, int Msg, int wParam, int lParam)
    {
        Point mousePos = this.PointToClient(System.Windows.Forms.Cursor.Position);
        Win32.Message msgId = (Win32.Message)Msg;
        switch (msgId)
        {
            case Win32.Message.WM_LBUTTONDOWN:
                System.Diagnostics.Debugger.Break();
                break;
                
            case Win32.Message.WM_SETCURSOR:
                {
                    Win32.SetCursor(Win32.LoadCursor(IntPtr.Zero, Win32.SystemCursor.IDC_NORMAL));
                    MouseEventArgs e = null;
                    delMouseEvent mouseEvent = null;
                    delStdEvent stdEvent = null;
                    Win32.Message MouseEvent = (Win32.Message)(lParam >> 16);
                    switch (MouseEvent)
                    {
                        case Win32.Message.WM_MOUSEMOVE:
                            {
                                if (m_isDown.Left && m_lockedPoint != mousePos)
                                {
                                    System.Diagnostics.Debug.WriteLine("Setting Capture");
                                    Win32.SetCapture(this.Handle);
                                    m_moving = true;
                                }
                                else
                                {
                                    e = new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, mousePos.X, mousePos.Y, 0);
                                    mouseEvent = new delMouseEvent(this.OnMouseMove);
                                }
                            }
                            break;

                        case Win32.Message.WM_LBUTTONDOWN:
                            if (m_lastClickMsg == MouseEvent && !m_isDown.Left && dblClick(mousePos))
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDoubleClick);
                                stdEvent = new delStdEvent(this.OnDoubleClick);

                                m_lastClickMsg = 0;
                            }
                            else if (!m_isDown.Left)
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDown);
                                m_lastClickMsg = MouseEvent;
                            }

                            m_clickTime = DateTime.Now;
                            m_lockedPoint = mousePos;

                            m_isDown.Left = true;
                            break;

                        case Win32.Message.WM_LBUTTONUP:
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseClick);
                                stdEvent = new delStdEvent(this.OnClick);
                                m_isDown.Left = false;
                            }
                            break;

                        case Win32.Message.WM_MBUTTONDOWN:
                            if (m_lastClickMsg == MouseEvent && !m_isDown.Middle && dblClick(mousePos))
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Middle, 2, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDoubleClick);
                                stdEvent = new delStdEvent(this.OnDoubleClick);
                                m_lastClickMsg = 0;
                            }
                            else if (!m_isDown.Middle)
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Middle, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDown);

                                m_lastClickMsg = MouseEvent;
                                m_clickTime = DateTime.Now;
                                m_lockedPoint = mousePos;
                            }
                            m_isDown.Middle = true;
                            break;

                        case Win32.Message.WM_MBUTTONUP:
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Middle, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseClick);
                                stdEvent = new delStdEvent(this.OnClick);

                                m_isDown.Middle = false;
                            }
                            break;

                        case Win32.Message.WM_RBUTTONDOWN:
                            if (m_lastClickMsg == MouseEvent && !m_isDown.Right && dblClick(mousePos))
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 2, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDoubleClick);
                                stdEvent = new delStdEvent(this.OnDoubleClick);

                                m_lastClickMsg = 0;
                            }
                            else if (!m_isDown.Right)
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDown);

                                m_lastClickMsg = MouseEvent;
                                m_clickTime = DateTime.Now;
                                m_lockedPoint = mousePos;
                            }
                            m_isDown.Right = true;
                            break;

                        case Win32.Message.WM_RBUTTONUP:
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseClick);
                                stdEvent = new delStdEvent(this.OnClick);

                                m_isDown.Right = false;
                            }
                            break;

                        case Win32.Message.WM_XBUTTONDOWN:
                            if (m_lastClickMsg == MouseEvent && !m_isDown.XBtn && dblClick(mousePos))
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 2, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDoubleClick);
                                stdEvent = new delStdEvent(this.OnDoubleClick);

                                m_lastClickMsg = 0;
                            }
                            else if (!m_isDown.XBtn)
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseDown);

                                m_lastClickMsg = MouseEvent;
                                m_clickTime = DateTime.Now;
                                m_lockedPoint = mousePos;
                            }
                            m_isDown.XBtn = true;
                            break;

                        case Win32.Message.WM_XBUTTONUP:
                            {
                                e = new MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 1, mousePos.X, mousePos.Y, 0);
                                mouseEvent = new delMouseEvent(this.OnMouseClick);
                                stdEvent = new delStdEvent(this.OnClick);
                                m_isDown.XBtn = false;
                            }
                            break;
                    }
                    bool mouseDown = m_isDown.Left || m_isDown.Middle || m_isDown.Right || m_isDown.XBtn;
                    if (mouseDown && Form.ActiveForm == null)
                    {
                        this.Activate();
                    }

                    if (e != null)
                    {
                        if (mouseEvent != null)
                            this.BeginInvoke(mouseEvent, e);
                        if (stdEvent != null)
                            this.BeginInvoke(stdEvent, e);
                    }
                    return 0;
                }

        }
        return Win32.CallWindowProc(m_layeredWindowProc, hWnd, Msg, wParam, lParam);
    }
}
