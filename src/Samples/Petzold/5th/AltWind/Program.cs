// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;

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
            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = "AltWind"
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                "AltWind",
                "Alternate and Winding Fill Modes",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static Point[] aptFigure =
        {
            new Point(10, 70),
            new Point(50, 70),
            new Point(50, 10),
            new Point(90, 10),
            new Point(90, 50),
            new Point(30, 50),
            new Point(30, 90),
            new Point(70, 90),
            new Point(70, 30),
            new Point(10, 30)
        };

        static int cxClient, cyClient;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.Paint:
                    Point[] apt = new Point[10];
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockBrush.Gray);
                        for (int i = 0; i < 10; i++)
                        {
                            apt[i].X = cxClient * aptFigure[i].X / 200;
                            apt[i].Y = cyClient * aptFigure[i].Y / 100;
                        }

                        dc.SetPolyFillMode(PolyFillMode.Alternate);
                        dc.Polygon(apt);

                        for (int i = 0; i < 10; i++)
                        {
                            apt[i].X += cxClient / 2;
                        }
                        dc.SetPolyFillMode(PolyFillMode.Winding);
                        dc.Polygon(apt);
                    }

                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
