// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Globalization;
using WInterop.Modules;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Windows.Native;

namespace WInterop.Windows;

public static partial class Windows
{
    public static Rectangle DefaultBounds
        => new(
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
            WindowsImports.DestroyWindow(mainWindow);
            throw;
        }
    }

    public static void Run(Window window)
    {
        try
        {
            window.ShowWindow(ShowWindowCommand.Normal);
            window.UpdateWindow();

            while (GetMessage(out WindowMessage message))
            {
                TranslateMessage(ref message);
                DispatchMessage(ref message);
            }

            // Make sure our window class doesn't get collected while we're pumping messages
            GC.KeepAlive(window);
        }
        catch
        {
            // Hit the P/Invoke directly as we want to throw the original error.
            WindowsImports.DestroyWindow(window);
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
        WindowHandle window = WindowsImports.CreateWindowExW(
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
            window = WindowsImports.CreateWindowExW(
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
        => Error.ThrowLastErrorIfFalse(WindowsImports.Beep(frequency, duration));

    /// <summary>
    ///  Play the specified sound (as defined in the Sound control panel).
    /// </summary>
    public static void MessageBeep(BeepType type = BeepType.SimpleBeep)
        => Error.ThrowLastErrorIfFalse(WindowsImports.MessageBeep(type));

    public static SystemParameters SystemParameters => SystemParameters.Instance;
    public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

    /// <summary>
    ///  Returns the average size in pixels of characters for the system font.
    /// </summary>
    public static Size GetDialogBaseUnits()
    {
        int result = WindowsImports.GetDialogBaseUnits();
        return new Size(Conversion.LowWord(result), Conversion.HighWord(result));
    }

    public static ModuleInstance GetModule<T>(this T window) where T : IHandle<WindowHandle>
        => window.GetWindowLong(WindowLong.InstanceHandle);

    /// <summary>
    ///  Wrapper to SetWindowLong for changing the window procedure. Returns the old
    ///  window procedure handle- use CallWindowProcedure to call the old method.
    /// </summary>
    public static WNDPROC SetWindowProcedure<T>(this T window, WindowProcedure newCallback)
        where T : IHandle<WindowHandle>
    {
        // It is possible that the returned window procedure will not be a direct handle.
        return new WNDPROC(SetWindowLong(
            window,
            WindowLong.WindowProcedure,
            Marshal.GetFunctionPointerForDelegate(newCallback)));
    }

    public static LResult CallWindowProcedure(
        WNDPROC previous,
        WindowHandle window,
        MessageType message,
        WParam wParam = default,
        LParam lParam = default)
        => WindowsImports.CallWindowProcW(previous, window, message, wParam, lParam);

    public static unsafe string GetClassName<T>(this T window) where T : IHandle<WindowHandle>
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return (uint)WindowsImports.GetClassNameW(window.Handle, b, (int)buffer.Length);
                }
            },
            ReturnSizeSemantics.BufferTruncates);
    }

    public static WindowHandle GetFocus() => WindowsImports.GetFocus();

    public static WindowHandle SetFocus<T>(this T window) where T : IHandle<WindowHandle>
    {
        WindowHandle prior = WindowsImports.SetFocus(window.Handle);
        if (prior.IsInvalid)
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

        return prior;
    }

    public static bool IsWindow<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.IsWindow(window.Handle);

    public static bool IsWindowVisible<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.IsWindowVisible(window.Handle);

    public static bool IsWindowUnicode<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.IsWindowUnicode(window.Handle);

    public static bool IsChild<TParent, TChild>(this TParent parent, TChild child)
        where TParent : IHandle<WindowHandle>
        where TChild : IHandle<WindowHandle>
        => WindowsImports.IsChild(parent.Handle, child.Handle);

    /// <summary>
    ///  Get the top child window in the specified window. If passed a null window
    ///  finds the window at the top of the Z order.
    /// </summary>
    public static WindowHandle GetTopWindow<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.GetTopWindow(window.Handle);

    public static WindowHandle GetForegroundWindow() => WindowsImports.GetForegroundWindow();

    public static WindowHandle GetShellWindow() => WindowsImports.GetShellWindow();

    public static WindowHandle GetActiveWindow() => WindowsImports.GetActiveWindow();

    /// <summary>
    ///  Gets the specified related Window to get given Window if it exists. Otherwise returns a null WindowHandle.
    /// </summary>
    public static WindowHandle GetWindow<T>(this T window, GetWindowOption option) where T : IHandle<WindowHandle>
        => WindowsImports.GetWindow(window.Handle, option);

    public static WindowHandle GetDesktopWindow() => WindowsImports.GetDesktopWindow();

    /// <summary>
    ///  Gets the parent window for the given window.
    /// </summary>
    /// <returns>
    ///  The parent window or a null handle if the window is the topmost window.
    /// </returns>
    public static WindowHandle GetParent<T>(this T window) where T : IHandle<WindowHandle>
    {
        WindowHandle parent = WindowsImports.GetParent(window.Handle);
        if (parent.IsNull)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return parent;
    }

    /// <summary>
    ///  Returns true if the current thread is a GUI thread.
    /// </summary>
    /// <param name="convertToGuiIfFalse">Tries to convert the thread to a GUI thread if it isn't already.</param>
    public static bool IsGuiThread(bool convertToGuiIfFalse = false)
    {
        int result = WindowsImports.IsGUIThread(convertToGuiIfFalse);
        return result != 0
            && !convertToGuiIfFalse | result != (int)WindowsError.ERROR_NOT_ENOUGH_MEMORY;
    }

    /// <summary>
    ///  Registers the given window class.
    /// </summary>
    public static unsafe Atom RegisterClass(ref WindowClassInfo windowClass)
    {
        Atom atom;
        using (WindowClassInfo.Marshaller marshaller = default)
        {
            marshaller.FillNative(out WNDCLASSEX native, ref windowClass);
            atom = WindowsImports.RegisterClassExW(ref native);
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
        if (!WindowsImports.UnregisterClassW(atom, module ?? ModuleInstance.Null))
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
                WindowsImports.UnregisterClassW((IntPtr)name, module ?? ModuleInstance.Null),
                className);
        }
    }

    public static void DestroyWindow<T>(this T window) where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(WindowsImports.DestroyWindow(window.Handle));

    public static IntPtr GetWindowLong<T>(this T window, WindowLong index) where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        IntPtr result = Environment.Is64BitProcess
            ? (IntPtr)WindowsImports.GetWindowLongPtrW(window.Handle, index)
            : (IntPtr)WindowsImports.GetWindowLongW(window.Handle, index);

        if (result == IntPtr.Zero)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static IntPtr SetWindowLong<T>(this T window, WindowLong index, IntPtr value)
        where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        IntPtr result = Environment.Is64BitProcess
            ? (IntPtr)WindowsImports.SetWindowLongPtrW(window.Handle, index, value.ToInt64())
            : (IntPtr)WindowsImports.SetWindowLongW(window.Handle, index, value.ToInt32());

        if (result == IntPtr.Zero)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static unsafe string GetWindowText<T>(this T window, int initialBuffer = 64)
        where T : IHandle<WindowHandle>
    {
        int result = initialBuffer;
        char[]? buffer = null;

        do
        {
            if (buffer is not null)
            {
                ArrayPool<char>.Shared.Return(buffer);
            }

            buffer = ArrayPool<char>.Shared.Rent(result * 2);

            fixed (char* c = buffer)
            {
                result = WindowsImports.GetWindowTextW(window.Handle, c, buffer.Length);
            }

            if (result == 0)
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
            }
        } while (result == buffer.Length - 1);

        string text = new(buffer, 0, result);
        ArrayPool<char>.Shared.Return(buffer);
        return text;
    }

    public static void SetWindowText<T>(this T window, string text) where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(WindowsImports.SetWindowTextW(window.Handle, text));

    public static IntPtr GetClassLong<T>(this T window, ClassLong index) where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        IntPtr result = Environment.Is64BitProcess
            ? (IntPtr)WindowsImports.GetClassLongPtrW(window.Handle, index)
            : (IntPtr)WindowsImports.GetClassLongW(window.Handle, index);

        if (result == IntPtr.Zero)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static IntPtr SetClassLong<T>(this T window, ClassLong index, IntPtr value)
        where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        // (Even though this is only documented on SetWindowLong, happens here too)
        Error.SetLastError(WindowsError.NO_ERROR);

        IntPtr result = Environment.Is64BitProcess
            ? (IntPtr)WindowsImports.SetClassLongPtrW(window.Handle, index, value.ToInt64())
            : (IntPtr)WindowsImports.SetClassLongW(window.Handle, index, value.ToInt32());

        if (result == IntPtr.Zero)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    /// <summary>
    ///  Sets the background brush for the window class. Returns the previous background brush.
    /// </summary>
    /// <param name="ownsHandle">
    ///  Whether or not the returned brush should own the handle. If true the brush handle
    ///  will be deleted when disposed.
    /// </param>
    public static BrushHandle SetClassBackgroundBrush<T>(
        this T window,
        BrushHandle value,
        bool ownsHandle = true) where T : IHandle<WindowHandle>
    {
        IntPtr result = SetClassLong(window, ClassLong.BackgroundBrush, value.HBRUSH.Value);
        return new BrushHandle(new Gdi.Native.HBRUSH(result), ownsHandle);
    }

    public static bool ShowWindow<T>(this T window, ShowWindowCommand command)
        where T : IHandle<WindowHandle>
    {
        return WindowsImports.ShowWindow(window.Handle, command);
    }

    /// <summary>
    ///  Moves the window to the requested location. For main windows this is in screen coordinates. For child
    ///  windows this is relative to the client area of the parent window.
    /// </summary>
    public static void MoveWindow<T>(this T window, Rectangle position, bool repaint)
        where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(
            WindowsImports.MoveWindow(window.Handle, position.X, position.Y, position.Width, position.Height, repaint));

    /// <summary>
    ///  Dispatches sent messages, waiting for the next message in the calling thread's message queue.
    /// </summary>
    /// <param name="window">
    ///  Get messages for the specified window or all thread windows and thread messages if default.
    ///  If set to -1, will only return thread messages.
    /// </param>
    /// <returns>False when <see cref="MessageType.Quit"/> is returned.</returns>
    public static bool GetMessage(
        out WindowMessage message,
        WindowHandle window = default,
        MessageType minMessage = MessageType.Null,
        MessageType maxMessage = MessageType.Null)
    {
        IntBoolean result = WindowsImports.GetMessageW(out message, window, (uint)minMessage, (uint)maxMessage);

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
        return WindowsImports.PeekMessageW(out message, window, minMessage, maxMessage, options);
    }

    public static bool TranslateMessage(ref WindowMessage message) => WindowsImports.TranslateMessage(ref message);

    public static bool DispatchMessage(ref WindowMessage message) => WindowsImports.DispatchMessageW(ref message);

    public static LResult DefaultWindowProcedure<T>(
        this T window,
        MessageType message,
        WParam wParam,
        LParam lParam) where T : IHandle<WindowHandle>
        => WindowsImports.DefWindowProcW(window.Handle, message, wParam, lParam);

    public static void PostQuitMessage(int exitCode) => WindowsImports.PostQuitMessage(exitCode);

    /// <summary>
    ///  Returns the logical client coordinates of the given <paramref name="window"/>.
    /// </summary>
    public static unsafe Rectangle GetClientRectangle<T>(this T window)
        where T : IHandle<WindowHandle>
    {
        Unsafe.SkipInit(out Rect rect);
        Error.ThrowLastErrorIfFalse(WindowsImports.GetClientRect(window.Handle, &rect));
        return rect;
    }

    /// <summary>
    ///  Dimensions of the bounding rectangle of the specified <paramref name="window"/>
    ///  in screen coordinates relative to the upper-left corner.
    /// </summary>
    public static Rectangle GetWindowRectangle<T>(this T window) where T : IHandle<WindowHandle>
    {
        Rect rect = default;
        Error.ThrowLastErrorIfFalse(WindowsImports.GetWindowRect(window.Handle, ref rect));
        return rect;
    }

    public static void SetScrollRange<T>(this T window, ScrollBar scrollBar, int min, int max, bool redraw)
        where T : IHandle<WindowHandle>
    {
        Error.ThrowLastErrorIfFalse(
            WindowsImports.SetScrollRange(window.Handle, scrollBar, min, max, redraw));
    }

    public static int SetScrollPosition<T>(this T window, ScrollBar scrollBar, int position, bool redraw)
        where T : IHandle<WindowHandle>
    {
        int result = WindowsImports.SetScrollPos(window.Handle, scrollBar, position, redraw);

        // There appears to be a bug in the V6 common controls where they set ERROR_ACCESSDENIED. Clearing
        // LastError doesn't help. Skip error checking if we've set position 0.
        if (result == 0 && position != 0)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static unsafe int SetScrollInfo<T>(
        this T window,
        ScrollBar scrollBar,
        ref ScrollInfo scrollInfo,
        bool redraw) where T : IHandle<WindowHandle>
    {
        scrollInfo.Size = (uint)sizeof(ScrollInfo);
        int result = WindowsImports.SetScrollInfo(window.Handle, scrollBar, ref scrollInfo, redraw);

        return result;
    }

    public static int GetScrollPosition<T>(this T window, ScrollBar scrollBar) where T : IHandle<WindowHandle>
    {
        int result = WindowsImports.GetScrollPos(window.Handle, scrollBar);
        if (result == 0)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static unsafe void GetScrollInfo<T>(this T window, ScrollBar scrollBar, ref ScrollInfo scrollInfo)
        where T : IHandle<WindowHandle>
    {
        scrollInfo.Size = (uint)sizeof(ScrollInfo);
        Error.ThrowLastErrorIfFalse(WindowsImports.GetScrollInfo(window.Handle, scrollBar, ref scrollInfo));
    }

    public static unsafe int ScrollWindow<T>(this T window, Point delta)
        where T : IHandle<WindowHandle>
    {
        int result = WindowsImports.ScrollWindowEx(
            window.Handle,
            delta.X,
            delta.Y,
            null,
            null,
            IntPtr.Zero,
            null,
            ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate);

        if (result == 0)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static unsafe int ScrollWindow<T>(this T window, Point delta, Rectangle scroll, Rectangle clip)
        where T : IHandle<WindowHandle>
    {
        Rect scrollRect = scroll;
        Rect clipRect = clip;

        int result = WindowsImports.ScrollWindowEx(
            window.Handle,
            delta.X,
            delta.Y,
            &scrollRect,
            &clipRect,
            IntPtr.Zero,
            null,
            ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate);

        if (result == 0)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static KeyboardType GetKeyboardType()
    {
        int result = WindowsImports.GetKeyboardType(0);
        if (result == 0)
            Error.ThrowLastError();

        return (KeyboardType)result;
    }

    public static int GetKeyboardSubType()
    {
        // Although not documented this API does not appear to clear last error
        Errors.Native.Imports.SetLastError(WindowsError.ERROR_SUCCESS);

        int result = WindowsImports.GetKeyboardType(1);
        if (result == 0)
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);

        return result;
    }

    public static int GetKeyboardFunctionKeyCount()
    {
        int result = WindowsImports.GetKeyboardType(2);
        if (result == 0)
            Error.ThrowLastError();

        return result;
    }

    public static KeyState GetKeyState(VirtualKey key) => WindowsImports.GetKeyState(key);

    public static unsafe string GetKeyNameText(LParam lParam)
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return checked((uint)WindowsImports.GetKeyNameTextW(lParam, b, (int)buffer.Length));
                }
            },
            ReturnSizeSemantics.BufferTruncates,

            // It is possible that there may be no name for a key,
            // in which case the api will return 0 with GetLastError of 0.
            shouldThrow: ErrorExtensions.Failed);
    }

    public static WindowHandle GetDialogItem<T>(this T window, int id) where T : IHandle<WindowHandle>
    {
        WindowHandle control = WindowsImports.GetDlgItem(window.Handle, id);
        if (control.IsInvalid)
            Error.ThrowLastError();
        return control;
    }

    public static WindowHandle SetCapture<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.SetCapture(window.Handle);

    public static void ReleaseCapture()
        => Error.ThrowLastErrorIfFalse(WindowsImports.ReleaseCapture());

    public static TimerId SetTimer<T>(
        this T window,
        TimerId id,
        uint interval,
        TimerProcedure? callback = null,
        uint delayTolerance = 0) where T : IHandle<WindowHandle>
    {
        TimerId result = WindowsImports.SetCoalescableTimer(window.Handle, id, interval, callback, delayTolerance);
        if (result == TimerId.Null)
            Error.ThrowLastError();

        return result;
    }

    public static void KillTimer<T>(this T window, TimerId id) where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(WindowsImports.KillTimer(window.Handle, id));

    public static Color GetSystemColor(SystemColor systemColor) => WindowsImports.GetSysColor(systemColor);

    /// <summary>
    ///  Gets the value for the given system metric.
    /// </summary>
    public static int GetSystemMetrics(SystemMetric metric) => WindowsImports.GetSystemMetrics(metric);

    public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok)
    {
        return MessageBox((WindowHandle)default, text, caption, type);
    }

    public static CommandId MessageBox<T>(
        this T owner,
        string text,
        string caption,
        MessageBoxType type = MessageBoxType.Ok)
        where T : IHandle<WindowHandle>
    {
        CommandId result = WindowsImports.MessageBoxExW(owner.Handle, text, caption, type, 0);
        if (result == CommandId.Error)
            Error.ThrowLastError();

        return result;
    }

    public static WindowClassInfo GetClassInfo(this ModuleInstance instance, Atom atom)
    {
        Error.ThrowLastErrorIfFalse(
            WindowsImports.GetClassInfoExW(instance ?? ModuleInstance.Null, atom, out WNDCLASSEX wndClass));

        return wndClass;
    }

    public static unsafe WindowClassInfo GetClassInfo(this ModuleInstance instance, string className)
    {
        WNDCLASSEX wndClass;

        fixed (char* c = className)
            Error.ThrowLastErrorIfFalse(WindowsImports.GetClassInfoExW(instance ?? ModuleInstance.Null, (IntPtr)c, out wndClass));

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
        int result = WindowsImports.LoadStringW(library, identifier, out char* buffer, 0);
        if (result <= 0)
            Error.ThrowLastError(identifier.ToString());

        // Null is not included in the result
        return new string(buffer, 0, result);
    }

    public static unsafe IconHandle LoadIcon(IconId id)
    {
        HICON handle = WindowsImports.LoadIconW(ModuleInstance.Null, (char*)(uint)id);
        if (handle.IsInvalid)
            Error.ThrowLastError();

        return new IconHandle(handle, ownsHandle: false);
    }

    public static unsafe IconHandle LoadIcon(string name, ModuleInstance module)
    {
        fixed (char* n = name)
        {
            HICON handle = WindowsImports.LoadIconW(module, n);

            if (handle.IsInvalid)
                Error.ThrowLastError();

            return new IconHandle(handle, ownsHandle: false);
        }
    }

    public static MonitorHandle MonitorFromWindow<T>(
        this T window,
        MonitorOption option = MonitorOption.DefaultToNull) where T : IHandle<WindowHandle>
        => WindowsImports.MonitorFromWindow(window.Handle, option);

    public static MonitorHandle MonitorFromPoint(Point point, MonitorOption option = MonitorOption.DefaultToNull)
        => WindowsImports.MonitorFromPoint(point, option);

    public static MonitorHandle MonitorFromRectangle(
        Rectangle rectangle,
        MonitorOption option = MonitorOption.DefaultToNull)
    {
        Rect rect = rectangle;
        return WindowsImports.MonitorFromRect(in rect, option);
    }

    public static unsafe MonitorInfo GetMonitorInfo(this MonitorHandle monitor)
    {
        MonitorInfo info = MonitorInfo.Create();
        WindowsImports.GetMonitorInfoW(monitor, &info);
        return info;
    }

    public static unsafe ExtendedMonitorInfo GetExtendedMonitorInfo(this MonitorHandle monitor)
    {
        ExtendedMonitorInfo info = ExtendedMonitorInfo.Create();
        WindowsImports.GetMonitorInfoW(monitor, &info);
        return info;
    }

    public static unsafe DllVersionInfo GetCommonControlsVersion()
    {
        DllVersionInfo info = new() { Size = (uint)sizeof(DllVersionInfo) };
        WindowsImports.ComctlGetVersion(ref info).ThrowIfFailed();
        return info;
    }

    public static DpiAwarenessContext GetThreadDpiAwarenessContext() => WindowsImports.GetThreadDpiAwarenessContext();

    public static DpiAwarenessContext SetThreadDpiAwarenessContext(DpiAwarenessContext dpiContext)
        => WindowsImports.SetThreadDpiAwarenessContext(dpiContext);

    public static DpiAwareness GetThreadDpiAwareness()
        => GetAwarenessFromDpiAwarenessContext(GetThreadDpiAwarenessContext());

    public static DpiAwareness GetAwarenessFromDpiAwarenessContext(DpiAwarenessContext context)
        => WindowsImports.GetAwarenessFromDpiAwarenessContext(context);

    public static uint GetDpiForSystem() => WindowsImports.GetDpiForSystem();

    public static bool SetProcessDpiAwarenessContext(DpiAwarenessContext context)
        => WindowsImports.SetProcessDpiAwarenessContext(context);

    public static uint GetDpiForWindow<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.GetDpiForWindow(window.Handle);

    /// <summary>
    ///  Converts the requested point size to height based on the DPI of the given window.
    /// </summary>
    public static int FontPointSizeToHeight<T>(this T window, int pointSize) where T : IHandle<WindowHandle>
        => WindowsImports.MulDiv(
            pointSize,
            (int)window.GetDpiForWindow(),
            72);

    /// <summary>
    ///  Enumerates child windows for the given <paramref name="window"/>.
    /// </summary>
    /// <param name="callback">
    ///  The provided function will be passed child window handles. Return true to continue enumeration.
    /// </param>
    /// <param name="parameter">Parameter to pass through to the <paramref name="callback"/>.</param>
    public static void EnumerateChildWindows<T>(
        this T window,
        Func<WindowHandle, LParam, bool> callback,
        LParam parameter) where T : IHandle<WindowHandle>
        => WindowsImports.EnumChildWindows(
            window.Handle,
            (WindowHandle handle, LParam parameter) => callback(handle, parameter),
            parameter);

    /// <summary>
    ///  Enumerates child windows for the given <paramref name="handle"/>.
    /// </summary>
    /// <param name="callback">
    ///  The provided function will be passed child window handles. Return true to continue enumeration.
    /// </param>
    public static void EnumerateChildWindows(
        WindowHandle handle,
        Func<WindowHandle, bool> callback,
        LParam parameter)
        => WindowsImports.EnumChildWindows(
            handle,
            (WindowHandle handle, LParam parameter) => callback(handle),
            parameter);

    /// <summary>
    ///  Sets the graphics mode for the window's device context.
    /// </summary>
    public static void SetGraphicsMode<T>(
        this T window,
        GraphicsMode graphicsMode) where T : IHandle<WindowHandle>
    {
        using var hdc = window.Handle.GetDeviceContext();
        hdc.SetGraphicsMode(graphicsMode);
    }

    /// <summary>
    ///  Sets the world transorm for the window's device context.
    /// </summary>
    public static void SetWorldTransform<T>(
        this T window,
        ref Matrix3x2 transform) where T : IHandle<WindowHandle>
    {
        using var hdc = window.Handle.GetDeviceContext();
        hdc.SetWorldTransform(ref transform);
    }
}