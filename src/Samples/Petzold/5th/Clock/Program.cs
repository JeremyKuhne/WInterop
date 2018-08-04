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
using WInterop.Gdi;
using WInterop.SystemInformation;
using WInterop.Windows;
using System.Drawing;

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
#if GDIPLUS
            UIntPtr token = GdiPlusMethods.Startup();
#endif

            Windows.CreateMainWindowAndRun(new Clock(), "Analog Clock");

#if GDIPLUS
            GdiPlusMethods.Shutdown(token);
#endif
        }
    }

    class Clock : WindowClass
    {
        public Clock() : base(backgroundBrush: StockBrush.White) { }

        void SetIsotropic(DeviceContext hdc, int cxClient, int cyClient)
        {
            hdc.SetMapMode(MapMode.Isotropic);
            hdc.SetWindowExtents(1000, 1000);
            hdc.SetViewportExtents(cxClient / 2, -cyClient / 2);
            hdc.SetViewportOrigin(cxClient / 2, cyClient / 2);
        }

        void RotatePoint(Point[] pt, int iNum, int iAngle)
        {
             for (int i = 0; i < iNum; i++)
            {
                pt[i] = new Point
                (
                    (int)(pt[i].X * Math.Cos(TWOPI * iAngle / 360) + pt[i].Y * Math.Sin(TWOPI * iAngle / 360)),
                    (int)(pt[i].Y * Math.Cos(TWOPI * iAngle / 360) - pt[i].X * Math.Sin(TWOPI * iAngle / 360))
                );
            }
        }

        void DrawClock(DeviceContext dc)
        {
            int iAngle;
            Point[] pt = new Point[3];

#if GDIPLUS
            using (var graphics = GdiPlusMethods.CreateGraphics(dc))
            using (var brush = GdiPlusMethods.CreateSolidBrush(Color.Black))
            {
                GdiPlusMethods.SetSmoothingMode(graphics, SmoothingMode.AntiAlias);
#else
                dc.SelectObject(StockBrush.Black);
#endif
                for (iAngle = 0; iAngle < 360; iAngle += 6)
                {
                    pt[0].X = 0;
                    pt[0].Y = 900;
                    RotatePoint(pt, 1, iAngle);
                    pt[2].X = pt[2].Y = iAngle % 5 != 0 ? 33 : 100;
                    pt[0].X -= pt[2].X / 2;
                    pt[0].Y -= pt[2].Y / 2;
                    pt[1].X = pt[0].X + pt[2].X;
                    pt[1].Y = pt[0].Y + pt[2].Y;

#if GDIPLUS
                    GdiPlusMethods.FillEllipse(graphics, brush, pt[0].X, pt[0].Y, pt[1].X - pt[0].X, pt[1].Y - pt[0].Y);
#else
                    dc.Ellipse(Rectangle.FromLTRB(pt[0].X, pt[0].Y, pt[1].X, pt[1].Y));
#endif
                }
#if GDIPLUS
            }
#endif
        }

        void DrawHands(DeviceContext dc, SYSTEMTIME pst, bool fChange)
        {
            int[] iAngle =
            {
                (pst.wHour * 30) % 360 + pst.wMinute / 2,
                pst.wMinute * 6,
                pst.wSecond * 6
            };

            Point[][] pt =
            {
                new Point[] { new Point(0, -150), new Point(100, 0), new Point(0, 600), new Point(-100, 0), new Point(0, -150) },
                new Point[] { new Point(0, -200), new Point(50, 0), new Point(0, 800), new Point(-50, 0), new Point(0, -200), },
                new Point[] { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 800) }
            };

            Color color = Gdi.GetPenColor(dc.GetCurrentPen());
            bool erase = color.Is(Color.White);

#if GDIPLUS
            using (var graphics = GdiPlus.CreateGraphics(dc))
            {
                if (erase)
                {
                    using (var brush = graphics.CreateSolidBrush(color))
                    {
                        graphics.FillEllipse(brush, -830, -830, 1660, 1660);
                    }
                    return;
                }

                using (var pen = graphics.CreatePen(color))
                {
                    graphics.SetSmoothingMode(SmoothingMode.HighQuality);

#endif

                    for (int i = fChange ? 0 : 2; i < 3; i++)
                    {
                        RotatePoint(pt[i], 5, iAngle[i]);

#if GDIPLUS
                        graphics.DrawLines(pen, pt[i]);
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

        protected override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
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
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
