// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Console;
using WInterop.ErrorHandling;
using WInterop.Gdi;
using WInterop.Globalization;
using WInterop.Modules;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation;
using WInterop.Windows.BufferWrappers;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Windows
    {
        public static Rectangle DefaultBounds => new Rectangle(WindowDefines.CW_USEDEFAULT, WindowDefines.CW_USEDEFAULT, WindowDefines.CW_USEDEFAULT, WindowDefines.CW_USEDEFAULT);

        public static void CreateMainWindowAndRun(
            WindowClass windowClass,
            string windowTitle = null,
            WindowStyles style = WindowStyles.OverlappedWindow,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            MenuHandle menuHandle = default)
        {
            CreateMainWindowAndRun(windowClass, DefaultBounds, windowTitle, style, extendedStyle, menuHandle);
        }

        /// <summary>
        /// Creates a window of the specified class and processes the message loop.
        /// Does not return until the message loop exits.
        /// </summary>
        public static void CreateMainWindowAndRun(
            WindowClass windowClass,
            Rectangle bounds,
            string windowTitle = null,
            WindowStyles style = WindowStyles.OverlappedWindow,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            MenuHandle menuHandle = default)
        {
            // Hack for launching as a .NET Core Windows Application
            ConsoleMethods.TryFreeConsole();

            if (!windowClass.IsRegistered)
                windowClass.Register();

            WindowHandle mainWindow = windowClass.CreateWindow(
                bounds,
                windowTitle,
                style,
                extendedStyle,
                isMainWindow: true,
                menuHandle: menuHandle);

            mainWindow.ShowWindow(ShowWindowCommand.Normal);
            mainWindow.UpdateWindow();

            while (GetMessage(out MSG message))
            {
                TranslateMessage(ref message);
                DispatchMessage(ref message);
            }

            // Make sure our window class doesn't get collected while were pumping
            GC.KeepAlive(windowClass);
        }

        public unsafe static WindowHandle CreateWindow(
            Atom classAtom,
            string windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            Rectangle bounds = default,
            WindowHandle parentWindow = default,
            MenuHandle menuHandle = default,
            ModuleInstance instance = null,
            IntPtr parameters = default)
        {
            WindowHandle window = Imports.CreateWindowExW(
                extendedStyle,
                (char*)classAtom.ATOM,
                windowName,
                style,
                bounds.X,
                bounds.Y,
                bounds.Width,
                bounds.Height,
                parentWindow,
                menuHandle,
                instance ?? ModuleInstance.Null,
                parameters);

            if (window.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        public unsafe static WindowHandle CreateWindow(
            string className,
            string windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            Rectangle bounds = default,
            WindowHandle parentWindow = default,
            MenuHandle menuHandle = default,
            ModuleInstance instance = default,
            IntPtr parameters = default)
        {
            WindowHandle window;
            fixed (char* name = className)
            {
                window = Imports.CreateWindowExW(
                    extendedStyle,
                    name,
                    windowName,
                    style,
                    bounds.X,
                    bounds.Y,
                    bounds.Width,
                    bounds.Height,
                    parentWindow,
                    menuHandle,
                    instance ?? ModuleInstance.Null,
                    parameters);
            }

            if (window.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return window;
        }

        /// <summary>
        /// Emit a beep.
        /// </summary>
        /// <param name="frequency">Frequency in hertz.</param>
        /// <param name="duration">Duration in milliseconds.</param>
        public static void Beep(uint frequency, uint duration)
        {
            if (!Imports.Beep(frequency, duration))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Play the specified sound (as defined in the Sound control panel).
        /// </summary>
        public static void MessageBeep(BeepType type = BeepType.SimpleBeep)
        {
            if (!Imports.MessageBeep(type))
                throw Errors.GetIoExceptionForLastError();
        }

        public static SystemParameters SystemParameters => SystemParameters.Instance;
        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

        public static SYSTEMTIME GetLocalTime() => SystemInformation.SystemInformation.GetLocalTime();

        /// <summary>
        /// Returns the average size in pixels of characters for the system font.
        /// </summary>
        public static Size GetDialogBaseUnits()
        {
            int result = Imports.GetDialogBaseUnits();
            return new Size(Conversion.LowWord(result), Conversion.HighWord(result));
        }

        public static ModuleInstance GetModule(this in WindowHandle window)
            => window.GetWindowLong(WindowLong.InstanceHandle);

        /// <summary>
        /// Wrapper to SetWindowLong for changing the window procedure. Returns the old
        /// window procedure handle- use CallWindowProcedure to call the old method.
        /// </summary>
        public static WNDPROC SetWindowProcedure(this in WindowHandle window, WindowProcedure newCallback)
        {
            // It is possible that the returned window procedure will not be a direct handle.
            return new WNDPROC(SetWindowLong(window,
                WindowLong.WindowProcedure,
                Marshal.GetFunctionPointerForDelegate(newCallback)));
        }

        public static LRESULT CallWindowProcedure(WNDPROC previous, WindowHandle window, WindowMessage message, WPARAM wParam = default, LPARAM lParam = default)
            => Imports.CallWindowProcW(previous, window, message, wParam, lParam);

        public static LRESULT SendMessage(this in WindowHandle window, ListBoxMessage message, WPARAM wParam = default, LPARAM lParam = default)
            => SendMessage(window, (WindowMessage)message, wParam, lParam);

        public static LRESULT SendMessage(this in WindowHandle window, WindowMessage message, WPARAM wParam = default, LPARAM lParam = default)
            => Imports.SendMessageW(window, message, wParam, lParam);

        public static string GetClassName(this in WindowHandle window)
        {
            var wrapper = new ClassNameWrapper { Window = window };
            return BufferHelper.TruncatingApiInvoke(ref wrapper);
        }

        public static WindowHandle GetFocus() => Imports.GetFocus();

        public static WindowHandle SetFocus(this in WindowHandle window)
        {
            WindowHandle prior = Imports.SetFocus(window);
            if (prior.IsInvalid)
                Errors.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

            return prior;
        }

        public static bool IsWindow(this in WindowHandle window) => Imports.IsWindow(window);

        public static bool IsWindowVisible(this in WindowHandle window) => Imports.IsWindowVisible(window);

        public static bool IsWindowUnicode(this in WindowHandle window) => Imports.IsWindowUnicode(window);

        /// <summary>
        /// Get the top child window in the specified window. If passed a null window
        /// finds the window at the top of the Z order.
        /// </summary>
        public static WindowHandle GetTopWindow(WindowHandle window) => Imports.GetTopWindow(window);

        public static WindowHandle GetForegroundWindow() => Imports.GetForegroundWindow();

        public static WindowHandle GetShellWindow() => Imports.GetShellWindow();

        /// <summary>
        /// Gets the specified related Window to get given Window if it exists. Otherwise
        /// returns a null WindowHandle.
        /// </summary>
        public static WindowHandle GetWindow(this in WindowHandle window, GetWindowOption option)
            => Imports.GetWindow(window, option);

        public static WindowHandle GetDesktopWindow() => Imports.GetDesktopWindow();

        public static WindowHandle GetParent(this in WindowHandle window)
        {
            WindowHandle parent = Imports.GetParent(window);
            if (parent.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return parent;
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

        public static unsafe Atom RegisterClass(ref WindowClassInfo windowClass)
        {
            Atom atom;
            using (var marshaller = new WindowClassInfo.Marshaller())
            {
                marshaller.FillNative(out WNDCLASSEX native, ref windowClass);
                atom = Imports.RegisterClassExW(ref native);
                if (!atom.IsValid)
                    throw Errors.GetIoExceptionForLastError();
            }

            return atom;
        }


        /// <summary>
        /// Unregisters the given class Atom.
        /// </summary>
        public static void UnregisterClass(Atom atom, ModuleInstance module = null)
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

        public static void DestroyWindow(this in WindowHandle window)
        {
            if (!Imports.DestroyWindow(window))
                throw Errors.GetIoExceptionForLastError();
        }

        public static IntPtr GetWindowLong(this in WindowHandle window, WindowLong index)
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

        public static IntPtr SetWindowLong(this in WindowHandle window, WindowLong index, IntPtr value)
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

        public static void SetWindowText(this in WindowHandle window, string text)
        {
            if (!Imports.SetWindowTextW(window, text))
                throw Errors.GetIoExceptionForLastError();
        }

        public static IntPtr GetClassLong(this in WindowHandle window, ClassLong index)
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

        public static IntPtr SetClassLong(this in WindowHandle window, ClassLong index, IntPtr value)
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

        /// <summary>
        /// Sets the background brush for the window class. Returns the previous background brush.
        /// </summary>
        /// <param name="ownsHandle">
        /// Whether or not the returned brush should own the handle. If true the brush handle
        /// will be deleted when disposed / finalized.
        /// </param>
        public static BrushHandle SetClassBackgroundBrush(this in WindowHandle window, BrushHandle value, bool ownsHandle = true)
        {
            IntPtr result = SetClassLong(window, ClassLong.BackgroundBrush, value.HBRUSH.Value);
            return new BrushHandle(new Gdi.Native.HBRUSH(result), ownsHandle);
        }

        public static bool ShowWindow(this in WindowHandle window, ShowWindowCommand command)
        {
            return Imports.ShowWindow(window, command);
        }

        public static void MoveWindow(this in WindowHandle window, Rectangle position, bool repaint)
        {
            if (!Imports.MoveWindow(window, position.X, position.Y, position.Width, position.Height, repaint))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Dispatches sent messages, waiting for the next message in the calling thread's message queue.
        /// </summary>
        /// <param name="window">
        /// Get messages for the specified window or all thread windows and thread messages if default.
        /// If set to -1, will only return thread messages.
        /// </param>
        /// <param name="minMessage"></param>
        /// <param name="maxMessage"></param>
        /// <returns>False when <see cref="WindowMessage.Quit"/> is returned.</returns>
        public static bool GetMessage(out MSG message, WindowHandle window = default, WindowMessage minMessage = WindowMessage.Null, WindowMessage maxMessage = WindowMessage.Null)
        {
            BOOL result = Imports.GetMessageW(out message, window, (uint)minMessage, (uint)maxMessage);

            // One special case here is -1 for an error
            if (result.RawValue == unchecked((uint)-1))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static bool PeekMessage(out MSG message, WindowHandle window = default, uint minMessage = 0, uint maxMessage = 0, PeekMessageOptions options = PeekMessageOptions.NoRemove)
        {
            return Imports.PeekMessageW(out message, window, minMessage, maxMessage, options);
        }

        public static bool TranslateMessage(ref MSG message)
        {
            return Imports.TranslateMessage(ref message);
        }

        public static bool DispatchMessage(ref MSG message)
        {
            return Imports.DispatchMessageW(ref message);
        }

        public static LRESULT DefaultWindowProcedure(this in WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            return Imports.DefWindowProcW(window, message, wParam, lParam);
        }

        public static void PostQuitMessage(int exitCode)
        {
            Imports.PostQuitMessage(exitCode);
        }

        public static Rectangle GetClientRectangle(this in WindowHandle window)
        {
            if (!Imports.GetClientRect(window, out Gdi.Native.RECT rect))
                throw Errors.GetIoExceptionForLastError();

            return rect;
        }

        public static Rectangle GetWindowRectangle(this in WindowHandle window)
        {
            if (!Imports.GetWindowRect(window, out Gdi.Native.RECT result))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static void SetScrollRange(this in WindowHandle window, ScrollBar scrollBar, int min, int max, bool redraw)
        {
            if (!Imports.SetScrollRange(window, scrollBar, min, max, redraw))
                throw Errors.GetIoExceptionForLastError();
        }

        public static int SetScrollPosition(this in WindowHandle window, ScrollBar scrollBar, int position, bool redraw)
        {
            int result = Imports.SetScrollPos(window, scrollBar, position, redraw);

            // There appears to be a bug in the V6 common controls where they set ERROR_ACCESSDENIED. Clearing
            // LastError doesn't help. Skip error checking if we've set position 0.
            if (result == 0 && position != 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static int SetScrollInfo(this in WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo, bool redraw)
        {
            scrollInfo.cbSize = (uint)sizeof(SCROLLINFO);
            int result = Imports.SetScrollInfo(window, scrollBar, ref scrollInfo, redraw);

            return result;
        }

        public static int GetScrollPosition(this in WindowHandle window, ScrollBar scrollBar)
        {
            int result = Imports.GetScrollPos(window, scrollBar);
            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static void GetScrollInfo(this in WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo)
        {
            scrollInfo.cbSize = (uint)sizeof(SCROLLINFO);
            if (!Imports.GetScrollInfo(window, scrollBar, ref scrollInfo))
                throw Errors.GetIoExceptionForLastError();
        }

        public unsafe static int ScrollWindow(this in WindowHandle window, Point delta)
        {
            int result = Imports.ScrollWindowEx(window, delta.X, delta.Y, null, null, IntPtr.Zero, null, ScrollWindowFlags.SW_ERASE | ScrollWindowFlags.SW_INVALIDATE);

            if (result == 0)
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public unsafe static int ScrollWindow(this in WindowHandle window, Point delta, Rectangle scroll, Rectangle clip)
        {
            Gdi.Native.RECT scrollRect = scroll;
            Gdi.Native.RECT clipRect = clip;

            int result = Imports.ScrollWindowEx(window, delta.X, delta.Y, &scrollRect, &clipRect, IntPtr.Zero, null, ScrollWindowFlags.SW_ERASE | ScrollWindowFlags.SW_INVALIDATE);

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
            // Although not documented this API does not appear to clear last error
            ErrorMethods.Imports.SetLastError(WindowsError.ERROR_SUCCESS);

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

        public static WindowHandle GetDialogItem(this in WindowHandle window, int id)
        {
            WindowHandle control = Imports.GetDlgItem(window, id);
            if (control.IsInvalid)
                throw Errors.GetIoExceptionForLastError();
            return control;
        }

        public static WindowHandle SetCapture(this in WindowHandle window)
        {
            return Imports.SetCapture(window);
        }

        public static void ReleaseCapture()
        {
            if (!Imports.ReleaseCapture())
                throw Errors.GetIoExceptionForLastError();
        }

        public static TimerId SetTimer(this in WindowHandle window, TimerId id, uint interval, TimerProcedure callback = null, uint delayTolerance = 0)
        {
            TimerId result = Imports.SetCoalescableTimer(window, id, interval, callback, delayTolerance);
            if (result == TimerId.Null)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static void KillTimer(this in WindowHandle window, TimerId id)
        {
            if (!Imports.KillTimer(window, id))
                throw Errors.GetIoExceptionForLastError();
        }

        public static Color GetSystemColor(SystemColor systemColor) => Imports.GetSysColor(systemColor);

        /// <summary>
        /// Gets the value for the given system metric.
        /// </summary>
        public static int GetSystemMetrics(SystemMetric metric)
        {
            return Imports.GetSystemMetrics(metric);
        }

        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            return MessageBox(default, text, caption, type);
        }

        public static CommandId MessageBox(this in WindowHandle owner, string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            CommandId result = Imports.MessageBoxExW(owner, text, caption, type, 0);
            if (result == CommandId.Error)
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        public static WindowClassInfo GetClassInfo(this ModuleInstance instance, Atom atom)
        {
            if (!Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, atom, out WNDCLASSEX wndClass))
                throw Errors.GetIoExceptionForLastError();

            return wndClass;
        }

        public unsafe static WindowClassInfo GetClassInfo(this ModuleInstance instance, string className)
        {
            WNDCLASSEX wndClass;

            fixed (char* c = className)
                if (!Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, (IntPtr)c, out wndClass))
                    throw Errors.GetIoExceptionForLastError();

            return wndClass;
        }

        /// <summary>
        /// Get the specified string resource from the given library.
        /// </summary>
        public unsafe static string LoadString(ModuleInstance library, int identifier)
        {
            // A string resource is mapped in with the dll, there is no need to allocate
            // or free a buffer.

            // Passing 0 will give us a read only handle to the string resource
            int result = Imports.LoadStringW(library, identifier, out char* buffer, 0);
            if (result <= 0)
                throw Errors.GetIoExceptionForLastError(identifier.ToString());

            // Null is not included in the result
            return new string(buffer, 0, result);
        }

        public unsafe static IconHandle LoadIcon(IconId id)
        {
            HICON handle = Imports.LoadIconW(ModuleInstance.Null, (char*)(uint)id);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return new IconHandle(handle, ownsHandle: false);
        }

        public unsafe static IconHandle LoadIcon(string name, ModuleInstance module)
        {
            fixed (char* n = name)
            {
                HICON handle = Imports.LoadIconW(module, n);

                if (handle.IsInvalid)
                    throw Errors.GetIoExceptionForLastError();

                return new IconHandle(handle, ownsHandle: false);
            }
        }

        public static MonitorHandle MonitorFromWindow(this in WindowHandle window, MonitorOption option = MonitorOption.DefaultToNull)
            => Imports.MonitorFromWindow(window, option);

        public static MonitorHandle MonitorFromPoint(Point point, MonitorOption option = MonitorOption.DefaultToNull)
            => Imports.MonitorFromPoint(point, option);

        public static MonitorHandle MonitorFromRectangle(Rectangle rectangle, MonitorOption option = MonitorOption.DefaultToNull)
        {
            Gdi.Native.RECT rect = rectangle;
            return Imports.MonitorFromRect(in rect, option);
        }

        public static MonitorInfo GetMonitorInfo(MonitorHandle monitor)
        {
            MonitorInfo info = MonitorInfo.Create();
            Imports.GetMonitorInfoW(monitor, ref info);
            return info;
        }
    }
}
