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
                GetWindowOption uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633520.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetWindowTextW(
                WindowHandle hWnd,
                SafeHandle lpString,
                int nMaxCount);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633546.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool SetWindowTextW(
                WindowHandle hWnd,
                string lpString);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633517.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern uint GetWindowModuleFileNameW(
                WindowHandle hwnd,
                SafeHandle lpszFileName,
                uint cchFileNameMax);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633584.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetWindowLongW(
                WindowHandle hWnd,
                WindowLong nIndex);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633585.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern long GetWindowLongPtrW(
                WindowHandle hWnd,
                WindowLong nIndex);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633591.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int SetWindowLongW(
                WindowHandle hWnd,
                WindowLong nIndex,
                int dwNewLong);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644898.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern long SetWindowLongPtrW(
                WindowHandle hWnd,
                WindowLong nIndex,
                long dwNewLong);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633580.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetClassLongW(
                WindowHandle hWnd,
                ClassLong nIndex);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633581.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern long GetClassLongPtrW(
                WindowHandle hWnd,
                ClassLong nIndex);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633588.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int SetClassLongW(
                WindowHandle hWnd,
                ClassLong nIndex,
                int dwNewLong);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633589.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern long SetClassLongPtrW(
                WindowHandle hWnd,
                ClassLong nIndex,
                long dwNewLong);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633509.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetNextWindow(
                WindowHandle hWnd,
                GetWindowOption uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633510.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetParent(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633502.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetAncestor(
                WindowHandle hWnd,
                GetWindowOption gaFlags);

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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633518.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetWindowPlacement(
                WindowHandle hwnd,
                ref WINDOWPLACEMENT lpwndpl);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633544.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetWindowPlacement(
                WindowHandle hwnd,
                [In] ref WINDOWPLACEMENT lpwndpl);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633519.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetWindowRect(
                WindowHandle hWnd,
                out RECT lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool MoveWindow(
                WindowHandle hWnd,
                int X,
                int Y,
                int nWidth,
                int nHeight,
                bool bRepaint);

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
                ShowWindow nCmdShow);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633549.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ShowWindowAsync(
                WindowHandle hWnd,
                ShowWindow nCmdShow);

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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646303.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsWindowEnabled(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646291.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool EnableWindow(
                WindowHandle hWnd,
                bool bEnable);

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
                ExtendedWindowStyles dwExStyle,
                IntPtr lpClassName,
                string lpWindowName,
                WindowStyles dwStyle,
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724947.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern bool SystemParametersInfoW(
                SystemParameterType uiAction,
                uint uiParam,
                void* pvParam,
                SystemParameterOptions fWinIni);

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
                WindowStyles dwStyle,
                bool bMenu,
                ExtendedWindowStyles dwExStyle);

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
                PeekMessageOptions wRemoveMsg);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644950.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern LRESULT SendMessageW(
                WindowHandle hWnd,
                WindowMessage Msg,
                WPARAM wParam,
                LPARAM lParam);

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
            public static extern LRESULT DefWindowProcW(
                WindowHandle hWnd,
                WindowMessage Msg,
                WPARAM wParam,
                LPARAM lParam);

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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646293.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern short GetAsyncKeyState(
                VirtualKey vKey);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646294.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetFocus();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646312.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle SetFocus(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724336.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetKeyboardType(
                int nTypeFlag);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646300.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetKeyNameTextW(
                int lParam,
                SafeHandle lpString,
                int cchSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646301.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern short GetKeyState(
                VirtualKey vKey);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646302.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool GetLastInputInfo(
                ref LASTINPUTINFO plii);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645441.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle CreateDialogIndirectParamW(
                SafeModuleHandle hInstance,
                SafeHandle lpTemplate,
                WindowHandle hWndParent,
                DialogProcedure lpDialogFunc,
                LPARAM lParamInit);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645445.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle CreateDialogParamW(
                SafeModuleHandle hInstance,
                string lpTemplateName,
                WindowHandle hWndParent,
                DialogProcedure lpDialogFunc,
                LPARAM dwInitParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645450.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern LRESULT DefDlgProcW(
                WindowHandle hDlg,
                WindowMessage Msg,
                WPARAM wParam,
                LPARAM lParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645461.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr DialogBoxIndirectParamW(
                SafeModuleHandle hInstance,
                SafeHandle hDialogTemplate,
                WindowHandle hWndParent,
                DialogProcedure lpDialogFunc,
                LPARAM dwInitParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645465.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr DialogBoxParamW(
                SafeModuleHandle hInstance,
                string lpTemplateName,
                WindowHandle hWndParent,
                DialogProcedure lpDialogFunc,
                LPARAM dwInitParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645472.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool EndDialog(
                WindowHandle hDlg,
                IntPtr nResult);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645475.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int GetDialogBaseUnits();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645478.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetDlgCtrlID(
                WindowHandle hwndCtl);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645481.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetDlgItem(
                WindowHandle hDlg,
                int nIDDlgItem);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645485.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetDlgItemInt(
                WindowHandle hDlg,
                int nIDDlgItem,
                ref bool lpTranslated,
                bool bSigned);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645489.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetDlgItemTextW(
                WindowHandle hDlg,
                int nIDDlgItem,
                SafeHandle lpString,
                int nMaxCount);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645492.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetNextDlgGroupItem(
                WindowHandle hDlg,
                WindowHandle hCtl,
                bool bPrevious);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645495.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetNextDlgTabItem(
                WindowHandle hDlg,
                WindowHandle hCtl,
                bool bPrevious);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645498.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsDialogMessageW(
                WindowHandle hDlg,
                [In] ref MSG lpMsg);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645502.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool MapDialogRect(
                WindowHandle hDlg,
                ref RECT lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dn910915.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern string MB_GetString(
                uint wBtn);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645515.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern LRESULT SendDlgItemMessageW(
                WindowHandle hDlg,
                int nIDDlgItem,
                WindowMessage Msg,
                WPARAM wParam,
                LPARAM lParam);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645518.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetDlgItemInt(
                WindowHandle hDlg,
                int nIDDlgItem,
                uint uValue,
                bool bSigned);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645521.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetDlgItemTextW(
                WindowHandle hDlg,
                int nIDDlgItem,
                string lpString);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646256.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool DragDetect(
                WindowHandle hwnd,
                POINT pt);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646257.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetCapture();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646261.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ReleaseCapture();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646262.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle SetCapture(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646258.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern uint GetDoubleClickTime();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646263.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetDoubleClickTime(
                uint uInterval);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646264.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool SwapMouseButton(
                bool fSwap);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646259.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetMouseMovePointsEx(
                uint cbSize,
                [In] ref MOUSEMOVEPOINT lppt,
                MOUSEMOVEPOINT[] lpptBuf,
                int nBufPoints,
                uint resolution);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646265.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool TrackMouseEvent(
                ref TRACKMOUSEEVENT lpEventTrack);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644906.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern TimerId SetTimer(
                WindowHandle hWnd,
                TimerId nIDEvent,
                uint uElapse,
                TimerProcedure lpTimerFunc);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644903.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool KillTimer(
                WindowHandle hWnd,
                TimerId uIDEvent);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh405404.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern TimerId SetCoalescableTimer(
                WindowHandle hWnd,
                TimerId nIdEvent,
                uint uElapse,
                TimerProcedure lpTimerFunc,
                uint uToleranceDelay);
        }
    }
}
