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
using System.Threading;

public class putAlphaForm
{
    AnimationPutForm putForm;
    public int form_x, form_y, form_width, form_height;
    public AlphaForm firstForm, secondForm;
    public Bitmap firstBmp, secondBmp;
    public double transparency;

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

    public void _putFormBegin (int _lx, int _ly, int _width, int _height, double _transparency)
    {
        form_x = _lx;
        form_y = _ly;
        form_width = _width;
        form_height = _height;
        transparency = _transparency * 255;
    }

    public void _getPutFormInfo (AlphaForm _form)
    {
        addFormInfo(_form, UiControlsMethod.ControlMethods.getFormControlToBmp(_form,_form.BlendedBackground));
    }

    public void _putForm (int _first, int _second, int _putSpeed, int rotorType )
    {
        
        firstForm = _formList[_first].listForm;
        secondForm = _formList[_second].listForm;

        firstBmp = _formList[_first].listImg;
        secondBmp = _formList[_second].listImg;

        Bitmap clone_firstBmp = firstBmp.Clone(new Rectangle(0, 0, firstBmp.Width, firstBmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        Bitmap clone_secondBmp = secondBmp.Clone(new Rectangle(0, 0, secondBmp.Width, secondBmp.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        if (rotorType == 0) clone_secondBmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
        if (rotorType == 1) clone_secondBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

        putForm = new AnimationPutForm();
        putForm.AnimationStart = true;
        putForm.AnimationBegin = false;
        putForm.PutForm_Initialize(firstForm.Location, clone_firstBmp, clone_secondBmp, transparency, _putSpeed, rotorType);
        
        Thread animationStat = new Thread(getAnimationPutState);
        animationStat.Start();
    }

    public void _closeForm ()
    {
        putForm.PutForm_Close();
    }

    private void getAnimationPutState()
    {
        bool _state = true;
        while (true)
        {
            if (putForm.AnimationBegin)
            {
                if (_state)
                {
                    firstForm.CrossThreadCalls(() => {
                        Thread.Sleep(10); // the same as [ AnimationPutForm.Timer.Interval * 2, becsuse flip delayed]
                        firstForm.Visible = false;
                        firstForm.SetOpacity(0);
                    });
                    _state = false;
                }
            }
            
            if (!putForm.AnimationStart)
            {
                secondForm.CrossThreadCalls(() => {
                    try
                    {
                        secondForm.Location = firstForm.Location;
                        secondForm.Visible = true;
                        secondForm.SetOpacity( (transparency/255) );
                    }
                    catch { }
                });
                Thread.Sleep(5); // the same as [ AnimationPutForm.Timer.Interval ]
                putForm.AnimationStart = true;
                putForm.PutForm_Close();
                
                break;
            }
        }
    }
}
