// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace SysMets2
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 4-10, Pages 103-106.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new SysMets2(), "System Metrics with Scrollbar");
        }
    }

    class SysMets2 : WindowClass
    {
        int cxChar, cxCaps, cyChar, cyClient, iVscrollPos;

        protected override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.GetTextMetrics(out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily.PitchTypes & FontPitchTypes.VariablePitch) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    window.SetScrollRange(ScrollBar.Vertical, 0, Metrics.SystemMetrics.Count - 1, false);
                    window.SetScrollPosition(ScrollBar.Vertical, iVscrollPos, true);

                    return 0;
                case WindowMessage.Size:
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.VerticalScroll:
                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.LineUp:
                            iVscrollPos -= 1;
                            break;
                        case ScrollCommand.LineDown:
                            iVscrollPos += 1;
                            break;
                        case ScrollCommand.PageUp:
                            iVscrollPos -= cyClient / cyChar;
                            break;
                        case ScrollCommand.PageDown:
                            iVscrollPos += cyClient / cyChar;
                            break;
                        case ScrollCommand.ThumbPosition:
                            iVscrollPos = wParam.HighWord;
                            break;
                    }

                    iVscrollPos = Math.Max(0, Math.Min(iVscrollPos, Metrics.SystemMetrics.Count - 1));

                    if (iVscrollPos != window.GetScrollPosition(ScrollBar.Vertical))
                    {
                        window.SetScrollPosition(ScrollBar.Vertical, iVscrollPos, true);
                        window.Invalidate(true);
                    }
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        int i = 0;
                        foreach (SystemMetric metric in Metrics.SystemMetrics.Keys)
                        {
                            int y = cyChar * (i - iVscrollPos);

                            dc.TextOut(new Point(0, y), metric.ToString().AsSpan());
                            dc.TextOut(new Point(22 * cxCaps, y), Metrics.SystemMetrics[metric].AsSpan());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Right, TextAlignment.Vertical.Top));
                            dc.TextOut(new Point(22 * cxCaps + 40 * cxChar, y), Windows.GetSystemMetrics(metric).ToString().AsSpan());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Left, TextAlignment.Vertical.Top));
                            i++;
                        }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
