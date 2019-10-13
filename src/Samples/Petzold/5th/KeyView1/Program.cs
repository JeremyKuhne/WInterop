// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Text;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Support;

namespace KeyView1
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 6-3, Pages 236-240.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new KeyView1(), "Keyboard Message Viewer #1");
        }
    }

    internal class KeyView1 : WindowClass
    {
        private int cyClientMax, cxClient, cyClient, cyChar;
        private int cLinesMax, cLines;
        private Rectangle rectScroll;
        private WindowMessage[] pmsg;
        private readonly StringBuilder _sb = new StringBuilder(256);
        private ReadOnlyMemory<char> _chunk;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    _chunk = _sb.GetChunk();
                    goto case MessageType.DisplayChange;
                case MessageType.DisplayChange:
                    // Get maximum size of client area
                    cyClientMax = Windows.GetSystemMetrics(SystemMetric.MaximizedHeight);

                    // Get character size for fixed-pitch font
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.GetTextMetrics(out TextMetrics tm);
                        cyChar = tm.Height;
                    }

                    cLinesMax = cyClientMax / cyChar;
                    pmsg = new WindowMessage[cLinesMax];
                    cLines = 0;
                    goto CalculateScroll;
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CalculateScroll:

                    rectScroll = Rectangle.FromLTRB(0, cyChar, cxClient, cyChar * (cyClient / cyChar));
                    window.Invalidate(true);

                    return 0;
                case MessageType.KeyDown:
                case MessageType.KeyUp:
                case MessageType.Char:
                case MessageType.DeadChar:
                case MessageType.SystemKeyDown:
                case MessageType.SystemKeyUp:
                case MessageType.SystemChar:
                case MessageType.SystemDeadChar:
                    // Rearrange storage array
                    for (int i = cLinesMax - 1; i > 0; i--)
                    {
                        pmsg[i] = pmsg[i - 1];
                    }
                    // Store new message
                    pmsg[0] = new WindowMessage(window, message, wParam, lParam);
                    cLines = Math.Min(cLines + 1, cLinesMax);

                    // Scroll up the display
                    window.ScrollWindow(new Point(0, -cyChar), rectScroll, rectScroll);
                    break; // i.e., call DefWindowProc so Sys messages work
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.SetBackgroundMode(BackgroundMode.Transparent);
                        dc.TextOut(default, "Message        Key       Char     Repeat Scan Ext ALT Prev Tran".AsSpan());
                        dc.TextOut(default, "_______        ___       ____     ______ ____ ___ ___ ____ ____".AsSpan());
                        for (int i = 0; i < Math.Min(cLines, cyClient / cyChar - 1); i++)
                        {
                            bool iType;
                            switch (pmsg[i].Type)
                            {
                                case MessageType.Char:
                                case MessageType.SystemChar:
                                case MessageType.DeadChar:
                                case MessageType.SystemDeadChar:
                                    iType = true;
                                    break;
                                default:
                                    iType = false;
                                    break;
                            }

                            _sb.Clear();
                            _sb.AppendFormat(iType
                                ? "{0,-13} {1,3} {2,15} {3,6} {4,4} {5,3} {6,3} {7,4} {8,4}"
                                : "{0,-13} {1,3} {2,-15} {3,6} {4,4} {5,3} {6,3} {7,4} {8,4}  VirtualKey: {9}",
                                    pmsg[i].Type,
                                    pmsg[i].wParam.ToString(),
                                    iType
                                        ? $"0x{((uint)pmsg[i].wParam):X4} {(char)(uint)pmsg[i].wParam}"
                                        : Windows.GetKeyNameText(pmsg[i].lParam),
                                    pmsg[i].lParam.LowWord,
                                    pmsg[i].lParam.HighWord & 0xFF,
                                    (0x01000000 & pmsg[i].lParam) != 0 ? "Yes" : "No",
                                    (0x20000000 & pmsg[i].lParam) != 0 ? "Yes" : "No",
                                    (0x40000000 & pmsg[i].lParam) != 0 ? "Down" : "Up",
                                    (0x80000000 & pmsg[i].lParam) != 0 ? "Up" : "Down",
                                    (VirtualKey)pmsg[i].wParam);

                            dc.TextOut(new Point(0, (cyClient / cyChar - 1 - i) * cyChar), _chunk.Span.Slice(0, _sb.Length));
                        }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
