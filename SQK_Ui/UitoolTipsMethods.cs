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
using System.Windows.Forms;
//using static AlphaForm;

public class UitoolTipsMethods
{
    public class toolTips
    {
        public AlphaForm FormTips;
        public bool _showTips = false;
        public string _tipsName;
        public Rectangle mouseRect;
        public string tipsTxt;
        public Control _obj;
        public Rectangle _rect;
        public int _tipsType;
        public Point _location;
        public double _trans;
        public Size _size;
        public int _animation;
        public int _animationSpeed;
    }
    private static List<toolTips> _toolTipsList = new List<toolTips>();
    public static void ToolTips(Control _obj, string _tipsName, Rectangle _rect, int _tipsType, Point _location, double _trans, Size _size, int _animation, int _animationSpeed)
    {
        _toolTipsList.Add(new toolTips
        {
            _obj = _obj,
            _tipsName = _tipsName,
            mouseRect = _rect,
            _tipsType = _tipsType,
            _location = _location,
            _trans = _trans,
            _size = _size,
            _animation = _animation,
            _animationSpeed = _animationSpeed
        });
    }

    private class toolTips_addImage
    {
        public string _mainName;
        public Bitmap _image;
        public Point _setXY;
    }
    private static List<toolTips_addImage> _addImgList = new List<toolTips_addImage>();
    public static void tips_addImage(string _mainName, Bitmap _addbmp, Point _XY)
    {
        _addImgList.Add(new toolTips_addImage
        {
            _mainName = _mainName,
            _image = _addbmp,
            _setXY = _XY
        }
        );
    }

    private class toolTips_addString
    {
        public string _mainName;
        public string _string;
        public Point _stringXY;
        public Font _stringFont;
        public Color _stringColor;
        public bool _shadow;
        public Point _distance;
    }
    private static List<toolTips_addString> _addStringList = new List<toolTips_addString>();
    public static void tips_addString(string _mainName, string _addstring, Point _stringXY, Font _strFont, Color _strColor, bool _strShadow, Point _shadowDistance)
    {
        _addStringList.Add(new toolTips_addString
        {
            _mainName = _mainName,
            _string = _addstring,
            _stringXY = _stringXY,
            _stringFont = _strFont,
            _stringColor = _strColor,
            _shadow = _strShadow,
            _distance = _shadowDistance
        }
        );
    }

    public static void _showToolTips(Control _obj, Point _mousePoint)
    {
        if ((_obj.Visible) && (_toolTipsList.Count > 0))
        {
            for (int i = 0; i < _toolTipsList.Count; i++)
            {
                if (
                     ((_mousePoint.X >= (_toolTipsList[i]._obj.Location.X + _toolTipsList[i].mouseRect.X)) &&
                       (_mousePoint.X <= (_toolTipsList[i]._obj.Location.X + _toolTipsList[i].mouseRect.X + _toolTipsList[i].mouseRect.Width))) &&
                     ((_mousePoint.Y >= (_toolTipsList[i]._obj.Location.Y + _toolTipsList[i].mouseRect.Y)) &&
                       (_mousePoint.Y <= (_toolTipsList[i]._obj.Location.Y + _toolTipsList[i].mouseRect.Y + _toolTipsList[i].mouseRect.Height)))
                    )
                {
                    if (!_toolTipsList[i]._showTips)
                    {
                        _toolTipsList[i].FormTips = new AlphaForm();
                        _toolTipsList[i].FormTips.FormBorderStyle = FormBorderStyle.None;
                        _toolTipsList[i].FormTips.StartPosition = FormStartPosition.Manual;
                        _toolTipsList[i].FormTips.Location = new Point(_toolTipsList[i]._location.X + _toolTipsList[i]._obj.Location.X, _toolTipsList[i]._location.Y + _toolTipsList[i]._obj.Location.Y);
                        _toolTipsList[i].FormTips.ShowInTaskbar = false;
                        _toolTipsList[i].FormTips.ShowIcon = false;
                        _toolTipsList[i].FormTips.SizeMode = AlphaForm.SizeModes.None;
                        _toolTipsList[i].FormTips.Opacity = _toolTipsList[i]._trans;
                        _toolTipsList[i].FormTips.TopMost = true;

                        switch (_toolTipsList[i]._animation)
                        {
                            case 0:
                                _toolTipsList[i].FormTips.Animation = AlphaForm.Animations.None;
                                _toolTipsList[i].FormTips.AnimationSpeed = _toolTipsList[i]._animationSpeed;
                                break;
                            case 1:
                                _toolTipsList[i].FormTips.Animation = AlphaForm.Animations.Fading;
                                _toolTipsList[i].FormTips.AnimationSpeed = _toolTipsList[i]._animationSpeed;
                                break;
                            case 2:
                                _toolTipsList[i].FormTips.Animation = AlphaForm.Animations.rotation;
                                _toolTipsList[i].FormTips.AnimationSpeed = _toolTipsList[i]._animationSpeed;
                                break;
                            default:
                                break;
                        }

                        switch (_toolTipsList[i]._tipsType)
                        {
                            case 1:
                                _toolTipsList[i].FormTips.窗体图像 = SQK_Ui.Tooltips.TipsResource.tips_1;
                                break;
                            case 2:
                                _toolTipsList[i].FormTips.窗体图像 = SQK_Ui.Tooltips.TipsResource.tips_2;
                                break;
                            case 3:
                                _toolTipsList[i].FormTips.窗体图像 = SQK_Ui.Tooltips.TipsResource.tips_3;
                                break;
                            case 4:
                                _toolTipsList[i].FormTips.窗体图像 = SQK_Ui.Tooltips.TipsResource.tips_4;
                                break;
                            case 5: // tips - 5
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(100, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 6: // tips - 6
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(121, _toolTipsList[i]._size.Width, SQK_Ui.Tooltips.TipsResource.tips_6.Height);
                                break;
                            case 7: // tips - 7
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(122, _toolTipsList[i]._size.Width, SQK_Ui.Tooltips.TipsResource.tips_7.Height);
                                break;
                            case 8: // tips - 8
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(123, _toolTipsList[i]._size.Width, SQK_Ui.Tooltips.TipsResource.tips_8.Height);
                                break;
                            case 9: // tips - 9
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(141, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 10: // tips - 10
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(142, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 11: // tips - 11
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(161, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 12:
                                _toolTipsList[i].FormTips.窗体图像 = SQK_Ui.Tooltips.TipsResource.tips_12;
                                break;
                            case 13: // tips - 13
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(124, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 14: // tips - 14
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(125, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 15: // tips - 15
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(143, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 16: // tips - 16
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(144, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            case 17: // tips - 17
                                _toolTipsList[i].FormTips.窗体图像 = new CutPixelAlphaImage().CutAlphaFormImage(145, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height);
                                break;
                            default:
                                break;
                        }

                        if (_addImgList.Count > 0)
                        {
                            for (int t = 0; t < _addImgList.Count; t++)
                            {
                                if (_addImgList[t]._mainName == _toolTipsList[i]._tipsName)
                                {
                                    Graphics _addbmp = Graphics.FromImage(_toolTipsList[i].FormTips.窗体图像);
                                    _addbmp.DrawImage(_addImgList[t]._image, _addImgList[t]._setXY);
                                }
                            }
                        }
                        if (_addStringList.Count > 0)
                        {
                            for (int t = 0; t < _addStringList.Count; t++)
                            {
                                if (_addStringList[t]._mainName == _toolTipsList[i]._tipsName)
                                {
                                    UiDrawTextMethod ds = new UiDrawTextMethod();
                                    ds.DrawString(
                                        _toolTipsList[i].FormTips.窗体图像,
                                        _addStringList[t]._string,
                                        _addStringList[t]._stringFont,
                                        _addStringList[t]._stringColor,
                                        new Rectangle(_addStringList[t]._stringXY.X, _addStringList[t]._stringXY.Y, _toolTipsList[i]._size.Width, _toolTipsList[i]._size.Height),
                                        _addStringList[t]._shadow,
                                        _addStringList[t]._distance
                                        );
                                }
                            }
                        }
                        _toolTipsList[i].FormTips.Size = new Size(_toolTipsList[i].FormTips.窗体图像.Width, _toolTipsList[i].FormTips.窗体图像.Height);
                        _toolTipsList[i]._showTips = true;
                        _toolTipsList[i].FormTips.Show();
                    }
                }
                else
                {
                    if (_toolTipsList[i]._showTips)
                    {
                        _toolTipsList[i]._showTips = false;
                        _toolTipsList[i].FormTips.Close();
                    }
                }
            }
        }
    }
}