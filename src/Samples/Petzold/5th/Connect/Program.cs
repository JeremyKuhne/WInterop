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

namespace Connect
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-1, Pages 278-280.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Connect";

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
                "Connect-the-Points Mouse Demo",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
            GdiMethods.UpdateWindow(window);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        const int MAXPOINTS = 1000;
        static POINT[] pt = new POINT[MAXPOINTS];
        static int iCount;
        static int sampleCount;
        const int TakeEvery = 10;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_LBUTTONDOWN:
                    iCount = 0;
                    GdiMethods.InvalidateRectangle(window, true);
                    return 0;
                case MessageType.WM_MOUSEMOVE:
                    // Machines are way to fast to make this look interesting now, adding TakeEvery
                    if ((MouseKeyState)wParam == MouseKeyState.MK_LBUTTON && iCount < MAXPOINTS && (sampleCount++ % TakeEvery == 0))
                    {
                        pt[iCount].x = lParam.LowWord;
                        pt[iCount++].y = lParam.HighWord;

                        using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                        {
                            GdiMethods.SetPixel(dc, lParam.LowWord, lParam.HighWord, 0);
                        }
                    }
                    return 0;
                case MessageType.WM_LBUTTONUP:
                    GdiMethods.InvalidateRectangle(window, false);
                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        ResourceMethods.SetCursor(ResourceMethods.LoadCursor(CursorId.IDC_WAIT));
                        ResourceMethods.ShowCursor(true);

                        for (int i = 0; i < iCount - 1; i++)
                            for (int j = i + 1; j < iCount; j++)
                            {
                                GdiMethods.MoveTo(dc, pt[i].x, pt[i].y);
                                GdiMethods.LineTo(dc, pt[j].x, pt[j].y);
                            }

                        ResourceMethods.ShowCursor(false);
                        ResourceMethods.SetCursor(ResourceMethods.LoadCursor(CursorId.IDC_ARROW));
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
