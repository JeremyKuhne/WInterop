// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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
        public static WindowHandle GetShellWindow()
        {
            return Imports.GetShellWindow();
        }

        /// <summary>
        /// Gets the specified related Window to get given Window if it exists. Otherwise
        /// returns a null WindowHandle.
        /// </summary>
        public static WindowHandle GetWindow(WindowHandle window, GetWindowOption option)
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
            return BufferHelper.CachedTruncatingApiInvoke((buffer) => Imports.GetClassNameW(window, buffer, (int)buffer.CharCapacity));
        }

        public static WindowHandle GetFocus()
        {
            return Imports.GetFocus();
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
            return MessageBox(WindowHandle.Null, text, caption, type);
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
            SafeModuleHandle instance,
            string className,
            string windowName,
            WindowStyle style,
            ExtendedWindowStyle extendedStyle = ExtendedWindowStyle.None,
            int x = WindowDefines.CW_USEDEFAULT,
            int y = WindowDefines.CW_USEDEFAULT,
            int width = WindowDefines.CW_USEDEFAULT,
            int height = WindowDefines.CW_USEDEFAULT)
        {
            return CreateWindow(instance, className, windowName, style, extendedStyle, x, y, width, height, WindowHandle.Null, IntPtr.Zero, IntPtr.Zero);
        }

        public unsafe static WindowHandle CreateWindow(
            SafeModuleHandle instance,
            string className,
            string windowName,
            WindowStyle style,
            ExtendedWindowStyle extendedStyle,
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

            if (window == WindowHandle.Null)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        public static IntPtr GetWindowLong(WindowHandle window, WindowLong index)
        {
            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.GetWindowLongPtrW(window, index)
                : (IntPtr)Imports.GetWindowLongW(window, index);

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr SetWindowLong(WindowHandle window, WindowLong index, IntPtr value)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            ErrorHandling.ErrorMethods.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.SetWindowLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetWindowLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr GetClassLong(WindowHandle window, ClassLong index)
        {
            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.GetClassLongPtrW(window, index)
                : (IntPtr)Imports.GetClassLongW(window, index);

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr SetClassLong(WindowHandle window, ClassLong index, IntPtr value)
        {
            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.SetClassLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetClassLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static bool ShowWindow(WindowHandle window, ShowWindowCommand command)
        {
            return Imports.ShowWindow(window, command);
        }

        public static void MoveWindow(WindowHandle window, int x, int y, int width, int height, bool repaint)
        {
            if (!Imports.MoveWindow(window, x, y, width, height, repaint))
                throw Errors.GetIoExceptionForLastError();
        }

        public static bool GetMessage(out MSG message, WindowHandle window, uint minMessage = 0, uint maxMessage = 0)
        {
            BOOL result = Imports.GetMessageW(out message, window, minMessage, maxMessage);
            if (result.RawValue == unchecked((uint)-1))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static bool PeekMessage(out MSG message, WindowHandle window, uint minMessage, uint maxMessage, PeekMessageOptions options)
        {
            return Imports.PeekMessageW(out message, window, minMessage, maxMessage, options);
        }

        public static LRESULT SendMessage(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            return Imports.SendMessageW(window, message, wParam, lParam);
        }

        public static bool TranslateMessage(ref MSG message)
        {
            return Imports.TranslateMessage(ref message);
        }

        public static bool DispatchMessage(ref MSG message)
        {
            return Imports.DispatchMessageW(ref message);
        }

        public static LRESULT DefaultWindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
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

        public static void SetScrollRange(WindowHandle window, ScrollBar scrollBar, int min, int max, bool redraw)
        {
            if (!Imports.SetScrollRange(window, scrollBar, min, max, redraw))
                throw Errors.GetIoExceptionForLastError();
        }

        public static int SetScrollPosition(WindowHandle window, ScrollBar scrollBar, int position, bool redraw)
        {
            int result = Imports.SetScrollPos(window, scrollBar, position, redraw);
            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static int SetScrollInfo(WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo, bool redraw)
        {
            scrollInfo.cbSize = (uint)sizeof(SCROLLINFO);
            int result = Imports.SetScrollInfo(window, scrollBar, ref scrollInfo, redraw);

            return result;
        }

        public static int GetScrollPosition(WindowHandle window, ScrollBar scrollBar)
        {
            int result = Imports.GetScrollPos(window, scrollBar);
            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static void GetScrollInfo(WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo)
        {
            scrollInfo.cbSize = (uint)sizeof(SCROLLINFO);
            if (!Imports.GetScrollInfo(window, scrollBar, ref scrollInfo))
                throw Errors.GetIoExceptionForLastError();
        }

        public unsafe static int ScrollWindow(WindowHandle window, int dx, int dy)
        {
            int result = Imports.ScrollWindowEx(window, dx, dy, null, null, IntPtr.Zero, null, ScrollWindowFlags.SW_ERASE | ScrollWindowFlags.SW_INVALIDATE);

            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static int ScrollWindow(WindowHandle window, int dx, int dy, RECT scroll, RECT clip)
        {
            int result = Imports.ScrollWindowEx(window, dx, dy, &scroll, &clip, IntPtr.Zero, null, ScrollWindowFlags.SW_ERASE | ScrollWindowFlags.SW_INVALIDATE);

            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static string GetKeyNameText(LPARAM lParam)
        {
            return BufferHelper.CachedTruncatingApiInvoke((buffer) => Imports.GetKeyNameTextW(lParam, buffer, (int)buffer.CharCapacity));
        }
    }
}
