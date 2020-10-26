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
using System.Drawing;
using System.Windows.Forms;

public class UiTrackBarMethods
{
    public int Track_Value = 0;

    private List<UiControlsMethod.ADraggableGDIObject> m_DraggableGDIObjects;
    private UiControlsMethod.PanelEx _track = new UiControlsMethod.PanelEx();
    private Bitmap Bmp_Background, Bmp_Block, Bmp_Flag, Bmp_Gress, gress_left, gress_middle;
    private int VH_type = 0;
    private int[] m_set;
    private int bar_Height = 1, gress_Height = 1;
    private int maxValue = 0, minValue = 0;
    private MouseEventHandler mEvent;

    public void InitializeTrack(Control _obj, int _type, int vh_type, Point _location, int _twidth, MouseEventHandler changeEvent)
    {
        VH_type = vh_type;
        mEvent = changeEvent;

        /*
                m_set[0] = bar left-width;     m_set[1] = bar right-width;
                m_set[2] = gress left-width;  m_set[3] = gress middle-width;
                m_set[4] = gress location x;  m_set[5] = gress location y;
                m_set[6] = block location x;  m_set[7] = block location y;
        */
        switch (_type)
        {
            case 0:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar1;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track1;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress1;
                m_set = new int[8] { 1,1,1,1,1,0,0,0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width+2;
                    minValue = m_set[6];
                } else 
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width+2;
                    minValue = m_set[6];
                }
                break;
            case 1:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar2;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track2;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress2;
                m_set = new int[8] { 13, 13, 6, 6, 8, 8, 10, 6 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 18;
                    minValue = m_set[6];
                } else 
                if (VH_type == 1)
                {
                    maxValue = _twidth  - Bmp_Flag.Height -  24;
                    minValue = m_set[6] - m_set[2]+4;
                }
                break;
            case 2:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar2;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track3;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress2;
                m_set = new int[8] { 13, 13, 6, 6, 8, 8, 10, 0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 18;
                    minValue = m_set[6];
                } else
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 16;
                    minValue = m_set[6] - m_set[2] +4;
                }
                break;
            case 3:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar4;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track4;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress4;
                m_set = new int[8] { 3, 3, 3, 3, 0, 0, 0, 0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 7;
                    minValue = m_set[6];
                } else
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 6;
                    minValue = m_set[6] - 1;
                }
                break;
            case 4:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar5;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track5;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress5;
                m_set = new int[8] { 3, 3, 3, 3, 0, 0, 0, 0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 6;
                    minValue = m_set[6];
                }
                else
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 6;
                    minValue = m_set[6];
                }
                break;
            case 5:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar6;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track6;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress6;
                m_set = new int[8] { 3, 3, 3, 3, 0, 0, 0, 0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 6;
                    minValue = m_set[6];
                }
                else
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 6;
                    minValue = m_set[6];
                }
                break;

            default:
                Bmp_Block = SQK_Ui.TrackBar.TrackResource.trackbar1;
                Bmp_Flag = SQK_Ui.TrackBar.TrackResource.track1;
                Bmp_Gress = SQK_Ui.TrackBar.TrackResource.trackgress1;
                m_set = new int[8] { 1, 1, 1, 1, 1, 0, 0, 0 };
                if (VH_type == 0)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 2;
                    minValue = m_set[6];
                }
                else
                if (VH_type == 1)
                {
                    maxValue = _twidth - Bmp_Flag.Width + 2;
                    minValue = m_set[6];
                }
                break;
        }
        bar_Height = Bmp_Block.Height;
        gress_Height = Bmp_Gress.Height;

        int _height = bar_Height;
        if (Bmp_Flag.Height > bar_Height) _height = Bmp_Flag.Height;

        if (VH_type == 0)
        {
            m_DraggableGDIObjects = new List<UiControlsMethod.ADraggableGDIObject>();
            UiControlsMethod.Draggable draggableBlock = new UiControlsMethod.Draggable(m_set[6], m_set[7], Bmp_Flag);
            m_DraggableGDIObjects.Add(draggableBlock);
        } else
        if (VH_type == 1)
        {
            Bmp_Flag.RotateFlip(RotateFlipType.Rotate270FlipNone);
            m_DraggableGDIObjects = new List<UiControlsMethod.ADraggableGDIObject>();
            UiControlsMethod.Draggable draggableBlock = new UiControlsMethod.Draggable(m_set[7], _twidth + m_set[0] + m_set[1] - Bmp_Flag.Height- m_set[6], Bmp_Flag);
            m_DraggableGDIObjects.Add(draggableBlock);
        }

        Bitmap sep_Left = Bmp_Block.Clone(new Rectangle(0, 0, m_set[0], bar_Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap sep_Mid = Bmp_Block.Clone(new Rectangle(m_set[0], 0, 1, bar_Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap sep_Right = Bmp_Block.Clone(new Rectangle(Bmp_Block.Width - m_set[1], 0, m_set[1], bar_Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap bmp_track = new Bitmap(m_set[0] + _twidth + m_set[1], bar_Height);
        Graphics bitmapTrack = Graphics.FromImage(bmp_track);
        bitmapTrack.DrawImage(sep_Left, 0, 0, m_set[0], bar_Height);
        for (int i = 0; i < _twidth; i++)
        {
            bitmapTrack.DrawImage(sep_Mid, m_set[0] + i, 0, 1, bar_Height);
        }
        bitmapTrack.DrawImage(sep_Right, m_set[0] + _twidth, 0, m_set[1], bar_Height);
        Bitmap Bmp_BackgroundTmp = bmp_track;

        Bmp_Background = Bmp_BackgroundTmp;

        gress_left = Bmp_Gress.Clone(new Rectangle(0, 0, m_set[2], gress_Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        gress_middle = Bmp_Gress.Clone(new Rectangle(m_set[2], 0, m_set[3], gress_Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
        bitmapBackground.DrawImage(gress_left,m_set[4],m_set[5], m_set[2], gress_Height);
        bitmapBackground.DrawImage(gress_middle, m_set[2]+m_set[4], m_set[5], m_set[3], gress_Height);

        _track.BackColor = Color.Transparent;
        _track.Location = _location;
        _track.BackgroundImageLayout = ImageLayout.Center;

        if (VH_type == 0)
        {
            _track.Size = new Size(_twidth + m_set[0] + m_set[1], _height);
            _track.BackgroundImage = Bmp_BackgroundTmp;
        } else
        if (VH_type == 1)
        {
            Bmp_BackgroundTmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            gress_middle.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _track.Size = new Size(_height, _twidth + m_set[0] + m_set[1]);
            _track.BackgroundImage = Bmp_BackgroundTmp;
        }

        _track.Paint += new PaintEventHandler(_Paint);
        _track.MouseDown += new MouseEventHandler(_MouseDown);
        _track.MouseUp += new MouseEventHandler(_MouseUp);
        _track.MouseMove += new MouseEventHandler(_MouseMove);

        _obj.Controls.Add(_track);
    }

    private void _MouseDown(object sender, MouseEventArgs e)
    {
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            if (item.Region.Contains(e.Location))
            {
                item.IsDragging = true;
                item.DraggingPoint = e.Location;
            }
        }
    }

    private void _MouseMove(object sender, MouseEventArgs e)
    {
        int set_x = 0, set_y = 0;
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            if (item.IsDragging)
            {
                if (VH_type == 0)
                {
                    set_x = item.Region.Left + e.X - item.DraggingPoint.X;
                    if (set_x > maxValue) set_x = maxValue;
                    if (set_x < minValue) set_x = minValue;

                    item.Region = new Rectangle(set_x, m_set[7], item.Region.Width, item.Region.Height);
                    item.DraggingPoint = e.Location;

                    Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                    bitmapBackground.DrawImage(gress_left, m_set[4], m_set[5], m_set[2], gress_Height);
                    for (int i = m_set[2]; i < set_x; i++)
                    {
                        bitmapBackground.DrawImage(gress_middle, i + m_set[4], m_set[5], m_set[2], gress_Height);
                    }
                    _track.BackgroundImage = Bmp_BackgroundTmp;
                    double _value = (set_x - minValue) / (float)(maxValue - minValue) * 100;
                    Track_Value = (int)(Math.Ceiling(_value));
                } else
                if (VH_type==1)
                {
                    set_y = item.Region.Top + e.Y - item.DraggingPoint.Y;
                    if (set_y > maxValue) set_y = maxValue;
                    if (set_y < minValue) set_y = minValue;
                    item.Region = new Rectangle(m_set[7], set_y, item.Region.Width, item.Region.Height);
                    item.DraggingPoint = e.Location;

                    Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                    for (int i = set_y; i < maxValue; i++)
                    {
                        bitmapBackground.DrawImage(gress_middle, m_set[5], i+Bmp_Flag.Height- m_set[4]- m_set[2], gress_Height, m_set[2]);
                    }
                    _track.BackgroundImage = Bmp_BackgroundTmp;
                    double _value = (1-(set_y - minValue) / (float)(maxValue - minValue)) * 100;
                    Track_Value = (int)(Math.Ceiling(_value));
                }
                _track.Invalidate();
                mEvent(null,null);
            }
        }
    }

    private void _MouseUp(object sender, MouseEventArgs e)
    {
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            if (item.IsDragging)
            {
                item.IsDragging = false;
                item.DraggingPoint = Point.Empty;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    int set_x = e.X, set_y = e.Y;
                    if (VH_type == 0)
                    {
                        if (set_x > maxValue) set_x = maxValue;
                        if (set_x < minValue) set_x = minValue;

                        item.Region = new Rectangle(set_x, m_set[7], item.Region.Width, item.Region.Height);
                        item.DraggingPoint = e.Location;
                        Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                        Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                        bitmapBackground.DrawImage(gress_left, m_set[4], m_set[5], m_set[2], gress_Height);
                        for (int i = m_set[2]; i < set_x; i++)
                        {
                            bitmapBackground.DrawImage(gress_middle, i + m_set[4], m_set[5], m_set[2], gress_Height);
                        }
                        _track.BackgroundImage = Bmp_BackgroundTmp;
                        double _value = (set_x - minValue) / (float)(maxValue - minValue) * 100;
                        Track_Value = (int)(Math.Ceiling(_value));
                    }
                    else 
                    if (VH_type == 1)
                    {
                        if (set_y > maxValue) set_y = maxValue;
                        if (set_y < minValue) set_y = minValue;
                        item.Region = new Rectangle(m_set[7], set_y, item.Region.Width, item.Region.Height);
                        item.DraggingPoint = e.Location;
                        Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                        Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                        for (int i = set_y; i < maxValue; i++)
                        {
                            bitmapBackground.DrawImage(gress_middle, m_set[5], i + Bmp_Flag.Height - m_set[4] - m_set[2], gress_Height, m_set[2]);
                        }
                        _track.BackgroundImage = Bmp_BackgroundTmp;
                        double _value = (1 - (set_y - minValue) / (float)(maxValue - minValue)) * 100;
                        Track_Value = (int)(Math.Ceiling(_value));
                    }
                    _track.Invalidate();
                    mEvent(null, null);
                }
            }
        }
    }

    public void Set_location (int percentage)
    {
        if (percentage > 100) percentage = 100;
        if (percentage < 0) percentage = 0;
        int _value = (int)(Math.Ceiling( (float)(percentage) * maxValue/100));
        
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            int set_x = _value, set_y = _value;
            if (VH_type == 0)
            {
                if (set_x > maxValue) set_x = maxValue;
                if (set_x < minValue) set_x = minValue;

                item.Region = new Rectangle(set_x, m_set[7], item.Region.Width, item.Region.Height);
                Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                bitmapBackground.DrawImage(gress_left, m_set[4], m_set[5], m_set[2], gress_Height);
                for (int i = m_set[2]; i < set_x; i++)
                {
                    bitmapBackground.DrawImage(gress_middle, i + m_set[4], m_set[5], m_set[2], gress_Height);
                }
                _track.BackgroundImage = Bmp_BackgroundTmp;
                Track_Value = percentage;
            }
            else
            if (VH_type == 1)
            {
                if (set_y > maxValue) set_y = maxValue;
                if (set_y < minValue) set_y = minValue;
                item.Region = new Rectangle(m_set[7], set_y, item.Region.Width, item.Region.Height);
                Bitmap Bmp_BackgroundTmp = Bmp_Background.Clone(new Rectangle(0, 0, Bmp_Background.Width, Bmp_Background.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics bitmapBackground = Graphics.FromImage(Bmp_BackgroundTmp);
                for (int i = set_y; i < maxValue; i++)
                {
                    bitmapBackground.DrawImage(gress_middle, m_set[5], i + Bmp_Flag.Height - m_set[4] - m_set[2], gress_Height, m_set[2]);
                }
                _track.BackgroundImage = Bmp_BackgroundTmp;
                Track_Value = percentage;

            }
            _track.Invalidate();
            mEvent(null, null);
        }
    }

    private void _Paint(object sender, PaintEventArgs e)
    {
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            item.OnPaint(e);
        }
    }
}

