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
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace PopPad1
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 9-7, Pages 396-397.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "PopPad1";

            ModuleInstance module = Marshal.GetHINSTANCE(typeof(Program).Module);
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

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "PopPad1",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static WindowHandle hwndEdit;
        const int ID_EDIT = 1;

        unsafe static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    hwndEdit = Windows.CreateWindow("edit", null,
                        WindowStyles.Child | WindowStyles.Visible | WindowStyles.HorizontalScroll | WindowStyles.VerticalScroll
                        | WindowStyles.Border | (WindowStyles)EditStyles.Left | (WindowStyles)EditStyles.Multiline
                        | (WindowStyles)EditStyles.AutoHorizontalScroll | (WindowStyles)EditStyles.AutoVerticalScroll,
                        ExtendedWindowStyles.Default, 0, 0, 0, 0, window, (IntPtr)ID_EDIT, ((CREATESTRUCT*)lParam)->hInstance, IntPtr.Zero);
                    return 0;
                case WindowMessage.SetFocus:
                    hwndEdit.SetFocus();
                    return 0;
                case WindowMessage.Size:
                    hwndEdit.MoveWindow(0, 0, lParam.LowWord, lParam.HighWord, true);
                    return 0;
                case WindowMessage.Command:
                    if (wParam.LowWord == ID_EDIT
                        && (wParam.HighWord == (ushort)EditNotification.ErrorSpace || wParam.HighWord == (ushort)EditNotification.MaxText))
                        window.MessageBox("Edit control out of space.", "PopPad1", MessageBoxType.Ok | MessageBoxType.IconStop);
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
