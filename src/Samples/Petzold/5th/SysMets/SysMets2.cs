// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace SysMets
{
    internal class SysMets2 : SysMets1
    {
        protected int cyClient, iVscrollPos;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    // We have to call the base first to get the character metrics as setting the
                    // scroll range will kick MessageType.Size events.

                    base.WindowProcedure(window, message, wParam, lParam);

                    window.SetScrollRange(ScrollBar.Vertical, 0, Metrics.SystemMetrics.Count - 1, false);
                    window.SetScrollPosition(ScrollBar.Vertical, iVscrollPos, true);

                    return 0;
                case MessageType.Size:
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.VerticalScroll:
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
                case MessageType.Paint:
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
