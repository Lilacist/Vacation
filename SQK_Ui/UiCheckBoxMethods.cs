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

public class UiCheckBoxMethods
{
    public bool State;
    private Bitmap _bmpCheck, _bmpChecked;

    public void CheckBox (Control _obj, int _type, bool _state, Point _location, MouseEventHandler _MouseClick)
    {
        State = _state;
        switch (_type)
        {
            case 0:
                _bmpCheck = SQK_Ui.CheckBox.CheckResource.chk1;
                _bmpChecked = SQK_Ui.CheckBox.CheckResource.chked1;
                break;
            case 1:
                _bmpCheck = SQK_Ui.CheckBox.CheckResource.chk2;
                _bmpChecked = SQK_Ui.CheckBox.CheckResource.chked2;
                break;
            case 2:
                _bmpCheck = SQK_Ui.CheckBox.CheckResource.chk3;
                _bmpChecked = SQK_Ui.CheckBox.CheckResource.chked3;
                break;

            default:
                _bmpCheck = SQK_Ui.CheckBox.CheckResource.chk1;
                _bmpChecked = SQK_Ui.CheckBox.CheckResource.chked1;
                break;
        }

        UiControlsMethod.PictureBoxEx check = new UiControlsMethod.PictureBoxEx();
        check.BackColor = Color.Transparent;
        check.Cursor = Cursors.Hand;
        check.Size = _bmpCheck.Size;
        check.Location = _location;
        if (_state) check.Image = _bmpChecked;
        else check.Image = _bmpCheck;
        check.Click += new EventHandler(_Click);
        check.MouseClick += _MouseClick;

        _obj.Controls.Add(check);
    }

    private void _Click(object sender, EventArgs e)
    {
        UiControlsMethod.PictureBoxEx pL = (UiControlsMethod.PictureBoxEx)sender;
        if (State)
        {
            State = false;
            pL.Image = _bmpCheck;
        } else
        {
            State = true;
            pL.Image = _bmpChecked;
        }
        pL.Update();
    }
}

