// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Bezier;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 5-16, Pages 156-159.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Bezier(), "Bezier Splines");
    }
}

internal class Bezier : WindowClass
{
    private void DrawBezier(DeviceContext dc, Point[] apt)
    {
        dc.PolyBezier(apt);
        dc.MoveTo(apt[0]);
        dc.LineTo(apt[1]);
        dc.MoveTo(apt[2]);
        dc.LineTo(apt[3]);
    }

    private readonly Point[] _apt = new Point[4];

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Size:
                int cxClient = lParam.LowWord;
                int cyClient = lParam.HighWord;

                _apt[0].X = cxClient / 4;
                _apt[0].Y = cyClient / 2;
                _apt[1].X = cxClient / 2;
                _apt[1].Y = cyClient / 4;
                _apt[2].X = cxClient / 2;
                _apt[2].Y = 3 * cyClient / 4;
                _apt[3].X = 3 * cxClient / 4;
                _apt[3].Y = cyClient / 2;

                return 0;

            case MessageType.LeftButtonDown:
            case MessageType.RightButtonDown:
            case MessageType.MouseMove:
                MouseKey mk = (MouseKey)wParam.LowWord;
                if ((mk & (MouseKey.LeftButton | MouseKey.RightButton)) != 0)
                {
                    using DeviceContext dc = window.GetDeviceContext();
                    dc.SelectObject(StockPen.White);
                    DrawBezier(dc, _apt);

                    if ((mk & MouseKey.LeftButton) != 0)
                    {
                        _apt[1].X = lParam.LowWord;
                        _apt[1].Y = lParam.HighWord;
                    }

                    if ((mk & MouseKey.RightButton) != 0)
                    {
                        _apt[2].X = lParam.LowWord;
                        _apt[2].Y = lParam.HighWord;
                    }

                    dc.SelectObject(StockPen.Black);
                    DrawBezier(dc, _apt);
                }
                return 0;
            case MessageType.Paint:
                window.Invalidate(true);
                using (DeviceContext dc = window.BeginPaint())
                {
                    DrawBezier(dc, _apt);
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
