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
using System.Threading;
using System.Windows.Forms;

public class AnimationForm
{
    public void shakeForm(Control _obj, int _range, int _number, int _speed)
    {
        Random ran = new Random((int)DateTime.Now.Ticks);
        Point point = _obj.Location;
        for (int i = 0; i < _number; i++)
        {
            _obj.Location = new Point(point.X + ran.Next(8) - _range, point.Y + ran.Next(8) - _range);
            Thread.Sleep(_speed);
            _obj.Location = point;
            Thread.Sleep(_speed);
        }
    }

    public static void rotationForm(AlphaForm _firstObj, AlphaForm _secondObj, int _first, int _second, int _x, int _y, int _width, int _height, double _transparency, int _speed, int _rotorType)
    {
        putAlphaForm _rotationForm = new putAlphaForm();
        _rotationForm._putFormBegin(_x, _y, _width, _height, _transparency);
        _rotationForm._getPutFormInfo(_firstObj);
        _rotationForm._getPutFormInfo(_secondObj);
        _rotationForm._putForm(_first, _second, _speed, _rotorType);
    }

    public static void fadingForm(AlphaForm _obj, Bitmap _bmp, double _transparency, int _speed)
    {
        Bitmap FadingBmp = UiControlsMethod.ControlMethods.getFormControlToBmp(_obj, _bmp);
        _obj.BlendedBackground = FadingBmp;
        Thread fading = new Thread(() =>
        {
            _obj.CrossThreadCalls(() =>
            {
                for (double i = 0; i < _transparency; i += 0.01 * _speed)
                {
                    _obj.SetOpacity(i);
                    Thread.Sleep(10);
                }
                _obj.SetOpacity(_transparency);
            });
        } );
        fading.Start();
    }

    
}

