// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.ProcessAndThreads;
using WInterop.Resources;
using WInterop.Windows;

namespace Environ
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 9-8, Pages 405-409.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Environ";

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

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Environment List Box",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static WindowHandle hwndList, hwndText;
        const int ID_LIST = 1;
        const int ID_TEXT = 2;

        unsafe static void FillListBox(WindowHandle hwndList)
        {
            foreach (var name in ProcessMethods.GetEnvironmentVariables().Keys)
            {
                if (name[0] != '=') // Skip variable names beginning with '='
                {
                    fixed (char* buffer = name)
                        hwndList.SendMessage(ListBoxMessage.AddString, 0, buffer);
                }
            }
        }

        unsafe static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    SIZE baseUnits = Windows.GetDialogBaseUnits();

                    // Create listbox and static text windows.
                    hwndList = Windows.CreateWindow("listbox", null,
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard, ExtendedWindowStyles.Default,
                        baseUnits.cx, baseUnits.cy * 3, baseUnits.cx * 64 + Windows.GetSystemMetrics(SystemMetric.CXVSCROLL), baseUnits.cy * 20,
                        window, (IntPtr)ID_LIST, ((CREATESTRUCT*)lParam)->hInstance, IntPtr.Zero);

                    hwndText = Windows.CreateWindow("static", null,
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left, ExtendedWindowStyles.Default,
                        baseUnits.cx, baseUnits.cy, Windows.GetSystemMetrics(SystemMetric.CYSCREEN), baseUnits.cy,
                        window, (IntPtr)ID_TEXT, ((CREATESTRUCT*)lParam)->hInstance, IntPtr.Zero);

                    FillListBox(hwndList);
                    return 0;
                case WindowMessage.SetFocus:
                    hwndList.SetFocus();
                    return 0;
                case WindowMessage.Command:
                    if (wParam.LowWord == ID_LIST
                        && (wParam.HighWord == (ushort)ListBoxNotification.SelectionChange))
                    {
                        // Get current selection.
                        uint iIndex = hwndList.SendMessage(ListBoxMessage.GetCurrentSelection, 0, 0);
                        int iLength = hwndList.SendMessage(ListBoxMessage.GetTextLength, iIndex, 0) + 1;
                        char* nameBuffer = stackalloc char[iLength];
                        int result = hwndList.SendMessage(ListBoxMessage.GetText, iIndex, nameBuffer);

                        // Get environment string.
                        string value = ProcessMethods.GetEnvironmentVariable(new string(nameBuffer, 0, result));

                        // Show it in window.
                        hwndText.SetWindowText(value);
                    }
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
