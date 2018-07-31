// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace SysMets1
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 4-5, Pages 91-93.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new SysMets1(), "System Metrics");
        }
    }

    class SysMets1 : WindowClass
    {
        int cxChar, cxCaps, cyChar;

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
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        int i = 0;
                        foreach (SystemMetric metric in Metrics.SystemMetrics.Keys)
                        {
                            dc.TextOut(new Point(0, cyChar * i), metric.ToString().AsSpan());
                            dc.TextOut(new Point(22 * cxCaps, cyChar * i), Metrics.SystemMetrics[metric].AsSpan());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Right, TextAlignment.Vertical.Top));
                            dc.TextOut(new Point(22 * cxCaps + 40 * cxChar, cyChar * i), Windows.GetSystemMetrics(metric).ToString().AsSpan());
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
