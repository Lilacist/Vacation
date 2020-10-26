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

public class UiStyleButtonMethods
{
    public void InitializeStyleButton (
        Control _obj, 
        int _type, int _width, 
        Bitmap _bmp, Point _bmplocation,
        Font fnt, string _string, Color _stringcolor, Rectangle _stringrec, 
        Point _location, MouseEventHandler _click
        )
    {
        Bitmap _BmpBlock;
        int[] cutImg; //cut image left,middle, right width;

        switch (_type)
        {
            case 0:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn1;
                cutImg = new int[3] { 40,1,10 };
                break;
            case 1:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn2;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 2:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn3;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 3:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn4;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 4:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn5;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 5:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn6;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 6:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn7;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 7:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn8;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 8:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn9;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 9:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn10;
                cutImg = new int[3] { 40, 1, 10 };
                break;
            case 10:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn11;
                cutImg = new int[3] { 10, 1, 10 };
                break;
            case 11:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn12;
                cutImg = new int[3] { 10, 1, 10 };
                break;
            case 12:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn13;
                cutImg = new int[3] { 3, 1, 3 };
                break;
            case 13:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn14;
                cutImg = new int[3] { 3, 1, 3 };
                break;
            case 14:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn15;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 15:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn16;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 16:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn17;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 17:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn18;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 18:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn19;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 19:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn20;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 20:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn21;
                cutImg = new int[3] { 45, 1, 12 };
                break;
            case 21:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn22;
                cutImg = new int[3] { 45, 1, 12 };
                break;

            default:
                _BmpBlock = SQK_Ui.StyleButton.StyleButtonResource.s_btn1;
                cutImg = new int[3] { 40, 1, 10 };
                break;
        }
        Bitmap sep_Left = _BmpBlock.Clone(new Rectangle(0, 0, cutImg[0], _BmpBlock.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap sep_Middle = _BmpBlock.Clone(new Rectangle(cutImg[0], 0, cutImg[1], _BmpBlock.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap sep_Right = _BmpBlock.Clone(new Rectangle(_BmpBlock.Width-cutImg[2],0, cutImg[2],_BmpBlock.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        Bitmap bmpBtn = new Bitmap(cutImg[0] + _width + cutImg[2], _BmpBlock.Height);
        Graphics btnImg = Graphics.FromImage(bmpBtn);
        btnImg.DrawImage(sep_Left, 0, 0);
        for (int i = 0; i < _width; i++)
        {
            btnImg.DrawImage(sep_Middle, cutImg[0] + i, 0);
        }
        btnImg.DrawImage(sep_Right, cutImg[0]+_width, 0);

        btnImg.DrawImage(_bmp, _bmplocation);
        UiDrawTextMethod ds = new UiDrawTextMethod();
        ds.DrawString(bmpBtn,_string,fnt,_stringcolor,_stringrec);

        UiControlsMethod.PictureBoxEx sbtn = new UiControlsMethod.PictureBoxEx();
        sbtn.BackColor = Color.Transparent;
        sbtn.Size = new Size(cutImg[0]+_width+cutImg[2],_BmpBlock.Height);
        sbtn.Location = _location;
        sbtn.Image = bmpBtn;
        sbtn.MouseClick += _click;
        _obj.Controls.Add(sbtn);

    }
}
