// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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
            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
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

            Windows.RegisterClass(wndclass);

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

        static void DrawBezier(DeviceContext dc, POINT[] apt)
        {
            dc.PolyBezier(apt);
            dc.MoveTo(apt[0].x, apt[0].y);
            dc.LineTo(apt[1].x, apt[1].y);
            dc.MoveTo(apt[2].x, apt[2].y);
            dc.LineTo(apt[3].x, apt[3].y);
        }

        static POINT[] apt = new POINT[4];

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    int cxClient = lParam.LowWord;
                    int cyClient = lParam.HighWord;

                    apt[0].x = cxClient / 4;
                    apt[0].y = cyClient / 2;
                    apt[1].x = cxClient / 2;
                    apt[1].y = cyClient / 4;
                    apt[2].x = cxClient / 2;
                    apt[2].y = 3 * cyClient / 4;
                    apt[3].x = 3 * cxClient / 4;
                    apt[3].y = cyClient / 2;

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
                                apt[1].x = lParam.LowWord;
                                apt[1].y = lParam.HighWord;
                            }

                            if ((mk & MouseKey.RightButton) != 0)
                            {
                                apt[2].x = lParam.LowWord;
                                apt[2].y = lParam.HighWord;
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
