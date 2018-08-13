// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// #define GDIPLUS

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
        void SetIsotropic(DeviceContext hdc)
        {
            hdc.SetMappingMode(MappingMode.Isotropic);
            hdc.SetWindowExtents(new Size(1000, 1000));
            hdc.SetViewportExtents(new Size(_clientSize.Width / 2, -_clientSize.Height / 2));
            hdc.SetViewportOrigin(new Point(_clientSize.Width / 2, _clientSize.Height / 2));
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

        void DrawHands(DeviceContext dc, SystemTime time, bool erase = false, bool drawHourAndMinuteHands = true)
        {
            int[] handAngles =
            {
                (time.Hour * 30) % 360 + time.Minute / 2,
                time.Minute * (360 / 60),
                time.Second * (360 / 60)
            };

            Point[][] handPoints =
            {
                new Point[] { new Point(0, -150), new Point(100, 0), new Point(0, 600), new Point(-100, 0), new Point(0, -150) },
                new Point[] { new Point(0, -200), new Point(50, 0), new Point(0, 800), new Point(-50, 0), new Point(0, -200), },
                new Point[] { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 800) }
            };

#if GDIPLUS
            using (var graphics = GdiPlus.CreateGraphics(dc))
            {
                if (erase)
                {
                    using (var brush = graphics.CreateSolidBrush(Color.White))
                    {
                        graphics.FillEllipse(brush, -830, -830, 1660, 1660);
                    }
                    return;
                }

                using (var pen = graphics.CreatePen(Color.Black))
                {
                    graphics.SetSmoothingMode(SmoothingMode.HighQuality);
#else
                    dc.SelectObject(erase ? StockPen.White : StockPen.Black);
#endif

                    for (int i = drawHourAndMinuteHands ? 0 : 2; i < 3; i++)
                    {
                        RotatePoint(handPoints[i], 5, handAngles[i]);

#if GDIPLUS
                        graphics.DrawLines(pen, handPoints[i]);
#else
                        dc.Polyline(handPoints[i]);
#endif
                    }
#if GDIPLUS
                }
            }
#endif
        }

        const int ID_TIMER = 1;
        const double TWOPI = Math.PI * 2;
        Size _clientSize;
        SystemTime _previousTime;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    window.SetTimer(ID_TIMER, 1000);
                    _previousTime = SystemInformation.GetLocalTime();
                    return 0;
                case MessageType.Size:
                    _clientSize = new Message.Size(wParam, lParam).NewSize;
                    return 0;
                case MessageType.Timer:
                    SystemTime time = SystemInformation.GetLocalTime();
                    bool drawAllHands = time.Hour != _previousTime.Hour ||
                        time.Minute != _previousTime.Minute;
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        SetIsotropic(dc);
                        DrawHands(dc, _previousTime, erase: true, drawAllHands);
                        DrawHands(dc, time);
                    }
                    _previousTime = time;
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        SetIsotropic(dc);
                        DrawClock(dc);
                        DrawHands(dc, _previousTime);
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
