// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Clover
{
    /// <summary>
    ///  Sample from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    ///  Figure 5-27, Pages 205-208.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Windows.CreateMainWindowAndRun(new Clover(), "Draw a Clover");
        }
    }

    internal class Clover : WindowClass
    {
        private int _cxClient, _cyClient;
        private RegionHandle _hRgnClip;
        private const double TWO_PI = Math.PI * 2;

        public Clover() : base(backgroundBrush: StockBrush.White) { }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    _cxClient = lParam.LowWord;
                    _cyClient = lParam.HighWord;

                    CursorHandle hCursor = Windows.SetCursor(CursorId.Wait);
                    Windows.ShowCursor(true);

                    _hRgnClip.Dispose();

                    Span<RegionHandle> hRgnTemp = stackalloc RegionHandle[6];

                    hRgnTemp[0] = Gdi.CreateEllipticRegion(Rectangle.FromLTRB(0, _cyClient / 3, _cxClient / 2, 2 * _cyClient / 3));
                    hRgnTemp[1] = Gdi.CreateEllipticRegion(Rectangle.FromLTRB(_cxClient / 2, _cyClient / 3, _cxClient, 2 * _cyClient / 3));
                    hRgnTemp[2] = Gdi.CreateEllipticRegion(Rectangle.FromLTRB(_cxClient / 3, 0, 2 * _cxClient / 3, _cyClient / 2));
                    hRgnTemp[3] = Gdi.CreateEllipticRegion(Rectangle.FromLTRB(_cxClient / 3, _cyClient / 2, 2 * _cxClient / 3, _cyClient));
                    hRgnTemp[4] = Gdi.CreateRectangleRegion(Rectangle.FromLTRB(0, 0, 1, 1));
                    hRgnTemp[5] = Gdi.CreateRectangleRegion(Rectangle.FromLTRB(0, 0, 1, 1));
                    _hRgnClip = Gdi.CreateRectangleRegion(Rectangle.FromLTRB(0, 0, 1, 1));
                    hRgnTemp[4].CombineRegion(hRgnTemp[0], hRgnTemp[1], CombineRegionMode.Or);
                    hRgnTemp[5].CombineRegion(hRgnTemp[2], hRgnTemp[3], CombineRegionMode.Or);
                    _hRgnClip.CombineRegion(hRgnTemp[4], hRgnTemp[5], CombineRegionMode.Xor);
                    for (int i = 0; i < 6; i++)
                        hRgnTemp[i].Dispose();

                    Windows.SetCursor(hCursor);
                    Windows.ShowCursor(false);

                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SetViewportOrigin(new Point(_cxClient / 2, _cyClient / 2));
                        dc.SelectClippingRegion(_hRgnClip);

                        double fRadius = Hypotenuse(_cxClient / 2.0, _cyClient / 2.0);

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
                    _hRgnClip.Dispose();
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }

        private static double Hypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
