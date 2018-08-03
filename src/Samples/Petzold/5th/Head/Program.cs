// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Text;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Storage;
using WInterop.Windows.Native;

namespace Head
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 9-9, Pages 411-415.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new Head(), "Head");
        }
    }

    class Head : WindowClass
    {
        public Head() : base(backgroundBrush: SystemColor.ButtonFace) { }

        static FileTypes DIRATTR = FileTypes.ReadWrite | FileTypes.ReadOnly | FileTypes.Hidden | FileTypes.System
            | FileTypes.Directory | FileTypes.Archive | FileTypes.Drives;
        static TextFormat DTFLAGS = TextFormat.WordBreak | TextFormat.ExpandTabs | TextFormat.NoClip | TextFormat.NoPrefix;

        WindowHandle hwndList, hwndText;
        WNDPROC _existingListBoxWndProc;
        WindowProcedure _listBoxProcedure;

        const int ID_LIST = 1;
        const int ID_TEXT = 2;
        const int MAXREAD = 8192;

        bool bValidFile;
        char[] szFile = new char[256];
        byte[] _buffer = new byte[MAXREAD];
        char[] _decoded = new char[MAXREAD];

        Rectangle rect;

        protected unsafe override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            const string filter = "*.*";

            switch (message)
            {
                case WindowMessage.Create:
                    Size baseUnits = Windows.GetDialogBaseUnits();
                    rect = Rectangle.FromLTRB(20 * baseUnits.Width, 3 * baseUnits.Height, rect.Right, rect.Bottom);

                    // Create listbox and static text windows.
                    hwndList = Windows.CreateWindow(
                        className: "listbox",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard,
                        bounds: new Rectangle(baseUnits.Width, baseUnits.Height * 3, baseUnits.Width * 13 + Windows.GetSystemMetrics(SystemMetric.CXVSCROLL), baseUnits.Height * 10),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_LIST,
                        instance: ModuleInstance);

                    hwndText = Windows.CreateWindow(
                        className: "static",
                        windowName: StorageMethods.GetCurrentDirectory(),
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left,
                        bounds: new Rectangle(baseUnits.Width, baseUnits.Height, baseUnits.Width * 260, baseUnits.Height),
                        parentWindow: window,
                        menuHandle: (MenuHandle)ID_TEXT,
                        instance: ModuleInstance);

                    _existingListBoxWndProc = hwndList.SetWindowProcedure(_listBoxProcedure = ListBoxProcedure);

                    fixed (char* f = filter)
                        hwndList.SendMessage(ListBoxMessage.Directory, (uint)DIRATTR, f);
                    return 0;
                case WindowMessage.Size:
                    rect = Rectangle.FromLTRB(rect.Left, rect.Top, lParam.LowWord, lParam.HighWord);
                    return 0;
                case WindowMessage.SetFocus:
                    hwndList.SetFocus();
                    return 0;
                case WindowMessage.Command:
                    if (wParam.LowWord == ID_LIST
                        && (wParam.HighWord == (ushort)ListBoxNotification.DoubleClick))
                    {
                        uint i = hwndList.SendMessage(ListBoxMessage.GetCurrentSelection, 0, 0);
                        if (i == WindowDefines.LB_ERR)
                            break;

                        int iLength = hwndList.SendMessage(ListBoxMessage.GetTextLength, i, 0) + 1;
                        fixed (char* textBuffer = szFile)
                        {
                            int result = hwndList.SendMessage(ListBoxMessage.GetText, i, textBuffer);
                            SafeFileHandle hFile = null;
                            try
                            {
                                using (hFile = StorageMethods.CreateFile(szFile.AsSpan(0, result),
                                    CreationDisposition.OpenExisting, DesiredAccess.GenericRead, ShareModes.Read))
                                {
                                    if (!hFile.IsInvalid)
                                    {
                                        bValidFile = true;
                                        hwndText.SetWindowText(StorageMethods.GetCurrentDirectory());
                                    }
                                }
                                hFile = null;
                            }
                            catch
                            {
                            }

                            Span<char> dir = stackalloc char[2];
                            if (hFile == null && szFile[0] == ('['))
                            {
                                bValidFile = false;

                                // If setting the directory doesn’t work, maybe it’s a drive change, so try that.
                                try
                                {
                                    szFile[result - 1] = '\0';
                                    StorageMethods.SetCurrentDirectory(szFile.AsSpan(1, result - 2));
                                }
                                catch
                                {
                                    dir[0] = szFile[2];
                                    dir[1] = ':';

                                    try { StorageMethods.SetCurrentDirectory(dir); }
                                    catch { }
                                }

                                // Get the new directory name and fill the list box.
                                hwndText.SetWindowText(StorageMethods.GetCurrentDirectory());
                                hwndList.SendMessage(ListBoxMessage.ResetContent, 0, 0);
                                fixed (char* f = filter)
                                    hwndList.SendMessage(ListBoxMessage.Directory, (uint)DIRATTR, f);
                            }
                        }

                        window.Invalidate();
                    }
                    return 0;
                case WindowMessage.Paint:
                    if (!bValidFile)
                        break;

                    uint bytesRead;
                    using (var hFile = StorageMethods.CreateFile(szFile, CreationDisposition.OpenExisting,
                        DesiredAccess.GenericRead, ShareModes.Read))
                    {
                        if (hFile.IsInvalid)
                        {
                            bValidFile = false;
                            break;
                        }

                        bytesRead = StorageMethods.ReadFile(hFile, _buffer);
                    }

                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.SetTextColor(SystemColor.ButtonText);
                        dc.SetBackgroundColor(SystemColor.ButtonFace);
                        Encoding.UTF8.GetDecoder().Convert(_buffer.AsSpan(0, (int)bytesRead), _decoded.AsSpan(), true, out _, out int charCount, out _);
                        dc.DrawText(_decoded.AsSpan(0, charCount), rect, DTFLAGS);
                    }

                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }

        LRESULT ListBoxProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            if (message == WindowMessage.KeyDown && (VirtualKey)wParam == VirtualKey.Return)
            {
                window.GetParent().SendMessage(
                    WindowMessage.Command,
                    new WPARAM(1, (ushort)ListBoxNotification.DoubleClick),
                    (LPARAM)window);
            }

            return Windows.CallWindowProcedure(_existingListBoxWndProc, window, message, wParam, lParam);
        }
    }
}
