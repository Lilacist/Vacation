/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System.Drawing;
using System.Windows.Forms;

public class UiProgressMethods
{
    public int _width = 20;
    public int _maxNum = 20;
    public string progressValue ="0";

    private int progressType = 0;
    private bool showFlag = false;
    private int flagType = 0, _operationWidth;
    private UiControlsMethod.PictureBoxEx _progress = new UiControlsMethod.PictureBoxEx();
    private UiControlsMethod.PictureBoxEx _progressFlag = new UiControlsMethod.PictureBoxEx();
    private Bitmap _bmpFlag, bmp_progress, gress_Left, gress_Mid, gress_Right;
    private Font fnt = new Font("微软雅黑", 7,FontStyle.Regular);
    private UiDrawTextMethod ud = new UiDrawTextMethod();
    private int[] m_set, m_flag;
    private Color txt_Color;

    public void InitializeProggress(Control _obj, int _type, Point _location, int _pwidth)
    {
        InitializeProggress(_obj, _type, _location, _pwidth, false, 0);
    }

    public void InitializeProggress (Control _obj, int _type, Point _location, int _pwidth,bool _showFgla, int _flagType)
    {
        progressType = _type;
        _width = _pwidth;
        showFlag = _showFgla;
        flagType = _flagType;
        Bitmap bmp_bar, bmp_gress;

        /*
         m_set[0] = bar left cut image width;    m_set[1] = bar middle cut image width;    m_set[2] = bar right cut image width;
         m_set[3] = gress left cut image width; m_set[4] = gress middle cut image width; m_set[5] = gress right cut image width;
         m_set[6] = gress left-distance to bar;  m_set[7] = gress right-distance to bar;
         m_set[8] = gress top-distance to bar;
        */
        switch (_type)
        {
            case 0:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar1;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress1;
                m_set = new int[9] { 20, 1, 20, 10, 1, 10, 7, 7, 7 };
                break;
            case 1:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar1;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress2;
                m_set = new int[9] { 20, 1, 20, 10, 1, 10, 7, 7, 7 };
                break;
            case 2:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar3;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress3;
                m_set = new int[9] { 20, 1, 20, 15, 1, 15, 7, 7, 4 };
                break;
            case 3:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar4;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress4;
                m_set = new int[9] { 3, 1, 3,   3, 1, 3,   0, 0,  0 };
                break;
            case 4:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar5;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress5;
                m_set = new int[9] { 8, 1, 8, 4, 1, 4, 4, 4, 6 };
                break;
            case 5:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar5;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress6;
                m_set = new int[9] { 8, 1, 8, 4, 1, 4, 4, 4, 6 };
                break;
            case 6:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar5;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress7;
                m_set = new int[9] { 8, 1, 8, 4, 1, 4, 4, 4, 6 };
                break;
            case 7:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar8;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress8;
                m_set = new int[9] { 3, 1, 3, 3, 1, 3, 0, 0, 0 };
                break;
            case 8:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar9;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress9;
                m_set = new int[9] { 3, 1, 3, 3, 1, 3, 1, 1, 1 };
                break;
            case 9:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar10;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress10;
                m_set = new int[9] { 1, 1, 1, 2, 1, 2, 0, 0, 0 };
                break;
            case 10:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar11;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress11;
                m_set = new int[9] { 5, 1, 5, 5, 1, 5, 0, 0, 0 };
                break;
            case 11:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar12;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress12;
                m_set = new int[9] { 10, 1, 10, 5, 1, 5, 4, 4, 4 };
                break;
            case 12:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar13;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress13;
                m_set = new int[9] { 5, 1, 5, 2, 1, 2, 2, 2, 2 };
                break;

            default:
                bmp_bar = SQK_Ui.Progress.ProgressResource.pbar1;
                bmp_gress = SQK_Ui.Progress.ProgressResource.progress1;
                m_set = new int[9] { 20,1,20,  10,1,10,  7,7,   7 };
                break;
        }

        Bitmap bar_Left = bmp_bar.Clone(new Rectangle(0, 0, m_set[0], bmp_bar.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        Bitmap bar_Mid = bmp_bar.Clone(new Rectangle(m_set[0], 0, 1, bmp_bar.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        Bitmap bar_Right = bmp_bar.Clone(new Rectangle(bmp_bar.Width - m_set[2], 0, m_set[2], bmp_bar.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        gress_Left = bmp_gress.Clone(new Rectangle(0,0,m_set[3], bmp_gress.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        gress_Mid = bmp_gress.Clone(new Rectangle(m_set[3], 0, 1, bmp_gress.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        gress_Right = bmp_gress.Clone(new Rectangle(bmp_gress.Width-m_set[5], 0, m_set[5], bmp_gress.Height), System.Drawing.Imaging.PixelFormat.DontCare);

        bmp_progress = new Bitmap(_pwidth, bmp_bar.Height);
        Graphics bitmapProgress = Graphics.FromImage(bmp_progress);
        bitmapProgress.DrawImage(bar_Left, 0, 0, m_set[0], bmp_bar.Height);
        for (int i = 0; i < _pwidth - m_set[0] - m_set[2]; i++)
        {
            bitmapProgress.DrawImage(bar_Mid, m_set[0] + i, 0, 1, bmp_bar.Height);
        }
        bitmapProgress.DrawImage(bar_Right, _pwidth- m_set[2], 0, m_set[2], bmp_bar.Height);

        _progress.BackColor = Color.Transparent;
        _progress.Size = new Size(_pwidth, bmp_bar.Height);
        _progress.Location = _location;
        _progress.Image = bmp_progress;

        _operationWidth = _pwidth - m_set[6] - m_set[7] - m_set[3] - m_set[5];
        
        if (_showFgla)
        {
            /*
                m_flag[0] = flag Height; m_flag[1] = text location x; m_flag[2] = text location y;
            */
            switch (_flagType)
            {
                case 0:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_1;
                    m_flag = new int[3] { 28,6,3 };
                    txt_Color = Color.Black;
                    break;
                case 1:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_2;
                    m_flag = new int[3] { 28, 6, 3 };
                    txt_Color = Color.Black;
                    break;
                case 2:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_3;
                    m_flag = new int[3] { 26, 4, 3 };
                    txt_Color = Color.White;
                    break;
                case 3:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_4;
                    m_flag = new int[3] { 31, 6, 5 };
                    txt_Color = Color.Black;
                    break;
                case 4:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_12;
                    m_flag = new int[3] { 36, 7, 7 };
                    txt_Color = Color.Black;
                    break;

                default:
                    _bmpFlag = SQK_Ui.Tooltips.TipsResource.tips_1;
                    m_flag = new int[4] { 28, 0, 6, 3 };
                    txt_Color = Color.Black;
                    break;
            }
            _progressFlag.Size = new Size(_pwidth + _bmpFlag.Width*2, m_flag[0]);
            _progressFlag.BackColor = Color.Transparent;
            _progressFlag.Location = new Point(_location.X- _bmpFlag.Width, _location.Y- m_flag[0]);
            //_obj.Controls.Add(_progressFlag);
        }
        _setProgress(0);
        _obj.Controls.Add(_progress);
    }


    public void _setProgress(int _skip)
    {
        
        if (_skip == _maxNum)
        {
            _skip = _operationWidth;
        }
        else
        {
            var t = (decimal)_operationWidth / _maxNum;
            _skip = (int)(_skip * t);
        }
      

        Bitmap _progressImage = bmp_progress.Clone(new Rectangle(0, 0, bmp_progress.Width, bmp_progress.Height), System.Drawing.Imaging.PixelFormat.DontCare);
        Graphics _gblock = Graphics.FromImage(_progressImage);

        _gblock.DrawImage(gress_Left, m_set[6], m_set[8], gress_Left.Width, gress_Left.Height);
        for (int i = 0; i < _skip; i++)
        {
            _gblock.DrawImage(gress_Mid, m_set[6] + m_set[3] + i, m_set[8], 1, gress_Mid.Height);
        }
        _gblock.DrawImage(gress_Right, m_set[6] + m_set[3] + _skip, m_set[8], m_set[5], gress_Right.Height);

        _progress.Image = _progressImage;
        _progress.Update();
        
        if (showFlag)
        {
            Bitmap bmp_flag = new Bitmap(_progressFlag.Width, _progressFlag.Height);
            Graphics gFlag = Graphics.FromImage(bmp_flag);
            gFlag.DrawImage(_bmpFlag, _skip  + (_bmpFlag.Width/2) +m_set[3] + m_set[5], 0, _bmpFlag.Width, _bmpFlag.Height);
            progressValue = (_skip / (float)_operationWidth * 100).ToString("#0");
            int str_x = m_flag[1];
            str_x = str_x - (progressValue.Length - 1) * 3;
            ud.DrawString(
                bmp_flag,
                progressValue + "%",
                fnt,
                txt_Color,
                new Rectangle(_skip + (_bmpFlag.Width / 2) + m_set[3] + m_set[5] + m_flag[1]+str_x, m_flag[2], 30, 20)
            );
            _progressFlag.Image = bmp_flag;
            _progressFlag.Update();
        }
    }
}

