// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
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
                Icon = IconId.IDI_APPLICATION,
                Cursor = CursorId.IDC_ARROW,
                Background = StockBrush.WHITE_BRUSH,
                ClassName = szAppName
            };

            Windows.RegisterClass(wndclass);

            wndclass.WindowProcedure = ChildWindowProcedure;
            wndclass.WindowExtraBytes = IntPtr.Size;
            wndclass.ClassName = szChildClass;

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Checker3 Mouse Hit-Test Demo",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            window.ShowWindow(ShowWindowCommand.SW_SHOWNORMAL);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
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
                            hwndChild[x, y] = Windows.CreateWindow(
                                window.GetWindowLong(WindowLong.GWL_HINSTANCE),
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
                            hwndChild[x, y].MoveWindow(
                                x * cxBlock,
                                y * cyBlock,
                                cxBlock,
                                cyBlock,
                                true);
                    return 0;
                case MessageType.WM_LBUTTONDOWN:
                    ErrorMethods.MessageBeep(MessageBeepType.MB_OK);
                    return 0;
                case MessageType.WM_DESTROY:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static LRESULT ChildWindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_CREATE:
                    window.SetWindowLong(0, IntPtr.Zero); // on/off flag
                    return 0;
                case MessageType.WM_LBUTTONDOWN:
                    window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong( 0)));
                    window.Invalidate(false);
                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        RECT rect = window.GetClientRect();
                        dc.Rectangle(rect);

                        if (window.GetWindowLong(0) != IntPtr.Zero)
                        {
                            dc.MoveTo(0, 0);
                            dc.LineTo(rect.right, rect.bottom);
                            dc.MoveTo(0, rect.bottom);
                            dc.LineTo(rect.right, 0);
                        }
                    }
                    return 0;
            }
            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
