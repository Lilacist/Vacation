/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/


using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public class Ui3dEffectForm
{
    private Cube cube;
    private Point[,] corners = new Point[90, 4];
    private FreeTransform[] filters = new FreeTransform[90];
    private PointF[] set = new PointF[90];
    private Bitmap[] display = new Bitmap[90];
    private double _transparency;
    private int _w, _h, _speed, type, order;

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

    public void Initialize3dPut(AlphaForm _firstObj, AlphaForm _secondObj, Point origin, int _first, int _second, int _type)
    {
        _getPutFormInfo(_firstObj);
        _getPutFormInfo(_secondObj);

        Bitmap firstBmp = _formList[_first].listImg;
        Bitmap secondBmp = _formList[_second].listImg;

        _w = _formList[_first].listImg.Width;
        _h = _formList[_first].listImg.Height;
        type = _type;

        cube = new Cube(firstBmp.Width, firstBmp.Height, 1);


        for (int i = 0; i < 90; i++)
        {
            if (_type == 0) cube.RotateY = i * 2;
            if (_type == 1) cube.RotateX = i * 2;

            cube.calcCube(origin);
            corners[i, 0] = cube.d;
            corners[i, 1] = cube.a;
            corners[i, 2] = cube.c;
            corners[i, 3] = cube.b;

            int t = 0;
            if (_type == 0)
            {
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
            }
            if (_type == 1)
            {
                if ((corners[i, 0].Y - corners[i, 2].Y) <= 1)
                {
                    if (corners[i, 0].Y < corners[i, 2].Y) t = corners[i, 0].Y;
                    else t = corners[i, 2].Y;
                    set[i] = new PointF(corners[i, 0].X + _w / 2, t + _h / 2);
                }
                else
                {
                    if (corners[i, 2].Y < corners[i, 0].Y) t = corners[i, 2].Y;
                    else t = corners[i, 0].Y;
                    set[i] = new PointF(corners[i, 0].X + _w / 2, t + _h / 2);
                }
            }

            filters[i] = new FreeTransform();
            if (i < 45) filters[i].Bitmap = firstBmp;
            else filters[i].Bitmap = secondBmp;
        }

        Parallel.For(0, 90, (i) => { updateImage(i); });
    }

    public void put3dEffectForm(int _order, int speed, double transparency)
    {
        int dfx = 0, dfy = 0;

        order = _order;

        if (order == 0)
        {
            dfx = _formList[0].listForm.Location.X - _formList[1].listForm.Location.X;
            dfy = _formList[0].listForm.Location.Y - _formList[1].listForm.Location.Y;
            _formList[1].listForm.Location = _formList[0].listForm.Location;
        }
        if (order == 1)
        {
            dfx = _formList[1].listForm.Location.X - _formList[0].listForm.Location.X;
            dfy = _formList[1].listForm.Location.Y - _formList[0].listForm.Location.Y;
            _formList[0].listForm.Location = _formList[1].listForm.Location;
        }
        for (int i = 0; i < 90; i++)
        {
            set[i].X = set[i].X + dfx;
            set[i].Y = set[i].Y + dfy;
        }

        _speed = speed;
        _transparency = transparency * 255;
        Bitmap _bmp = UiControlsMethod.ControlMethods.getFormControlToBmp(_formList[order].listForm, _formList[order].listForm.BlendedBackground);

        int t = order * 45;
        while (t < (order + 1) * 45)
        {
            filters[t].Bitmap = _bmp;
            t++;
        }

        Parallel.For(order * 45, (order + 1) * 45, (i) =>
        {
            updateImage(i);
        });


        Thread outForm = new Thread(() => chg_Half(order));
        outForm.Start();
    }

    [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
    public static extern uint _BeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
    public static extern uint _EndPeriod(uint uMilliseconds);


    private void chg_Half(int half)
    {
        PerPixelAlphaForm Put_form = new PerPixelAlphaForm();
        Put_form.ShowInTaskbar = false;
        Put_form.ShowIcon = false;
        Put_form.Location = Point.Round(set[0]);
        Put_form.SetBitmap(display[89 * half], (byte)_transparency);
        Put_form.Show();

        _formList[half].listForm.CrossThreadCalls(() =>
        {
            _formList[half].listForm.SetOpacity(0);
        });

        _BeginPeriod((uint)_speed);
        if (half == 0)
        {
            for (int i = 0; i < 90; i++)
            {
                Put_form.Location = Point.Round(set[i]);
                Put_form.SetBitmap(display[i], (byte)_transparency);
                Thread.Sleep(_speed);
            }
        }
        else
        if (half == 1)
        {
            for (int i = 89; i >= 0; i--)
            {
                Put_form.Location = Point.Round(set[i]);
                Put_form.SetBitmap(display[i], (byte)_transparency);
                Thread.Sleep(_speed);
            }
        }
        _EndPeriod((uint)_speed);

        int t = 0;
        if (half == 0) t = 1;
        _formList[t].listForm.CrossThreadCalls(() =>
        {
            _formList[t].listForm.SetOpacity(_transparency);
        });

    }
    

    private void updateImage(int num)
    {
        if (type == 0)
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
        }
        if (type == 1)
        {
            if ((corners[num, 0].Y - corners[num, 2].Y) <= 1)
            {
                filters[num].VertexLeftTop = corners[num, 0];
                filters[num].VertexTopRight = corners[num, 1];
                filters[num].VertexBottomLeft = corners[num, 2];
                filters[num].VertexRightBottom = corners[num, 3];
            }
            else
            {
                filters[num].VertexLeftTop = corners[num, 2];
                filters[num].VertexTopRight = corners[num, 3];
                filters[num].VertexBottomLeft = corners[num, 0];
                filters[num].VertexRightBottom = corners[num, 1];
            }
        }
        display[num] = filters[num].Bitmap;
    }
}
