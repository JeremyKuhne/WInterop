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

namespace AltWind
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-21, Pages 171-173.
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
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                ClassName = "AltWind"
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                "AltWind",
                "Alternate and Winding Fill Modes",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
            GdiMethods.UpdateWindow(window);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }
        }

        static POINT[] aptFigure =
        {
            new POINT(10, 70),
            new POINT(50, 70),
            new POINT(50, 10),
            new POINT(90, 10),
            new POINT(90, 50),
            new POINT(30, 50),
            new POINT(30, 90),
            new POINT(70, 90),
            new POINT(70, 30),
            new POINT(10, 30)
        };

        static int cxClient, cyClient;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.WM_PAINT:
                    POINT[] apt = new POINT[10];
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        GdiMethods.SelectObject(dc, GdiMethods.GetStockBrush(StockBrush.GRAY_BRUSH));
                        for (int i = 0; i < 10; i++)
                        {
                            apt[i].x = cxClient * aptFigure[i].x / 200;
                            apt[i].y = cyClient * aptFigure[i].y / 100;
                        }

                        GdiMethods.SetPolyFillMode(dc, PolyFillMode.ALTERNATE);
                        GdiMethods.Polygon(dc, apt);

                        for (int i = 0; i < 10; i++)
                        {
                            apt[i].x += cxClient / 2;
                        }
                        GdiMethods.SetPolyFillMode(dc, PolyFillMode.WINDING);
                        GdiMethods.Polygon(dc, apt);
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
