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
using WInterop.Support;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace LineDemo
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-14, Pages 153-155.
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
                ClassName = "LineDemo"
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                "LineDemo",
                "Line Demonstration",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.NullWindowHandle, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        static int cxClient, cyClient;

        static IntPtr WindowProcedure(WindowHandle window, MessageType message, UIntPtr wParam, IntPtr lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
                    cxClient = Conversion.LowWord(lParam);
                    cyClient = Conversion.HighWord(lParam);
                    return (IntPtr)0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        GdiMethods.Rectangle(dc, cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8);
                        GdiMethods.MoveTo(dc, 0, 0);
                        GdiMethods.LineTo(dc, cxClient, cyClient);
                        GdiMethods.MoveTo(dc, 0, cyClient);
                        GdiMethods.LineTo(dc, cxClient, 0);
                        GdiMethods.Ellipse(dc, cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8);
                        GdiMethods.RoundRectangle(dc, cxClient / 4, cyClient / 4, 3 * cxClient / 4, 3 * cyClient / 4,
                            cxClient / 4, cyClient / 4);
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
