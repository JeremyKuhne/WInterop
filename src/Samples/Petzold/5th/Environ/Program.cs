// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ProcessAndThreads;
using WInterop.Windows;
using WInterop.Gdi;

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
            Windows.CreateMainWindowAndRun(new Environ(), "Environment List Box");
        }
    }

    class Environ : WindowClass
    {
        const int ID_LIST = 1;
        const int ID_TEXT = 2;

        WindowHandle hwndList, hwndText;

        unsafe static void FillListBox(WindowHandle hwndList)
        {
            foreach (var name in Processes.GetEnvironmentVariables().Keys)
            {
                if (name[0] != '=') // Skip variable names beginning with '='
                {
                    fixed (char* buffer = name)
                        hwndList.SendMessage(ListBoxMessage.AddString, 0, buffer);
                }
            }
        }

        protected unsafe override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    Size baseUnits = Windows.GetDialogBaseUnits();
                    Rectangle bounds = window.GetClientRectangle();

                    // Create listbox and static text windows.
                    hwndList = Windows.CreateWindow(
                        className: "listbox",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard,
                        bounds: new Rectangle(
                            baseUnits.Width,
                            baseUnits.Height * 3,
                            bounds.Width - baseUnits.Width - Windows.GetSystemMetrics(SystemMetric.CXVSCROLL),
                            bounds.Height - baseUnits.Height * 4),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_LIST,
                        instance: ModuleInstance);

                    hwndText = Windows.CreateWindow(
                        className: "static",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left,
                        bounds: new Rectangle(
                            baseUnits.Width,
                            baseUnits.Height,
                            bounds.Width - baseUnits.Width - Windows.GetSystemMetrics(SystemMetric.CXVSCROLL),
                            baseUnits.Height),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_TEXT,
                        instance: ModuleInstance);

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
                        string value = Processes.GetEnvironmentVariable(new string(nameBuffer, 0, result));

                        // Show it in window.
                        hwndText.SetWindowText(value);
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
