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

public class UiPageCodeMethod
{
    private int count;
    private int pading;
    private int fclick_count = 10;
    private int[] fclick_value = new int[14] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    private int pageRecord = 0;
    private int pageCount;
    private int pageing;
    private Font fontcode;
    private int fpagebtn = -1;
    private UiControlsMethod.PanelEx codePanel;
    private UiControlsMethod.PanelEx recPanel;
    private Control obj;
    private Control[] record;
    private Size size;
    private Point location;
    private Bitmap pag ,paged, pagdot;

    public void pageCode( Control _obj, Point _location, Size _size, Control[] _record, int _count, int _pading, int _pageing, Point _locationcode, Font _fontcode, int _type )
    {
        obj = _obj;
        record = _record;
        size = _size;
        location = _location;
        pageing = _pageing;
        count = _count;
        pading = _pading;
        fontcode = _fontcode;
        switch(_type)
        {
            case 0:
                pag = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_pag.Width, SQK_Ui.pageCode.pageCodeResource.type1_pag.Height);
                paged = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_paged.Width, SQK_Ui.pageCode.pageCodeResource.type1_paged.Height);
                pagdot = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_pagdot.Width, SQK_Ui.pageCode.pageCodeResource.type1_pagdot.Height);
                pag = SQK_Ui.pageCode.pageCodeResource.type1_pag;
                paged = SQK_Ui.pageCode.pageCodeResource.type1_paged;
                pagdot = SQK_Ui.pageCode.pageCodeResource.type1_pagdot;
                break;
            case 1:
                pag = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type2_pag.Width, SQK_Ui.pageCode.pageCodeResource.type2_pag.Height);
                paged = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type2_paged.Width, SQK_Ui.pageCode.pageCodeResource.type2_paged.Height);
                pagdot = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type2_pagdot.Width, SQK_Ui.pageCode.pageCodeResource.type2_pagdot.Height);
                pag = SQK_Ui.pageCode.pageCodeResource.type2_pag;
                paged = SQK_Ui.pageCode.pageCodeResource.type2_paged;
                pagdot = SQK_Ui.pageCode.pageCodeResource.type2_pagdot;
                break;
            default:
                pag = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_pag.Width, SQK_Ui.pageCode.pageCodeResource.type1_pag.Height);
                paged = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_paged.Width, SQK_Ui.pageCode.pageCodeResource.type1_paged.Height);
                pagdot = new Bitmap(SQK_Ui.pageCode.pageCodeResource.type1_pagdot.Width, SQK_Ui.pageCode.pageCodeResource.type1_pagdot.Height);
                pag = SQK_Ui.pageCode.pageCodeResource.type1_pag;
                paged = SQK_Ui.pageCode.pageCodeResource.type1_paged;
                pagdot = SQK_Ui.pageCode.pageCodeResource.type1_pagdot;
                break;
        }
        recPanel = new UiControlsMethod.PanelEx();
        recPanel.BackColor = Color.Transparent;
        recPanel.Size = size;
        recPanel.Location = location;
        showList();
        codePanel = new UiControlsMethod.PanelEx();
        codePanel.BackColor = Color.Transparent;
        codePanel.Size = new Size(pag.Width * 14 + 5 * 14, pag.Height + 5);
        codePanel.Location = _locationcode;
        codePanel.BackgroundImage = showPage();
        codePanel.MouseMove += new MouseEventHandler(codePanel_MouseMove);
        codePanel.Click += new EventHandler(codePanel_Click);
        _obj.Controls.Add(codePanel);
    }

    private int GetPageCount(int total, int paging)
    {
        int _page = total / paging;
        if (_page < ((double)total / (double)paging)) _page += 1;
        return _page;
    }

    private void showList ()
    {
        int pg_begin = (pageing - 1) * pading;
        int pg_end = pg_begin + pading;
        if (pg_end > count) pg_end = count;
        pageRecord = pg_end - pg_begin;
        UiControlsMethod.PanelEx tmpPanel = new UiControlsMethod.PanelEx();
        tmpPanel.BackColor = Color.Transparent;
        tmpPanel.Size = recPanel.Size;
        tmpPanel.Dock = DockStyle.Fill;
        for (int i = pg_begin; i < pg_end; i++)
        {
            record[i].Location = new Point(0, (i- pg_begin) * record[i].Height);
            tmpPanel.Controls.Add(record[i]);
        }
        recPanel.Controls.Clear();
        recPanel.Controls.Add(tmpPanel);
        obj.Controls.Add(recPanel);
    }

    private Bitmap showPage()
    {
        pageCount = GetPageCount(count, pading);
        for (int i = 0; i < 14; i++) fclick_value[i] = -1;
        int p_begin = 0, p_end = 10;
        bool view_first, view_last;
        if (pageCount <= 10)
        {
            p_begin = 0;
            p_end = pageCount;
            view_first = false;
            view_last = false;
            fclick_count = pageCount;
        }
        else
        {
            view_first = true;
            view_last = true;
            p_begin = (pageing - 1) - 5;
            if (p_begin <= 0)
            {
                p_begin = 0;
                p_end = 10;
                view_first = false;
                view_last = true;
            }
            else
            {
                p_end = p_begin + 10;
                if (p_end >= pageCount)
                {
                    p_begin = pageCount - 10;
                    p_end = pageCount;
                    view_first = true;
                    view_last = false;
                }
                else
                {
                    p_end = p_begin + 10;
                    view_first = true;
                    view_last = true;
                }
            }
        }
        Bitmap _bmp = new Bitmap(pag.Width*14+5*14, pag.Height+5);
        Graphics file_bitmapGraphics = Graphics.FromImage(_bmp);
        file_bitmapGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        file_bitmapGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        int set_x = 0;
        int width = 0;
        int begin_press;
        if (view_first) begin_press = 2;
        else begin_press = 0;
        for (int i = p_begin; i < p_end; i++)
        {
            if (view_first)
            {
                set_x = pag.Width*2+15 + (width * (pag.Width + 5));
            }
            else
            {
                set_x = 5 + (width * (pag.Width + 5));
            }
            if ((i + 1) == pageing) file_bitmapGraphics.DrawImage(paged, set_x, 2, paged.Width, paged.Height);
            else file_bitmapGraphics.DrawImage(pag, set_x, 2, paged.Width, paged.Height);
            int wleft = 10;
            if ((i + 1) > 9) wleft = 5;
            if ((i + 1) > 99) wleft = 0;
            file_bitmapGraphics.DrawString((i + 1).ToString(), fontcode, Brushes.Black, wleft + set_x, 9);
            fclick_value[begin_press] = (i + 1);
            width += 1;
            begin_press += 1;
        }
        if (view_first)
        {
            fclick_count += 2;
            file_bitmapGraphics.DrawImage(pag, 5, 2, pag.Width, pag.Height);
            file_bitmapGraphics.DrawString("1", fontcode, Brushes.Black, 15, 9);
            file_bitmapGraphics.DrawImage(pagdot, pagdot.Width+10, 2, pagdot.Width, pagdot.Height);
            fclick_value[0] = 1;
        }
        if (view_last)
        {
            fclick_count += 2;
            int end_x = 11;
            if (view_first) end_x = 13;
            file_bitmapGraphics.DrawImage(pagdot, (end_x - 1) * (pagdot.Width+5) + 5, 2, pagdot.Width, pagdot.Height);
            file_bitmapGraphics.DrawImage(pag, end_x * (pag.Width + 5) + 5, 2, pag.Width, pag.Height);
            int wright = 10;
            if (pageCount > 99) wright = 5;
            file_bitmapGraphics.DrawString(pageCount.ToString(), fontcode, Brushes.Black, end_x * (pag.Width+5) + wright, 9);
            fclick_value[begin_press + 1] = pageCount;
        }
        return _bmp;
    }

    public void codePanel_MouseMove(object sender, MouseEventArgs e)
    {
        UiControlsMethod.PanelEx pL = (UiControlsMethod.PanelEx)sender;
        bool select = false;
        Point p = pL.PointToClient(Control.MousePosition);
        if ((p.Y >= 2) && (p.Y <= (pag.Height+2)))
        {
            for (int i = 0; i < fclick_count; i++)
            {
                if ((p.X >= 5 + i * (pag.Width+5)) && (p.X <= (pag.Width + 5) + i * (pag.Width + 5)))
                {
                    if (fclick_value[i] != -1)
                    {
                        fpagebtn = fclick_value[i];
                        select = true;
                    }
                    break;
                }
            }
        }
        if (select) pL.Cursor = Cursors.Hand;
        else
        {
            pL.Cursor = Cursors.Default;
            fpagebtn = -1;
        }
    }

    public void codePanel_Click(object sender, EventArgs e)
    {
        UiControlsMethod.PanelEx pL = (UiControlsMethod.PanelEx)sender;
        if (fpagebtn != -1)
        {
            pageing = fpagebtn;
            pL.BackgroundImage = showPage();
            showList();
        }
        
    }
}

