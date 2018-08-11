// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
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
            Windows.CreateMainWindowAndRun(new AltWind(), "Alternate and Winding Fill Modes");
        }
    }

    public class AltWind : WindowClass
    {
        Point[] aptFigure =
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

        int cxClient, cyClient;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.Paint:
                    Span<Point> apt = stackalloc Point[10];
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
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
