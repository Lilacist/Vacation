/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System.Drawing;

public class UiDrawTextMethod
{
    public void DrawString(Bitmap _bmp, string text, Font fnt, Color textColor, Rectangle rect)
    {
        DrawString(_bmp, text, fnt, textColor, rect, false, new Point(0,0));
    }

    public void DrawString(Bitmap _bmp, string text, Font fnt, Color textColor, Rectangle rect, bool shadow, Point distance)
    {
        Graphics txt_bmp = Graphics.FromImage(_bmp);
        if (shadow)
        {
            PixelTextShadow _Txtshadow= new PixelTextShadow();
            _Txtshadow.Draw(txt_bmp, text, fnt, new PointF(rect.X+ distance.X, rect.Y+ distance.Y));
        }
        txt_bmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        SolidBrush _brush = new SolidBrush(textColor);
        txt_bmp.DrawString(text, fnt, _brush, rect);
    }

    public void DrawFontAwesome (Bitmap _bmp, FontAwesome.Type type, int size, Color color, Point location, bool Border)
    {
        Bitmap font_bmp = new Bitmap(size,size);
        font_bmp = new FontAwesome.Properties(type) { ForeColor = color, Size = size - 4, ShowBorder = Border }.AsImage();

        Graphics add_bmp = Graphics.FromImage(_bmp);
        add_bmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        add_bmp.DrawImage(font_bmp, location);
    }
}

