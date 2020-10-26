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

public class UiDropListMethods
{
    public bool DropClickArea = false;

    private int[] m_set;
    private Rectangle dropClick;
    private UiControlsMethod.PictureBoxEx _drop = new UiControlsMethod.PictureBoxEx();
    private UiInputBoxMethods _uInput = new UiInputBoxMethods();
    private AlphaForm listForm;
    private Control Obj;
    private Point location;
    private Bitmap bmp_drop, bmp_list;
    private bool _showState = false;
    private int transparency;
    private Color _moveColor, _leaveColor, _select_fntcolor, _fntcolor;
    private Point _bmpItemXY, _stringItemXY;
    private string _stringItem;
    private Font _fnt;
    private Color backcolor;

    public string Value
    {
        get { return _uInput.Value; }
    }
    
    private class dropItem
    {
        public Font item_font;
        public UiControlsMethod.PanelEx dropItemPanel = new UiControlsMethod.PanelEx();
        public string _string;
        public Bitmap _bmpItem;
        public Bitmap _select_bmpItem;
    }

    private List<dropItem> _dropItemList = new List<dropItem>();

    public void add_dropItemList(
        int _type,
        Size panelSize,
        Bitmap bmpItem, Point bmpItemXY,
        string stringItem, Point stringItemXY, Font fnt, Color fnt_color,
        Color _selectBkColor, Bitmap select_bmpItem, Color select_fntcolor
        )
    {
        switch(_type)
        {
            case 0:
                backcolor = Color.FromArgb(246, 246, 246);
                break;
            case 1:
                backcolor = Color.FromArgb(40, 45, 52);
                break;
            case 2:
                backcolor = Color.FromArgb(239, 238, 238);
                break;
            case 3:
                backcolor = Color.FromArgb(52, 73, 94);
                break;

            default:
                backcolor = Color.FromArgb(246, 246, 246);
                break;
        }
        _moveColor = _selectBkColor;
        _leaveColor = backcolor;
        _select_fntcolor = select_fntcolor;
        _bmpItemXY = bmpItemXY;
        _stringItem = stringItem;
        _stringItemXY = stringItemXY;
        _fnt = fnt;
        _fntcolor = fnt_color;

        UiControlsMethod.PanelEx _panel = new UiControlsMethod.PanelEx();
        UiControlsMethod.PictureBoxEx _itemImage = new UiControlsMethod.PictureBoxEx();
        _panel.Size = panelSize;
        _panel.BackColor = Color.Transparent;

        Bitmap bmp_item = new Bitmap(panelSize.Width, panelSize.Height);
        Graphics gItem = Graphics.FromImage(bmp_item);
        gItem.DrawImage(bmpItem, bmpItemXY);
        UiDrawTextMethod _ds = new UiDrawTextMethod();
        _ds.DrawString(bmp_item, stringItem, fnt, fnt_color, new Rectangle(stringItemXY, panelSize));
        _itemImage.BackColor = backcolor;
        _itemImage.Size = panelSize;
        _itemImage.Image = bmp_item;
        _itemImage.Location = new Point(0, 0);
        _itemImage.MouseMove += new MouseEventHandler(_ImgMouseMove);
        _itemImage.MouseLeave += new EventHandler(_ImgMouseLeave);
        _itemImage.Click += new EventHandler(_ImgClick);
        _panel.Controls.Add(_itemImage);

        _dropItemList.Add(new dropItem {
            item_font = fnt,
            dropItemPanel = _panel,
            _string = stringItem,
            _bmpItem = bmpItem,
            _select_bmpItem = select_bmpItem
        }
        );
    }

    private void _ImgClick(object sender, EventArgs e)
    {
        UiControlsMethod.PictureBoxEx Lg = (UiControlsMethod.PictureBoxEx)sender;
        _uInput.Value = _dropItemList[(int)Lg.Parent.Tag]._string;
        DropListHidden();
    }

    private void _ImgMouseMove(object sender, MouseEventArgs e)
    {
        UiControlsMethod.PictureBoxEx Lg = (UiControlsMethod.PictureBoxEx)sender;
        Lg.BackColor = _moveColor;

        Bitmap bmp_item = new Bitmap(Lg.Width, Lg.Height);
        Graphics gItem = Graphics.FromImage(bmp_item);
        gItem.DrawImage(_dropItemList[(int)Lg.Parent.Tag]._select_bmpItem, _bmpItemXY);
        UiDrawTextMethod _ds = new UiDrawTextMethod();
        _ds.DrawString(bmp_item, _dropItemList[(int)Lg.Parent.Tag]._string, _fnt, _select_fntcolor, new Rectangle(_stringItemXY, bmp_item.Size));

        Lg.Image = bmp_item;
        Lg.Update();
    }

    private void _ImgMouseLeave(object sender, EventArgs e)
    {
        UiControlsMethod.PictureBoxEx Lg = (UiControlsMethod.PictureBoxEx)sender;
        Lg.BackColor = _leaveColor;

        Bitmap bmp_item = new Bitmap(Lg.Width, Lg.Height);
        Graphics gItem = Graphics.FromImage(bmp_item);
        gItem.DrawImage(_dropItemList[(int)Lg.Parent.Tag]._bmpItem, _bmpItemXY);
        UiDrawTextMethod _ds = new UiDrawTextMethod();
        _ds.DrawString(bmp_item, _dropItemList[(int)Lg.Parent.Tag]._string, _fnt, _fntcolor, new Rectangle(_stringItemXY, bmp_item.Size));

        Lg.Image = bmp_item;
        Lg.Update();
    }

    public void InitializeDropList(Control _obj, int _type, int _width, Point _location, int _transparency, bool _readonly, Color _inputColor)
    {
        Obj = _obj;
        location = _location;
        transparency = _transparency;
        /*
         m_set[0] = drop left-width;      m_set[1] = drop right-width
         m_set[2] = list width-Adjust;    m_set[3] = list locationY-Adjust;
         m_set[4] = list add panel size offset
         m_set[5] = inputbox offset
         m_set[6] = listitem offsetY
         m_set[7] = form type
        */
        switch (_type)
        {
            case 0:
                bmp_drop = SQK_Ui.dropList.DropListResource.drop1;
                m_set = new int[8] { 10, 41, 3, -8, 12, 10, 0, 60 };
                dropClick = new Rectangle(m_set[0] + _width + 3, 5, m_set[1] - 9, bmp_drop.Height - 12);
                break;
            case 1:
                bmp_drop = SQK_Ui.dropList.DropListResource.drop2;
                m_set = new int[8] { 20, 37, 3, 0, 12, 5, 0, 61 };
                dropClick = new Rectangle(m_set[0] + _width + 3, 5, m_set[1] - 9, bmp_drop.Height - 12);
                break;
            case 2:
                bmp_drop = SQK_Ui.dropList.DropListResource.drop3;
                m_set = new int[8] { 20, 72, 0, -13, 20, 12, 0, 62 };
                dropClick = new Rectangle(m_set[0] + _width + 3, 5, m_set[1] - 9, bmp_drop.Height - 12);
                break;
            case 3:
                bmp_drop = SQK_Ui.dropList.DropListResource.drop4;
                m_set = new int[8] { 6, 46, 3, 3, 15, 8, 3, 63 };
                dropClick = new Rectangle(m_set[0] + _width + 3, 5, m_set[1] - 9, bmp_drop.Height - 12);
                break;

            default:
                bmp_drop = SQK_Ui.dropList.DropListResource.drop1;
                m_set = new int[8] { 10, 41, 3, -8, 12, 10, 0, 61 };
                dropClick = new Rectangle(m_set[0] + _width + 3, 5, m_set[1] - 9, bmp_drop.Height - 12);
                break;
        }

        Bitmap drop_left = bmp_drop.Clone(new Rectangle(0, 0, m_set[0], bmp_drop.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap drop_middle = bmp_drop.Clone(new Rectangle(m_set[0], 0, 1, bmp_drop.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap drop_right = bmp_drop.Clone(new Rectangle(bmp_drop.Width - m_set[1], 0, m_set[1], bmp_drop.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);


        UiControlsMethod.PanelEx pL = new UiControlsMethod.PanelEx();
        int list_height = 0;
        for (int i = 0; i < _dropItemList.Count; i++)
        {
            _dropItemList[i].dropItemPanel.Location = new Point(0, list_height);
            _dropItemList[i].dropItemPanel.Tag = i;

            pL.Controls.Add(_dropItemList[i].dropItemPanel);
            list_height += _dropItemList[i].dropItemPanel.Height;
        }
        pL.Size = new Size(m_set[0] + _width + m_set[1] + m_set[2] - m_set[4] * 2, list_height);
        pL.Location = new Point(m_set[4], m_set[4]+m_set[6]);

        bmp_list = new CutPixelAlphaImage().CutAlphaFormImage(m_set[7], m_set[0] + _width + m_set[1] + m_set[2], list_height + m_set[4] * 2); //form type , in CutPixelAlphaImage.cs
        listForm = new AlphaForm();
        listForm.窗体图像 = bmp_list;
        listForm.ShowInTaskbar = false;
        listForm.ShowIcon = false;
        listForm.FormBorderStyle = FormBorderStyle.None;
        listForm.StartPosition = FormStartPosition.Manual;
        listForm.Animation = AlphaForm.Animations.None;
        listForm.TopMost = true;
        listForm.Size = new Size(bmp_list.Width + m_set[1], bmp_list.Height);
        Point flist = Obj.PointToScreen(new Point(location.X, location.Y));
        listForm.Location = new Point(flist.X, flist.Y + bmp_drop.Height + m_set[3]);
        listForm.SetOpacity(0);
        listForm.Controls.Add(pL);
        listForm.Show();

        Control mainObj = Obj;
        while (true)
        {
            if (mainObj is Form) break;
            else mainObj = mainObj.Parent;
        }

        Bitmap dropImage = new Bitmap(m_set[0] + _width + m_set[1], bmp_drop.Height);
        Graphics gdrop = Graphics.FromImage(dropImage);
        gdrop.DrawImage(drop_left, 0, 0, drop_left.Width, drop_left.Height);
        for (int i = 0; i < _width; i++)
        {
            gdrop.DrawImage(drop_middle, m_set[0] + i, 0, drop_middle.Width, drop_middle.Height);
        }
        gdrop.DrawImage(drop_right, m_set[0] + _width, 0, drop_right.Width, drop_right.Height);

        _drop.BackColor = Color.Transparent;
        _drop.Size = new Size(dropImage.Width + m_set[2], dropImage.Height);
        _drop.Location = _location;
        _drop.Image = dropImage;
        _drop.MouseMove += new MouseEventHandler(_dropMouseMove);
        _drop.Click += new EventHandler(_dropClick);

        _uInput.inputBox(_drop, new Size(_width, bmp_drop.Height - m_set[5] * 2), new Point(m_set[0], m_set[5]), _fnt, _inputColor, null, 0, true, 120, Color.White, 1, 1, _readonly, _InputClick);

        mainObj.LocationChanged += new EventHandler(Obj_LocationChanged);
        _obj.Controls.Add(_drop);
    }

    private void _InputClick(object sender, MouseEventArgs e)
    {
        if (_showState) DropListHidden();
    }

    private void _dropMouseMove(object sender, MouseEventArgs e)
    {
        if ((e.X>= dropClick.X) && (e.X<= dropClick.X+ dropClick.Width) && (e.Y>= dropClick.Y) && (e.Y<= dropClick.Y+dropClick.Height))
        {
            _drop.Cursor = Cursors.Hand;
            DropClickArea = true;
        } else
        {
            _drop.Cursor = Cursors.Default;
            DropClickArea = false;
        }
    }

    private void _dropClick (object sender, EventArgs e)
    {
        if (DropClickArea)
        {
            if (!_showState)
            {
                Point flist = Obj.PointToScreen(new Point(location.X, location.Y));
                listForm.Location = new Point(flist.X, flist.Y + bmp_drop.Height + m_set[3]);
                listForm.SetOpacity(transparency*255);
                _showState = true;
            } else
            {
                DropListHidden();
            }
        } else
        {
            DropListHidden();
        }
    }

    private void DropListHidden ()
    {
        listForm.SetOpacity(0);
        _showState = false;
    }

    private void Obj_LocationChanged(object sender, EventArgs e)
    {
        Point flist = Obj.PointToScreen(new Point(location.X, location.Y));
        listForm.Location = new Point(flist.X, flist.Y + bmp_drop.Height + m_set[3]);
    }

    public void closeList ()
    {
        try { listForm.Close(); } 
        catch { }
    }
}

