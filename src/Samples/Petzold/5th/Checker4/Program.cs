// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi;
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

            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
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

            Windows.RegisterClass(ref wndclass);

            wndclass.WindowProcedure = ChildWindowProcedure;
            wndclass.WindowExtraBytes = IntPtr.Size;
            wndclass.ClassName = szChildClass;

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Checker4 Mouse Hit-Test Demo",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
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

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            int x, y;
            switch (message)
            {
                case WindowMessage.Create:
                    for (x = 0; x < DIVISIONS; x++)
                        for (y = 0; y < DIVISIONS; y++)
                            hwndChild[x, y] = Windows.CreateWindow(
                                szChildClass,
                                null,
                                WindowStyles.ChildWindow | WindowStyles.Visible,
                                ExtendedWindowStyles.Default,
                                0, 0, 0, 0,
                                window,
                                (IntPtr)(y << 8 | x),
                                window.GetWindowLong(WindowLong.InstanceHandle),
                                IntPtr.Zero);
                    return 0;
                case WindowMessage.Size:
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
                case WindowMessage.LeftButtonDown:
                    ErrorMethods.MessageBeep(BeepType.Ok);
                    return 0;
                // On set-focus message, set focus to child window
                case WindowMessage.SetFocus:
                    window.GetDialogItem(idFocus).SetFocus();
                    return 0;
                // On key-down message, possibly change the focus window
                case WindowMessage.KeyDown:
                    x = idFocus & 0xFF;
                    y = idFocus >> 8;
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Up: y--; break;
                        case VirtualKey.Down: y++; break;
                        case VirtualKey.Left: x--; break;
                        case VirtualKey.Right: x++; break;
                        case VirtualKey.Home: x = y = 0; break;
                        case VirtualKey.End: x = y = DIVISIONS - 1; break;
                        default: return 0;
                    }
                    x = (x + DIVISIONS) % DIVISIONS;
                    y = (y + DIVISIONS) % DIVISIONS;
                    idFocus = y << 8 | x;
                    window.GetDialogItem(idFocus).SetFocus();
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static LRESULT ChildWindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    window.SetWindowLong(0, IntPtr.Zero); // on/off flag
                    return 0;
                case WindowMessage.KeyDown:
                    // Send most key presses to the parent window
                    if ((VirtualKey)wParam != VirtualKey.Return && (VirtualKey)wParam != VirtualKey.Space)
                    {
                        window.GetParent().SendMessage(message, wParam, lParam);
                        return 0;
                    }

                    // For Return and Space, fall through to toggle the square
                    goto case WindowMessage.LeftButtonDown;
                case WindowMessage.LeftButtonDown:
                    window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong(0)));
                    window.SetFocus();
                    window.Invalidate(false);
                    return 0;
                // For focus messages, invalidate the window for repaint
                case WindowMessage.SetFocus:
                    idFocus = (int)window.GetWindowLong(WindowLong.Id);
                    // Fall through
                    goto case WindowMessage.KillFocus;
                case WindowMessage.KillFocus:
                    window.Invalidate();
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        RECT rect = window.GetClientRectangle();
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
                            dc.SelectObject(StockBrush.Null);
                            using (PenHandle pen = Windows.CreatePen(PenStyle.Dash, 0, 0))
                            {
                                dc.SelectObject(pen);
                                dc.Rectangle(rect);
                                dc.SelectObject(StockPen.Black);
                            }
                        }
                    }
                    return 0;
            }
            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
