/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 2.0 - .Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.1.10
-----------------------------------------------*/

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
struct TEXTMETRIC
{
	public Int32 tmHeight;
	public Int32 tmAscent;
    public Int32 tmDescent;
    public Int32 tmInternalLeading;
    public Int32 tmExternalLeading;
    public Int32 tmAveCharWidth;
    public Int32 tmMaxCharWidth;
    public Int32 tmWeight;
    public Int32 tmOverhang;
    public Int32 tmDigitizedAspectX;
    public Int32 tmDigitizedAspectY;
	public Char tmFirstChar;
	public Char tmLastChar;
	public Char tmDefaultChar;
	public Char tmBreakChar;
    public Byte tmItalic;
    public Byte tmUnderlined;
    public Byte tmStruckOut;
    public Byte tmPitchAndFamily;
    public Byte tmCharSet;
}

[StructLayout(LayoutKind.Sequential)]
struct RECT
{
	public Int32 left;
	public Int32 top;
    public Int32 right;
    public Int32 bottom;
}

sealed class Gdi
{
	public const Int32 TRANSPARENT			= 1;

	public const Int32 FW_DONTCARE			= 0;
	public const Int32 FW_THIN				= 100;
	public const Int32 FW_EXTRALIGHT		= 200;
	public const Int32 FW_ULTRALIGHT		= 200;
	public const Int32 FW_LIGHT				= 300;
	public const Int32 FW_NORMAL			= 400;
	public const Int32 FW_REGULAR			= 400;
	public const Int32 FW_MEDIUM			= 500;
	public const Int32 FW_SEMIBOLD			= 600;
	public const Int32 FW_DEMIBOLD			= 600;
	public const Int32 FW_BOLD				= 700;
	public const Int32 FW_EXTRABOLD			= 800;
	public const Int32 FW_ULTRABOLD			= 800;
	public const Int32 FW_HEAVY				= 900;
	public const Int32 FW_BLACK				= 900;

	public const Int32 ANSI_CHARSET			= 0;
	public const Int32 DEFAULT_CHARSET		= 1;
	public const Int32 SYMBOL_CHARSET		= 2;

	public const Int32 OUT_DEFAULT_PRECIS	= 0;

	public const Int32 CLIP_DEFAULT_PRECIS	= 0;

	public const Int32 DEFAULT_QUALITY		= 0;

	public const Int32 FF_DONTCARE			= (0<<4);

	public const Int32 R2_BLACK				= 1;
	public const Int32 R2_NOTMERGEPEN		= 2;
	public const Int32 R2_MASKNOTPEN		= 3;
	public const Int32 R2_NOTCOPYPEN		= 4;
	public const Int32 R2_MASKPENNOT		= 5;
	public const Int32 R2_NOT				= 6;
	public const Int32 R2_XORPEN			= 7;
	public const Int32 R2_NOTMASKPEN		= 8;
	public const Int32 R2_MASKPEN			= 9;
	public const Int32 R2_NOTXORPEN			= 10;
	public const Int32 R2_NOP				= 11;
	public const Int32 R2_MERGENOTPEN		= 12;
	public const Int32 R2_COPYPEN			= 13;
	public const Int32 R2_MERGEPENNOT		= 14;
	public const Int32 R2_MERGEPEN			= 15;
	public const Int32 R2_WHITE				= 16;
	public const Int32 R2_LAST				= 16;

	public const UInt32 SRCCOPY				= 0x00CC0020;
	public const UInt32 SRCPAINT			= 0x00EE0086;
	public const UInt32 SRCAND				= 0x008800C6;
	public const UInt32 SRCINVERT			= 0x00660046;
	public const UInt32 SRCERASE			= 0x00440328;
	public const UInt32 NOTSRCCOPY			= 0x00330008;
	public const UInt32 NOTSRCERASE			= 0x001100A6;
	public const UInt32 MERGECOPY			= 0x00C000CA;
	public const UInt32 MERGEPAINT			= 0x00BB0226;
	public const UInt32 PATCOPY				= 0x00F00021;
	public const UInt32 PATPAINT			= 0x00FB0A09;
	public const UInt32 PATINVERT			= 0x005A0049;
	public const UInt32 DSTINVERT			= 0x00550009;
	public const UInt32 BLACKNESS			= 0x00000042;
	public const UInt32 WHITENESS			= 0x00FF0062;

	public const Int32 WHITE_BRUSH			= 0;
	public const Int32 LTGRAY_BRUSH			= 1;
	public const Int32 GRAY_BRUSH			= 2;
	public const Int32 DKGRAY_BRUSH			= 3;
	public const Int32 BLACK_BRUSH			= 4;
	public const Int32 NULL_BRUSH			= 5;
	public const Int32 HOLLOW_BRUSH			= NULL_BRUSH;
	public const Int32 WHITE_PEN			= 6;
	public const Int32 BLACK_PEN			= 7;
	public const Int32 NULL_PEN				= 8;
	public const Int32 OEM_FIXED_FONT		= 10;
	public const Int32 ANSI_FIXED_FONT		= 11;
	public const Int32 ANSI_VAR_FONT		= 12;
	public const Int32 SYSTEM_FONT			= 13;
	public const Int32 DEVICE_DEFAULT_FONT	= 14;
	public const Int32 DEFAULT_PALETTE		= 15;
	public const Int32 SYSTEM_FIXED_FONT	= 16;

	public static UInt32 RGB(Byte r, Byte g, Byte b)
	{
		UInt32 color = r;
		color |= (UInt32)((Int32)g << 8);
		color |= (UInt32)((Int32)b << 16);
		return color;
	}

	[DllImport("GDI32.DLL")]
	public static extern Boolean TextOut(IntPtr hdc, Int32 x, Int32 y, String text, Int32 count);
	[DllImport("GDI32.DLL")]
	public static extern Boolean GetCharWidth(IntPtr hdc, Int32 firstChar, Int32 lastChar, Int32[] widths);
	[DllImport("GDI32.DLL")]
	public static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
	[DllImport("GDI32.DLL")]
	public static extern Int32 SetBkMode(IntPtr hdc, Int32 mode);
	[DllImport("GDI32.DLL")]
	public static extern Int32 SaveDC(IntPtr hdc);
	[DllImport("GDI32.DLL")]
	public static extern Int32 RestoreDC(IntPtr hdc, Int32 savedDC);
	[DllImport("GDI32.DLL")]
	public static extern IntPtr CreateFont(Int32 height, Int32 width, Int32 escapement, Int32 orientation, Int32 weight, UInt32 italic, UInt32 underline, UInt32 strikeout, UInt32 charset, UInt32 outputPrecision, UInt32 clipPrecision, UInt32 quality, UInt32 pitchAndFamily, String face);
	[DllImport("GDI32.DLL")]
	public static extern Boolean DeleteObject(IntPtr obj);
	[DllImport("GDI32.DLL")]
	public static extern Boolean GetTextMetrics(IntPtr hdc, ref TEXTMETRIC tm);
	[DllImport("GDI32.DLL")]
	public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
	[DllImport("GDI32.DLL")]
	public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, Int32 width, Int32 height);
	[DllImport("GDI32.DLL")]
	public static extern Boolean DeleteDC(IntPtr hdc);
	[DllImport("GDI32.DLL")]
	public static extern Boolean BitBlt(IntPtr hdcDest, Int32 xDest, Int32 yDest, Int32 width, Int32 height, IntPtr hdSrc, Int32 xSrc, Int32 ySrc, UInt32 rop);
	[DllImport("GDI32.DLL")]
	public static extern Int32 FillRect(IntPtr hdc, ref RECT rc, IntPtr hbr);
	[DllImport("GDI32.DLL")]
	public static extern IntPtr GetStockObject(Int32 obj);
	[DllImport("GDI32.DLL")]
	public static extern Boolean Rectangle(IntPtr hdc, Int32 left, Int32 top, Int32 right, Int32 bottom);
	[DllImport("GDI32.DLL")]
	public static extern Int32 SetROP2(IntPtr hdc, Int32 drawMode);
	[DllImport("GDI32.DLL")]
	public static extern Int32 GetROP2(IntPtr hdc);
	[DllImport("GDI32.DLL")]
	public static extern Boolean LineTo(IntPtr hdc, Int32 xEnd, Int32 yEnd);
	[DllImport("GDI32.DLL")]
	public static extern Boolean MoveToEx(IntPtr hdc, Int32 x, Int32 y, ref System.Drawing.Point oldPos);
	[DllImport("GDI32.DLL")]
	public static extern Boolean PatBlt(IntPtr hdc, Int32 x, Int32 y, Int32 width, Int32 height, UInt32 rop);
	[DllImport("GDI32.DLL")]
	public static extern UInt32 SetPixel(IntPtr hdc, Int32 x, Int32 y, UInt32 color);
	[DllImport("GDI32.DLL")]
	public static extern Boolean Ellipse(IntPtr hdc, Int32 left, Int32 top, Int32 right, Int32 bottom);
}

