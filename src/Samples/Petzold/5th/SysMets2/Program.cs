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
using WInterop.Windows;
using WInterop.Windows.Types;

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
            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = "SysMets2"
            };

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                "SysMets2",
                "Get System Metrics No. 2",
                WindowStyle.OverlappedWindow | WindowStyle.VerticalScroll);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxChar, cxCaps, cyChar, cyClient, iVscrollPos;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.GetTextMetrics(out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily & PitchAndFamily.TMPF_FIXED_PITCH) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    window.SetScrollRange(ScrollBar.SB_VERT, 0, SysMets.sysmetrics.Count - 1, false);
                    window.SetScrollPosition(ScrollBar.SB_VERT, iVscrollPos, true);

                    return 0;
                case WindowMessage.Size:
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.VerticalScroll:
                    switch ((ScrollBarCommand)wParam.LowWord)
                    {
                        case ScrollBarCommand.SB_LINEUP:
                            iVscrollPos -= 1;
                            break;
                        case ScrollBarCommand.SB_LINEDOWN:
                            iVscrollPos += 1;
                            break;
                        case ScrollBarCommand.SB_PAGEUP:
                            iVscrollPos -= cyClient / cyChar;
                            break;
                        case ScrollBarCommand.SB_PAGEDOWN:
                            iVscrollPos += cyClient / cyChar;
                            break;
                        case ScrollBarCommand.SB_THUMBPOSITION:
                            iVscrollPos = wParam.HighWord;
                            break;
                    }

                    iVscrollPos = Math.Max(0, Math.Min(iVscrollPos, SysMets.sysmetrics.Count - 1));

                    if (iVscrollPos != window.GetScrollPosition(ScrollBar.SB_VERT))
                    {
                        window.SetScrollPosition(ScrollBar.SB_VERT, iVscrollPos, true);
                        window.Invalidate(true);
                    }
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        int i = 0;
                        foreach (SystemMetric metric in SysMets.sysmetrics.Keys)
                        {
                            int y = cyChar * (i - iVscrollPos);

                            dc.TextOut(0, y, metric.ToString());
                            dc.TextOut(22 * cxCaps, y, SysMets.sysmetrics[metric]);
                            dc.SetTextAlignment(TextAlignment.TA_RIGHT | TextAlignment.TA_TOP);
                            dc.TextOut(22 * cxCaps + 40 * cxChar, y, Windows.GetSystemMetrics(metric).ToString());
                            dc.SetTextAlignment(TextAlignment.TA_LEFT | TextAlignment.TA_TOP);
                            i++;
                        }
                    }
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
