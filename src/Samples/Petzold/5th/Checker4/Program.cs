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

namespace Checker4
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-8, Pages 303-308.
    /// </summary>
    static class Program
    {
        const string szChildClass = "Checker4_Child";

        [STAThread]
        static void Main()
        {
            const string szAppName = "Checker4";

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
                "Checker4 Mouse Hit-Test Demo",
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
        static int idFocus = 0;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            int x, y;
            switch (message)
            {
                case MessageType.WM_CREATE:
                    for (x = 0; x < DIVISIONS; x++)
                        for (y = 0; y < DIVISIONS; y++)
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
                    for (x = 0; x < DIVISIONS; x++)
                        for (y = 0; y < DIVISIONS; y++)
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
                // On set-focus message, set focus to child window
                case MessageType.WM_SETFOCUS:
                    window.GetDialogItem(idFocus).SetFocus();
                    return 0;
                // On key-down message, possibly change the focus window
                case MessageType.WM_KEYDOWN:
                    x = idFocus & 0xFF;
                    y = idFocus >> 8;
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.VK_UP: y--; break;
                        case VirtualKey.VK_DOWN: y++; break;
                        case VirtualKey.VK_LEFT: x--; break;
                        case VirtualKey.VK_RIGHT: x++; break;
                        case VirtualKey.VK_HOME: x = y = 0; break;
                        case VirtualKey.VK_END: x = y = DIVISIONS - 1; break;
                        default: return 0;
                    }
                    x = (x + DIVISIONS) % DIVISIONS;
                    y = (y + DIVISIONS) % DIVISIONS;
                    idFocus = y << 8 | x;
                    window.GetDialogItem(idFocus).SetFocus();
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
                case MessageType.WM_KEYDOWN:
                    // Send most key presses to the parent window
                    if ((VirtualKey)wParam != VirtualKey.VK_RETURN && (VirtualKey)wParam != VirtualKey.VK_SPACE)
                    {
                        window.GetParent().SendMessage(message, wParam, lParam);
                        return 0;
                    }

                    // For Return and Space, fall through to toggle the square
                    goto case MessageType.WM_LBUTTONDOWN;
                case MessageType.WM_LBUTTONDOWN:
                    window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong(0)));
                    window.SetFocus();
                    window.Invalidate(false);
                    return 0;
                // For focus messages, invalidate the window for repaint
                case MessageType.WM_SETFOCUS:
                    idFocus = (int)window.GetWindowLong(WindowLong.GWL_ID);
                    // Fall through
                    goto case MessageType.WM_KILLFOCUS;
                case MessageType.WM_KILLFOCUS:
                    window.Invalidate();
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

                        // Draw the "focus" rectangle
                        if (window == Windows.GetFocus())
                        {
                            rect.left += rect.right / 10;
                            rect.right -= rect.left;
                            rect.top += rect.bottom / 10;
                            rect.bottom -= rect.top;
                            dc.SelectObject(StockBrush.NULL_BRUSH);
                            using (PenHandle pen = Windows.CreatePen(PenStyle.PS_DASH, 0, 0))
                            {
                                dc.SelectObject(pen);
                                dc.Rectangle(rect);
                                dc.SelectObject(StockPen.BLACK_PEN);
                            }
                        }
                    }
                    return 0;
            }
            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
