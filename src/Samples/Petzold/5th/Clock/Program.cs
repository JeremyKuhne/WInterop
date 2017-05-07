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
using WInterop.Support;
using WInterop.SystemInformation.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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
            const string szAppName = "Clock";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.IDI_APPLICATION,
                Cursor = CursorId.IDC_ARROW,
                Background = StockBrush.WHITE_BRUSH,
                ClassName = szAppName
            };

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Analog Clock",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            window.ShowWindow(ShowWindowCommand.SW_SHOWNORMAL);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static void SetIsotropic(DeviceContext hdc, int cxClient, int cyClient)
        {
            hdc.SetMapMode(MapMode.MM_ISOTROPIC);
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

        static void DrawClock(DeviceContext hdc)
        {
            int iAngle;
            POINT[] pt = new POINT[3];
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
                hdc.SelectObject(StockBrush.BLACK_BRUSH);
                hdc.Ellipse(pt[0].x, pt[0].y, pt[1].x, pt[1].y);
            }
        }

        static void DrawHands(DeviceContext hdc, SYSTEMTIME pst, BOOL fChange)
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

            for (int i = fChange? 0 : 2 ; i< 3 ; i++)
            {
                RotatePoint(pt[i], 5, iAngle[i]);
                hdc.Polyline(pt[i]);
            }
        }

        const int ID_TIMER = 1;
        const double TWOPI = Math.PI * 2;
        static int cxClient, cyClient;
        static SYSTEMTIME stPrevious;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:
                    window.SetTimer(ID_TIMER, 1000);
                    stPrevious = Windows.GetLocalTime();
                    return 0;
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.WM_TIMER:
                    SYSTEMTIME st = Windows.GetLocalTime();
                    bool fChange = st.wHour != stPrevious.wHour ||
                        st.wMinute != stPrevious.wMinute;
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        SetIsotropic(dc, cxClient, cyClient);
                        dc.SelectObject(StockPen.WHITE_PEN);
                        DrawHands(dc, stPrevious, fChange);
                        dc.SelectObject(StockPen.BLACK_PEN);
                        DrawHands(dc, st, true);
                    }
                    stPrevious = st;
                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        SetIsotropic(dc, cxClient, cyClient);
                        DrawClock(dc);
                        DrawHands(dc, stPrevious, true);
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    window.KillTimer(ID_TIMER);
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
