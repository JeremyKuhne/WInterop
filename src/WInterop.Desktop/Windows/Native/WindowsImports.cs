// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Modules;

namespace WInterop.Windows.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class WindowsImports
{
    // https://msdn.microsoft.com/library/windows/desktop/ms724947.aspx
    [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe bool SystemParametersInfoW(
        SystemParameterType uiAction,
        uint uiParam,
        void* pvParam,
        SystemParameterOptions fWinIni);

    // https://msdn.microsoft.com/library/windows/desktop/ms644950.aspx
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern LResult SendMessageW(
        HWND hWnd,
        MessageType Msg,
        WParam wParam,
        LParam lParam);

    // https://msdn.microsoft.com/library/windows/desktop/ms646294.aspx
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern HWND GetFocus();

    // https://msdn.microsoft.com/library/windows/desktop/ms646262.aspx
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern HWND SetCapture(
        HWND hWnd);

    // https://docs.microsoft.com/windows/win32/api/shlwapi/nc-shlwapi-dllgetversionproc
    [DllImport(Libraries.Comctl32, EntryPoint = "DllGetVersion")]
    public static extern HResult ComctlGetVersion(
        ref DllVersionInfo versionInfo);

    // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-enumchildwindows
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern bool EnumChildWindows(
        HWND hWndParent,
        EnumerateWindowProcedure lpEnumFunc,
        LParam lParam);

    // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-loadstringw
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe int LoadStringW(
        ModuleInstance hInstance,
        int uID,
        out char* lpBuffer,
        int nBufferMax);

    // https://msdn.microsoft.com/library/windows/desktop/ms648405.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetCaretPos(
        int X,
        int Y);

    // https://msdn.microsoft.com/library/windows/desktop/ms648406.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool ShowCaret(
        HWND hWnd);

    // https://msdn.microsoft.com/library/windows/desktop/ms648403.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool HideCaret(
        HWND hWnd);

    // https://msdn.microsoft.com/library/windows/desktop/ms648399.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool CreateCaret(
        HWND hWnd,
        HBITMAP hBitmap,
        int nWidth,
        int nHeight);

    // https://msdn.microsoft.com/library/windows/desktop/ms648400.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool DestroyCaret();

    // https://msdn.microsoft.com/library/windows/desktop/ms648063.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool DestroyIcon(
        HICON hIcon);

    // https://msdn.microsoft.com/library/windows/desktop/ms648384.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern CursorHandle CopyCursor(
        CursorHandle pcur);

    // https://msdn.microsoft.com/library/windows/desktop/ms648386.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool DestroyCursor(
        HCURSOR hCursor);

    // https://msdn.microsoft.com/library/windows/desktop/ms648387.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetClipCursor(
        out Rect lpRect);

    // https://msdn.microsoft.com/library/windows/desktop/ms648390.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetCursorPos(
        out Point lpPoint);

    // https://msdn.microsoft.com/library/windows/desktop/aa969464.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetPhysicalCursorPos(
        out Point lpPoint);

    // https://msdn.microsoft.com/library/windows/desktop/ms648393.aspx
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern HCURSOR SetCursor(
        CursorHandle hCursor);

    // https://msdn.microsoft.com/library/windows/desktop/ms648394.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetCursorPos(
        int X,
        int Y);

    // https://msdn.microsoft.com/library/windows/desktop/aa969465.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetPhysicalCursorPos(
        int X,
        int Y);

    // https://msdn.microsoft.com/library/windows/desktop/ms648395.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetSystemCursor(
        CursorHandle hcur,
        SystemCursor id);

    // https://msdn.microsoft.com/library/windows/desktop/ms648396.aspx
    [DllImport(Libraries.User32, ExactSpelling = true)]
    public static extern int ShowCursor(
        bool bShow);

    // https://msdn.microsoft.com/library/windows/desktop/ms647616.aspx
    [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool AppendMenuW(
        HMENU hMenu,
        MenuFlags uFlags,
        IntPtr uIDNewItem,
        IntPtr lpNewItem);

    // https://msdn.microsoft.com/library/windows/desktop/ms647624.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern HMENU CreateMenu();

    // https://msdn.microsoft.com/library/windows/desktop/ms647631.aspx
    [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
    public static extern bool DestroyMenu(
        HMENU hMenu);
}