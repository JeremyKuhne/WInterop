// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace Bezier
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-16, Pages 156-159.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ModuleInstance module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = "Bezier"
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                "Bezier",
                "Bezier Splines",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static void DrawBezier(DeviceContext dc, Point[] apt)
        {
            dc.PolyBezier(apt);
            dc.MoveTo(apt[0].X, apt[0].Y);
            dc.LineTo(apt[1].X, apt[1].Y);
            dc.MoveTo(apt[2].X, apt[2].Y);
            dc.LineTo(apt[3].X, apt[3].Y);
        }

        static Point[] apt = new Point[4];

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    int cxClient = lParam.LowWord;
                    int cyClient = lParam.HighWord;

                    apt[0].X = cxClient / 4;
                    apt[0].Y = cyClient / 2;
                    apt[1].X = cxClient / 2;
                    apt[1].Y = cyClient / 4;
                    apt[2].X = cxClient / 2;
                    apt[2].Y = 3 * cyClient / 4;
                    apt[3].X = 3 * cxClient / 4;
                    apt[3].Y = cyClient / 2;

                    return 0;

                case WindowMessage.LeftButtonDown:
                case WindowMessage.RightButtonDown:
                case WindowMessage.MouseMove:
                    MouseKey mk = (MouseKey)wParam.LowWord;
                    if ((mk & (MouseKey.LeftButton | MouseKey.RightButton)) != 0)
                    {
                        using (DeviceContext dc = window.GetDeviceContext())
                        {
                            dc.SelectObject(StockPen.White);
                            DrawBezier(dc, apt);

                            if ((mk & MouseKey.LeftButton) != 0)
                            {
                                apt[1].X = lParam.LowWord;
                                apt[1].Y = lParam.HighWord;
                            }

                            if ((mk & MouseKey.RightButton) != 0)
                            {
                                apt[2].X = lParam.LowWord;
                                apt[2].Y = lParam.HighWord;
                            }

                            dc.SelectObject(StockPen.Black);
                            DrawBezier(dc, apt);
                        }
                    }
                    return 0;
                case WindowMessage.Paint:
                    window.Invalidate(true);
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        DrawBezier(dc, apt);
                    }
                    return 0;
                case WindowMessage.Destroy:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
