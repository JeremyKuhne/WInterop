// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#define GDIPLUS

#if GDIPLUS
using WInterop.GdiPlus;
#endif

using System;
using System.Runtime.InteropServices;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.SystemInformation.Types;
using WInterop.Windows;
using WInterop.Windows.Types;
using WInterop.Console;

namespace Clock
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 8-5, Pages 346-350.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Hack for launching as a .NET Core Windows Application
            ConsoleMethods.TryFreeConsole();

#if GDIPLUS
            UIntPtr token = GdiPlusMethods.Startup();
#endif
            const string szAppName = "Clock";

            ModuleInstance module = Marshal.GetHINSTANCE(typeof(Program).Module);
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
                "Analog Clock",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }

#if GDIPLUS
            GdiPlusMethods.Shutdown(token);
#endif
        }

        static void SetIsotropic(DeviceContext hdc, int cxClient, int cyClient)
        {
            hdc.SetMapMode(MapMode.Isotropic);
            hdc.SetWindowExtents(1000, 1000);
            hdc.SetViewportExtents(cxClient / 2, -cyClient / 2);
            hdc.SetViewportOrigin(cxClient / 2, cyClient / 2);
        }

        static void RotatePoint(POINT[] pt, int iNum, int iAngle)
        {
            POINT ptTemp;
            for (int i = 0; i < iNum; i++)
            {
                ptTemp.x = (int)(pt[i].x * Math.Cos(TWOPI * iAngle / 360) +
                pt[i].y * Math.Sin(TWOPI * iAngle / 360));
                ptTemp.y = (int)(pt[i].y * Math.Cos(TWOPI * iAngle / 360) -
                pt[i].x * Math.Sin(TWOPI * iAngle / 360));
                pt[i] = ptTemp;
            }
        }

        static void DrawClock(DeviceContext dc)
        {
            int iAngle;
            POINT[] pt = new POINT[3];

#if GDIPLUS
            using (var graphics = GdiPlusMethods.CreateGraphics(dc))
            using (var brush = GdiPlusMethods.CreateSolidBrush(new ARGB(0, 0, 0)))
            {
                GdiPlusMethods.SetSmoothingMode(graphics, SmoothingMode.AntiAlias);
#else
                dc.SelectObject(StockBrush.Black);
#endif
                for (iAngle = 0; iAngle < 360; iAngle += 6)
                {
                    pt[0].x = 0;
                    pt[0].y = 900;
                    RotatePoint(pt, 1, iAngle);
                    pt[2].x = pt[2].y = iAngle % 5 != 0 ? 33 : 100;
                    pt[0].x -= pt[2].x / 2;
                    pt[0].y -= pt[2].y / 2;
                    pt[1].x = pt[0].x + pt[2].x;
                    pt[1].y = pt[0].y + pt[2].y;

#if GDIPLUS
                    GdiPlusMethods.FillEllipse(graphics, brush, pt[0].x, pt[0].y, pt[1].x - pt[0].x, pt[1].y - pt[0].y);
#else
                    dc.Ellipse(pt[0].x, pt[0].y, pt[1].x, pt[1].y);
#endif
                }
#if GDIPLUS
            }
#endif
        }

        static void DrawHands(DeviceContext dc, SYSTEMTIME pst, bool fChange)
        {
            int[] iAngle =
            {
                (pst.wHour * 30) % 360 + pst.wMinute / 2,
                pst.wMinute * 6,
                pst.wSecond * 6
            };

            POINT[][] pt =
            {
                new POINT[] { new POINT(0, -150), new POINT(100, 0), new POINT(0, 600), new POINT(-100, 0), new POINT(0, -150) },
                new POINT[] { new POINT(0, -200), new POINT(50, 0), new POINT(0, 800), new POINT(-50, 0), new POINT(0, -200), },
                new POINT[] { new POINT(0, 0), new POINT(0, 0), new POINT(0, 0), new POINT(0, 0), new POINT(0, 800) }
            };

            COLORREF color = GdiMethods.GetPenColor(dc.GetCurrentPen());
            bool erase = (color != new COLORREF());

#if GDIPLUS
            using (var graphics = GdiPlusMethods.CreateGraphics(dc))
            {
                if (erase)
                {
                    using (var brush = GdiPlusMethods.CreateSolidBrush(color))
                    {
                        GdiPlusMethods.FillEllipse(graphics, brush, -830, -830, 1660, 1660);
                    }
                    return;
                }

                using (var pen = GdiPlusMethods.CreatePen(color))
                {
                    GdiPlusMethods.SetSmoothingMode(graphics, SmoothingMode.HighQuality);

#endif

                    for (int i = fChange ? 0 : 2; i < 3; i++)
                    {
                        RotatePoint(pt[i], 5, iAngle[i]);

#if GDIPLUS
                        GdiPlusMethods.DrawLines(graphics, pen, pt[i]);
#else
                        dc.Polyline(pt[i]);
#endif
                    }
#if GDIPLUS
                }
            }
#endif
        }

        const int ID_TIMER = 1;
        const double TWOPI = Math.PI * 2;
        static int cxClient, cyClient;
        static SYSTEMTIME stPrevious;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    window.SetTimer(ID_TIMER, 1000);
                    stPrevious = Windows.GetLocalTime();
                    return 0;
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.Timer:
                    SYSTEMTIME st = Windows.GetLocalTime();
                    bool fChange = st.wHour != stPrevious.wHour ||
                        st.wMinute != stPrevious.wMinute;
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        SetIsotropic(dc, cxClient, cyClient);
                        dc.SelectObject(StockPen.White);
                        DrawHands(dc, stPrevious, fChange);
                        dc.SelectObject(StockPen.Black);
                        DrawHands(dc, st, true);
                    }
                    stPrevious = st;
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        SetIsotropic(dc, cxClient, cyClient);
                        DrawClock(dc);
                        DrawHands(dc, stPrevious, true);
                    }
                    return 0;
                case WindowMessage.Destroy:
                    window.KillTimer(ID_TIMER);
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
