// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop;
using WInterop.Gdi;
using WInterop.SystemInformation;
using WInterop.Windows;

namespace DigClock
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 8-3, Pages 338-342.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new DigClock(), "Digital Clock");
        }
    }

    class DigClock : WindowClass
    {
        static BOOL[,] fSevenSegment =
        {
            { 1, 1, 1, 0, 1, 1, 1 }, // 0
            { 0, 0, 1, 0, 0, 1, 0 }, // 1
            { 1, 0, 1, 1, 1, 0, 1 }, // 2
            { 1, 0, 1, 1, 0, 1, 1 }, // 3
            { 0, 1, 1, 1, 0, 1, 0 }, // 4
            { 1, 1, 0, 1, 0, 1, 1 }, // 5
            { 1, 1, 0, 1, 1, 1, 1 }, // 6
            { 1, 0, 1, 0, 0, 1, 0 }, // 7
            { 1, 1, 1, 1, 1, 1, 1 }, // 8
            { 1, 1, 1, 1, 0, 1, 1 }  // 9
        };

        static Point[][] ptSegment =
        {
            new Point[] { new Point(7, 6), new Point(11, 2), new Point(31, 2), new Point(35, 6), new Point(31, 10), new Point(11, 10) },
            new Point[] { new Point(6, 7), new Point(10, 11), new Point(10, 31), new Point(6, 35), new Point(2, 31), new Point(2, 11) },
            new Point[] { new Point(36, 7), new Point(40, 11), new Point(40, 31), new Point(36, 35), new Point(32, 31), new Point(32, 11) },
            new Point[] { new Point(7, 36), new Point(11, 32), new Point(31, 32), new Point(35, 36), new Point(31, 40), new Point(11, 40) },
            new Point[] { new Point(6, 37), new Point(10, 41), new Point(10, 61), new Point(6, 65), new Point(2, 61), new Point(2, 41) },
            new Point[] { new Point(36, 37), new Point(40, 41), new Point(40, 61), new Point(36, 65), new Point(32, 61), new Point(32, 41) },
            new Point[] { new Point(7, 66), new Point(11, 62), new Point(31, 62), new Point(35, 66), new Point(31, 70), new Point(11, 70) }
        };

        static void DisplayDigit(DeviceContext hdc, int iNumber)
        {
            for (int iSeg = 0; iSeg < 7; iSeg++)
                if (fSevenSegment[iNumber, iSeg])
                    hdc.Polygon(ptSegment[iSeg]);
        }

        static void DisplayTwoDigits(DeviceContext hdc, int iNumber, bool fSuppress)
        {
            if (!fSuppress || (iNumber / 10 != 0))
                DisplayDigit(hdc, iNumber / 10);
            hdc.OffsetWindowOrigin(-42, 0);
            DisplayDigit(hdc, iNumber % 10);
            hdc.OffsetWindowOrigin(-42, 0);
        }

        static void DisplayColon(DeviceContext hdc)
        {
            Point[][] ptColon =
            {
                new Point[] { new Point(2, 21), new Point(6, 17), new Point(10, 21), new Point(6, 25) },
                new Point[] { new Point(2, 51), new Point(6, 47), new Point(10, 51), new Point(6, 55) }
            };
            hdc.Polygon(ptColon[0]);
            hdc.Polygon(ptColon[1]);
            hdc.OffsetWindowOrigin(-12, 0);
        }

        static void DisplayTime(DeviceContext hdc, BOOL f24Hour, BOOL fSuppress)
        {
            SYSTEMTIME st = Windows.GetLocalTime();
            if (f24Hour)
                DisplayTwoDigits(hdc, st.wHour, fSuppress);
            else
                DisplayTwoDigits(hdc, (st.wHour %= 12) != 0 ? st.wHour : 12, fSuppress);
            DisplayColon(hdc);
            DisplayTwoDigits(hdc, st.wMinute, false);
            DisplayColon(hdc);
            DisplayTwoDigits(hdc, st.wSecond, false);
        }

        const int ID_TIMER = 1;
        BrushHandle hBrushRed;
        int cxClient, cyClient;
        bool f24Hour, fSuppress;

        protected override LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    hBrushRed = Gdi.CreateSolidBrush(Color.Red);
                    window.SetTimer(ID_TIMER, 1000);
                    return 0;
                case MessageType.SettingChange:
                    f24Hour = Windows.LocaleInfo.GetIs24HourClock();
                    fSuppress = Windows.LocaleInfo.GetHoursHaveLeadingZeros();
                    window.Invalidate(true);
                    return 0;
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.Timer:
                    window.Invalidate(true);
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SetMapMode(MapMode.Isotropic);
                        dc.SetWindowExtents(276, 72);
                        dc.SetViewportExtents(cxClient, cyClient);
                        dc.SetWindowOrigin(138, 36);
                        dc.SetViewportOrigin(cxClient / 2, cyClient / 2);
                        dc.SelectObject(StockPen.Null);
                        dc.SelectObject(hBrushRed);
                        DisplayTime(dc, f24Hour, fSuppress);
                    }
                    return 0;
                case MessageType.Destroy:
                    window.KillTimer(ID_TIMER);
                    hBrushRed.Dispose();
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
