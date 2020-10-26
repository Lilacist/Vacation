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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

public class UiSwitchButtonMethods
{
    public int switch_flag;
    private Cube cube;
    private Point[,] corners = new Point[90, 4];
    private FreeTransform[] filters = new FreeTransform[90];
    private PointF[] set = new PointF[90];
    private Bitmap[] display = new Bitmap[90];
    bool[] begin = new bool[90];
    private Thread[] rtd = new Thread[90];
    private int _speed;
    private int _w, _h;
    private UiControlsMethod.PanelEx switchPanel;
    private UiControlsMethod.PictureBoxEx picSwitch;
    private Bitmap firstBmp, secondBmp;
    private Point _origin;

    public void InitializeSwitchEffect(Control _obj, Point origin, int _flag, int switchType, int speed, MouseEventHandler _click)
    {
        firstBmp = SQK_Ui.switchButton.switchResource.switch1_a;
        secondBmp = SQK_Ui.switchButton.switchResource.switch1_b;
        _origin = origin;
        _speed = speed;
        switch_flag = _flag;

        switch (switchType)
        {
            case 0:
                firstBmp = SQK_Ui.switchButton.switchResource.switch1_a;
                secondBmp = SQK_Ui.switchButton.switchResource.switch1_b;
                break;
            case 1:
                firstBmp = SQK_Ui.switchButton.switchResource.switch2_a;
                secondBmp = SQK_Ui.switchButton.switchResource.switch2_b;
                break;
            case 2:
                firstBmp = SQK_Ui.switchButton.switchResource.switch3_a;
                secondBmp = SQK_Ui.switchButton.switchResource.switch3_b;
                break;
            default:
                break;
        }
        _w = firstBmp.Width;
        _h = firstBmp.Height;

        Point lc = new Point(0, 0);
        cube = new Cube(_w,_h, 1);
        for (int i = 0; i < 90; i++)
        {
            cube.RotateY = i * 2;
            cube.calcCube(lc);
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

        Parallel.For(0, 90, (i) => { updateImage(i); });

        switchPanel = new UiControlsMethod.PanelEx();
        switchPanel.Size = new Size(_w,_h);
        switchPanel.Location = origin;
        switchPanel.BackColor = Color.Transparent;
        
        picSwitch = new UiControlsMethod.PictureBoxEx();
        picSwitch.BackColor = Color.Transparent;
        picSwitch.Size = new Size(_w,_h);
        picSwitch.Location = new Point(0, 0);
        if (_flag==0)  picSwitch.Image = firstBmp;
        else picSwitch.Image = secondBmp;
        picSwitch.Click += new EventHandler(picSwitch_Click);
        picSwitch.MouseClick += _click;
        picSwitch.Cursor = Cursors.Hand;

        switchPanel.Controls.Add(picSwitch);
        
        _obj.Controls.Add(switchPanel);
    }

    [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
    public static extern uint _BeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
    public static extern uint _EndPeriod(uint uMilliseconds);

    private void picSwitch_Click(object sender, EventArgs e)
    {
        _BeginPeriod((uint)_speed);

        if (switch_flag==0)
        {
            for (int i=0;i<90;i++)
            {
                picSwitch.Image = display[i];
                picSwitch.Location = Point.Round(set[i]);
                picSwitch.Update();
                switchPanel.Invalidate();
                Thread.Sleep(_speed);
            }
            picSwitch.Location = new Point(0, 0);
            picSwitch.Image = secondBmp;
            switch_flag = 1;
        } else
        if (switch_flag == 1)
        {
            for (int i = 89; i >= 0; i--)
            {
                picSwitch.Image = display[i];
                picSwitch.Location = Point.Round(set[i]);
                picSwitch.Update();
                switchPanel.Invalidate();
                Thread.Sleep(_speed);
            }
            picSwitch.Location = new Point(0, 0);
            picSwitch.Image = firstBmp;
            switch_flag = 0;
        }

        _EndPeriod((uint)_speed);
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
