/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System.Drawing;

public class CutPixelAlphaImage
{
    public Bitmap CutAlphaFormImage(int _style, int _width, int _height)
    {
        int select_wh = 25;
        Bitmap _bmp = SQK_Ui.AlphaForm.frmResource.Res;
        switch (_style)
        {
            case 1:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res;
                select_wh = 25;
                break;
            case 2:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res1;
                select_wh = 25;
                break;
            case 3:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res2;
                select_wh = 20;
                break;
            case 4:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res3;
                select_wh = 45;
                break;
            case 5:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res4;
                select_wh = 40;
                break;
            case 6:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res5;
                select_wh = 59;
                break;
            case 7:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res6;
                select_wh = 96;
                break;
            case 8:
                _bmp = SQK_Ui.AlphaForm.frmResource.Res7;
                select_wh = 65;
                break;

            case 60: /*droplist 60*/
                _bmp = SQK_Ui.dropList.DropListResource.dropdown1;
                select_wh = 10;
                break;
            case 61: 
                _bmp = SQK_Ui.dropList.DropListResource.dropdown2;
                select_wh = 26;
                break;
            case 62:
                _bmp = SQK_Ui.dropList.DropListResource.dropdown3;
                select_wh = 26;
                break;
            case 63:
                _bmp = SQK_Ui.dropList.DropListResource.dropdown4;
                select_wh = 32;
                break;

            /*tips 100-120 tips-structure : All image*/
            case 100:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_5;
                select_wh = 25;
                break;

            /*tips 121-140 tips-structure : left or right ,single line*/
            case 121:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_6;
                select_wh = 35;
                break;
            case 122:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_7;
                select_wh = 35;
                break;
            case 123:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_8;
                select_wh = 45;
                break;
            case 124:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_13;
                select_wh = 35;
                break;
            case 125:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_14;
                select_wh = 35;
                break;

            /*tips 141-160 tips-structure : top or bottom ,middle */
            case 141:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_9;
                select_wh = 20;
                break;
            case 142:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_10;
                select_wh = 20;
                break;
            case 143:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_15;
                select_wh = 15;
                break;
            case 144:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_16;
                select_wh = 15;
                break;
            case 145:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_3;
                select_wh = 6;
                break;

            /*tips 161-180 tips-structure : left or right ,middle */
            case 161:
                _bmp = SQK_Ui.Tooltips.TipsResource.tips_11;
                select_wh = 30;
                break;


            default:
                break;

        }

        Bitmap bmp_form = new Bitmap(_width, _height);
        Graphics bmp_block = Graphics.FromImage(bmp_form);

        /*tips 121-140 tips-structure : left or right ,single line*/
        if ((_style == 121) || (_style == 122) || (_style == 123) || (_style == 124) || (_style == 125))
        {
            Bitmap sep_Left = _bmp.Clone(new Rectangle(0, 0, select_wh, _bmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_Right = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, 0, select_wh, _bmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_TopFull = _bmp.Clone(new Rectangle(select_wh, 0, select_wh, _bmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            int ta = (_width - select_wh * 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_TopFull, select_wh + i * select_wh, 0, select_wh, _bmp.Height);
            }
            int tb = _width - select_wh * 2 - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_TopFull, _width - select_wh - tb, 0, tb, _bmp.Height);
            }
            bmp_block.DrawImage(sep_Left, 0, 0, select_wh, _bmp.Height);
            bmp_block.DrawImage(sep_Right, _width - select_wh, 0, select_wh, _bmp.Height);
        }

        else /*tips 141-160 tips-structure : top or bottom ,middle */
        if ((_style == 141) || (_style == 142) || (_style == 143) || (_style == 144) || (_style == 145))
        {
            Bitmap sep_Left = _bmp.Clone(new Rectangle(0, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_Right = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftBottom = _bmp.Clone(new Rectangle(0, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightBottom = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftFull = _bmp.Clone(new Rectangle(0, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightFull = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_TopFull = _bmp.Clone(new Rectangle(select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_BottomFull = _bmp.Clone(new Rectangle(select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_TopMiddle = _bmp.Clone(new Rectangle(_bmp.Width / 2 - select_wh / 2, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_BottomMiddle = _bmp.Clone(new Rectangle(_bmp.Width / 2 - select_wh / 2, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            int ta = (_width / 2 - select_wh / 2 - select_wh) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_TopFull, select_wh + i * select_wh, 0, select_wh, select_wh);
                bmp_block.DrawImage(sep_BottomFull, select_wh + i * select_wh, _height - select_wh, select_wh, select_wh);

                bmp_block.DrawImage(sep_TopFull, _width / 2 + select_wh / 2 + i * select_wh, 0, select_wh, select_wh);
                bmp_block.DrawImage(sep_BottomFull, _width / 2 + select_wh / 2 + i * select_wh, _height - select_wh, select_wh, select_wh);
            }
            int tb = (_width / 2 - select_wh / 2 - select_wh) - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_TopFull, _width / 2 - select_wh / 2 - tb, 0, tb, select_wh);
                bmp_block.DrawImage(sep_BottomFull, _width / 2 - select_wh / 2 - tb, _height - select_wh, tb, select_wh);

                bmp_block.DrawImage(sep_TopFull, _width - select_wh - tb, 0, tb, select_wh);
                bmp_block.DrawImage(sep_BottomFull, _width - select_wh - tb, _height - select_wh, tb, select_wh);
            }

            bmp_block.DrawImage(sep_Left, 0, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_Right, _width - select_wh, 0, select_wh, select_wh);

            bmp_block.DrawImage(sep_LeftBottom, 0, _height - select_wh, select_wh, select_wh);
            bmp_block.DrawImage(sep_RightBottom, _width - select_wh, _height - select_wh, select_wh, select_wh);

            bmp_block.DrawImage(sep_TopMiddle, _width / 2 - select_wh / 2, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_BottomMiddle, _width / 2 - select_wh / 2, _height - select_wh, select_wh, select_wh);


            ta = (_height - select_wh * 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, select_wh + i * select_wh, select_wh, select_wh);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, select_wh + i * select_wh, select_wh, select_wh);
            }
            tb = _height - select_wh * 2 - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, _height - select_wh - tb, select_wh, tb);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, _height - select_wh - tb, select_wh, tb);
            }
            bmp_block.FillRectangle(new SolidBrush(_bmp.GetPixel(_bmp.Width / 2, _bmp.Height / 2)),
                select_wh, select_wh, _width - select_wh * 2, _height - select_wh * 2);
        }

        else /*tips 161-180 tips-structure : left or right ,middle */
        if ((_style == 161))
        {
            Bitmap sep_Left = _bmp.Clone(new Rectangle(0, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_Right = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftBottom = _bmp.Clone(new Rectangle(0, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightBottom = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftFull = _bmp.Clone(new Rectangle(0, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightFull = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_TopFull = _bmp.Clone(new Rectangle(select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_BottomFull = _bmp.Clone(new Rectangle(select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            Bitmap sep_LeftMiddle = _bmp.Clone(new Rectangle(0, _bmp.Height / 2 - select_wh / 2, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightMiddle = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, _bmp.Height / 2 - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            int ta = (_width - select_wh * 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_TopFull, select_wh + i * select_wh, 0, select_wh, select_wh);
                bmp_block.DrawImage(sep_BottomFull, select_wh + i * select_wh, _height - select_wh, select_wh, select_wh);
            }
            int tb = _width - select_wh * 2 - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_TopFull, _width - select_wh - tb, 0, tb, select_wh);
                bmp_block.DrawImage(sep_BottomFull, _width - select_wh - tb, _height - select_wh, tb, select_wh);
            }
            bmp_block.DrawImage(sep_Left, 0, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_Right, _width - select_wh, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_LeftBottom, 0, _height - select_wh, select_wh, select_wh);
            bmp_block.DrawImage(sep_RightBottom, _width - select_wh, _height - select_wh, select_wh, select_wh);


            ta = (_height / 2 - select_wh - select_wh / 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, select_wh + i * select_wh, select_wh, select_wh);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, select_wh + i * select_wh, select_wh, select_wh);

                bmp_block.DrawImage(sep_LeftFull, 0, (_height / 2 + select_wh / 2) + i * select_wh, select_wh, select_wh);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, (_height / 2 + select_wh / 2) + i * select_wh, select_wh, select_wh);
            }
            tb = (_height / 2 - select_wh - select_wh / 2) - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, _height / 2 - select_wh / 2 - tb, select_wh, tb);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, _height / 2 - select_wh / 2 - tb, select_wh, tb);
                bmp_block.DrawImage(sep_LeftFull, 0, _height - select_wh - tb, select_wh, tb);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, _height - select_wh - tb, select_wh, tb);
            }
            bmp_block.DrawImage(sep_LeftMiddle, 0, _height / 2 - select_wh / 2, select_wh, select_wh);
            bmp_block.DrawImage(sep_RightMiddle, _width - select_wh, _height / 2 - select_wh / 2, select_wh, select_wh);
            bmp_block.FillRectangle(new SolidBrush(_bmp.GetPixel(_bmp.Width / 2, _bmp.Height / 2)),
                select_wh, select_wh, _width - select_wh * 2, _height - select_wh * 2);
        }
        else
        {
            Bitmap sep_Left = _bmp.Clone(new Rectangle(0, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_Right = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftBottom = _bmp.Clone(new Rectangle(0, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightBottom = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_LeftFull = _bmp.Clone(new Rectangle(0, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_RightFull = _bmp.Clone(new Rectangle(_bmp.Width - select_wh, select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_TopFull = _bmp.Clone(new Rectangle(select_wh, 0, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap sep_BottomFull = _bmp.Clone(new Rectangle(select_wh, _bmp.Height - select_wh, select_wh, select_wh), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            int ta = (_width - select_wh * 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_TopFull, select_wh + i * select_wh, 0, select_wh, select_wh);
                bmp_block.DrawImage(sep_BottomFull, select_wh + i * select_wh, _height - select_wh, select_wh, select_wh);
            }
            int tb = _width - select_wh * 2 - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_TopFull, _width - select_wh - tb, 0, tb, select_wh);
                bmp_block.DrawImage(sep_BottomFull, _width - select_wh - tb, _height - select_wh, tb, select_wh);
            }

            bmp_block.DrawImage(sep_Left, 0, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_Right, _width - select_wh, 0, select_wh, select_wh);
            bmp_block.DrawImage(sep_LeftBottom, 0, _height - select_wh, select_wh, select_wh);
            bmp_block.DrawImage(sep_RightBottom, _width - select_wh, _height - select_wh, select_wh, select_wh);

            ta = (_height - select_wh * 2) / select_wh;
            for (int i = 0; i < ta; i++)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, select_wh + i * select_wh, select_wh, select_wh);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, select_wh + i * select_wh, select_wh, select_wh);
            }

            tb = _height - select_wh * 2 - ta * select_wh;
            if (tb > 0)
            {
                bmp_block.DrawImage(sep_LeftFull, 0, _height - select_wh - tb, select_wh, tb);
                bmp_block.DrawImage(sep_RightFull, _width - select_wh, _height - select_wh - tb, select_wh, tb);
            }
            bmp_block.FillRectangle(new SolidBrush(_bmp.GetPixel(_bmp.Width / 2, _bmp.Height / 2)),
                select_wh, select_wh, _width - select_wh * 2, _height - select_wh * 2);
        }
        return bmp_form;
    }
}

