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
using System.Threading;
using System.Windows.Forms;

public class UiScrollBoxMethod
{
    public void scrollBox(Control _formobj, Point _location, Size _size, Image _backimage, BorderStyle _borderStyle, Control _obj, int _type)
    {
        switch (_type)
        {
            case 0:
                CoustomScroll(
                    _formobj, _location, _size, _backimage, _borderStyle, _obj,
                    27,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType1_scrollbar,
                    ImageLayout.Tile,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType1_uparrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType1_downarrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType1_scrollblock,
                    27,
                    83,
                    0, 0
                );
                break;
            case 1:
                CoustomScroll(
                    _formobj, _location, _size, _backimage, _borderStyle, _obj,
                    25,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType2_scrollbar,
                    ImageLayout.Stretch,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType2_uparrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType2_downarrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType2_scrollblock,
                    25,
                    25,
                    0, 0
                );
                break;
            case 2:
                CoustomScroll(
                    _formobj, _location, _size, _backimage,  _borderStyle, _obj,
                    8,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType3_scrollbar,
                    ImageLayout.Stretch,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType3_uparrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType3_downarrow,
                    SQK_Ui.scrollBox.scrollBoxResource.ScrollType3_scrollblock,
                    8,
                    68,
                    0, 0
                );
                break;
            default:
                break;
        }
    }

    private UiControlsMethod.PanelEx scrollPanelBox;
    private int ScrollBox_Height;
    private UiControlsMethod.PanelEx ScrollBox_;
    private Control ObjectControl;
    private int setX;
    private int limtY;
    private Thread controlReflash;
    private bool _reflash = false;
    private int controlTop = 0;
    private Bitmap blockBmp;
    private List<UiControlsMethod.ADraggableGDIObject> m_DraggableGDIObjects;

    private void CoustomScroll(
        Control _formObj,
        Point _Location,
        Size _Size,
        Image backimage,
        BorderStyle borderStyle,
        Control _Obj,
        int scrollWidth,
        Image ScrollBox_BackImg,
        ImageLayout ScrollBox_BackImgLayout,
        Image UpArrowImg,
        Image DownArrowImg,
        Image ScrollBox_MoveImg,
        int ScrollBox_MoveWidth,
        int ScrollBox_MoveHeight,
        int BeginSetX,
        int BeginSetY
        )
    {
        ObjectControl = _Obj;
        blockBmp = new Bitmap(ScrollBox_MoveImg.Width, ScrollBox_MoveImg.Height);
        blockBmp = (Bitmap)ScrollBox_MoveImg;

        scrollPanelBox = new UiControlsMethod.PanelEx();
        scrollPanelBox.Location = _Location;
        scrollPanelBox.Size = _Size;
        scrollPanelBox.BackgroundImage = backimage;
        scrollPanelBox.BorderStyle = borderStyle;
        if (backimage == null) scrollPanelBox.BackColor = Color.Transparent;
        else scrollPanelBox.BackgroundImage = backimage;

        UiControlsMethod.PanelEx placement_Panel = new UiControlsMethod.PanelEx();
        placement_Panel.BackColor = Color.Transparent;
        placement_Panel.Dock = DockStyle.Right;
        placement_Panel.Width = scrollWidth;
        placement_Panel.Location = new Point(scrollPanelBox.Location.X + scrollPanelBox.Width, scrollPanelBox.Location.Y);
        placement_Panel.Height = scrollPanelBox.Height;

        ScrollBox_Height = placement_Panel.Height;
        UiControlsMethod.PanelEx UpArrow = new UiControlsMethod.PanelEx();
        UpArrow.Size = UpArrowImg.Size;
        UpArrow.BackgroundImage = UpArrowImg;
        UpArrow.Dock = DockStyle.Top;
        UpArrow.Click += new EventHandler(UpArrow_Click);
        placement_Panel.Controls.Add(UpArrow);
        UiControlsMethod.PanelEx DownArrow = new UiControlsMethod.PanelEx();
        DownArrow.Size = DownArrowImg.Size;
        DownArrow.BackgroundImage = DownArrowImg;
        DownArrow.Dock = DockStyle.Bottom;
        DownArrow.Click += new EventHandler(DownArrow_Click);
        placement_Panel.Controls.Add(DownArrow);

        ScrollBox_ = new UiControlsMethod.PanelEx();
        ScrollBox_.Size = new Size(ScrollBox_MoveWidth, placement_Panel.Height - UpArrow.Height - DownArrow.Height);
        ScrollBox_.Location = new Point(0, UpArrow.Height);
        ScrollBox_.BackgroundImage = ScrollBox_BackImg;
        ScrollBox_.BackgroundImageLayout = ScrollBox_BackImgLayout;
        ScrollBox_.BackColor = Color.Transparent;
        ScrollBox_.Paint += new PaintEventHandler(ScrollBox_Paint);
        ScrollBox_.MouseDown += new MouseEventHandler(ScrollBox_MouseDown);
        ScrollBox_.MouseUp += new MouseEventHandler(ScrollBox_MouseUp);
        ScrollBox_.MouseMove += new MouseEventHandler(ScrollBox_MouseMove);

        placement_Panel.Controls.Add(ScrollBox_);
        m_DraggableGDIObjects = new List<UiControlsMethod.ADraggableGDIObject>();
        UiControlsMethod.Draggable draggableBlock = new UiControlsMethod.Draggable(0, 0, blockBmp);
        m_DraggableGDIObjects.Add(draggableBlock);
        setX = BeginSetX;
        limtY = placement_Panel.Height - blockBmp.Height - DownArrow.Height - UpArrow.Height;
        scrollPanelBox.MouseWheel += new MouseEventHandler(OnMouseWheel);
        scrollPanelBox.Controls.Add(placement_Panel);
        _formObj.Controls.Add(scrollPanelBox);
        
        ObjectControl.Location = new Point(0, 0);
        ObjectControl.AutoSize = true;
        ObjectControl.Parent = scrollPanelBox;
        scrollPanelBox.Controls.Add(ObjectControl);
        
        controlReflash = new Thread(_controlreflash);
        controlReflash.Start();
    }

    private void _controlreflash()
    {
        /*
        ObjectControl.Location = new Point(0, 0);
        ObjectControl.AutoSize = true;
        scrollPanelBox.Controls.Add(ObjectControl);
        */
        while (true)
        {
            if (_reflash)
            {
                ObjectControl.CrossThreadCalls(() =>
                {
                    if (ObjectControl.BackColor == Color.Transparent)
                    {
                        ObjectControl.Update();
                        ObjectControl.Invalidate(new Rectangle(0, -(controlTop), ObjectControl.Width, scrollPanelBox.Height), false);
                    }
                    ObjectControl.Top = controlTop;
                });
                _reflash = false;
            }
        }
    }

    private void OnMouseWheel(object sender, MouseEventArgs e)
    {
        int set_y = 0;
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            if (e.Delta > 0)
            {
                set_y = item.Region.Top - 10;
                if (set_y < 0)
                {
                    set_y = 0;
                    controlTop = 0;
                    _reflash = true;
                }
                if (ObjectControl.Top < 0)
                {
                    if (ObjectControl.Top + (ObjectControl.Height / limtY) * 10 > 0)
                    {
                        set_y = 0;

                        controlTop = 0;
                        _reflash = true;
                    }
                    else
                    {
                        controlTop = ObjectControl.Top + (ObjectControl.Height / limtY) * 10;
                        _reflash = true;
                    }
                }
                else
                {
                    controlTop = 0;
                    _reflash = true;
                    set_y = 0;
                }
            }
            if (e.Delta < 0)
            {
                set_y = item.Region.Top + 10;
                if (set_y > limtY) { set_y = limtY; }
                if (-ObjectControl.Top < ObjectControl.Height - ScrollBox_Height)
                {
                    if (-set_y * (ObjectControl.Height / limtY) < -(ObjectControl.Height - ScrollBox_Height))
                    {
                        controlTop = -(ObjectControl.Height - ScrollBox_Height);
                        _reflash = true;
                        set_y = limtY;
                    }
                    else
                    {
                        controlTop = -set_y * (ObjectControl.Height / limtY);
                        _reflash = true;
                    }
                }
                else
                {
                    controlTop = -(ObjectControl.Height - ScrollBox_Height);
                    _reflash = true;
                    set_y = limtY;
                }
            }

            item.Region = new Rectangle(0, set_y, item.Region.Width, item.Region.Height);
            item.DraggingPoint = e.Location;
            ScrollBox_.Invalidate();
        }
    }

    private void ScrollBox_MouseDown(object sender, MouseEventArgs e)
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

    private void ScrollBox_MouseUp(object sender, MouseEventArgs e)
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
                    int set_y = e.Y;
                    if (-set_y * (ObjectControl.Height / limtY) < -(ObjectControl.Height - ScrollBox_Height))
                    {
                        controlTop = -(ObjectControl.Height - ScrollBox_Height);
                        _reflash = true;
                        set_y = limtY;
                    }
                    else
                    {
                        controlTop = -set_y * (ObjectControl.Height / limtY);
                        _reflash = true;
                    }
                    item.Region = new Rectangle(0, set_y, item.Region.Width, item.Region.Height);
                    item.DraggingPoint = e.Location;
                    ScrollBox_.Invalidate();
                }
            }
        }
    }

    private void ScrollBox_MouseMove(object sender, MouseEventArgs e)
    {
        int set_y = 0;
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            if (item.IsDragging)
            {
                set_y = item.Region.Top + e.Y - item.DraggingPoint.Y;
                if (set_y < 0)
                {
                    set_y = 0;
                    controlTop = 0;
                    _reflash = true;
                }
                if (set_y > limtY)
                {
                    set_y = limtY;
                    controlTop = -(ObjectControl.Height - ScrollBox_Height);
                    _reflash = true;
                }
                else
                {
                    if (-set_y * (ObjectControl.Height / limtY) < -(ObjectControl.Height - ScrollBox_Height))
                    {
                        controlTop = -(ObjectControl.Height - ScrollBox_Height);
                        _reflash = true;
                    }
                    else
                    {
                        controlTop = -set_y * (ObjectControl.Height / limtY);
                        _reflash = true;
                    }
                }
                item.Region = new Rectangle(0, set_y, item.Region.Width, item.Region.Height);
                item.DraggingPoint = e.Location;
                ScrollBox_.Invalidate();
            }
        }
    }

    private void ScrollBox_Paint(object sender, PaintEventArgs e)
    {
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            item.OnPaint(e);
        }
    }

    private void UpArrow_Click(object sender, EventArgs e)
    {
        int set_y = 0;
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            set_y = item.Region.Top - 10;
            if (set_y < 0)
            {
                set_y = 0;
                controlTop = 0;
                _reflash = true;
            }
            if (ObjectControl.Top < 0)
            {
                if (ObjectControl.Top + (ObjectControl.Height / limtY) * 10 > 0)
                {
                    set_y = 0;

                    controlTop = 0;
                    _reflash = true;
                }
                else
                {
                    controlTop = ObjectControl.Top + (ObjectControl.Height / limtY) * 10;
                    _reflash = true;
                }
            }
            else
            {
                controlTop = 0;
                _reflash = true;
                set_y = 0;
            }
            item.Region = new Rectangle(0, set_y, item.Region.Width, item.Region.Height);
            ScrollBox_.Invalidate();
        }
    }

    private void DownArrow_Click(object sender, EventArgs e)
    {
        int set_y = 0;
        foreach (UiControlsMethod.ADraggableGDIObject item in m_DraggableGDIObjects)
        {
            set_y = item.Region.Top + 10;
            if (set_y > limtY) { set_y = limtY; }
            if (-ObjectControl.Top < ObjectControl.Height - ScrollBox_Height)
            {
                if (-set_y * (ObjectControl.Height / limtY) < -(ObjectControl.Height - ScrollBox_Height))
                {
                    controlTop = -(ObjectControl.Height - ScrollBox_Height);
                    _reflash = true;
                    set_y = limtY;
                }
                else
                {
                    controlTop = -set_y * (ObjectControl.Height / limtY);
                    _reflash = true;
                }
            }
            else
            {
                controlTop = -(ObjectControl.Height - ScrollBox_Height);
                _reflash = true;
                set_y = limtY;
            }
            item.Region = new Rectangle(0, set_y, item.Region.Width, item.Region.Height);
            ScrollBox_.Invalidate();
        }
    }
}