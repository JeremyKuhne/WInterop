// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace WhatClr
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 3-1, Pages 44-46.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new WhatColor(), "What Color");
        }
    }

    class WhatColor : WindowClass
    {
        public WhatColor() : base(backgroundBrush: BrushHandle.NoBrush) { }

        static void FindWindowSize(ref int cxWindow, ref int cyWindow)
        {
            TEXTMETRIC tm;
            using (DeviceContext dcScreen = Gdi.CreateInformationContext("DISPLAY", null))
            {
                dcScreen.GetTextMetrics(out tm);
            }

            cxWindow = 2 * Windows.GetSystemMetrics(SystemMetric.CXBORDER) + 12 * tm.tmAveCharWidth;
            cyWindow = 2 * Windows.GetSystemMetrics(SystemMetric.CYBORDER) + Windows.GetSystemMetrics(SystemMetric.CYCAPTION) + 2 * tm.tmHeight;
        }

        const int ID_TIMER = 1;
        DeviceContext dcScreen;
        Color cr, crLast;
        
        protected override LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    dcScreen = Gdi.CreateDeviceContext("DISPLAY", null);
                    window.SetTimer(ID_TIMER, 100);
                    return 0;
                case MessageType.Timer:
                    Point pt = Windows.GetCursorPosition();
                    cr = dcScreen.GetPixel(pt);
                    // Not sure why the sample did this
                    // dcScreen.SetPixel(pt, 0);
                    if (cr != crLast)
                    {
                        crLast = cr;
                        window.GetDeviceContext().SetBackgroundColor(cr);
                        window.Invalidate(erase: false);
                    }
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        Rectangle rc = window.GetClientRectangle();
                        dc.FillRectangle(rc, dc.GetCurrentBrush());
                        dc.DrawText($"0x{cr.R:X2} 0x{cr.G:X2} 0x{cr.B:X2}".AsSpan(), rc,
                            TextFormat.SingleLine | TextFormat.Center | TextFormat.VerticallyCenter);
                    }
                    return 0;
                case MessageType.Destroy:
                    dcScreen.Dispose();
                    window.KillTimer(ID_TIMER);
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
