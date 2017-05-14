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
            const string szAppName = "Clover";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = szAppName
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Draw a Clover",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxClient, cyClient;
        static RegionHandle hRgnClip;
        const double TWO_PI = Math.PI * 2;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CursorHandle hCursor = Windows.SetCursor(CursorId.Wait);
                    Windows.ShowCursor(true);

                    hRgnClip?.Dispose();

                    RegionHandle[] hRgnTemp = new RegionHandle[6];

                    hRgnTemp[0] = Windows.CreateEllipticRegion(0, cyClient / 3, cxClient / 2, 2 * cyClient / 3);
                    hRgnTemp[1] = Windows.CreateEllipticRegion(cxClient / 2, cyClient / 3, cxClient, 2 * cyClient / 3);
                    hRgnTemp[2] = Windows.CreateEllipticRegion(cxClient / 3, 0, 2 * cxClient / 3, cyClient / 2);
                    hRgnTemp[3] = Windows.CreateEllipticRegion(cxClient / 3, cyClient / 2, 2 * cxClient / 3, cyClient);
                    hRgnTemp[4] = Windows.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnTemp[5] = Windows.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnClip = Windows.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnTemp[4].CombineRegion(hRgnTemp[0], hRgnTemp[1], CombineRegionMode.Or);
                    hRgnTemp[5].CombineRegion(hRgnTemp[2], hRgnTemp[3], CombineRegionMode.Or);
                    hRgnClip.CombineRegion(hRgnTemp[4], hRgnTemp[5], CombineRegionMode.Xor);
                    for (int i = 0; i < 6; i++)
                        hRgnTemp[i]?.Dispose();

                    Windows.SetCursor(hCursor);
                    Windows.ShowCursor(false);

                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SetViewportOrigin(cxClient / 2, cyClient / 2);
                        dc.SelectClippingRegion(hRgnClip);

                        double fRadius = Hypotenuse(cxClient / 2.0, cyClient / 2.0);

                        for (double fAngle = 0.0; fAngle < TWO_PI; fAngle += TWO_PI / 360)
                        {
                            dc.MoveTo(0, 0);
                            dc.LineTo(
                                (int)(fRadius * Math.Cos(fAngle) + 0.5),
                                (int)(-fRadius * Math.Sin(fAngle) + 0.5));
                        }
                    }
                    return 0;
                case WindowMessage.Destroy:
                    hRgnClip?.Dispose();
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static double Hypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
