/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System;
using System.Windows.Forms;
using System.Drawing;


class LayeredWindow : Form
{
    private Rectangle m_rect;

    public Point LayeredPos
    {
        get { return m_rect.Location; }
        set { m_rect.Location = value; }
    }

    public Size LayeredSize
    {
        get { return m_rect.Size; }
    }

    public LayeredWindow()
    {
        this.ShowInTaskbar = false;
        this.FormBorderStyle = FormBorderStyle.None;
    }

    public void UpdateWindow(Bitmap image, byte opacity)
    {
        UpdateWindow(image, opacity, -1, -1, this.LayeredPos);
    }

    public void UpdateWindow(Bitmap image, byte opacity, int width, int height, Point pos)
    {
        IntPtr hdcWindow = Win32.GetWindowDC(this.Handle);
        IntPtr hDC = Win32.CreateCompatibleDC(hdcWindow);
        IntPtr hBitmap = image.GetHbitmap(Color.FromArgb(0));
        IntPtr hOld = Win32.SelectObject(hDC, hBitmap);
        Size size = new Size(0, 0);
        Point zero = new Point(0, 0);

        if (width == -1 || height == -1)
        {
            size.Width = image.Width;
            size.Height = image.Height;
        }
        else
        {
            size.Width = Math.Min(image.Width, width);
            size.Height = Math.Min(image.Height, height);
        }
        m_rect.Size = size;
        m_rect.Location = pos;

        Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
        blend.BlendOp = (byte)Win32.BlendOps.AC_SRC_OVER;
        blend.SourceConstantAlpha = opacity;
        blend.AlphaFormat = (byte)Win32.BlendOps.AC_SRC_ALPHA;
        blend.BlendFlags = (byte)Win32.BlendFlags.None;

        Win32.UpdateLayeredWindow(this.Handle, hdcWindow, ref pos, ref size, hDC, ref zero, 0, ref blend, Win32.BlendFlags.ULW_ALPHA);

        Win32.SelectObject(hDC, hOld);
        Win32.DeleteObject(hBitmap);
        Win32.DeleteDC(hDC);
        Win32.ReleaseDC(this.Handle, hdcWindow);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= (int)Win32.WindowStyles.WS_EX_LAYERED;
            return cp;
        }
    }
}

