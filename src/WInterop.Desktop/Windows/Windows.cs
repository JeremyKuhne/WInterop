// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Globalization;
using WInterop.Modules;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Windows
    {
        public static Rectangle DefaultBounds
            => new Rectangle(
                WindowDefines.CW_USEDEFAULT,
                WindowDefines.CW_USEDEFAULT,
                WindowDefines.CW_USEDEFAULT,
                WindowDefines.CW_USEDEFAULT);

        public static void CreateMainWindowAndRun(
            WindowClass windowClass,
            string? windowTitle = null,
            WindowStyles style = WindowStyles.OverlappedWindow,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            MenuHandle menuHandle = default)
        {
            CreateMainWindowAndRun(windowClass, DefaultBounds, windowTitle, style, extendedStyle, menuHandle);
        }

        /// <summary>
        ///  Creates a window of the specified class and processes the message loop.
        ///  Does not return until the message loop exits.
        /// </summary>
        public static void CreateMainWindowAndRun(
            WindowClass windowClass,
            Rectangle bounds,
            string? windowTitle = null,
            WindowStyles style = WindowStyles.OverlappedWindow,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            MenuHandle menuHandle = default)
        {
            if (!windowClass.IsRegistered)
                windowClass.Register();

            WindowHandle mainWindow = windowClass.CreateWindow(
                bounds,
                windowTitle,
                style,
                extendedStyle,
                isMainWindow: true,
                menuHandle: menuHandle);

            try
            {
                mainWindow.ShowWindow(ShowWindowCommand.Normal);
                mainWindow.UpdateWindow();

                while (GetMessage(out WindowMessage message))
                {
                    TranslateMessage(ref message);
                    DispatchMessage(ref message);
                }

                // Make sure our window class doesn't get collected while we're pumping messages
                GC.KeepAlive(windowClass);
            }
            catch
            {
                // Hit the P/Invoke directly as we want to throw the original error.
                Imports.DestroyWindow(mainWindow);
                throw;
            }
        }

        public static unsafe WindowHandle CreateWindow(
            Atom classAtom,
            string? windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            Rectangle bounds = default,
            WindowHandle parentWindow = default,
            MenuHandle menuHandle = default,
            ModuleInstance? instance = null,
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
                Error.ThrowLastError();

            return window;
        }

        public static unsafe WindowHandle CreateWindow(
            string className,
            string? windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            Rectangle bounds = default,
            WindowHandle parentWindow = default,
            MenuHandle menuHandle = default,
            ModuleInstance? instance = default,
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
                Error.ThrowLastError();

            return window;
        }

        /// <summary>
        ///  Emit a beep.
        /// </summary>
        /// <param name="frequency">Frequency in hertz.</param>
        /// <param name="duration">Duration in milliseconds.</param>
        public static void Beep(uint frequency, uint duration)
            => Error.ThrowLastErrorIfFalse(Imports.Beep(frequency, duration));

        /// <summary>
        ///  Play the specified sound (as defined in the Sound control panel).
        /// </summary>
        public static void MessageBeep(BeepType type = BeepType.SimpleBeep)
            => Error.ThrowLastErrorIfFalse(Imports.MessageBeep(type));

        public static SystemParameters SystemParameters => SystemParameters.Instance;
        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

        /// <summary>
        ///  Returns the average size in pixels of characters for the system font.
        /// </summary>
        public static Size GetDialogBaseUnits()
        {
            int result = Imports.GetDialogBaseUnits();
            return new Size(Conversion.LowWord(result), Conversion.HighWord(result));
        }

        public static ModuleInstance GetModule(this in WindowHandle window)
            => window.GetWindowLong(WindowLong.InstanceHandle);

        /// <summary>
        ///  Wrapper to SetWindowLong for changing the window procedure. Returns the old
        ///  window procedure handle- use CallWindowProcedure to call the old method.
        /// </summary>
        public static WNDPROC SetWindowProcedure(this in WindowHandle window, WindowProcedure newCallback)
        {
            // It is possible that the returned window procedure will not be a direct handle.
            return new WNDPROC(SetWindowLong(
                window,
                WindowLong.WindowProcedure,
                Marshal.GetFunctionPointerForDelegate(newCallback)));
        }

        public static LResult CallWindowProcedure(WNDPROC previous, WindowHandle window, MessageType message, WParam wParam = default, LParam lParam = default)
            => Imports.CallWindowProcW(previous, window, message, wParam, lParam);

        public static LResult SendMessage(this in WindowHandle window, ListBoxMessage message, WParam wParam = default, LParam lParam = default)
            => SendMessage(window, (MessageType)message, wParam, lParam);

        public static LResult SendMessage(this in WindowHandle window, MessageType message, WParam wParam = default, LParam lParam = default)
            => Imports.SendMessageW(window, message, wParam, lParam);

        public static unsafe string GetClassName(this WindowHandle window)
        {
            return PlatformInvoke.GrowableBufferInvoke(
                (ref ValueBuffer<char> buffer) =>
                {
                    fixed (char* b = buffer)
                    {
                        return (uint)Imports.GetClassNameW(window, b, (int)buffer.Length);
                    }
                },
                ReturnSizeSemantics.BufferTruncates);
        }

        public static WindowHandle GetFocus() => Imports.GetFocus();

        public static WindowHandle SetFocus(this in WindowHandle window)
        {
            WindowHandle prior = Imports.SetFocus(window);
            if (prior.IsInvalid)
                Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

            return prior;
        }

        public static bool IsWindow(this in WindowHandle window) => Imports.IsWindow(window);

        public static bool IsWindowVisible(this in WindowHandle window) => Imports.IsWindowVisible(window);

        public static bool IsWindowUnicode(this in WindowHandle window) => Imports.IsWindowUnicode(window);

        /// <summary>
        ///  Get the top child window in the specified window. If passed a null window
        ///  finds the window at the top of the Z order.
        /// </summary>
        public static WindowHandle GetTopWindow(WindowHandle window) => Imports.GetTopWindow(window);

        public static WindowHandle GetForegroundWindow() => Imports.GetForegroundWindow();

        public static WindowHandle GetShellWindow() => Imports.GetShellWindow();

        public static WindowHandle GetActiveWindow() => Imports.GetActiveWindow();

        /// <summary>
        ///  Gets the specified related Window to get given Window if it exists. Otherwise
        ///  returns a null WindowHandle.
        /// </summary>
        public static WindowHandle GetWindow(this in WindowHandle window, GetWindowOption option)
            => Imports.GetWindow(window, option);

        public static WindowHandle GetDesktopWindow() => Imports.GetDesktopWindow();

        public static WindowHandle GetParent(this in WindowHandle window)
        {
            WindowHandle parent = Imports.GetParent(window);
            if (parent.IsInvalid)
                Error.ThrowLastError();

            return parent;
        }

        /// <summary>
        ///  Returns true if the current thread is a GUI thread.
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
            using (WindowClassInfo.Marshaller marshaller = default)
            {
                marshaller.FillNative(out WNDCLASSEX native, ref windowClass);
                atom = Imports.RegisterClassExW(ref native);
                if (!atom.IsValid)
                    Error.ThrowLastError();
            }

            return atom;
        }

        /// <summary>
        ///  Unregisters the given class Atom.
        /// </summary>
        public static void UnregisterClass(Atom atom, ModuleInstance? module = null)
        {
            if (!Imports.UnregisterClassW(atom, module ?? ModuleInstance.Null))
                Error.ThrowLastError();
        }

        /// <summary>
        ///  Unregisters the given class name.
        /// </summary>
        public static unsafe void UnregisterClass(string className, ModuleInstance? module)
        {
            if (className == null)
                throw new ArgumentNullException(nameof(className));

            fixed (char* name = className)
            {
                Error.ThrowLastErrorIfFalse(
                    Imports.UnregisterClassW((IntPtr)name, module ?? ModuleInstance.Null),
                    className);
            }
        }

        public static void DestroyWindow(this in WindowHandle window)
            => Error.ThrowLastErrorIfFalse(Imports.DestroyWindow(window));

        public static IntPtr GetWindowLong(this in WindowHandle window, WindowLong index)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            Error.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Environment.Is64BitProcess
                ? (IntPtr)Imports.GetWindowLongPtrW(window, index)
                : (IntPtr)Imports.GetWindowLongW(window, index);

            if (result == IntPtr.Zero)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr SetWindowLong(this in WindowHandle window, WindowLong index, IntPtr value)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            Error.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Environment.Is64BitProcess
                ? (IntPtr)Imports.SetWindowLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetWindowLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static void SetWindowText(this in WindowHandle window, string text)
            => Error.ThrowLastErrorIfFalse(Imports.SetWindowTextW(window, text));

        public static IntPtr GetClassLong(this in WindowHandle window, ClassLong index)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            Error.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Environment.Is64BitProcess
                ? (IntPtr)Imports.GetClassLongPtrW(window, index)
                : (IntPtr)Imports.GetClassLongW(window, index);

            if (result == IntPtr.Zero)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static IntPtr SetClassLong(this in WindowHandle window, ClassLong index, IntPtr value)
        {
            // Unfortunate, but this is necessary to tell if there is really an error
            // (Even though this is only documented on SetWindowLong, happens here too)
            Error.SetLastError(WindowsError.NO_ERROR);

            IntPtr result = Environment.Is64BitProcess
                ? (IntPtr)Imports.SetClassLongPtrW(window, index, value.ToInt64())
                : (IntPtr)Imports.SetClassLongW(window, index, value.ToInt32());

            if (result == IntPtr.Zero)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        /// <summary>
        ///  Sets the background brush for the window class. Returns the previous background brush.
        /// </summary>
        /// <param name="ownsHandle">
        ///  Whether or not the returned brush should own the handle. If true the brush handle
        ///  will be deleted when disposed or finalized.
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
            => Error.ThrowLastErrorIfFalse(
                Imports.MoveWindow(window, position.X, position.Y, position.Width, position.Height, repaint));

        /// <summary>
        ///  Dispatches sent messages, waiting for the next message in the calling thread's message queue.
        /// </summary>
        /// <param name="window">
        ///  Get messages for the specified window or all thread windows and thread messages if default.
        ///  If set to -1, will only return thread messages.
        /// </param>
        /// <returns>False when <see cref="MessageType.Quit"/> is returned.</returns>
        public static bool GetMessage(out WindowMessage message, WindowHandle window = default, MessageType minMessage = MessageType.Null, MessageType maxMessage = MessageType.Null)
        {
            IntBoolean result = Imports.GetMessageW(out message, window, (uint)minMessage, (uint)maxMessage);

            // One special case here is -1 for an error
            if (result.RawValue == -1)
                Error.ThrowLastError();

            return result;
        }

        public static bool PeekMessage(
            out WindowMessage message,
            WindowHandle window = default,
            uint minMessage = 0,
            uint maxMessage = 0,
            PeekMessageOptions options = PeekMessageOptions.NoRemove)
        {
            return Imports.PeekMessageW(out message, window, minMessage, maxMessage, options);
        }

        public static bool TranslateMessage(ref WindowMessage message)
        {
            return Imports.TranslateMessage(ref message);
        }

        public static bool DispatchMessage(ref WindowMessage message)
        {
            return Imports.DispatchMessageW(ref message);
        }

        public static LResult DefaultWindowProcedure(
            this in WindowHandle window,
            MessageType message,
            WParam wParam,
            LParam lParam)
        {
            return Imports.DefWindowProcW(window, message, wParam, lParam);
        }

        public static void PostQuitMessage(int exitCode) => Imports.PostQuitMessage(exitCode);

        /// <summary>
        ///  Returns the logical client coordinates of the given <paramref name="window"/>.
        /// </summary>
        public static Rectangle GetClientRectangle(this in WindowHandle window)
        {
            Rect rect = default;
            Error.ThrowLastErrorIfFalse(Imports.GetClientRect(window, ref rect));
            return rect;
        }

        /// <summary>
        ///  Dimensions of the bounding rectangle of the specified <paramref name="window"/>
        ///  in screen coordinates relative to the upper-left corner.
        /// </summary>
        public static Rectangle GetWindowRectangle(this in WindowHandle window)
        {
            Rect rect = default;
            Error.ThrowLastErrorIfFalse(Imports.GetWindowRect(window, ref rect));
            return rect;
        }

        public static void SetScrollRange(this in WindowHandle window, ScrollBar scrollBar, int min, int max, bool redraw)
        {
            Error.ThrowLastErrorIfFalse(
                Imports.SetScrollRange(window, scrollBar, min, max, redraw));
        }

        public static int SetScrollPosition(this in WindowHandle window, ScrollBar scrollBar, int position, bool redraw)
        {
            int result = Imports.SetScrollPos(window, scrollBar, position, redraw);

            // There appears to be a bug in the V6 common controls where they set ERROR_ACCESSDENIED. Clearing
            // LastError doesn't help. Skip error checking if we've set position 0.
            if (result == 0 && position != 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static unsafe int SetScrollInfo(this in WindowHandle window, ScrollBar scrollBar, ref ScrollInfo scrollInfo, bool redraw)
        {
            scrollInfo.Size = (uint)sizeof(ScrollInfo);
            int result = Imports.SetScrollInfo(window, scrollBar, ref scrollInfo, redraw);

            return result;
        }

        public static int GetScrollPosition(this in WindowHandle window, ScrollBar scrollBar)
        {
            int result = Imports.GetScrollPos(window, scrollBar);
            if (result == 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static unsafe void GetScrollInfo(this in WindowHandle window, ScrollBar scrollBar, ref ScrollInfo scrollInfo)
        {
            scrollInfo.Size = (uint)sizeof(ScrollInfo);
            Error.ThrowLastErrorIfFalse(
                Imports.GetScrollInfo(window, scrollBar, ref scrollInfo));
        }

        public static unsafe int ScrollWindow(this in WindowHandle window, Point delta)
        {
            int result = Imports.ScrollWindowEx(window, delta.X, delta.Y, null, null, IntPtr.Zero, null, ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate);

            if (result == 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static unsafe int ScrollWindow(this in WindowHandle window, Point delta, Rectangle scroll, Rectangle clip)
        {
            Rect scrollRect = scroll;
            Rect clipRect = clip;

            int result = Imports.ScrollWindowEx(window, delta.X, delta.Y, &scrollRect, &clipRect, IntPtr.Zero, null, ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate);

            if (result == 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static KeyboardType GetKeyboardType()
        {
            int result = Imports.GetKeyboardType(0);
            if (result == 0)
                Error.ThrowLastError();

            return (KeyboardType)result;
        }

        public static int GetKeyboardSubType()
        {
            // Although not documented this API does not appear to clear last error
            Errors.Native.Imports.SetLastError(WindowsError.ERROR_SUCCESS);

            int result = Imports.GetKeyboardType(1);
            if (result == 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

            return result;
        }

        public static int GetKeyboardFunctionKeyCount()
        {
            int result = Imports.GetKeyboardType(2);
            if (result == 0)
                Error.ThrowLastError();

            return result;
        }

        public static KeyState GetKeyState(VirtualKey key)
        {
            return Imports.GetKeyState(key);
        }

        public static unsafe string GetKeyNameText(LParam lParam)
        {
            return PlatformInvoke.GrowableBufferInvoke(
                (ref ValueBuffer<char> buffer) =>
                {
                    fixed (char* b = buffer)
                    {
                        return checked((uint)Imports.GetKeyNameTextW(lParam, b, (int)buffer.Length));
                    }
                },
                ReturnSizeSemantics.BufferTruncates,

                // It is possible that there may be no name for a key,
                // in which case the api will return 0 with GetLastError of 0.
                shouldThrow: ErrorExtensions.Failed);
        }

        public static WindowHandle GetDialogItem(this in WindowHandle window, int id)
        {
            WindowHandle control = Imports.GetDlgItem(window, id);
            if (control.IsInvalid)
                Error.ThrowLastError();
            return control;
        }

        public static WindowHandle SetCapture(this in WindowHandle window)
        {
            return Imports.SetCapture(window);
        }

        public static void ReleaseCapture()
            => Error.ThrowLastErrorIfFalse(Imports.ReleaseCapture());

        public static TimerId SetTimer(
            this in WindowHandle window,
            TimerId id,
            uint interval,
            TimerProcedure? callback = null,
            uint delayTolerance = 0)
        {
            TimerId result = Imports.SetCoalescableTimer(window, id, interval, callback, delayTolerance);
            if (result == TimerId.Null)
                Error.ThrowLastError();

            return result;
        }

        public static void KillTimer(this in WindowHandle window, TimerId id)
            => Error.ThrowLastErrorIfFalse(Imports.KillTimer(window, id));

        public static Color GetSystemColor(SystemColor systemColor) => Imports.GetSysColor(systemColor);

        /// <summary>
        ///  Gets the value for the given system metric.
        /// </summary>
        public static int GetSystemMetrics(SystemMetric metric) => Imports.GetSystemMetrics(metric);

        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            return MessageBox(default, text, caption, type);
        }

        public static CommandId MessageBox(this in WindowHandle owner, string text, string caption, MessageBoxType type = MessageBoxType.Ok)
        {
            CommandId result = Imports.MessageBoxExW(owner, text, caption, type, 0);
            if (result == CommandId.Error)
                Error.ThrowLastError();

            return result;
        }

        public static WindowClassInfo GetClassInfo(this ModuleInstance instance, Atom atom)
        {
            Error.ThrowLastErrorIfFalse(
                Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, atom, out WNDCLASSEX wndClass));

            return wndClass;
        }

        public static unsafe WindowClassInfo GetClassInfo(this ModuleInstance instance, string className)
        {
            WNDCLASSEX wndClass;

            fixed (char* c = className)
                Error.ThrowLastErrorIfFalse(Imports.GetClassInfoExW(instance ?? ModuleInstance.Null, (IntPtr)c, out wndClass));

            return wndClass;
        }

        // How can I tell that somebody used the MAKEINTRESOURCE macro to smuggle an integer inside a pointer?
        // https://blogs.msdn.microsoft.com/oldnewthing/20130925-00/?p=3123/
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648029.aspx

        /// <summary>
        ///  Makes a resource pointer for the given value. [MAKEINTRESOURCE]
        /// </summary>
        public static IntPtr MakeIntResource(ushort integer) => (IntPtr)integer;

        /// <summary>
        ///  Returns true if the given pointer is an int resource. [IS_INTRESOURCE]
        /// </summary>
        public static bool IsIntResource(IntPtr pointer) => ((ulong)pointer) >> 16 == 0;

        /// <summary>
        ///  Get the specified string resource from the given library.
        /// </summary>
        public static unsafe string LoadString(ModuleInstance library, int identifier)
        {
            // A string resource is mapped in with the dll, there is no need to allocate
            // or free a buffer.

            // Passing 0 will give us a read only handle to the string resource
            int result = Imports.LoadStringW(library, identifier, out char* buffer, 0);
            if (result <= 0)
                Error.ThrowLastError(identifier.ToString());

            // Null is not included in the result
            return new string(buffer, 0, result);
        }

        public static unsafe IconHandle LoadIcon(IconId id)
        {
            HICON handle = Imports.LoadIconW(ModuleInstance.Null, (char*)(uint)id);
            if (handle.IsInvalid)
                Error.ThrowLastError();

            return new IconHandle(handle, ownsHandle: false);
        }

        public static unsafe IconHandle LoadIcon(string name, ModuleInstance module)
        {
            fixed (char* n = name)
            {
                HICON handle = Imports.LoadIconW(module, n);

                if (handle.IsInvalid)
                    Error.ThrowLastError();

                return new IconHandle(handle, ownsHandle: false);
            }
        }

        public static MonitorHandle MonitorFromWindow(this in WindowHandle window, MonitorOption option = MonitorOption.DefaultToNull)
            => Imports.MonitorFromWindow(window, option);

        public static MonitorHandle MonitorFromPoint(Point point, MonitorOption option = MonitorOption.DefaultToNull)
            => Imports.MonitorFromPoint(point, option);

        public static MonitorHandle MonitorFromRectangle(Rectangle rectangle, MonitorOption option = MonitorOption.DefaultToNull)
        {
            Rect rect = rectangle;
            return Imports.MonitorFromRect(in rect, option);
        }

        public static unsafe MonitorInfo GetMonitorInfo(this MonitorHandle monitor)
        {
            MonitorInfo info = MonitorInfo.Create();
            Imports.GetMonitorInfoW(monitor, &info);
            return info;
        }

        public static unsafe ExtendedMonitorInfo GetExtendedMonitorInfo(this MonitorHandle monitor)
        {
            ExtendedMonitorInfo info = ExtendedMonitorInfo.Create();
            Imports.GetMonitorInfoW(monitor, &info);
            return info;
        }

        public static unsafe DllVersionInfo GetCommonControlsVersion()
        {
            DllVersionInfo info = new DllVersionInfo { Size = (uint)sizeof(DllVersionInfo) };
            Imports.ComctlGetVersion(ref info).ThrowIfFailed();
            return info;
        }

        public static DpiAwarenessContext GetThreadDpiAwarenessContext() => Imports.GetThreadDpiAwarenessContext();

        public static DpiAwareness GetThreadDpiAwareness()
            => GetAwarenessFromDpiAwarenessContext(GetThreadDpiAwarenessContext());

        public static DpiAwareness GetAwarenessFromDpiAwarenessContext(DpiAwarenessContext context)
            => Imports.GetAwarenessFromDpiAwarenessContext(context);

        public static uint GetDpiForSystem() => Imports.GetDpiForSystem();

        public static uint GetDpiForWindow(this in WindowHandle window)
            => Imports.GetDpiForWindow(window);
    }
}
