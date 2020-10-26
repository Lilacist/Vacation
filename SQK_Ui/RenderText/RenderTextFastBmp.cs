/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 2.0 - .Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.1.10
-----------------------------------------------*/

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

class FastBitmap : IDisposable, ICloneable
{
    internal Bitmap _bitmap;
    private BitmapData _bitmapData;

    public FastBitmap(Int32 width, Int32 height, PixelFormat fmt)
    {
        _bitmap = new Bitmap(width, height, fmt);
    }

    ~FastBitmap()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    protected virtual void Dispose(Boolean disposing)
    {
        Unlock();
        if (disposing)
        {
            _bitmap.Dispose();
        }
    }

    private FastBitmap()
    {
    }

    public Object Clone()
    {
        FastBitmap clone = new FastBitmap();
        clone._bitmap = (Bitmap)_bitmap.Clone();
        return clone;
    }

    public Int32 Width
    {
        get { return _bitmap.Width; }
    }

    public Int32 Height
    {
        get { return _bitmap.Height; }
    }

    public void Lock()
    {
        _bitmapData = _bitmap.LockBits(
            new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
            ImageLockMode.ReadWrite,
            _bitmap.PixelFormat
            );
    }

    unsafe public Color GetPixel(Int32 x, Int32 y)
    {
        if (_bitmapData.PixelFormat == PixelFormat.Format32bppArgb)
        {
            Byte* b = (Byte*)_bitmapData.Scan0 + (y * _bitmapData.Stride) + (x * 4);
            return Color.FromArgb(*(b + 3), *(b + 2), *(b + 1), *b);
        }
        if (_bitmapData.PixelFormat == PixelFormat.Format24bppRgb)
        {
            Byte* b = (Byte*)_bitmapData.Scan0 + (y * _bitmapData.Stride) + (x * 3);
            return Color.FromArgb(*(b + 2), *(b + 1), *b);
        }
        return Color.Empty;
    }

    unsafe public void SetPixel(Int32 x, Int32 y, Color c)
    {
        if (_bitmapData.PixelFormat == PixelFormat.Format32bppArgb)
        {
            Byte* b = (Byte*)_bitmapData.Scan0 + (y * _bitmapData.Stride) + (x * 4);
            *b = c.B;
            *(b + 1) = c.G;
            *(b + 2) = c.R;
            *(b + 3) = c.A;
        }
        if (_bitmapData.PixelFormat == PixelFormat.Format24bppRgb)
        {
            Byte* b = (Byte*)_bitmapData.Scan0 + (y * _bitmapData.Stride) + (x * 3);
            *b = c.B;
            *(b + 1) = c.G;
            *(b + 2) = c.R;
        }
    }

    public Byte GetIntensity(Int32 x, Int32 y)
    {
        Color c = GetPixel(x, y);
        return (Byte)((c.R * 0.30 + c.G * 0.59 + c.B * 0.11) + 0.5);
    }

    public void Unlock()
    {
        if (_bitmapData != null)
        {
            _bitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
        }
    }

    public void Save(String filename, ImageFormat format)
    {
        _bitmap.Save(filename, format);
    }

    public void Save(String filename)
    {
        _bitmap.Save(filename);
    }
}

class Layer : ICloneable
{
    internal FastBitmap _bitmap;
    private FastBitmap _mask;
    public Double _opacity;
    private Int32 _offsetx, _offsety;

    public Layer(Int32 width, Int32 height)
    {
        _bitmap = new FastBitmap(width, height, PixelFormat.Format32bppArgb);
        Clear(Color.Transparent);
        _opacity = 1.0;
    }

    public Double Opacity
    {
        get { return _opacity; }
        set { _opacity = value; }
    }

    public FastBitmap Bitmap
    {
        get { return _bitmap; }
    }

    public FastBitmap Mask
    {
        get { return _mask; }
        set { _mask = value; }
    }

    public Int32 OffsetX
    {
        get { return _offsetx; }
        set { _offsetx = value; }
    }

    public Int32 OffsetY
    {
        get { return _offsety; }
        set { _offsety = value; }
    }

    public Object Clone()
    {
        Layer clone = new Layer(_bitmap.Width, _bitmap.Height);
        clone._bitmap = (FastBitmap)_bitmap.Clone();
        return clone;
    }

    public void Clear(Color c)
    {
        _bitmap.Lock();
        for (Int32 y = 0; y < _bitmap.Height; y++)
            for (Int32 x = 0; x < _bitmap.Width; x++)
                _bitmap.SetPixel(x, y, c);
        _bitmap.Unlock();
    }

    public void DrawText(Int32 x, Int32 y, String text, Font font,
        Brush brush)
    {
        Graphics g = Graphics.FromImage(_bitmap._bitmap);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        g.DrawString(text, font, brush, x, y, StringFormat.GenericTypographic);
        g.Dispose();
    }

    public void FillRectangle(Int32 x, Int32 y, Int32 width,
        Int32 height, Brush brush)
    {
        Graphics g = Graphics.FromImage(_bitmap._bitmap);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        g.FillRectangle(brush, x, y, width, height);
        g.Dispose();
    }

    public Color GetPixel(Int32 x, Int32 y)
    {
        return _bitmap.GetPixel(x, y);
    }

    public void Invert()
    {
        _bitmap.Lock();
        for (Int32 y = 0; y < _bitmap.Height; y++)
        {
            for (Int32 x = 0; x < _bitmap.Width; x++)
            {
                Color c = _bitmap.GetPixel(x, y);
                _bitmap.SetPixel(x, y, Color.FromArgb(c.A,
                    255 - c.R, 255 - c.G, 255 - c.B));
            }
        }
        _bitmap.Unlock();
    }

    private Single Gauss(Single x, Single middle, Single width)
    {
        if (width == 0)
            return 1F;

        Double t = -(1.0 / width) * ((middle - x) * (middle - x));
        return (Single)Math.Pow(Math.E, t);
    }

    public void Blur(Int32 horz, Int32 vert)
    {
        Single weightsum;
        Single[] weights;

        FastBitmap t = (FastBitmap)_bitmap.Clone();

        _bitmap.Lock();
        t.Lock();

        weights = new Single[horz * 2 + 1];
        for (Int32 i = 0; i < horz * 2 + 1; i++)
        {
            Single y = Gauss(-horz + i, 0, horz);
            weights[i] = y;
        }

        for (Int32 row = 0; row < _bitmap.Height; row++)
        {
            for (Int32 col = 0; col < _bitmap.Width; col++)
            {
                Double r = 0;
                Double g = 0;
                Double b = 0;
                weightsum = 0;
                for (Int32 i = 0; i < horz * 2 + 1; i++)
                {
                    Int32 x = col - horz + i;
                    if (x < 0)
                    {
                        i += -x;
                        x = 0;
                    }
                    if (x > _bitmap.Width - 1)
                        break;
                    Color c = _bitmap.GetPixel(x, row);
                    r += c.R * weights[i];
                    g += c.G * weights[i];
                    b += c.B * weights[i];
                    weightsum += weights[i];
                }
                r /= weightsum;
                g /= weightsum;
                b /= weightsum;
                Byte br = (Byte)Math.Round(r);
                Byte bg = (Byte)Math.Round(g);
                Byte bb = (Byte)Math.Round(b);
                if (br > 255) br = 255;
                if (bg > 255) bg = 255;
                if (bb > 255) bb = 255;
                t.SetPixel(col, row, Color.FromArgb(br, bg, bb));
            }
        }

        weights = new Single[vert * 2 + 1];
        for (Int32 i = 0; i < vert * 2 + 1; i++)
        {
            Single y = Gauss(-vert + i, 0, vert);
            weights[i] = y;
        }

        for (Int32 col = 0; col < _bitmap.Width; col++)
        {
            for (Int32 row = 0; row < _bitmap.Height; row++)
            {
                Double r = 0;
                Double g = 0;
                Double b = 0;
                weightsum = 0;
                for (Int32 i = 0; i < vert * 2 + 1; i++)
                {
                    Int32 y = row - vert + i;
                    if (y < 0)
                    {
                        i += -y;
                        y = 0;
                    }
                    if (y > _bitmap.Height - 1)
                        break;
                    Color c = t.GetPixel(col, y);
                    r += c.R * weights[i];
                    g += c.G * weights[i];
                    b += c.B * weights[i];
                    weightsum += weights[i];
                }
                r /= weightsum;
                g /= weightsum;
                b /= weightsum;
                Byte br = (Byte)Math.Round(r);
                Byte bg = (Byte)Math.Round(g);
                Byte bb = (Byte)Math.Round(b);
                if (br > 255) br = 255;
                if (bg > 255) bg = 255;
                if (bb > 255) bb = 255;
                _bitmap.SetPixel(col, row, Color.FromArgb(br, bg, bb));
            }
        }
        t.Dispose(); 
        _bitmap.Unlock();
    }

    private Byte GetBumpMapPixel(FastBitmap bmp, Int32 x, Int32 y)
    {
        if (x < 0)
            x = 0;
        if (x > _bitmap.Width - 1)
            x = _bitmap.Width - 1;

        if (y < 0)
            y = 0;
        if (y > _bitmap.Height - 1)
            y = _bitmap.Height - 1;

        return bmp.GetIntensity(x, y);
    }

    public void BumpMap(Layer bumpmap, Int32 azimuth, Int32 elevation,
        Int32 bevelwidth, Boolean lightzalways1)
    {
        bumpmap._bitmap.Lock();
        _bitmap.Lock();

        for (Int32 row = 0; row < _bitmap.Height; row++)
        {
            for (Int32 col = 0; col < _bitmap.Width; col++)
            {
                Byte[] x = new Byte[6];
                x[0] = GetBumpMapPixel(bumpmap._bitmap, col - 1, row - 1);
                x[1] = GetBumpMapPixel(bumpmap._bitmap, col - 1, row - 0);
                x[2] = GetBumpMapPixel(bumpmap._bitmap, col - 1, row + 1);
                x[3] = GetBumpMapPixel(bumpmap._bitmap, col + 1, row - 1);
                x[4] = GetBumpMapPixel(bumpmap._bitmap, col + 1, row - 0);
                x[5] = GetBumpMapPixel(bumpmap._bitmap, col + 1, row + 1);
                Single normal_x = x[0] + x[1] + x[2] - x[3] - x[4] - x[5];
                x[0] = GetBumpMapPixel(bumpmap._bitmap, col - 1, row + 1);
                x[1] = GetBumpMapPixel(bumpmap._bitmap, col - 0, row + 1);
                x[2] = GetBumpMapPixel(bumpmap._bitmap, col + 1, row + 1);
                x[3] = GetBumpMapPixel(bumpmap._bitmap, col - 1, row - 1);
                x[4] = GetBumpMapPixel(bumpmap._bitmap, col - 0, row - 1);
                x[5] = GetBumpMapPixel(bumpmap._bitmap, col + 1, row - 1);
                Single normal_y = x[0] + x[1] + x[2] - x[3] - x[4] - x[5];
                Single normal_z = (6F * 255F) / bevelwidth;
                Single length = (Single)Math.Sqrt(
                    normal_x * normal_x +
                    normal_y * normal_y +
                    normal_z * normal_z);

                if (length != 0)
                {
                    normal_x /= length;
                    normal_y /= length;
                    normal_z /= length;
                }
                Double azimuth_rad = azimuth / 180.0 * Math.PI;
                Double elevation_rad = elevation / 180.0 * Math.PI;
                Single light_x = (Single)(Math.Cos(azimuth_rad) *
                    Math.Cos(elevation_rad));
                Single light_y = (Single)(Math.Sin(azimuth_rad) *
                    Math.Cos(elevation_rad));
                Single light_z = 1F;
                if (!lightzalways1) light_z = (Single)Math.Sin(elevation_rad);
                Single cos_light_normal = 0;
                cos_light_normal += normal_x * light_x;
                cos_light_normal += normal_y * light_y;
                cos_light_normal += normal_z * light_z;
                Color c = _bitmap.GetPixel(col, row);
                Single r = c.R;
                Single g = c.G;
                Single b = c.B;
                r *= cos_light_normal;
                g *= cos_light_normal;
                b *= cos_light_normal;
                Byte red = (Byte)Math.Min(Math.Round(r), 255);
                Byte green = (Byte)Math.Min(Math.Round(g), 255);
                Byte blue = (Byte)Math.Min(Math.Round(b), 255);
                _bitmap.SetPixel(col, row, Color.FromArgb(red, green, blue));
            }
        }
        _bitmap.Unlock();
        bumpmap._bitmap.Unlock();
    }
}

class Layers
{
    LayeredImage _image;
    ArrayList _layers = new ArrayList();

    public Layers(LayeredImage image)
    {
        _image = image;
    }

    public Int32 Count
    {
        get { return _layers.Count; }
    }

    public Layer Add()
    {
        Layer layer = new Layer(_image.Width, _image.Height);
        _layers.Add(layer);
        return layer;
    }

    public Layer Copy(Layer layer)
    {
        Layer copy = (Layer)layer.Clone();
        _layers.Add(copy);
        return copy;
    }

    public Layer this[Int32 i]
    {
        get { return (Layer)_layers[i]; }
    }
}

class LayeredImage
{
    Int32 _width, _height;
    Bitmap _checkerboard;
    Layers _layers;

    public LayeredImage(Int32 width, Int32 height)
    {
        _width = width;
        _height = height;
        _layers = new Layers(this);
        _checkerboard = new Bitmap(32, 32, PixelFormat.Format24bppRgb);
        Color darkgray = Color.FromArgb(102, 102, 102);
        Color lightgray = Color.FromArgb(153, 153, 153);
        for (Int32 i = 0; i < 32; i++)
        {
            for (Int32 j = 0; j < 32; j++)
            {
                if ((i < 16 && j < 16) || (i >= 16 && j >= 16))
                    _checkerboard.SetPixel(j, i, lightgray);
                else
                    _checkerboard.SetPixel(j, i, darkgray);
            }
        }
        Layer bglayer = _layers.Add();
        Graphics g = Graphics.FromImage(bglayer._bitmap._bitmap);
        TextureBrush brush = new TextureBrush(_checkerboard);
        g.FillRectangle(brush, 0, 0, _width, _height);
        brush.Dispose();
        g.Dispose();
    }

    public Int32 Width
    {
        get { return _width; }
    }

    public Int32 Height
    {
        get { return _height; }
    }

    public Layers Layers
    {
        get { return _layers; }
    }

    internal FastBitmap Flatten()
    {
        FastBitmap final = new FastBitmap(_width, _height,
            PixelFormat.Format24bppRgb);
        final.Lock();
        for (Int32 i = 0; i < _layers.Count; i++)
        {
            Layer l = _layers[i];
            l._bitmap.Lock();
            if (l.Mask != null)
                l.Mask.Lock();
        }
        for (Int32 y = 0; y < _height; y++)
        {
            for (Int32 x = 0; x < _width; x++)
            {
                Color c0 = _layers[0]._bitmap.GetPixel(x, y);
                for (Int32 i = 1; i < _layers.Count; i++)
                {
                    Layer layer = _layers[i];
                    Color c1 = Color.Transparent;
                    if (x >= layer.OffsetX &&
                        x <= layer.OffsetX + layer._bitmap.Width - 1 &&
                        y >= layer.OffsetY &&
                        y <= layer.OffsetY + layer._bitmap.Height - 1)
                    {
                        c1 = layer._bitmap.GetPixel(x - layer.OffsetX,
                            y - layer.OffsetY);
                    }
                    if (c1.A == 255 && layer.Opacity == 1.0 &&
                        layer.Mask == null)
                    {
                        c0 = c1;
                    }
                    else
                    {
                        Double tr, tg, tb, a;
                        a = c1.A / 255.0 * layer.Opacity;
                        if (layer.Mask != null)
                        {
                            a *= layer.Mask.GetIntensity(x, y) / 255.0;
                        }
                        tr = c1.R * a + c0.R * (1.0 - a);
                        tg = c1.G * a + c0.G * (1.0 - a);
                        tb = c1.B * a + c0.B * (1.0 - a);
                        tr = Math.Round(tr);
                        tg = Math.Round(tg);
                        tb = Math.Round(tb);
                        tr = Math.Min(tr, 255);
                        tg = Math.Min(tg, 255);
                        tb = Math.Min(tb, 255);
                        c0 = Color.FromArgb((Byte)tr, (Byte)tg, (Byte)tb);
                    }
                }
                final.SetPixel(x, y, c0);
            }
        }
        for (Int32 i = 0; i < _layers.Count; i++)
        {
            Layer l = _layers[i];
            l._bitmap.Unlock();
            if (l.Mask != null)
                l.Mask.Unlock();
        }
        final.Unlock();
        return final;
    }
}

