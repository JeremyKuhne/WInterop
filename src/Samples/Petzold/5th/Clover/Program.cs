// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Clover
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-27, Pages 205-208.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new Clover(), "Draw a Clover");
        }
    }

    class Clover : WindowClass
    {
        int cxClient, cyClient;
        RegionHandle hRgnClip;
        const double TWO_PI = Math.PI * 2;

        public Clover() : base(backgroundBrush: StockBrush.White) { }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CursorHandle hCursor = Windows.SetCursor(CursorId.Wait);
                    Windows.ShowCursor(true);

                    hRgnClip.Dispose();

                    Span<RegionHandle> hRgnTemp = stackalloc RegionHandle[6];

                    hRgnTemp[0] = Gdi.CreateEllipticRegion(0, cyClient / 3, cxClient / 2, 2 * cyClient / 3);
                    hRgnTemp[1] = Gdi.CreateEllipticRegion(cxClient / 2, cyClient / 3, cxClient, 2 * cyClient / 3);
                    hRgnTemp[2] = Gdi.CreateEllipticRegion(cxClient / 3, 0, 2 * cxClient / 3, cyClient / 2);
                    hRgnTemp[3] = Gdi.CreateEllipticRegion(cxClient / 3, cyClient / 2, 2 * cxClient / 3, cyClient);
                    hRgnTemp[4] = Gdi.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnTemp[5] = Gdi.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnClip = Gdi.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnTemp[4].CombineRegion(hRgnTemp[0], hRgnTemp[1], CombineRegionMode.Or);
                    hRgnTemp[5].CombineRegion(hRgnTemp[2], hRgnTemp[3], CombineRegionMode.Or);
                    hRgnClip.CombineRegion(hRgnTemp[4], hRgnTemp[5], CombineRegionMode.Xor);
                    for (int i = 0; i < 6; i++)
                        hRgnTemp[i].Dispose();

                    Windows.SetCursor(hCursor);
                    Windows.ShowCursor(false);

                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SetViewportOrigin(cxClient / 2, cyClient / 2);
                        dc.SelectClippingRegion(hRgnClip);

                        double fRadius = Hypotenuse(cxClient / 2.0, cyClient / 2.0);

                        for (double fAngle = 0.0; fAngle < TWO_PI; fAngle += TWO_PI / 360)
                        {
                            dc.MoveTo(default);
                            dc.LineTo(new Point(
                                (int)(fRadius * Math.Cos(fAngle) + 0.5),
                                (int)(-fRadius * Math.Sin(fAngle) + 0.5)));
                        }
                    }
                    return 0;
                case MessageType.Destroy:
                    hRgnClip.Dispose();
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }

        static double Hypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
