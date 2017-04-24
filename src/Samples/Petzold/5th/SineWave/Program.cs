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

namespace SineWave
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-6, Pages 147-148.
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
                ClassName = "SineWave"
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                "SineWave",
                "Sine Wave Using PolyLine",
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
                        GdiMethods.MoveTo(dc, 0, cyClient / 2);
                        GdiMethods.LineTo(dc, cxClient, cyClient / 2);

                        POINT[] apt = new POINT[1000];
                        for (int i = 0; i < apt.Length; i++)
                        {
                            apt[i].x = i * cxClient / apt.Length;
                            apt[i].y = (int)(cyClient / 2 * (1 - Math.Sin(Math.PI * 2 * i / apt.Length)));
                        }
                        GdiMethods.Polyline(dc, apt);
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
