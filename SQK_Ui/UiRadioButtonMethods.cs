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

public class UiRadioButtonMethods
{
    public bool[] State;

    private UiControlsMethod.PictureBoxEx[] radio;
    private Bitmap _bmpCheck, _bmpChecked;
    private int SelectType = 0, Num = 0;

    public void RadioButton (Control _obj, int _selectType, int _type, int _num, bool[] _state, Point[] _location, MouseEventHandler[] _MouseClick)
    {
        SelectType = _selectType;
        Num = _num;
        State = new bool[_num];
        State = _state;

        switch (_type)
        {
            case 0:
                _bmpCheck = SQK_Ui.RadioButton.RedioResource.radio1;
                _bmpChecked = SQK_Ui.RadioButton.RedioResource.radioed1;
                break;
            case 1:
                _bmpCheck = SQK_Ui.RadioButton.RedioResource.radio2;
                _bmpChecked = SQK_Ui.RadioButton.RedioResource.radioed2;
                break;
            case 2:
                _bmpCheck = SQK_Ui.RadioButton.RedioResource.radio3;
                _bmpChecked = SQK_Ui.RadioButton.RedioResource.radioed3;
                break;
            case 3:
                _bmpCheck = SQK_Ui.RadioButton.RedioResource.radio4;
                _bmpChecked = SQK_Ui.RadioButton.RedioResource.radioed4;
                break;
            case 4:
                _bmpCheck = SQK_Ui.RadioButton.RedioResource.radio5;
                _bmpChecked = SQK_Ui.RadioButton.RedioResource.radioed5;
                break;
            default:
                break;
        }

        radio = new UiControlsMethod.PictureBoxEx[_num];
        for (int i=0;i<_num;i++)
        {
            radio[i] = new UiControlsMethod.PictureBoxEx();
            radio[i].BackColor = Color.Transparent;
            radio[i].Cursor = Cursors.Hand;
            radio[i].Size = _bmpCheck.Size;
            radio[i].Location = _location[i];
            radio[i].Tag = i;
            radio[i].Click += new EventHandler(_Click);
            radio[i].MouseClick += _MouseClick[i];
            if (_state[i])
            {
                radio[i].Image = _bmpChecked;
            } else radio[i].Image = _bmpCheck;

            _obj.Controls.Add(radio[i]);
        }
    }

    private void _Click (object sender, EventArgs e)
    {
        UiControlsMethod.PictureBoxEx pL = (UiControlsMethod.PictureBoxEx)sender;
        if (SelectType==0) //single-select
        {
            for (int i=0;i<Num;i++)
            {
                if (i != (int)pL.Tag)
                {
                    radio[i].Image = _bmpCheck;
                    State[i] = false;
                } else
                {
                    radio[i].Image = _bmpChecked;
                    State[i] = true;
                }
                radio[i].Update();
            }
        } else 
        if (SelectType==1) //multi-select
        {
            if (State[(int)pL.Tag]==true)
            {
                State[(int)pL.Tag] = false;
                radio[(int)pL.Tag].Image = _bmpCheck;
            } else
            {
                State[(int)pL.Tag] = true;
                radio[(int)pL.Tag].Image = _bmpChecked;
            }
            radio[(int)pL.Tag].Update();
        }
    }
}

