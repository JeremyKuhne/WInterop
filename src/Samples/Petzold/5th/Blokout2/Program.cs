// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Blokout2;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 7-11, Pages 314-317.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Blockout2(), "Mouse Button & Capture Demo");
    }
}

internal class Blockout2 : WindowClass
{
    private bool _fBlocking, _fValidBox;
    private Point _ptBeg, _ptEnd, _ptBoxBeg, _ptBoxEnd;

    private void DrawBoxOutline(WindowHandle window, Point ptBeg, Point ptEnd)
    {
        using DeviceContext dc = window.GetDeviceContext();
        dc.SetRasterOperation(PenMixMode.Not);
        dc.SelectObject(StockBrush.Null);
        dc.Rectangle(Rectangle.FromLTRB(ptBeg.X, ptBeg.Y, ptEnd.X, ptEnd.Y));
    }

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.LeftButtonDown:
                _ptBeg.X = _ptEnd.X = lParam.LowWord;
                _ptBeg.Y = _ptEnd.Y = lParam.HighWord;
                DrawBoxOutline(window, _ptBeg, _ptEnd);
                window.SetCapture();
                Windows.SetCursor(CursorId.Cross);
                _fBlocking = true;
                return 0;
            case MessageType.MouseMove:
                if (_fBlocking)
                {
                    Windows.SetCursor(CursorId.Cross);
                    DrawBoxOutline(window, _ptBeg, _ptEnd);
                    _ptEnd.X = lParam.LowWord;
                    _ptEnd.Y = lParam.HighWord;
                    DrawBoxOutline(window, _ptBeg, _ptEnd);
                }
                return 0;
            case MessageType.LeftButtonUp:
                if (_fBlocking)
                {
                    DrawBoxOutline(window, _ptBeg, _ptEnd);
                    _ptBoxBeg = _ptBeg;
                    _ptBoxEnd.X = lParam.LowWord;
                    _ptBoxEnd.Y = lParam.HighWord;
                    Windows.ReleaseCapture();
                    Windows.SetCursor(CursorId.Arrow);
                    _fBlocking = false;
                    _fValidBox = true;
                    window.Invalidate(true);
                }
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    if (_fValidBox)
                    {
                        dc.SelectObject(StockBrush.Black);
                        dc.Rectangle(Rectangle.FromLTRB(_ptBoxBeg.X, _ptBoxBeg.Y, _ptBoxEnd.X, _ptBoxEnd.Y));
                    }
                    if (_fBlocking)
                    {
                        dc.SetRasterOperation(PenMixMode.Not);
                        dc.SelectObject(StockBrush.Null);
                        dc.Rectangle(Rectangle.FromLTRB(_ptBeg.X, _ptBeg.Y, _ptEnd.X, _ptEnd.Y));
                    }
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
