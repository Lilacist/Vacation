/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 2.0 - .Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.1.2
-----------------------------------------------*/

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

public class Ui3dEffectForm
{
    private Cube cube;
    private Point[,] corners = new Point[90, 4];
    private FreeTransform[] filters = new FreeTransform[90];
    private PointF[] set = new PointF[90];
    private Bitmap[] display = new Bitmap[90];
    bool[] begin = new bool[90];
    private PerPixelAlphaForm Put_form;
    private Thread[] rtd = new Thread[90];
    private int _rotationtype;
    private int _speed;
    private double _transparency;
    private int _w, _h;

    public class formList
    {
        public AlphaForm listForm;
        public Bitmap listImg;
    }
    public List<formList> _formList = new List<formList>();

    private void addFormInfo(AlphaForm form, Bitmap img)
    {
        _formList.Add(new formList
        {
            listForm = form,
            listImg = img
        });
    }

    public void _getPutFormInfo(AlphaForm _form)
    {
        addFormInfo(_form, UiControlsMethod.ControlMethods.getFormControlToBmp(_form, _form.BlendedBackground));
    }

    public void Initialize3dEffect(Point origin, int _first, int _second, int rotationType, double transparency, int speed)
    {
        Thread check_all = new Thread(ck_All);
        check_all.Start();

        Bitmap firstBmp = _formList[_first].listImg;
        Bitmap secondBmp = _formList[_second].listImg;

        _w = _formList[_first].listImg.Width;
        _h = _formList[_first].listImg.Height;

        Put_form = new PerPixelAlphaForm();
        Put_form.ShowInTaskbar = false;
        Put_form.ShowIcon = false;
        _rotationtype = rotationType;
        _speed = speed;
        _transparency = transparency * 255;

        cube = new Cube(firstBmp.Width, firstBmp.Height, 1);
        for (int i = 0; i < 90; i++)
        {
            cube.RotateY = i * 2;
            cube.calcCube(origin);
            corners[i, 0] = cube.d;
            corners[i, 1] = cube.a;
            corners[i, 2] = cube.c;
            corners[i, 3] = cube.b;
            int t = 0;
            if ((corners[i, 0].X - corners[i, 1].X) <= 1)
            {
                if (corners[i, 0].Y < corners[i, 1].Y) t = corners[i, 0].Y;
                else t = corners[i, 1].Y;
                set[i] = new PointF(corners[i, 0].X + _w / 2, t + _h / 2);
            }
            else
            {
                if (corners[i, 1].Y < corners[i, 0].Y) t = corners[i, 1].Y;
                else t = corners[i, 0].Y;
                set[i] = new PointF(corners[i, 1].X + _w / 2, t + _h / 2);
            }
            begin[i] = false;
            filters[i] = new FreeTransform();
            if (i < 45) filters[i].Bitmap = firstBmp;
            else filters[i].Bitmap = secondBmp;
        }
        for (int i = 0; i < 90; i++)
        {
            rtd[i] = new Thread(getimg);
            rtd[i].Start(i);
        }
    }

    [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
    public static extern uint _BeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
    public static extern uint _EndPeriod(uint uMilliseconds);

    private void ck_All()
    {
        bool ck = false;
        while (true)
        {
            for (int i = 0; i < 90; i++)
            {
                ck = begin[i];
                if (!ck) break;
            }
            if (ck)
            {
                Put_form.CrossThreadCalls(() => {
                    Put_form.Location = Point.Round(set[0]);
                    Put_form.SetBitmap(display[0], (byte)_transparency);
                    Put_form.Show();
                    _BeginPeriod((uint)_speed);
                    if (_rotationtype == 0)
                    {
                        
                        for (int i = 0; i < 90; i++)
                        {
                            Put_form.Location = Point.Round(set[i]);
                            Put_form.SetBitmap(display[i], (byte)_transparency);
                            Thread.Sleep(_speed);
                        }
                    }
                    else
                    if (_rotationtype == 1)
                    {
                        for (int i = 89; i >= 0; i--)
                        {
                            Put_form.Location = Point.Round(set[i]);
                            Put_form.SetBitmap(display[i], (byte)_transparency);
                            Thread.Sleep(_speed);
                        }
                    }
                    _EndPeriod((uint)_speed);
                });
                _formList[0].listForm.CrossThreadCalls(() => {
                    _formList[0].listForm.SetOpacity(_transparency / 255);
                });
                break;
            }
        }
    }

    private void getimg(object num)
    {
        updateImage((int)num);
        begin[(int)num] = true;
    }

    private void updateImage(int num)
    {
        if ((corners[num, 0].X - corners[num, 1].X) <= 1)
        {
            filters[num].VertexLeftTop = corners[num, 0];
            filters[num].VertexTopRight = corners[num, 1];
            filters[num].VertexBottomLeft = corners[num, 2];
            filters[num].VertexRightBottom = corners[num, 3];
        }
        else
        {
            filters[num].VertexLeftTop = corners[num, 1];
            filters[num].VertexTopRight = corners[num, 0];
            filters[num].VertexBottomLeft = corners[num, 3];
            filters[num].VertexRightBottom = corners[num, 2];
        }
        display[num] = filters[num].Bitmap;
    }
}
