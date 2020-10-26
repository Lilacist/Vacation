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

public class UiInputBoxMethods
{
    private UiControlsMethod.InputBox _inBox = new UiControlsMethod.InputBox();
    private string pasword = "";
    private int rang = 0;
    private int selx = 0;
    private int intype = 0;
    private bool multiline = false;
    private string paswordmask = "*";
    public string getPasword = "";

    private void getIndex()
    {
        rang = _inBox.SelectedText.Length;
        selx = _inBox.SelectionStart;
    }

    public string Value
    {
        get { return _inBox.Text; }
        set { _inBox.Text = value;  }
    }

    public void inputBox(Control _obj, Size _size, Point _location, Font _fnt, Color _fntcolor, string _text, int _type, bool _multiline, int _maxlength, Color _backcolor, int _backradius, int _opacity, bool _readOnly, MouseEventHandler _click)
    {
        inputBox(_obj, _size, _location, _fnt, _fntcolor, _text, _type, _multiline, _maxlength, _backcolor, _backradius, _opacity, "*", _readOnly, _click);
    }

    public void inputBox(Control _obj, Size _size, Point _location, Font _fnt, Color _fntcolor, string _text, int _type, bool _multiline, int _maxlength, Color _backcolor, int _backradius, int _opacity, string _mask, bool _readOnly, MouseEventHandler _click)
    {
        UiTransparentRectMethod boxFram = new UiTransparentRectMethod();
        boxFram.BackColor = _backcolor;
        boxFram.Radius = _backradius;
        boxFram.ShapeBorderStyle = UiTransparentRectMethod.ShapeBorderStyles.ShapeBSNone;
        boxFram.Size = _size;
        boxFram.Location = _location;
        boxFram.Opacity = _opacity;
        paswordmask = _mask;
        intype = _type;
        multiline = _multiline;
        _inBox.WordWrap = false;
        _inBox.KeyDown += new KeyEventHandler(inBox_KeyDown);
        if (_type == 1) //pasword mode
        {
            if (_maxlength > 100) _maxlength = 100;
            _inBox.KeyPress += new KeyPressEventHandler(inBox_KeyPress);
            _inBox.MouseUp += new MouseEventHandler(inBox_MouseUp);
        } else
        {
            if (_multiline) _inBox.WordWrap = true;
            _inBox.Text = _text;
        }
        _inBox.ScrollBars = RichTextBoxScrollBars.None;
        _inBox.Size = new Size(_size.Width - _backradius, _size.Height - _backradius);
        _inBox.Location = new Point(_backradius / 2, _backradius / 2);
        _inBox.MaxLength = _maxlength;
        _inBox.Font = _fnt;
        _inBox.ForeColor = _fntcolor;
        _inBox.ReadOnly = _readOnly;
        _inBox.MouseClick += _click;

        boxFram.Controls.Add(_inBox);
        _obj.Controls.Add(boxFram);
    }

    private void inBox_KeyDown(object sender, KeyEventArgs e)
    {
        getIndex();
        if (intype == 0) //normal mode
        {
            if (multiline) //single mode
            {
                if ((e.KeyCode == Keys.Enter) || ((e.Control) && (e.KeyCode == Keys.R))) { e.Handled = true; }
                if (((e.Control) && (e.KeyCode == Keys.V)) || ((e.Shift) && (e.KeyCode == Keys.Insert)))
                {
                    e.Handled = true;
                    string clipstr = "";
                    IDataObject data = Clipboard.GetDataObject();
                    if (data.GetDataPresent(typeof(string)))
                    {
                        clipstr = (string)data.GetData(typeof(string));
                    }
                    if (clipstr != "")
                    {
                        clipstr = clipstr.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                        if (rang != 0)
                        {
                            _inBox.Text = _inBox.Text.Remove(selx, rang);
                        }
                        _inBox.Text = _inBox.Text.Insert(selx, clipstr);
                        _inBox.SelectionStart = selx + clipstr.Length;
                    }
                }
            }
        }

        if (intype == 1) //pasword mode
        {
            if ((e.KeyCode == Keys.Enter) || ((e.Control) && (e.KeyCode == Keys.V)) || ((e.Shift) && (e.KeyCode == Keys.Insert)) || ((e.Control) && (e.KeyCode == Keys.X))) //disable key
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                if ((selx != 0) || (rang != 0))
                {
                    if (rang == 0) pasword = pasword.Remove(selx, 1);
                    else pasword = pasword.Remove(selx, rang);

                    getPasword = UiControlsMethod.GetMd5Str32(pasword);
                }
            }
        }
    }

    private void inBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = true;
        if (e.KeyChar == Convert.ToChar(Keys.Back))
        {
            if ((selx != 0) || (rang != 0))
            {
                if (rang == 0) pasword = pasword.Remove(selx - 1, 1);
                else pasword = pasword.Remove(selx, rang);

                getPasword = UiControlsMethod.GetMd5Str32(pasword);
            }
        }
        else
        {
            if (
                (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
                (e.KeyChar == '!') || (e.KeyChar == '@') || (e.KeyChar == '#') || (e.KeyChar == '$') || (e.KeyChar == '%') || (e.KeyChar == '^') || (e.KeyChar == '&') || (e.KeyChar == '(') || (e.KeyChar == ')') ||
                (e.KeyChar == '_') || (e.KeyChar == '+') || (e.KeyChar == '-') || (e.KeyChar == '=') || (e.KeyChar == '[') || (e.KeyChar == ']') || (e.KeyChar == '{') || (e.KeyChar == '}') ||
                (e.KeyChar == '`') || (e.KeyChar == '~') || (e.KeyChar == '|') || (e.KeyChar == '\\') || (e.KeyChar == ';') || (e.KeyChar == ':') || (e.KeyChar == '"') || (e.KeyChar == '\'') ||
                (e.KeyChar == ',') || (e.KeyChar == '<') || (e.KeyChar == '.') || (e.KeyChar == '>') || (e.KeyChar == '/') || (e.KeyChar == '?')
                )
            {
                if (rang != 0)
                {
                    pasword = pasword.Remove(selx, rang);
                    pasword = pasword.Insert(selx, e.KeyChar.ToString());
                }
                else { pasword = pasword.Insert(selx, e.KeyChar.ToString()); }

                getPasword = UiControlsMethod.GetMd5Str32(pasword);

                string code = "";
                for (int i = 0; i < pasword.Length; i++) code += paswordmask;
                _inBox.Text = code;
                _inBox.SelectionStart = selx + 1;
            }
        }
    }

    private void inBox_MouseUp(object sender, MouseEventArgs e)
    {
        getIndex();
    }

   
}

