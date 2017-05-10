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
using WInterop.Multimedia;
using WInterop.Multimedia.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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
            const string szAppName = "WhatClr";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = szAppName
            };

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "What Color",
                WindowStyle.Overlapped | WindowStyle.Caption | WindowStyle.SystemMenu | WindowStyle.Border);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static void FindWindowSize(ref int cxWindow, ref int cyWindow)
        {
            TEXTMETRIC tm;
            using (DeviceContext dcScreen = Windows.CreateInformationContext("DISPLAY", null))
            {
                dcScreen.GetTextMetrics(out tm);
            }

            cxWindow = 2 * Windows.GetSystemMetrics(SystemMetric.CXBORDER) + 12 * tm.tmAveCharWidth;
            cyWindow = 2 * Windows.GetSystemMetrics(SystemMetric.CYBORDER) + Windows.GetSystemMetrics(SystemMetric.CYCAPTION) + 2 * tm.tmHeight;
        }

        static DeviceContext dcScreen;
        static COLORREF cr, crLast;
        const int ID_TIMER = 1;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    dcScreen = Windows.CreateDeviceContext("DISPLAY", null);
                    window.SetTimer(ID_TIMER, 100);
                    return 0;
                case WindowMessage.Timer:
                    POINT pt = Windows.GetCursorPosition();
                    cr = dcScreen.GetPixel(pt);
                    // Not sure why the sample did this
                    // dcScreen.SetPixel(pt, 0);
                    if (cr != crLast)
                    {
                        crLast = cr;
                        window.Invalidate(false);
                    }
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        RECT rc = window.GetClientRect();
                        dc.DrawText($"0x{cr.R:X2} 0x{cr.G:X2} 0x{cr.B:X2}", rc,
                            TextFormat.DT_SINGLELINE | TextFormat.DT_CENTER | TextFormat.DT_VCENTER);
                    }
                    return 0;
                case WindowMessage.Destroy:
                    dcScreen.Dispose();
                    window.KillTimer(ID_TIMER);
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
