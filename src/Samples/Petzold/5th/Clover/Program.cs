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

namespace HelloWin
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
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                ClassName = szAppName
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                szAppName,
                "Draw a Clover",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
            GdiMethods.UpdateWindow(window);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }
        }

        static int cxClient, cyClient;
        static RegionHandle hRgnClip;
        const double TWO_PI = Math.PI * 2;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CursorHandle hCursor = ResourceMethods.SetCursor(ResourceMethods.LoadCursor(CursorId.IDC_WAIT));
                    ResourceMethods.ShowCursor(true);

                    hRgnClip?.Dispose();

                    RegionHandle[] hRgnTemp = new RegionHandle[6];

                    hRgnTemp[0] = GdiMethods.CreateEllipticRegion(0, cyClient / 3, cxClient / 2, 2 * cyClient / 3);
                    hRgnTemp[1] = GdiMethods.CreateEllipticRegion(cxClient / 2, cyClient / 3, cxClient, 2 * cyClient / 3);
                    hRgnTemp[2] = GdiMethods.CreateEllipticRegion(cxClient / 3, 0, 2 * cxClient / 3, cyClient / 2);
                    hRgnTemp[3] = GdiMethods.CreateEllipticRegion(cxClient / 3, cyClient / 2, 2 * cxClient / 3, cyClient);
                    hRgnTemp[4] = GdiMethods.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnTemp[5] = GdiMethods.CreateRectangleRegion(0, 0, 1, 1);
                    hRgnClip = GdiMethods.CreateRectangleRegion(0, 0, 1, 1);
                    GdiMethods.CombineRegion(hRgnTemp[4], hRgnTemp[0], hRgnTemp[1], CombineRegionMode.RGN_OR);
                    GdiMethods.CombineRegion(hRgnTemp[5], hRgnTemp[2], hRgnTemp[3], CombineRegionMode.RGN_OR);
                    GdiMethods.CombineRegion(hRgnClip, hRgnTemp[4], hRgnTemp[5], CombineRegionMode.RGN_XOR);
                    for (int i = 0; i < 6; i++)
                        hRgnTemp[i]?.Dispose();

                    ResourceMethods.SetCursor(hCursor);
                    ResourceMethods.ShowCursor(false);

                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        GdiMethods.SetViewportOrigin(dc, cxClient / 2, cyClient / 2);
                        GdiMethods.SelectClippingRegion(dc, hRgnClip);

                        double fRadius = Hypotenuse(cxClient / 2.0, cyClient / 2.0);

                        for (double fAngle = 0.0; fAngle < TWO_PI; fAngle += TWO_PI / 360)
                        {
                            GdiMethods.MoveTo(dc, 0, 0);
                            GdiMethods.LineTo(
                                dc,
                                (int)(fRadius * Math.Cos(fAngle) + 0.5),
                                (int)(-fRadius * Math.Sin(fAngle) + 0.5));
                        }
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    hRgnClip?.Dispose();
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static double Hypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
