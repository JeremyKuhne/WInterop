// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ErrorHandling;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Resources;
using WInterop.Windows;

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
                "Checker3 Mouse Hit-Test Demo",
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

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    for (int x = 0; x < DIVISIONS; x++)
                        for (int y = 0; y < DIVISIONS; y++)
                            hwndChild[x, y] = Windows.CreateWindow(
                                szChildClass,
                                null,
                                WindowStyles.ChildWindow | WindowStyles.Visible,
                                ExtendedWindowStyles.Default,
                                new Rectangle(),
                                window,
                                (IntPtr)(y << 8 | x),
                                window.GetWindowLong(WindowLong.InstanceHandle),
                                IntPtr.Zero);
                    return 0;
                case WindowMessage.Size:
                    cxBlock = lParam.LowWord / DIVISIONS;
                    cyBlock = lParam.HighWord / DIVISIONS;
                    for (int x = 0; x < DIVISIONS; x++)
                        for (int y = 0; y < DIVISIONS; y++)
                            hwndChild[x, y].MoveWindow(
                                new Rectangle(x * cxBlock, y * cyBlock, cxBlock, cyBlock),
                                repaint: true);
                    return 0;
                case WindowMessage.LeftButtonDown:
                    ErrorMethods.MessageBeep(BeepType.Ok);
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
                case WindowMessage.LeftButtonDown:
                    window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong( 0)));
                    window.Invalidate(false);
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        Rectangle rect = window.GetClientRectangle();
                        dc.Rectangle(rect);

                        if (window.GetWindowLong(0) != IntPtr.Zero)
                        {
                            dc.MoveTo(0, 0);
                            dc.LineTo(rect.Right, rect.Bottom);
                            dc.MoveTo(0, rect.Bottom);
                            dc.LineTo(rect.Right, 0);
                        }
                    }
                    return 0;
            }
            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
