// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ProcessAndThreads;
using WInterop.Windows;

namespace Environ
{
    /// <summary>
    ///  Sample from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    ///  Figure 9-8, Pages 405-409.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Windows.CreateMainWindowAndRun(new Environ(), "Environment List Box");
        }
    }

    internal class Environ : WindowClass
    {
        private const int ID_LIST = 1;
        private const int ID_TEXT = 2;
        private WindowHandle _hwndList, _hwndText;

        private static unsafe void FillListBox(WindowHandle hwndList)
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

        protected unsafe override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    Size baseUnits = Windows.GetDialogBaseUnits();
                    Rectangle bounds = window.GetClientRectangle();

                    // Create listbox and static text windows.
                    _hwndList = Windows.CreateWindow(
                        className: "listbox",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard,
                        bounds: new Rectangle(
                            baseUnits.Width,
                            baseUnits.Height * 3,
                            bounds.Width - baseUnits.Width - Windows.GetSystemMetrics(SystemMetric.VerticalScrollWidth),
                            bounds.Height - baseUnits.Height * 4),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_LIST,
                        instance: ModuleInstance);

                    _hwndText = Windows.CreateWindow(
                        className: "static",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left,
                        bounds: new Rectangle(
                            baseUnits.Width,
                            baseUnits.Height,
                            bounds.Width - baseUnits.Width - Windows.GetSystemMetrics(SystemMetric.VerticalScrollWidth),
                            baseUnits.Height),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_TEXT,
                        instance: ModuleInstance);

                    FillListBox(_hwndList);
                    return 0;
                case MessageType.SetFocus:
                    _hwndList.SetFocus();
                    return 0;
                case MessageType.Command:
                    if (wParam.LowWord == ID_LIST
                        && (wParam.HighWord == (ushort)ListBoxNotification.SelectionChange))
                    {
                        // Get current selection.
                        uint iIndex = _hwndList.SendMessage(ListBoxMessage.GetCurrentSelection, 0, 0);
                        int iLength = _hwndList.SendMessage(ListBoxMessage.GetTextLength, iIndex, 0) + 1;
                        char* nameBuffer = stackalloc char[iLength];
                        int result = _hwndList.SendMessage(ListBoxMessage.GetText, iIndex, nameBuffer);

                        // Get environment string.
                        string value = Processes.GetEnvironmentVariable(new string(nameBuffer, 0, result));

                        // Show it in window.
                        _hwndText.SetWindowText(value);
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
