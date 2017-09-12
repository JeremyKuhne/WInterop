// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.DirectoryManagement;
using WInterop.Extensions.WindowExtensions;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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
            const string szAppName = "head";

            ModuleInstance module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = SystemColor.ButtonFace,
                ClassName = szAppName
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "head",
                WindowStyles.OverlappedWindow | WindowStyles.ClipChildren);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static FileTypes DIRATTR = FileTypes.ReadWrite | FileTypes.ReadOnly | FileTypes.Hidden | FileTypes.System
            | FileTypes.Directory | FileTypes.Archive | FileTypes.Drives;
        static TextFormat DTFLAGS = TextFormat.WordBreak | TextFormat.ExpandTabs | TextFormat.NoClip | TextFormat.NoPrefix;

        static WindowHandle hwndList, hwndText;
        static IntPtr OldList;

        // We need to put the delegate in a static to prevent the callback from being collected
        static WindowProcedure s_ListBoxProcedure = ListBoxProcedure;

        const int ID_LIST = 1;
        const int ID_TEXT = 2;
        const int MAXREAD = 8192;

        static bool bValidFile;
        static string szFile;
        static byte[] buffer = new byte[MAXREAD];
        static RECT rect;


        unsafe static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            const string filter = "*.*";

            switch (message)
            {
                case WindowMessage.Create:
                    SIZE baseUnits = Windows.GetDialogBaseUnits();
                    rect.left = 20 * baseUnits.cx;
                    rect.top = 3 * baseUnits.cy;

                    // Create listbox and static text windows.
                    hwndList = Windows.CreateWindow("listbox", null,
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard, ExtendedWindowStyles.None,
                        baseUnits.cx, baseUnits.cy * 3, baseUnits.cx * 13 + Windows.GetSystemMetrics(SystemMetric.CXVSCROLL), baseUnits.cy * 10,
                        window, (IntPtr)ID_LIST, ((CREATESTRUCT*)lParam)->hInstance, IntPtr.Zero);

                    hwndText = Windows.CreateWindow("static", DirectoryMethods.GetCurrentDirectory(),
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left, ExtendedWindowStyles.None,
                        baseUnits.cx, baseUnits.cy, baseUnits.cx * 260, baseUnits.cy,
                        window, (IntPtr)ID_TEXT, ((CREATESTRUCT*)lParam)->hInstance, IntPtr.Zero);

                    OldList = hwndList.SetWindowProcedure(s_ListBoxProcedure);

                    fixed (char* f = filter)
                        hwndList.SendMessage(ListBoxMessage.Directory, (uint)DIRATTR, f);
                    return 0;
                case WindowMessage.Size:
                    rect.right = lParam.LowWord;
                    rect.bottom = lParam.HighWord;
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
                        char* textBuffer = stackalloc char[iLength];
                        int result = hwndList.SendMessage(ListBoxMessage.GetText, i, textBuffer);
                        szFile = new string(textBuffer, 0, result);

                        SafeFileHandle hFile = null;
                        try
                        {
                            using (hFile = FileMethods.CreateFile(szFile, CreationDisposition.OpenExisting,
                                DesiredAccess.GenericRead, ShareMode.Read))
                            {
                                if (!hFile.IsInvalid)
                                {
                                    bValidFile = true;
                                    hwndText.SetWindowText(DirectoryMethods.GetCurrentDirectory());
                                }
                            }
                            hFile = null;
                        }
                        catch
                        {
                        }

                        if (hFile == null && szFile[0] == ('['))
                        {
                            bValidFile = false;

                            // If setting the directory doesn’t work, maybe it’s a drive change, so try that.
                            try { DirectoryMethods.SetCurrentDirectory(szFile.Substring(1, szFile.Length - 2)); }
                            catch
                            {
                                try { DirectoryMethods.SetCurrentDirectory($"{szFile[2]}:"); }
                                catch { }
                            }

                            // Get the new directory name and fill the list box.
                            hwndText.SetWindowText(DirectoryMethods.GetCurrentDirectory());
                            hwndList.SendMessage(ListBoxMessage.ResetContent, 0, 0);
                            fixed (char* f = filter)
                                hwndList.SendMessage(ListBoxMessage.Directory, (uint)DIRATTR, f);
                        }

                        window.Invalidate();
                    }
                    return 0;
                case WindowMessage.Paint:
                    if (!bValidFile)
                        break;

                    uint bytesRead;
                    using (var hFile = FileMethods.CreateFile(szFile, CreationDisposition.OpenExisting,
                        DesiredAccess.GenericRead, ShareMode.Read))
                    {
                        if (hFile.IsInvalid)
                        {
                            bValidFile = false;
                            break;
                        }

                        bytesRead = FileMethods.ReadFile(hFile, buffer, MAXREAD);
                    }

                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.SetTextColor(Windows.GetSystemColor(SystemColor.ButtonText));
                        dc.SetBackgroundColor(Windows.GetSystemColor(SystemColor.ButtonFace));
                        dc.DrawText(Encoding.UTF8.GetString(buffer), rect, DTFLAGS);
                    }

                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        unsafe static LRESULT ListBoxProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            if (message == WindowMessage.KeyDown && (VirtualKey)wParam == VirtualKey.Return)
                window.GetParent().SendMessage(WindowMessage.Command,
                    new WPARAM(1, (ushort)ListBoxNotification.DoubleClick), (LPARAM)window);

            return WindowMethods.CallWindowProcedure(OldList, window, message, wParam, lParam);
        }
    }
}
