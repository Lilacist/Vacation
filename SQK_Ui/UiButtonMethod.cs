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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class UiButtonMethod
{
    /// <summary>
    /// 缩放按钮
    /// </summary>
    public class zoomButton
    {
        private Bitmap zoomGetbtn(Bitmap b, int destHeight, int destWidth)
        {
            Bitmap imgSource = b.Clone(new Rectangle(0, 0, b.Width, b.Height), PixelFormat.Format32bppPArgb);
            int sW = 0, sH = 0;
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if ((sWidth * destHeight) > (sHeight * destWidth))
            {
                sW = destWidth;
                sH = (destWidth * sHeight) / sWidth;
            }
            else
            {
                sH = destHeight;
                sW = (sWidth * destHeight) / sHeight;
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);

            g.Dispose();
            imgSource.Dispose();
            return outBmp;
        }

        private Point sourceLocation, destLocation;
        private Size sourceSize, destSize;
        private Bitmap btnImage, enterImg;

        public void zoomBtn(Control _obj, Bitmap _btnImg, Point _location, Size _size, Size _destsize, EventHandler _click)
        {
            sourceSize = _size;
            destSize = _destsize;
            btnImage = zoomGetbtn( _btnImg.Clone(new Rectangle(0, 0, _btnImg.Width, _btnImg.Height), PixelFormat.Format32bppPArgb), _size.Width, _size.Height);
            enterImg = zoomGetbtn(_btnImg.Clone(new Rectangle(0, 0, _btnImg.Width, _btnImg.Height), PixelFormat.Format32bppPArgb), _destsize.Width, _destsize.Height);

            sourceLocation = _location;
            destLocation.X = _location.X + (_size.Width - _destsize.Width) / 2;
            destLocation.Y = _location.Y + (_size.Height - _destsize.Height);

            UiControlsMethod.PanelEx btnPanel = new UiControlsMethod.PanelEx();
            btnPanel.Parent = _obj;
            btnPanel.BackColor = Color.Transparent;
            btnPanel.Location = _location;
            btnPanel.Size = _size;
            btnPanel.BackgroundImage = btnImage;

            btnPanel.MouseEnter += new EventHandler(btnPanel_MouseEnter);
            btnPanel.MouseLeave += new EventHandler(btnPanel_MouseLeave);
            btnPanel.Click += _click;
            _obj.Controls.Add(btnPanel);
        }

        private void btnPanel_MouseLeave(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.BackgroundImage = null;
            btn.Cursor = Cursors.Default;
            btn.Size = sourceSize;
            btn.Location = sourceLocation;
            btn.BackgroundImage = btnImage;
        }

        private void btnPanel_MouseEnter(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.BackgroundImage = null;
            btn.Cursor = Cursors.Hand;
            btn.Size = destSize;
            btn.Location = destLocation;
            btn.BackgroundImage = enterImg;
        }
    }

    /// <summary>
    /// 变色按钮
    /// </summary>
    public class discolorButton
    {
        private Color entercolor;
        UiTransparentRectMethod trans;
        int opacity;

        public void discolorBtn(Control _obj, Point _location, Size _size, Bitmap _bmp, Color _entercolor, int _opacity, EventHandler _click)
        {
            trans = new UiTransparentRectMethod();
            trans.BackColor = _entercolor;
            trans.Radius = 1;
            trans.ShapeBorderStyle = UiTransparentRectMethod.ShapeBorderStyles.ShapeBSNone;
            trans.Size = _size; 
            trans.Location = _location;
            trans.Opacity = 0;
            opacity = _opacity;

            UiControlsMethod.PanelEx btnPanel = new UiControlsMethod.PanelEx();
            btnPanel.Parent = _obj;
            btnPanel.Location = new Point(0,0);
            btnPanel.Size = _size;
            btnPanel.BackgroundImage = _bmp;
            entercolor = _entercolor;
            btnPanel.BackgroundImageLayout = ImageLayout.Center;
            btnPanel.BackColor = Color.Transparent;
            btnPanel.MouseEnter += new EventHandler(discoloButton_MouseEnter);
            btnPanel.MouseLeave += new EventHandler(discoloButton_MouseLeave);
            btnPanel.Click += _click;

            trans.Controls.Add(btnPanel);
            _obj.Controls.Add(trans);
        }

        private void discoloButton_MouseEnter(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.Cursor = Cursors.Hand;
            trans.Opacity = opacity;
        }

        private void discoloButton_MouseLeave(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.Cursor = Cursors.Default;
            trans.Opacity = 0;
        }
    }

    /// <summary>
    /// 切换图像按钮
    /// </summary>
    public class toggleButton
    {
        UiTransparentRectMethod trans;
        private Bitmap btnImage, enterImg;
        int opacity;

        public void toggleBtn(Control _obj, Bitmap _btnImg, Bitmap _enterImg, Point _location, Size _size, Color _entercolor, int _opacity, EventHandler _click)
        {
            trans = new UiTransparentRectMethod();
            trans.BackColor = _entercolor;
            trans.Radius = 1;
            trans.ShapeBorderStyle = UiTransparentRectMethod.ShapeBorderStyles.ShapeBSNone;
            trans.Size = _size;
            trans.Location = _location;
            trans.Opacity = 0;
            opacity = _opacity;

            btnImage = _btnImg.Clone(new Rectangle(0, 0, _btnImg.Width, _btnImg.Height), PixelFormat.Format32bppPArgb);
            enterImg = _enterImg.Clone(new Rectangle(0, 0, _enterImg.Width, _enterImg.Height), PixelFormat.Format32bppPArgb);

            UiControlsMethod.PanelEx btnPanel = new UiControlsMethod.PanelEx();
            btnPanel.Parent = _obj;
            btnPanel.BackColor = Color.Transparent;
            btnPanel.Location = new Point(0,0);
            btnPanel.Size = _size;
            btnPanel.BackgroundImage = _btnImg;

            btnPanel.MouseEnter += new EventHandler(btnPanel_MouseEnter);
            btnPanel.MouseLeave += new EventHandler(btnPanel_MouseLeave);
            btnPanel.Click += _click;

            trans.Controls.Add(btnPanel);
            _obj.Controls.Add(trans);
        }

        private void btnPanel_MouseLeave(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.BackgroundImage = null;
            btn.Cursor = Cursors.Default;
            btn.BackgroundImage = btnImage;
            trans.Opacity = 0;
        }

        private void btnPanel_MouseEnter(object sender, EventArgs e)
        {
            UiControlsMethod.PanelEx btn = (UiControlsMethod.PanelEx)sender;
            btn.BackgroundImage = null;
            btn.Cursor = Cursors.Hand;
            btn.BackgroundImage = enterImg;
            trans.Opacity = opacity;
        }
    }
}

