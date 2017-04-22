// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Gdi.DataTypes;
using WInterop.Modules.DataTypes;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Windows.DataTypes;

namespace WInterop.Windows
{
    public static partial class WindowsMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
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
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern Atom RegisterClass(
                WNDCLASS lpWndClass);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633587.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern Atom RegisterClassEx(
                WNDCLASSEX lpwcx);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644899.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool UnregisterClass(
                IntPtr lpClassName,
                ModuleHandle hInstance);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633582.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetClassNameW(
                WindowHandle hWnd,
                SafeHandle lpClassName,
                int nMaxCount);

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
                WindowsStyle dwStyle,
                bool bMenu,
                ExtendedWindowsStyle dwExStyle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648402.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCaretPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648405.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648406.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ShowCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648403.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool HideCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648399.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool CreateCaret(
                WindowHandle hWnd,
                BitmapHandle hBitmap,
                int nWidth,
                int nHeight);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648400.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCaret();

            // uint.MaxValue is INFINITE, or doesn't blink
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648401.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCaretBlinkTime();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648404.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretBlinkTime(uint uMSeconds);
        }

        public static WindowHandle GetShellWindow()
        {
            return Direct.GetShellWindow();
        }

        /// <summary>
        /// Gets the specified related Window to get given Window if it exists. Otherwise
        /// returns a null WindowHandle.
        /// </summary>
        public static WindowHandle GetWindow(WindowHandle handle, GetWindowOptions option)
        {
            return Direct.GetWindow(handle, option);
        }

        public static WindowHandle GetDesktopWindow()
        {
            return Direct.GetDesktopWindow();
        }

        public static bool IsWindow(WindowHandle handle)
        {
            return Direct.IsWindow(handle);
        }

        public static bool IsWindowVisible(WindowHandle handle)
        {
            return Direct.IsWindowVisible(handle);
        }

        public static bool IsWindowUnicode(WindowHandle handle)
        {
            return Direct.IsWindowUnicode(handle);
        }

        /// <summary>
        /// Get the top child window in the specified window. If passed a null window
        /// finds the window at the top of the Z order.
        /// </summary>
        public static WindowHandle GetTopWindow(WindowHandle handle)
        {
            return Direct.GetTopWindow(handle);
        }

        public static WindowHandle GetForegroundWindow()
        {
            return Direct.GetForegroundWindow();
        }

        public static string GetClassName(WindowHandle handle)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                int count;
                while ((count = Direct.GetClassNameW(handle, buffer, (int)buffer.CharCapacity)) != 0)
                {
                    if (count == buffer.CharCapacity - 1)
                    {
                        // The buffer may not be big enough, this api simply truncates
                        buffer.EnsureCharCapacity(checked(buffer.CharCapacity * 2));
                    }
                    else
                    {
                        buffer.Length = (uint)count;
                        return buffer.ToString();
                    }
                }

                throw Errors.GetIoExceptionForLastError();
            });
        }

        /// <summary>
        /// Returns true if the current thread is a GUI thread.
        /// </summary>
        /// <param name="convertToGuiIfFalse">Tries to convert the thread to a GUI thread if it isn't already.</param>
        public static bool IsGuiThread(bool convertToGuiIfFalse = false)
        {
            int result = Direct.IsGUIThread(convertToGuiIfFalse);
            if (result == 0
                || (convertToGuiIfFalse & result == (int)WindowsError.ERROR_NOT_ENOUGH_MEMORY))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Unregisters the given class Atom.
        /// </summary>
        public static void UnregisterClass(Atom atom, ModuleHandle module)
        {
            if (!Direct.UnregisterClass(atom, module))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Unregisters the given class name.
        /// </summary>
        public static void UnregisterClass(string className, ModuleHandle module)
        {
            if (className == null)
                throw new ArgumentNullException(nameof(className));

            unsafe
            {
                fixed (char* name = className)
                {
                    if (!Direct.UnregisterClass((IntPtr)name, module))
                        throw Errors.GetIoExceptionForLastError();
                }
            }
        }

        /// <summary>
        /// Gets the value for the given system metric.
        /// </summary>
        public static int GetSystemMetrics(SystemMetric metric)
        {
            return Direct.GetSystemMetrics(metric);
        }
    }
}
