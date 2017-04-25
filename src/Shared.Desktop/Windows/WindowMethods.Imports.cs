// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Windows
{
    public static partial class WindowMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633515.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetWindow(
                WindowHandle hWnd,
                GetWindowOptions uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633509.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetNextWindow(
                WindowHandle hWnd,
                GetWindowOptions uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633510.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetParent(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633502.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetAncestor(
                WindowHandle hWnd,
                GetWindowOptions gaFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633514.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetTopWindow(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632673.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool BringWindowToTop(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetWindowPos(
                WindowHandle hWnd,
                WindowHandle hWndInsertAfter,
                int X,
                int Y,
                int cx,
                int cy,
                WindowPosition uFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646292.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetActiveWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646311.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle SetActiveWindow(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632669.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool AnimateWindow(
                WindowHandle hwnd,
                uint dwTime,
                WindowAnimationType dwFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ShowWindow(
                WindowHandle hWnd,
                ShowWindowCommand nCmdShow);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633549.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ShowWindowAsync(
                WindowHandle hWnd,
                ShowWindowCommand nCmdShow);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetForegroundWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632668.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool AllowSetForegroundWindow(
                uint dwProcessId);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool SetForegroundWindow(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633532.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool LockSetForegroundWindow(
                LockCode uLockCode);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633512.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetShellWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633504.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetDesktopWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633528.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsWindow(WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633529.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsWindowUnicode(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633530.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsWindowVisible(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633524.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsChild(
                WindowHandle hWndParent,
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633525.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int IsGUIThread(
                bool bConvert);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633586.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern Atom RegisterClassW(
                WindowClass lpWndClass);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633587.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern Atom RegisterClassExW(
                WNDCLASSEX lpwcx);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644899.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool UnregisterClassW(
                IntPtr lpClassName,
                SafeModuleHandle hInstance);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633578.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool GetClassInfoW(
                SafeModuleHandle hInstance,
                IntPtr lpClassName,
                out WNDCLASS lpWndClass);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633582.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetClassNameW(
                WindowHandle hWnd,
                SafeHandle lpClassName,
                int nMaxCount);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632679.aspx
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632680.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern WindowHandle CreateWindowExW(
                ExtendedWindowStyle dwExStyle,
                IntPtr lpClassName,
                string lpWindowName,
                WindowStyle dwStyle,
                int x,
                int y,
                int nWidth,
                int nHeight,
                WindowHandle hWndParent,
                IntPtr hMenu,
                SafeModuleHandle hInstance,
                IntPtr lpParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int GetSystemMetrics(
                SystemMetric nIndex);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633503.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetClientRect(
                WindowHandle hWnd,
                out RECT lpRect);

            // Note that AdjustWindowRect simply calls this method with an extended style of 0.
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632667.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool AdjustWindowRectEx(
                ref RECT lpRect,
                WindowStyle dwStyle,
                bool bMenu,
                ExtendedWindowStyle dwExStyle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645507.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern CommandId MessageBoxExW(
                WindowHandle hWnd,
                string lpText,
                string lpCaption,
                MessageBoxType uType,
                ushort wLanguageId);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644936.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL GetMessageW(
                out MSG lpMsg,
                WindowHandle hWnd,
                uint wMsgFilterMin,
                uint wMsgFilterMax);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644943.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool PeekMessageW(
                out MSG lpMsg,
                WindowHandle hWnd,
                uint wMsgFilterMin,
                uint wMsgFilterMax,
                ProcessMessage wRemoveMsg);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644955.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool TranslateMessage(
                ref MSG lpMsg);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644934.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool DispatchMessageW(
                ref MSG lpMsg);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633572.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr DefWindowProcW(
                WindowHandle hWnd,
                MessageType Msg,
                UIntPtr wParam,
                IntPtr lParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644945.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern void PostQuitMessage(
                int nExitCode);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787585.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetScrollPos(
                WindowHandle hWnd,
                ScrollBar nBar);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787595.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetScrollInfo(
                WindowHandle hwind,
                ScrollBar fnBar,
                [In] ref SCROLLINFO lpsi);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787599.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetScrollRange(
                WindowHandle hWnd,
                ScrollBar nBar,
                int nMinPos,
                int nMaxPos,
                bool bRedraw);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787597.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int SetScrollPos(
                WindowHandle hWnd,
                ScrollBar nBar,
                int nPos,
                bool bRedraw);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787595.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int SetScrollInfo(
                WindowHandle hwind,
                ScrollBar fnBar,
                [In] ref SCROLLINFO lpsi,
                bool fRedraw);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787601.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ShowScrollBar(
                WindowHandle hWnd,
                ScrollBar wBar,
                bool bShow);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787593.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern int ScrollWindowEx(
                WindowHandle hWnd,
                int dx,
                int dy,
                RECT* prcScroll,
                RECT* prcClip,
                IntPtr hrgnUpdate,
                RECT* prcUpdate,
                ScrollWindowFlags flags);
        }
    }
}
