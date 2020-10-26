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
using System.Drawing.Imaging;
using System.Drawing.Text;

public class UiRenderTextHQMethods
{
    private PrivateFontCollection privateFonts; 
    private Font font;

    private static Color HexColorToColor(String s)
    {
        if (s.Length != 6)
            return Color.Empty;

        UInt32 r, g, b;
        r = g = b = 0;
        for (Int32 i = 0; i < 6; i++)
        {
            Int32 n = "0123456789ABCDEF".IndexOf(Char.ToUpper(s[i]));
            if (n == -1)
                return Color.Empty;
            UInt32 x = (UInt32)n;
            switch (i)
            {
                case 0:
                    r |= x << 4;
                    break;
                case 1:
                    r |= x;
                    break;
                case 2:
                    g |= x << 4;
                    break;
                case 3:
                    g |= x;
                    break;
                case 4:
                    b |= x << 4;
                    break;
                case 5:
                    b |= x;
                    break;
            }
        }
        return Color.FromArgb((Byte)r, (Byte)g, (Byte)b);
    }

    public Bitmap TextRenderHQ(int _size, string _font, string _color, int _offset, string _str, bool _shadow)
    {
        privateFonts = new PrivateFontCollection();
        privateFonts.AddFontFile(_font);
        font = new Font(privateFonts.Families[0], _size);

        String s = _str;

        String[] rgbs = _color.Split(new Char[] { ' ', ',', ';', ':' });
        Color[] colors = new Color[rgbs.Length];
        for (Int32 i = 0; i < rgbs.Length; i++)
        {
            colors[i] = HexColorToColor(rgbs[i]);
        }

        Bitmap bmp = new Bitmap(10, 10, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);
        IntPtr hdc = g.GetHdc();
        IntPtr oldfont = Gdi.SelectObject(hdc, font.ToHfont());
        TEXTMETRIC tm = new TEXTMETRIC();
        Gdi.GetTextMetrics(hdc, ref tm);
        Int32 totalwidth = 0;
        Int32[] width = new Int32[1];
        Int32[] widths = new Int32[s.Length];

        Graphics gs = Graphics.FromImage(new Bitmap(1, 1));

        for (Int32 i = 0; i < s.Length; i++)
        {
            Gdi.GetCharWidth(hdc, s[i], s[i], width);

            SizeF sizeW = gs.MeasureString(s[i].ToString(), new Font(_font, _size));
            if (s[i] >= 0x4e00 && s[i] <= 0x9fbb)
            {
                widths[i] = (int)sizeW.Width - _offset;
            }
            else  widths[i] = width[0];
        }

        SizeF sizeF = gs.MeasureString(s, font);
        totalwidth = (int)sizeF.Width;

        Gdi.SelectObject(hdc, oldfont);
        g.ReleaseHdc(hdc);
        g.Dispose();
        bmp.Dispose();
        LayeredImage image = new LayeredImage(totalwidth + 4, tm.tmHeight + 4 + 4);
        Layer bg = image.Layers.Add();
        bg.Clear(Color.Black);
        bg.DrawText(0, 0, s, font, new SolidBrush(Color.White));
        Layer copybg = image.Layers.Copy(bg);
        copybg.Blur(4, 4);
        Layer white = image.Layers.Add();
        white.Clear(Color.White);

        if (_shadow)
        {
            Layer shadow = image.Layers.Copy(copybg);
            shadow.Invert();
            shadow.Opacity = 0.5;
            shadow.OffsetX += 4;
            shadow.OffsetY += 4;
        }

        Int32 offsetx = 0;
        Int32 colorindex = 0;
        Layer final = image.Layers.Add();
        for (Int32 i = 0; i < s.Length; i++)
        {
            Color c = colors[colorindex];
            colorindex++;
            if (colorindex >= colors.Length)
                colorindex = 0;
            SolidBrush brush = new SolidBrush(c);
            final.FillRectangle(offsetx, 0, widths[i], tm.tmHeight, brush);
            offsetx += widths[i];
        }
        final.BumpMap(copybg, 135, 45, 3, false);
        final.Mask = (FastBitmap)bg.Bitmap.Clone();
        FastBitmap result = image.Flatten();
        result._bitmap.MakeTransparent();
        return result._bitmap;
    }
}

