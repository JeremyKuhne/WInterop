// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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
            const string szAppName = "KeyView1";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                ClassExtraBytes = 0,
                WindowExtraBytes = 0,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                MenuName = null,
                ClassName = szAppName
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                szAppName,
                "Keyboard Message Viewer #1",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        static int cxClientMax, cyClientMax, cxClient, cyClient, cxChar, cyChar;
        static int cLinesMax, cLines;
        static RECT rectScroll;
        static MSG[] pmsg;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:
                case MessageType.WM_DISPLAYCHANGE:
                    // Get maximum size of client area
                    cxClientMax = WindowMethods.GetSystemMetrics(SystemMetric.SM_CXMAXIMIZED);
                    cyClientMax = WindowMethods.GetSystemMetrics(SystemMetric.SM_CYMAXIMIZED);

                    // Get character size for fixed-pitch font
                    using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                    {
                        GdiMethods.SelectObject(dc, GdiMethods.GetStockObject(StockObject.SYSTEM_FIXED_FONT));
                        GdiMethods.GetTextMetrics(dc, out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cyChar = tm.tmHeight;
                    }

                    cLinesMax = cyClientMax / cyChar;
                    pmsg = new MSG[cLinesMax];
                    cLines = 0;
                    goto CalculateScroll;
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CalculateScroll:

                    rectScroll.left = 0;
                    rectScroll.right = cxClient;
                    rectScroll.top = cyChar;
                    rectScroll.bottom = cyChar * (cyClient / cyChar);
                    GdiMethods.InvalidateRectangle(window, true);

                    return 0;
                case MessageType.WM_KEYDOWN:
                case MessageType.WM_KEYUP:
                case MessageType.WM_CHAR:
                case MessageType.WM_DEADCHAR:
                case MessageType.WM_SYSKEYDOWN:
                case MessageType.WM_SYSKEYUP:
                case MessageType.WM_SYSCHAR:
                case MessageType.WM_SYSDEADCHAR:
                    // Rearrange storage array
                    for (int i = cLinesMax - 1; i > 0; i--)
                    {
                        pmsg[i] = pmsg[i - 1];
                    }
                    // Store new message
                    pmsg[0].hwnd = window;
                    pmsg[0].message = message;
                    pmsg[0].wParam = wParam;
                    pmsg[0].lParam = lParam;
                    cLines = Math.Min(cLines + 1, cLinesMax);

                    // Scroll up the display
                    WindowMethods.ScrollWindow(window, 0, -cyChar, rectScroll, rectScroll);
                    break; // i.e., call DefWindowProc so Sys messages work
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        GdiMethods.SelectObject(dc, GdiMethods.GetStockFont(StockFont.SYSTEM_FIXED_FONT));
                        GdiMethods.SetBackgroundMode(dc, BackgroundMode.TRANSPARENT);
                        GdiMethods.TextOut(dc, 0, 0, "Message        Key       Char     Repeat Scan Ext ALT Prev Tran");
                        GdiMethods.TextOut(dc, 0, 0, "_______        ___       ____     ______ ____ ___ ___ ____ ____");
                        for (int i = 0; i < Math.Min(cLines, cyClient / cyChar - 1); i++)
                        {
                            bool iType;
                            switch (pmsg[i].message)
                            {
                                case MessageType.WM_CHAR:
                                case MessageType.WM_SYSCHAR:
                                case MessageType.WM_DEADCHAR:
                                case MessageType.WM_SYSDEADCHAR:
                                    iType = true;
                                    break;
                                default:
                                    iType = false;
                                    break;
                            }

                            GdiMethods.TextOut(dc, 0, (cyClient / cyChar - 1 - i) * cyChar,
                                string.Format(iType
                                ? "{0,-13} {1,3} {2,15} {3,6} {4,4} {5,3} {6,3} {7,4} {8,4}"
                                : "{0,-13} {1,3} {2,-15} {3,6} {4,4} {5,3} {6,3} {7,4} {8,4}",
                                    pmsg[i].message,
                                    pmsg[i].wParam.ToString(),
                                    iType
                                        ? $"0x{((uint)pmsg[i].wParam):X4} {(char)(uint)pmsg[i].wParam}"
                                        : WindowMethods.GetKeyNameText(pmsg[i].lParam),
                                    pmsg[i].lParam.LowWord,
                                    pmsg[i].lParam.HighWord & 0xFF,
                                    (0x01000000 & pmsg[i].lParam) != 0 ? "Yes" : "No",
                                    (0x20000000 & pmsg[i].lParam) != 0 ? "Yes" : "No",
                                    (0x40000000 & pmsg[i].lParam) != 0 ? "Down" : "Up",
                                    (0x80000000 & pmsg[i].lParam) != 0 ? "Up" : "Down"));
                        }
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
