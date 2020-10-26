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
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class UiCalendarMethods
{
    public class dayList
    {
        public int day;
        public Color dcolor;
        public bool today;
        public string dayStr;
        public Rectangle mouseRect;
    }

    private List<dayList> _dayList = new List<dayList>();

    private void addDay(int _day, Color _dcolor, bool _today, string _dayStr, Rectangle _rect)
    {
        _dayList.Add(new dayList
        {
            day = _day,
            dcolor = _dcolor,
            today = _today,
            dayStr = _dayStr,
            mouseRect = _rect
        });
    }

    private int dy, dm, dd;
    private Bitmap _bmp = new Bitmap(245, 200);
    private int mouse_Flag;
    private int timeType, dispType;
    private string setTime;
    private Point location;
    private double transparency;
    private bool _show = false;
    private int[,] m_set;

    AlphaForm calenForm;
    Bitmap bkImg;
    UiControlsMethod.PictureBoxEx pb = new UiControlsMethod.PictureBoxEx();
    Control obj;

    public void InitializeCalendar (Control _obj, int _timeType, string _setTime, int _dispType, Point _location, double _transparency)
    {
        _dayList.Clear();
        obj = _obj;
        timeType = _timeType;
        setTime = _setTime;
        dispType = _dispType;
        location = _location;
        transparency = _transparency;

        int str_X = 0, str_Y = 0, skip_X = 0, skip_Y = 0;
        int head_X = 0, head_Y = 0;
        Size ImgSize;
        int cut_X = 0, cut_Y = 0;
        UiDrawTextMethod ud = new UiDrawTextMethod();
        Font fnt;

        if (_timeType == 1)
        {
            try
            {
                string[] splitString = _setTime.Split(new string[] { "-" }, StringSplitOptions.None);
                dy = int.Parse(splitString[0]);
                dm = int.Parse(splitString[1]);
            }
            catch { }
        }
        else
        {
            dy = DateTime.Today.Year;
            dm = DateTime.Today.Month;
            dd = DateTime.Today.Day;
        }
        
        //year before last
        int qdy = dy;
        int qdm;
        if (dm == 1)
        {
            qdm = 12;
            qdy = dy - 1;
        }
        else qdm = dm - 1;
        int q_mday = DateTime.DaysInMonth(qdy, qdm);

        //Next year
        int ndy = dy + 1;
        int ndm;
        if ((dm + 1) == 13) ndm = 1;
        else ndm = dm + 1;

        // now
        int t_mday = DateTime.DaysInMonth(dy, dm); 
        DateTime tmDate = new DateTime(dy, dm, 1); 
        int t_week = (int)tmDate.DayOfWeek;
        if (t_week == 0) t_week = 7;

        string str_YMD;
        for (int i = 0; i < t_week - 1; i++)
        {
            if (dm == 1) str_YMD = (dy - 1) + "-12-" + (q_mday - t_week + i + 2);
            else str_YMD = dy + "-" + (dm - 1) + "-" + (q_mday - t_week + i + 2);
            addDay(q_mday - t_week + i + 2, Color.Gainsboro, false, str_YMD, new Rectangle(0,0,0,0));
        }

        for (int i = 0; i < t_mday; i++)
        {
            str_YMD = dy + "-" + dm + "-" + (i + 1);
            if (DateTime.Today.ToString("yyyy-M-d")==str_YMD)
            {
                addDay(i + 1, Color.Crimson, false, str_YMD, new Rectangle(0, 0, 0, 0));
            } else  addDay(i + 1, Color.Black, false, str_YMD, new Rectangle(0, 0, 0, 0));
        }

        for (int i = 0; i < (42 - t_week - t_mday) + 1; i++)
        {
            if (dm == 12) str_YMD = (dy + 1) + "-1-" + (i + 1);
            else str_YMD = dy + "-" + (dm + 1) + "-" + (i + 1);
            addDay(i + 1, Color.Gainsboro, false, str_YMD, new Rectangle(0, 0, 0, 0));
        }

        bkImg = new Bitmap(235,185);
        switch (_dispType)
        {
            case 0:
                _bmp = SQK_Ui.Calendar.calendarResource.cal_1;
                bkImg = _bmp.Clone(new Rectangle(5, 5, 235, 185), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                str_X = 15;
                str_Y = 62;
                skip_X = 33;
                skip_Y = 21;
                head_X = 80;
                head_Y = 7;
                ImgSize = new Size(245, 200);
                cut_X = 5;
                cut_Y = 5;
                fnt = new Font("微软雅黑", 10, FontStyle.Bold);
                m_set = new int[4,4]{  { 5,21,11,21}, { 28,38,11,21}, { 194,204,11,21}, {213,228,11,21 } };
                break;
            case 1:
                _bmp = SQK_Ui.Calendar.calendarResource.cal_2;
                bkImg = _bmp.Clone(new Rectangle(5, 5, 235, 185), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                str_X = 15;
                str_Y = 62;
                skip_X = 33;
                skip_Y = 21;
                head_X = 80;
                head_Y = 7;
                ImgSize = new Size(245, 200);
                cut_X = 5;
                cut_Y = 5;
                fnt = new Font("微软雅黑", 10, FontStyle.Bold);
                m_set = new int[4, 4]{ { 5,21,11,21}, { 28,38,11,21}, { 194,204,11,21}, {213,228,11,21 } };
                break;
            case 2:
                _bmp = SQK_Ui.Calendar.calendarResource.cal_3;
                bkImg = _bmp.Clone(new Rectangle(8, 8, 272, 284), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                str_X = 9;
                str_Y = 58;
                skip_X = 40;
                skip_Y = 40;
                head_X = 95;
                head_Y = 5;
                ImgSize = new Size(288, 300);
                cut_X = 8;
                cut_Y = 8;
                fnt = new Font("微软雅黑", 14, FontStyle.Bold);
                m_set = new int[4, 4]{ { 8,24,5,21}, { 48,58,5,21}, { 211,221,5,21}, {246,261,5,21 } };
                break;

            default:
                _bmp = SQK_Ui.Calendar.calendarResource.cal_1;
                bkImg = _bmp.Clone(new Rectangle(5, 5, 235, 185), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                str_X = 15;
                str_Y = 62;
                skip_X = 33;
                skip_Y = 21;
                head_X = 80;
                head_Y = 7;
                ImgSize = new Size(245, 200);
                cut_X = 5;
                cut_Y = 5;
                fnt = new Font("微软雅黑", 10, FontStyle.Bold);
                break;
        }
        ud.DrawString(bkImg, dy + "年" + dm + "月", fnt, Color.White, new Rectangle(head_X, head_Y, 120, 20));
        for (int i = 0; i < 6; i++)
        {
            for (int u = 0; u < 7; u++)
            {
                if (_dayList[i * 7 + u].day < 10)
                {
                    ud.DrawString(bkImg, _dayList[i * 7 + u].day.ToString(), fnt, _dayList[i * 7 + u].dcolor, new Rectangle(str_X + u * skip_X, str_Y + i * skip_Y, 25, 20));
                    _dayList[i * 7 + u].mouseRect = new Rectangle(str_X-5 + u * skip_X, str_Y + i * skip_Y, 22, 18);
                }
                else
                {
                    ud.DrawString(bkImg, _dayList[i * 7 + u].day.ToString(), fnt, _dayList[i * 7 + u].dcolor, new Rectangle(str_X-5 + u * skip_X, str_Y + i * skip_Y, 25, 20));
                    _dayList[i * 7 + u].mouseRect = new Rectangle(str_X-5 + u * skip_X, str_Y + i * skip_Y, 22, 18);
                }
            }
        }
        if (!_show)
        {
            calenForm = new AlphaForm();
            calenForm.ShowInTaskbar = false;
            calenForm.ShowIcon = false;
            calenForm.FormBorderStyle = FormBorderStyle.None;
            calenForm.StartPosition = FormStartPosition.Manual;
            calenForm.Animation = AlphaForm.Animations.None;
            calenForm.Opacity = _transparency;
            calenForm.TopMost = true;
            calenForm.Size = new Size(_bmp.Width, _bmp.Height);
            calenForm.Location = _location;
            calenForm.窗体图像 = _bmp;

            pb.Size = ImgSize;
            pb.Location = new Point(cut_X, cut_Y);
            pb.Image = bkImg;
            pb.MouseMove += new MouseEventHandler(_MouseMove);
            pb.MouseClick += new MouseEventHandler(_MouseClick);
            pb.MouseDown += new MouseEventHandler(_MouseDown);
            calenForm.Controls.Add(pb);

            calenForm.Show();
            _show = true;
        } else
        {
            pb.Image = bkImg;
            calenForm.SetOpacity(_transparency * 255);
        }
    }

    private void _MouseMove (object sender, MouseEventArgs e)
    {
        mouse_Flag = -1;
        for (int i=0;i<4;i++)
        {
            if (e.X >= m_set[i, 0] && e.X <= m_set[i, 1] && e.Y >= m_set[i, 2] && e.Y <= m_set[i, 3])
            {
                pb.Cursor = Cursors.Hand;
                mouse_Flag = 96+i;
                break;
            }
        }
        for (int i=0;i<42;i++)
        {
            if (e.X >= _dayList[i].mouseRect.X && e.X <= _dayList[i].mouseRect.X+22 && e.Y >= _dayList[i].mouseRect.Y && e.Y <= _dayList[i].mouseRect.Y+18)
            {
                pb.Cursor = Cursors.Hand;
                mouse_Flag = i;
                break;
            }
        }
        if (mouse_Flag==-1)
        {
            pb.Cursor = Cursors.Default;
            mouse_Flag = -1;
        }
    }

    private void _MouseClick(object sender, MouseEventArgs e)
    {
        if (mouse_Flag!=-1)
        {
            if (mouse_Flag >= 0 && mouse_Flag < 42)
            {
                obj.Text = _dayList[mouse_Flag].dayStr;
                calenFormClose();
            }
            else
            {
                if (mouse_Flag == 96)
                {
                    dy = dy - 1;
                }
                else
                if (mouse_Flag == 97)
                {
                    if (dm == 1)
                    {
                        dy = dy - 1;
                        dm = 12;
                    }
                    else dm = dm - 1;
                }
                else
                if (mouse_Flag == 98)
                {
                    if (dm == 12)
                    {
                        dy = dy + 1;
                        dm = 1;
                    }
                    else dm = dm + 1;
                }
                else
                if (mouse_Flag == 99)
                {
                    dy = dy + 1;
                }
                InitializeCalendar(obj, 1, dy + "-" + dm, dispType, location, transparency);
            }
        }
    }

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    private void _MouseDown(object sender, MouseEventArgs e)
    {
        if (mouse_Flag == -1)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(calenForm.Handle, 0xA1, 0x02, 0);
            }
        }
    }

    public void calenFormClose ()
    {
        calenForm.SetOpacity(0);
    }
}

