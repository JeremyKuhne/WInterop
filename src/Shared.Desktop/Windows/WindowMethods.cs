// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Windows.BufferWrappers;
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

        public static WindowHandle GetParent(WindowHandle window)
        {
            WindowHandle parent = Imports.GetParent(window);
            if (!parent.IsValid)
                throw Errors.GetIoExceptionForLastError();

            return parent;
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
            var wrapper = new ClassNameWrapper { Window = window };
            return BufferHelper.TruncatingApiInvoke(ref wrapper);
        }

        public static WindowHandle GetFocus()
        {
            return Imports.GetFocus();
        }

        public static WindowHandle SetFocus(WindowHandle window)
        {
            WindowHandle prior = Imports.SetFocus(window);
            if (!prior.IsValid)
                throw Errors.GetIoExceptionForLastError();

            return prior;
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
        public static void UnregisterClass(Atom atom, ModuleInstance module)
        {
            if (!Imports.UnregisterClassW(atom, module ?? ModuleInstance.Null))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Unregisters the given class name.
        /// </summary>
        public static void UnregisterClass(string className, ModuleInstance module)
        {
            if (className == null)
                throw new ArgumentNullException(nameof(className));

            unsafe
            {
                fixed (char* name = className)
                {
                    if (!Imports.UnregisterClassW((IntPtr)name, module ?? ModuleInstance.Null))
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

        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            return MessageBox(WindowHandle.Null, text, caption, type);
        }

        public static CommandId MessageBox(WindowHandle owner, string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            CommandId result = Imports.MessageBoxExW(owner, text, caption, type, 0);
            if (result == CommandId.Error)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static unsafe Atom RegisterClass(ref WindowClass windowClass)
        {
            Atom atom;
            using (var marshaller = new WindowClass.Marshaller())
            {
                marshaller.FillNative(out WNDCLASSEX native, ref windowClass);
                atom = Imports.RegisterClassExW(ref native);
                if (!atom.IsValid)
                    throw Errors.GetIoExceptionForLastError();
            }

            return atom;
        }

        public static WindowClass GetClassInfo(ModuleInstance instance, Atom atom)
        {
            if (!Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, atom, out WNDCLASSEX wndClass))
                throw Errors.GetIoExceptionForLastError();

            return wndClass;
        }

        public unsafe static WindowClass GetClassInfo(ModuleInstance instance, string className)
        {
            WNDCLASSEX wndClass;

            fixed (char* c = className)
                if (!Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, (IntPtr)c, out wndClass))
                    throw Errors.GetIoExceptionForLastError();

            return wndClass;
        }

        public static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.None,
            int x = WindowDefines.CW_USEDEFAULT,
            int y = WindowDefines.CW_USEDEFAULT,
            int width = WindowDefines.CW_USEDEFAULT,
            int height = WindowDefines.CW_USEDEFAULT,
            ModuleInstance instance = null)
        {
            return CreateWindow(className, windowName, style, extendedStyle, x, y, width, height, WindowHandle.Null, IntPtr.Zero, instance, IntPtr.Zero);
        }

        public static WindowHandle CreateWindow(
            Atom className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.None,
            int x = WindowDefines.CW_USEDEFAULT,
            int y = WindowDefines.CW_USEDEFAULT,
            int width = WindowDefines.CW_USEDEFAULT,
            int height = WindowDefines.CW_USEDEFAULT,
            ModuleInstance instance = null)
        {
            return CreateWindow(className, windowName, style, extendedStyle, x, y, width, height, WindowHandle.Null, IntPtr.Zero, instance, IntPtr.Zero);
        }

        public static WindowHandle CreateWindow(
            Atom className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle,
            int x,
            int y,
            int width,
            int height,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            ModuleInstance instance,
            IntPtr parameters)
        {
            WindowHandle window = Imports.CreateWindowExW(
                extendedStyle,
                className,
                windowName,
                style,
                x,
                y,
                width,
                height,
                parentWindow,
                menuHandle,
                instance ?? ModuleInstance.Null,
                parameters);

            if (window == WindowHandle.Null)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        public unsafe static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle,
            int x,
            int y,
            int width,
            int height,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            ModuleInstance instance,
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
                    instance ?? ModuleInstance.Null,
                    parameters);
            }

            if (window == WindowHandle.Null)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        public static void DestroyWindow(WindowHandle window)
        {
            if (!Imports.DestroyWindow(window))
                throw Errors.GetIoExceptionForLastError();
        }

        public static IntPtr GetWindowLong(WindowHandle window, WindowLong index)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            ErrorMethods.SetLastError(WindowsError.NO_ERROR);

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
            ErrorMethods.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.SetWindowLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetWindowLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static void SetWindowText(WindowHandle window, string text)
        {
            if (!Imports.SetWindowTextW(window, text))
                throw Errors.GetIoExceptionForLastError();
        }

        public static IntPtr GetClassLong(WindowHandle window, ClassLong index)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            ErrorMethods.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.GetClassLongPtrW(window, index)
                : (IntPtr)Imports.GetClassLongW(window, index);

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr SetClassLong(WindowHandle window, ClassLong index, IntPtr value)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            // (Even though this is only documented on SetWindowLong, happens here too)
            ErrorMethods.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Support.Environment.Is64BitProcess
                ? (IntPtr)Imports.SetClassLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetClassLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static bool ShowWindow(WindowHandle window, ShowWindow command)
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

        public static LRESULT SendMessage(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
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

        public static LRESULT DefaultWindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            return Imports.DefWindowProcW(window, message, wParam, lParam);
        }

        public static void PostQuitMessage(int exitCode)
        {
            Imports.PostQuitMessage(exitCode);
        }

        public static RECT GetClientRectangle(WindowHandle window)
        {
            if (!Imports.GetClientRect(window, out RECT rect))
                throw Errors.GetIoExceptionForLastError();

            return rect;
        }

        public static RECT GetWindowRectangle(WindowHandle window)
        {
            if (!Imports.GetWindowRect(window, out RECT result))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static void SetScrollRange(WindowHandle window, ScrollBar scrollBar, int min, int max, bool redraw)
        {
            if (!Imports.SetScrollRange(window, scrollBar, min, max, redraw))
                throw Errors.GetIoExceptionForLastError();
        }

        public static int SetScrollPosition(WindowHandle window, ScrollBar scrollBar, int position, bool redraw)
        {
            int result = Imports.SetScrollPos(window, scrollBar, position, redraw);

            // There appears to be a bug in the V6 common controls where they set ERROR_ACCESSDENIED. Clearing
            // LastError doesn't help. Skip error checking if we've set position 0.
            if (result == 0 && position != 0)
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

        public static KeyboardType GetKeyboardType()
        {
            int result = Imports.GetKeyboardType(0);
            if (result == 0)
                throw Errors.GetIoExceptionForLastError();

            return (KeyboardType)result;
        }

        public static int GetKeyboardSubType()
        {
            int result = Imports.GetKeyboardType(1);
            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static int GetKeyboardFunctionKeyCount()
        {
            int result = Imports.GetKeyboardType(2);
            if (result == 0)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static KeyState GetKeyState(VirtualKey key)
        {
            return Imports.GetKeyState(key);
        }

        public static string GetKeyNameText(LPARAM lParam)
        {
            var wrapper = new KeyNameTextWrapper { LParam = lParam };

            // It is possible that there may be no name for a key, in which case the api will return 0 with GetLastError of 0.
            return BufferHelper.TruncatingApiInvoke(ref wrapper, null, Errors.Failed);
        }

        public static SIZE GetDialogBaseUnits()
        {
            int result = Imports.GetDialogBaseUnits();

            return new SIZE
            {
                cx = Conversion.LowWord(result),
                cy = Conversion.HighWord(result)
            };
        }

        public static WindowHandle GetDialogItem(WindowHandle window, int id)
        {
            WindowHandle control = Imports.GetDlgItem(window, id);
            if (!control.IsValid)
                throw Errors.GetIoExceptionForLastError();
            return control;
        }

        public static WindowHandle SetCapture(WindowHandle window)
        {
            return Imports.SetCapture(window);
        }

        public static void ReleaseCapture()
        {
            if (!Imports.ReleaseCapture())
                throw Errors.GetIoExceptionForLastError();
        }

        public static TimerId SetTimer(WindowHandle window, TimerId id, uint interval, TimerProcedure callback = null, uint delayTolerance = 0)
        {
            TimerId result = Imports.SetCoalescableTimer(window, id, interval, callback, delayTolerance);
            if (result == TimerId.Null)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static void KillTimer(WindowHandle window, TimerId id)
        {
            if (!Imports.KillTimer(window, id))
                throw Errors.GetIoExceptionForLastError();
        }

        public static COLORREF GetSystemColor(SystemColor systemColor)
        {
            return Imports.GetSysColor(systemColor);
        }

        /// <summary>
        /// Wrapper to SetWindowLong for changing the window procedure. Returns the old
        /// window procedure handle- use CallWindowProcedure to call the old method.
        /// </summary>
        public static IntPtr SetWindowProcedure(WindowHandle window, WindowProcedure newCallback)
        {
            // It is possible that the returned window procedure will not be a direct handle.
            return SetWindowLong(window,
                WindowLong.WindowProcedure,
                Marshal.GetFunctionPointerForDelegate(newCallback));
        }

        public static LRESULT CallWindowProcedure(IntPtr previous, WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            return Imports.CallWindowProcW(previous, window, message, wParam, lParam);
        }

    }
}
