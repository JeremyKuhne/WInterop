// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
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
        }

        public static WindowHandle GetShellWindow()
        {
            return Imports.GetShellWindow();
        }

        /// <summary>
        /// Gets the specified related Window to get given Window if it exists. Otherwise
        /// returns a null WindowHandle.
        /// </summary>
        public static WindowHandle GetWindow(WindowHandle window, GetWindowOptions option)
        {
            return Imports.GetWindow(window, option);
        }

        public static WindowHandle GetDesktopWindow()
        {
            return Imports.GetDesktopWindow();
        }

        public static bool IsWindow(WindowHandle window)
        {
            return Imports.IsWindow(window);
        }

        public static bool IsWindowVisible(WindowHandle window)
        {
            return Imports.IsWindowVisible(window);
        }

        public static bool IsWindowUnicode(WindowHandle window)
        {
            return Imports.IsWindowUnicode(window);
        }

        /// <summary>
        /// Get the top child window in the specified window. If passed a null window
        /// finds the window at the top of the Z order.
        /// </summary>
        public static WindowHandle GetTopWindow(WindowHandle window)
        {
            return Imports.GetTopWindow(window);
        }

        public static WindowHandle GetForegroundWindow()
        {
            return Imports.GetForegroundWindow();
        }

        public static string GetClassName(WindowHandle window)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                int count;
                while ((count = Imports.GetClassNameW(window, buffer, (int)buffer.CharCapacity)) != 0)
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
            int result = Imports.IsGUIThread(convertToGuiIfFalse);
            if (result == 0
                || (convertToGuiIfFalse & result == (int)WindowsError.ERROR_NOT_ENOUGH_MEMORY))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Unregisters the given class Atom.
        /// </summary>
        public static void UnregisterClass(Atom atom, SafeModuleHandle module)
        {
            if (!Imports.UnregisterClassW(atom, module))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Unregisters the given class name.
        /// </summary>
        public static void UnregisterClass(string className, SafeModuleHandle module)
        {
            if (className == null)
                throw new ArgumentNullException(nameof(className));

            unsafe
            {
                fixed (char* name = className)
                {
                    if (!Imports.UnregisterClassW((IntPtr)name, module))
                        throw Errors.GetIoExceptionForLastError();
                }
            }
        }

        /// <summary>
        /// Gets the value for the given system metric.
        /// </summary>
        public static int GetSystemMetrics(SystemMetric metric)
        {
            return Imports.GetSystemMetrics(metric);
        }

        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.MB_OK)
        {
            return MessageBox(WindowHandle.NullWindowHandle, text, caption, type);
        }

        public static CommandId MessageBox(WindowHandle owner, string text, string caption, MessageBoxType type = MessageBoxType.MB_OK)
        {
            CommandId result = Imports.MessageBoxExW(owner, text, caption, type, 0);
            if (result == CommandId.Error)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static Atom RegisterClass(WindowClass windowClass)
        {
            Atom atom = Imports.RegisterClassW(windowClass);
            if (!atom.IsValid)
                throw Errors.GetIoExceptionForLastError();

            return atom;
        }

        public static WindowClass GetClassInfo(SafeModuleHandle instance, Atom atom)
        {
            if (!Imports.GetClassInfoW(instance, atom, out WNDCLASS windowClass))
                throw Errors.GetIoExceptionForLastError();

            return new WindowClass(windowClass);
        }

        public static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyle style,
            ExtendedWindowStyle extendedStyle,
            SafeModuleHandle instance,
            int x = WindowDefines.CW_USEDEFAULT,
            int y = WindowDefines.CW_USEDEFAULT,
            int width = WindowDefines.CW_USEDEFAULT,
            int height = WindowDefines.CW_USEDEFAULT)
        {
            return CreateWindow(className, windowName, style, extendedStyle, instance, x, y, width, height, WindowHandle.NullWindowHandle, IntPtr.Zero, IntPtr.Zero);
        }

        public unsafe static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyle style,
            ExtendedWindowStyle extendedStyle,
            SafeModuleHandle instance,
            int x,
            int y,
            int width,
            int height,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            IntPtr parameters)
        {
            WindowHandle window;
            fixed (char* name = className)
            {
                window = Imports.CreateWindowExW(
                    extendedStyle,
                    (IntPtr)name,
                    windowName,
                    style,
                    x,
                    y,
                    width,
                    height,
                    parentWindow,
                    menuHandle,
                    instance,
                    parameters);
            }

            if (window == WindowHandle.NullWindowHandle)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        public static bool ShowWindow(WindowHandle window, ShowWindowCommand command)
        {
            return Imports.ShowWindow(window, command);
        }

        public static bool GetMessage(out MSG message, WindowHandle window, uint minMessage = 0, uint maxMessage = 0)
        {
            BOOL result = Imports.GetMessageW(out message, window, minMessage, maxMessage);
            if (result.RawValue == unchecked((uint)-1))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static bool TranslateMessage(ref MSG message)
        {
            return Imports.TranslateMessage(ref message);
        }

        public static bool DispatchMessage(ref MSG message)
        {
            return Imports.DispatchMessageW(ref message);
        }

        public static IntPtr DefaultWindowProcedure(WindowHandle window, MessageType message, UIntPtr wParam, IntPtr lParam)
        {
            return Imports.DefWindowProcW(window, message, wParam, lParam);
        }

        public static void PostQuitMessage(int exitCode)
        {
            Imports.PostQuitMessage(exitCode);
        }

        public static RECT GetClientRect(WindowHandle window)
        {
            if (!Imports.GetClientRect(window, out RECT rect))
                throw Errors.GetIoExceptionForLastError();

            return rect;
        }
    }
}
