// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
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
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                ClassExtraBytes = 0,
                WindowExtraBytes = 0,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                MenuName = null,
                ClassName = "Bezier"
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                "Bezier",
                "Bezier Splines",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        static void DrawBezier(DeviceContext dc, POINT[] apt)
        {
            GdiMethods.PolyBezier(dc, apt);
            GdiMethods.MoveTo(dc, apt[0].x, apt[0].y);
            GdiMethods.LineTo(dc, apt[1].x, apt[1].y);
            GdiMethods.MoveTo(dc, apt[2].x, apt[2].y);
            GdiMethods.LineTo(dc, apt[3].x, apt[3].y);
        }

        static POINT[] apt = new POINT[4];

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
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

                case MessageType.WM_LBUTTONDOWN:
                case MessageType.WM_RBUTTONDOWN:
                case MessageType.WM_MOUSEMOVE:
                    MouseKeyState mk = (MouseKeyState)wParam.LowWord;
                    if ((mk & (MouseKeyState.MK_LBUTTON | MouseKeyState.MK_RBUTTON)) != 0)
                    {
                        using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                        {
                            GdiMethods.SelectObject(dc, GdiMethods.GetStockPen(StockPen.WHITE_PEN));
                            DrawBezier(dc, apt);

                            if ((mk & MouseKeyState.MK_LBUTTON) != 0)
                            {
                                apt[1].x = lParam.LowWord;
                                apt[1].y = lParam.HighWord;
                            }

                            if ((mk & MouseKeyState.MK_RBUTTON) != 0)
                            {
                                apt[2].x = lParam.LowWord;
                                apt[2].y = lParam.HighWord;
                            }

                            GdiMethods.SelectObject(dc, GdiMethods.GetStockPen(StockPen.BLACK_PEN));
                            DrawBezier(dc, apt);
                        }
                    }
                    return 0;
                case MessageType.WM_PAINT:
                    GdiMethods.InvalidateRectangle(window, true);
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        DrawBezier(dc, apt);
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
