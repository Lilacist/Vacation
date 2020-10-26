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

public class AnimationPutForm
{
    public bool AnimationStart, AnimationBegin;

    private PerPixelAlphaForm Put_form;
    private Bitmap m_PageA, m_PageB;
    private float m_X1;
    private int m_X2;
    private float m_Y1;
    private float m_Y2;

    private int c_X;
    private int c_Y;

    private int m_AngleCounter = 0;
    private int m_XIncrement = 1;
    private Graphics bmp_block;
    private double _transparency;
    private int AnimationSpeed = 5;
    private int rotorType;
    private System.Windows.Forms.Timer Timer1;

    public void PutForm_Initialize (Point _location, Bitmap _m_PageA, Bitmap _m_PageB, double transparency, int _AnimationSpeed, int _rotorType)
    {
        rotorType = _rotorType;

        m_PageA = _m_PageA;
        m_PageB = _m_PageB;
        m_X1 = 0;
        m_X2 = m_PageA.Width;
        m_Y1 = 0;
        m_Y2 = m_PageA.Height;

        c_X = 1;
        c_Y = 1;

        _transparency = transparency;
        AnimationSpeed = _AnimationSpeed;

        Put_form = new PerPixelAlphaForm();
        Put_form.ShowInTaskbar = false;
        Put_form.ShowIcon = false;
        Put_form.SetBitmap(m_PageA, (byte)_transparency);
        Put_form.Location = _location;
        Put_form.Show();

        Timer1 = new System.Windows.Forms.Timer(); 
        Timer1.Interval = 5;
        Timer1.Tick += Timer1_Tick;
        Timer1.Enabled = true;
    }

    public void PutForm_Close ()
    {
        Put_form.CrossThreadCalls(() =>
        {
            Put_form.Close();
        });
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
        Bitmap _tmp = new Bitmap(m_PageA.Width, m_PageA.Height);
        Bitmap _tmpCut = new Bitmap(m_PageA.Width, m_PageA.Height);

        bmp_block = Graphics.FromImage(_tmp);
        bmp_block.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        if (rotorType == 0) //horizontal rotor
        {
            PointF[] destPoints = { new PointF(m_X1, 0), new PointF(m_X2, m_Y1), new PointF(m_X1, m_PageA.Height) };
            float ang = (float)(m_AngleCounter * (float)(180f / m_PageA.Width));
            if (m_X1 < m_PageA.Width / 2)
            {
                bmp_block.DrawImage(m_PageA, destPoints);
                m_Y1 += 0.5f;
            }
            else
            {
                bmp_block.DrawImage(m_PageB, destPoints);
                m_Y1 -= 0.5f;
            }

            m_X1 += m_XIncrement + AnimationSpeed;
            m_X2 -= m_XIncrement + AnimationSpeed;

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;
            //_tmp.Dispose();

            if (m_X2 < -1)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            }
            else
            {
                AnimationStart = true;
            }
        }

        if (rotorType == 1) //flip horizontal
        {
            PointF[] destPoints = { new PointF(m_X1, m_Y1), new PointF(m_X2, m_Y1), new PointF(m_X1, m_Y2) };
            float ang = (float)(m_AngleCounter * (float)(180f / m_PageA.Width));
            if (m_Y1 < m_PageA.Height / 2)
            {
                bmp_block.DrawImage(m_PageA, destPoints);
                m_X1 += 0.5f;
            }
            else
            {
                bmp_block.DrawImage(m_PageB, destPoints);
                m_X1 -= 0.5f;
            }
            m_Y1 += m_XIncrement + AnimationSpeed;
            m_Y2 -= m_XIncrement + AnimationSpeed;

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;

            if (m_Y2 < -1)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            }
            else
            {
                AnimationStart = true;
            }
        }

        if (rotorType == 2) //Up to down
        {
            bmp_block.DrawImage(m_PageA,0,0, m_PageA.Width,m_PageA.Height);
            _tmpCut = m_PageB.Clone(new Rectangle(0, m_PageB.Height - c_Y, m_PageB.Width, c_Y), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmp_block.DrawImage(_tmpCut, 0, 0, _tmpCut.Width, _tmpCut.Height);

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;

            c_Y += AnimationSpeed;
            if (c_Y >= m_PageB.Height)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            } else
            {
                AnimationStart = true;
            }
        }

        if (rotorType == 3) //Down to Up
        {
            bmp_block.DrawImage(m_PageA, 0, 0, m_PageA.Width, m_PageA.Height);
            _tmpCut = m_PageB.Clone(new Rectangle(0, 0, m_PageB.Width, c_Y), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmp_block.DrawImage(_tmpCut, 0, m_PageB.Height-c_Y, _tmpCut.Width, _tmpCut.Height);

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;

            c_Y += AnimationSpeed;
            if (c_Y >= m_PageB.Height)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            }
            else
            {
                AnimationStart = true;
            }
        }

        if (rotorType == 4) //Left to Right
        {
            bmp_block.DrawImage(m_PageA, 0, 0, m_PageA.Width, m_PageA.Height);
            _tmpCut = m_PageB.Clone(new Rectangle(m_PageB.Width-c_X, 0, c_X, m_PageB.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmp_block.DrawImage(_tmpCut, 0, 0, _tmpCut.Width, _tmpCut.Height);

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;

            c_X += AnimationSpeed;
            if (c_X >= m_PageB.Width)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            }
            else
            {
                AnimationStart = true;
            }
        }

        if (rotorType == 5) //Right to Left
        {
            bmp_block.DrawImage(m_PageA, 0, 0, m_PageA.Width, m_PageA.Height);
            _tmpCut = m_PageB.Clone(new Rectangle(0, 0, c_X, m_PageB.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmp_block.DrawImage(_tmpCut, m_PageB.Width-c_X, 0, _tmpCut.Width, _tmpCut.Height);

            Put_form.SetBitmap(_tmp, (byte)_transparency);
            AnimationBegin = true;

            c_X += AnimationSpeed;
            if (c_X >= m_PageB.Width)
            {
                Timer1.Enabled = false;
                AnimationStart = false;
            }
            else
            {
                AnimationStart = true;
            }
        }

        _tmp.Dispose();
        _tmpCut.Dispose();
        bmp_block.Dispose();
    }

}

