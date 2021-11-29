// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace WhatClr;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 3-1, Pages 44-46.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new WhatColor(), "What Color");
    }
}

internal class WhatColor : WindowClass
{
    public WhatColor() : base(backgroundBrush: BrushHandle.NoBrush) { }

    private static void FindWindowSize(ref int cxWindow, ref int cyWindow)
    {
        TextMetrics tm;
        using (DeviceContext dcScreen = Gdi.CreateInformationContext("DISPLAY", null))
        {
            dcScreen.GetTextMetrics(out tm);
        }

        cxWindow = 2 * Windows.GetSystemMetrics(SystemMetric.BorderWidth) + 12 * tm.AverageCharWidth;
        cyWindow = 2 * Windows.GetSystemMetrics(SystemMetric.BorderHeight) + Windows.GetSystemMetrics(SystemMetric.CaptionAreaHeight) + 2 * tm.Height;
    }

    private const int ID_TIMER = 1;
    private Color cr, crLast;

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Create:
                window.SetTimer(ID_TIMER, 100);
                return 0;
            case MessageType.Timer:
                Point pt = Windows.GetCursorPosition();

                using (DeviceContext dc = Gdi.CreateDesktopDeviceContext())
                {
                    cr = dc.GetPixel(pt);

                    if (cr != crLast)
                    {
                        crLast = cr;
                        window.GetDeviceContext().SetBackgroundColor(cr);
                        window.Invalidate(erase: false);
                    }
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
                window.KillTimer(ID_TIMER);
                break;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
