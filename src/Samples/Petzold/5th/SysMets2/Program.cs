﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Resources;
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
            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
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

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                "SysMets2",
                "Get System Metrics No. 2",
                WindowStyles.OverlappedWindow | WindowStyles.VerticalScroll);

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
                        cxCaps = ((tm.tmPitchAndFamily.PitchTypes & FontPitchTypes.VariablePitch) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    window.SetScrollRange(ScrollBar.Vertical, 0, SysMets.sysmetrics.Count - 1, false);
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

                    iVscrollPos = Math.Max(0, Math.Min(iVscrollPos, SysMets.sysmetrics.Count - 1));

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
                        foreach (SystemMetric metric in SysMets.sysmetrics.Keys)
                        {
                            int y = cyChar * (i - iVscrollPos);

                            dc.TextOut(0, y, metric.ToString());
                            dc.TextOut(22 * cxCaps, y, SysMets.sysmetrics[metric]);
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Right, TextAlignment.Vertical.Top));
                            dc.TextOut(22 * cxCaps + 40 * cxChar, y, Windows.GetSystemMetrics(metric).ToString());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Left, TextAlignment.Vertical.Top));
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
