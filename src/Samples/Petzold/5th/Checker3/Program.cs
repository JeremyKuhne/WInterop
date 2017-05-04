// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace Checker3
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-7, Pages 299-301.
    /// </summary>
    static class Program
    {
        const string szChildClass = "Checker3_Child";

        [STAThread]
        static void Main()
        {
            const string szAppName = "Checker3";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                ClassName = szAppName
            };

            WindowMethods.RegisterClass(wndclass);

            wndclass.WindowProcedure = ChildWindowProcedure;
            wndclass.WindowExtraBytes = IntPtr.Size;
            wndclass.ClassName = szChildClass;

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                szAppName,
                "Checker3 Mouse Hit-Test Demo",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
            GdiMethods.UpdateWindow(window);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }
        }

        const int DIVISIONS = 5;
        static WindowHandle[,] hwndChild = new WindowHandle[DIVISIONS, DIVISIONS];
        static int cxBlock, cyBlock;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:
                    for (int x = 0; x < DIVISIONS; x++)
                        for (int y = 0; y < DIVISIONS; y++)
                            hwndChild[x, y] = WindowMethods.CreateWindow(
                                WindowMethods.GetWindowLong(window, WindowLong.GWL_HINSTANCE),
                                szChildClass,
                                null,
                                WindowStyle.WS_CHILDWINDOW | WindowStyle.WS_VISIBLE,
                                ExtendedWindowStyle.None,
                                0, 0, 0, 0,
                                window,
                                (IntPtr)(y << 8 | x),
                                IntPtr.Zero);
                    return 0;
                case MessageType.WM_SIZE:
                    cxBlock = lParam.LowWord / DIVISIONS;
                    cyBlock = lParam.HighWord / DIVISIONS;
                    for (int x = 0; x < DIVISIONS; x++)
                        for (int y = 0; y < DIVISIONS; y++)
                            WindowMethods.MoveWindow(hwndChild[x,y],
                                x * cxBlock, y * cyBlock,
                                cxBlock, cyBlock, true);
                    return 0;
                case MessageType.WM_LBUTTONDOWN:
                    ErrorMethods.MessageBeep(MessageBeepType.MB_OK);
                    return 0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static LRESULT ChildWindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:
                    WindowMethods.SetWindowLong(window, 0, IntPtr.Zero); // on/off flag
                    return 0;
                case MessageType.WM_LBUTTONDOWN:
                    WindowMethods.SetWindowLong(window, 0, (IntPtr)(1 ^ (int)WindowMethods.GetWindowLong(window, 0)));
                    GdiMethods.InvalidateRectangle(window, false);
                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        RECT rect = WindowMethods.GetClientRect(window);
                        GdiMethods.Rectangle(dc, rect);

                        if (WindowMethods.GetWindowLong(window, 0) != IntPtr.Zero)
                        {
                            GdiMethods.MoveTo(dc, 0, 0);
                            GdiMethods.LineTo(dc, rect.right, rect.bottom);
                            GdiMethods.MoveTo(dc, 0, rect.bottom);
                            GdiMethods.LineTo(dc, rect.right, 0);
                        }
                    }
                    return 0;
            }
            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
