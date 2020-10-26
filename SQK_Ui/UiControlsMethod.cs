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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class UiControlsMethod
{
    static public string GetMd5Str16(string ConvertString)
    {
        try
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string c16 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
                return c16.Replace("-", "").ToLower();
            }
        }
        catch { return null; }
    }

    static public string GetMd5Str32(string ConvertString)
    {
        try
        {
            using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
            {
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(ConvertString));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString().ToLower();
            }
        }
        catch { return null; }
    }

    /// <summary>
    /// 透明 Input Box
    /// </summary>
    public class InputBox : RichTextBox
    {
        public InputBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            BorderStyle = BorderStyle.None;
            ImeMode = ImeMode.On;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
    
    /// <summary>
    /// 双缓冲 panel
    /// </summary>
    public class PanelEx : Panel
    {
        public PanelEx()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
    }

    /// <summary>
    /// 双缓冲 pictureBox
    /// </summary>
    public class PictureBoxEx : PictureBox
    {
        public PictureBoxEx()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
    }

    /// <summary>
    /// 移动重绘 方法
    /// </summary>
    public abstract class ADraggableGDIObject
    {
        public abstract Rectangle Region { get; set; }

        public abstract bool IsDragging { get; set; }

        public abstract Point DraggingPoint { get; set; }

        public abstract void OnPaint(PaintEventArgs e);

    }

    public class Draggable : ADraggableGDIObject
    {
        private bool m_IsDragging;
        private Point m_DraggingPoint;
        private Rectangle m_Region;
        private Bitmap image;

        public Draggable(int startx, int starty, Bitmap _bmp)
        {
            image = _bmp.Clone(new Rectangle(0, 0, _bmp.Width, _bmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            m_Region = new Rectangle(startx, starty, image.Width, image.Height);
        }

        public override Rectangle Region
        {
            get { return m_Region; }
            set { m_Region = value; }
        }

        public override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, m_Region);
        }

        public override bool IsDragging
        {
            get { return m_IsDragging; }
            set { m_IsDragging = value; }
        }

        public override Point DraggingPoint
        {
            get { return m_DraggingPoint; }
            set { m_DraggingPoint = value; }
        }
    }

    /// <summary>
    /// WebBrows 方法
    /// </summary>
    public class WebBrowsMethods
    {
        [ComImport]
        [Guid("0000010D-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IViewObject
        {
            void Draw([MarshalAs(UnmanagedType.U4)] uint dwAspect, int lindex, IntPtr pvAspect, [In] IntPtr ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [MarshalAs(UnmanagedType.Struct)] ref RECT lprcBounds, [In] IntPtr lprcWBounds, IntPtr pfnContinue, [MarshalAs(UnmanagedType.U4)] uint dwContinue);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static void webBrowsToImage(object obj, Image destination, Color backgroundColor)
        {
            using (Graphics graphics = Graphics.FromImage(destination))
            {
                IntPtr deviceContextHandle = IntPtr.Zero;
                RECT rectangle = new RECT();

                rectangle.Right = destination.Width;
                rectangle.Bottom = destination.Height;

                graphics.Clear(backgroundColor);

                try
                {
                    deviceContextHandle = graphics.GetHdc();

                    IViewObject viewObject = obj as IViewObject;
                    viewObject.Draw(1, -1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, deviceContextHandle, ref rectangle, IntPtr.Zero, IntPtr.Zero, 0);
                }
                finally
                {
                    if (deviceContextHandle != IntPtr.Zero)
                    {
                        graphics.ReleaseHdc(deviceContextHandle);
                    }
                }
            }
        }
    }

    /// <summary>
    ///  控件 方法
    /// </summary>
    public class ControlMethods
    {
        public static Bitmap getFormControlToBmp(Control obj, Bitmap backImg)
        {
            Bitmap bmp_clone = backImg.Clone(new Rectangle(0, 0, backImg.Width, backImg.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics bmp_block = Graphics.FromImage(bmp_clone);
            for (int i = obj.Controls.Count - 1; i >= 0; i--)
            {
                if (obj.Controls[i] is WebBrowser)
                {
                    WebBrowser wb = (WebBrowser)obj.Controls[i];
                    while (wb.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                    Bitmap screenshot = new Bitmap(wb.Width, wb.Height);
                    UiControlsMethod.WebBrowsMethods.webBrowsToImage(wb.ActiveXInstance, screenshot, Color.White);
                    bmp_block.DrawImage(screenshot, obj.Controls[i].Location);
                }
                else
                {
                    Bitmap controlBmp = new Bitmap(obj.Controls[i].Width, obj.Controls[i].Height);
                    obj.Controls[i].DrawToBitmap(controlBmp, obj.Controls[i].ClientRectangle);
                    controlBmp.MakeTransparent();
                    bmp_block.DrawImage(controlBmp, obj.Controls[i].Location);
                }
            }
            return bmp_clone;
        }
    }

}

