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
using WInterop.Multimedia;
using WInterop.Multimedia.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace SysMets1
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
                ClassName = "SysMets1"
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                "SysMets1",
                "Get System Metrics No. 1",
                WindowStyle.WS_OVERLAPPEDWINDOW,
                ExtendedWindowStyle.None,
                module);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.NullWindowHandle, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        static int cxChar, cxCaps, cyChar;

        static IntPtr WindowProcedure(WindowHandle window, MessageType message, UIntPtr wParam, IntPtr lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:

                    using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                    {
                        GdiMethods.GetTextMetrics(dc, out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily & PitchAndFamily.TMPF_FIXED_PITCH) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }
                    return (IntPtr)0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        int i = 0;
                        foreach (SystemMetric metric in SysMets.sysmetrics.Keys)
                        {
                            GdiMethods.TextOut(dc, 0, cyChar * i, metric.ToString());
                            GdiMethods.TextOut(dc, 22 * cxCaps, cyChar * i, SysMets.sysmetrics[metric]);
                            GdiMethods.SetTextAlignment(dc, TextAlignment.TA_RIGHT | TextAlignment.TA_TOP);
                            GdiMethods.TextOut(dc, 22 * cxCaps + 40 * cxChar, cyChar * i, WindowMethods.GetSystemMetrics(metric).ToString());
                            GdiMethods.SetTextAlignment(dc, TextAlignment.TA_LEFT | TextAlignment.TA_TOP);
                            i++;
                        }
                    }
                    return (IntPtr)0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return (IntPtr)0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
