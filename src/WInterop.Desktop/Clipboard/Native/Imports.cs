// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Windows.Native;

namespace WInterop.Clipboard.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://msdn.microsoft.com/library/windows/desktop/ms649048.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool OpenClipboard(
        HWND hWndNewOwner);

    // https://msdn.microsoft.com/library/windows/desktop/ms649035.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool CloseClipboard();

    // https://msdn.microsoft.com/library/windows/desktop/ms649037.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool EmptyClipboard();

    // https://msdn.microsoft.com/library/windows/desktop/ms649047.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool IsClipboardFormatAvailable(
        uint format);

    // https://msdn.microsoft.com/library/windows/desktop/ms649039.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern IntPtr GetClipboardData(
        uint uFormat);

    // https://msdn.microsoft.com/library/windows/desktop/ms649036.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern int CountClipboardFormats();

    // https://msdn.microsoft.com/library/windows/desktop/ms649041.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern HWND GetClipboardOwner();

    // https://msdn.microsoft.com/library/windows/desktop/ms649040.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern int GetClipboardFormatNameW(
        uint format,
        SafeHandle lpszFormatName,
        int cchMaxCount);

    // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-getupdatedclipboardformats
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool GetUpdatedClipboardFormats(
        ref uint lpuiFormats,
        uint cFormats,
        out uint pcFormatsOut);

    // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-setclipboarddata
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern IntPtr SetClipboardData(
        uint uFormat,
        IntPtr hMem);

    // https://msdn.microsoft.com/library/windows/desktop/ms649045.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern int GetPriorityClipboardFormat(
        uint[] paFormatPriorityList,
        int cFormats);

    // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-getopenclipboardwindow
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern HWND GetOpenClipboardWindow();

    // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-registerclipboardformatw
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern uint RegisterClipboardFormatW(
        string lpszFormat);
}