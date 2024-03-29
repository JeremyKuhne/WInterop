﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            TerraFXWindows.DestroyWindow(mainWindow);
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
            TerraFXWindows.DestroyWindow(window);
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
        fixed (char* n = windowName)
        {
            WindowHandle window = TerraFXWindows.CreateWindowExW(
                (uint)extendedStyle,
                (ushort*)classAtom.ATOM,
                (ushort*)n,
                (uint)style,
                bounds.X,
                bounds.Y,
                bounds.Width,
                bounds.Height,
                parentWindow,
                menuHandle,
                instance ?? HINSTANCE.NULL,
                (void*)parameters);

            if (window.IsInvalid)
            {
                Error.ThrowLastError();
            }

            return window;
        }
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
        fixed (char* cn = className)
        fixed (char* wn = windowName)
        {
            window = TerraFXWindows.CreateWindowExW(
                (uint)extendedStyle,
                (ushort*)cn,
                (ushort*)wn,
                (uint)style,
                bounds.X,
                bounds.Y,
                bounds.Width,
                bounds.Height,
                parentWindow,
                menuHandle,
                instance ?? HINSTANCE.NULL,
                (void*)parameters);
        }

        if (window.IsInvalid)
        {
            Error.ThrowLastError();
        }

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
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.MessageBeep((uint)type));

    public static SystemParameters SystemParameters => SystemParameters.Instance;
    public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

    /// <summary>
    ///  Returns the average size in pixels of characters for the system font.
    /// </summary>
    public static Size GetDialogBaseUnits()
    {
        int result = TerraFXWindows.GetDialogBaseUnits();
        return new Size(Conversion.LowWord(result), Conversion.HighWord(result));
    }

    public static ModuleInstance GetModule<T>(this T window) where T : IHandle<WindowHandle>
        => window.GetWindowLong(WindowLong.InstanceHandle);

    /// <summary>
    ///  Wrapper to SetWindowLong for changing the window procedure. Returns the old
    ///  window procedure handle- use CallWindowProcedure to call the old method.
    /// </summary>
    public static unsafe WNDPROC SetWindowProcedure<T>(this T window, WindowProcedure newCallback)
        where T : IHandle<WindowHandle>
    {
        // It is possible that the returned window procedure will not be a direct handle.
        return new WNDPROC((delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT>)SetWindowLong(
            window,
            WindowLong.WindowProcedure,
            Marshal.GetFunctionPointerForDelegate(newCallback)));
    }

    public static unsafe LResult CallWindowProcedure(
        WNDPROC previous,
        WindowHandle window,
        MessageType message,
        WParam wParam = default,
        LParam lParam = default)
        => TerraFXWindows.CallWindowProcW(previous, window, (uint)message, wParam, lParam);

    public static unsafe string GetClassName<T>(this T window) where T : IHandle<WindowHandle>
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return (uint)TerraFXWindows.GetClassNameW(window.Handle, (ushort*)b, (int)buffer.Length);
                }
            },
            ReturnSizeSemantics.BufferTruncates);
    }

    public static WindowHandle GetFocus() => WindowsImports.GetFocus();

    public static WindowHandle SetFocus<T>(this T window) where T : IHandle<WindowHandle>
    {
        WindowHandle prior = TerraFXWindows.SetFocus(window.Handle);
        if (prior.IsInvalid)
        {
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);
        }

        return prior;
    }

    public static bool IsWindow<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.IsWindow(window.Handle);

    public static bool IsWindowVisible<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.IsWindowVisible(window.Handle);

    public static bool IsWindowUnicode<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.IsWindowUnicode(window.Handle);

    public static bool IsChild<TParent, TChild>(this TParent parent, TChild child)
        where TParent : IHandle<WindowHandle>
        where TChild : IHandle<WindowHandle>
        => TerraFXWindows.IsChild(parent.Handle, child.Handle);

    /// <summary>
    ///  Get the top child window in the specified window. If passed a null window
    ///  finds the window at the top of the Z order.
    /// </summary>
    public static WindowHandle GetTopWindow<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.GetTopWindow(window.Handle);

    public static WindowHandle GetForegroundWindow() => TerraFXWindows.GetForegroundWindow();

    public static WindowHandle GetShellWindow() => TerraFXWindows.GetShellWindow();

    public static WindowHandle GetActiveWindow() => TerraFXWindows.GetActiveWindow();

    /// <summary>
    ///  Gets the specified related Window to get given Window if it exists. Otherwise returns a null WindowHandle.
    /// </summary>
    public static WindowHandle GetWindow<T>(this T window, GetWindowOption option) where T : IHandle<WindowHandle>
        => TerraFXWindows.GetWindow(window.Handle, (uint)option);

    public static WindowHandle GetDesktopWindow() => TerraFXWindows.GetDesktopWindow();

    /// <summary>
    ///  Gets the parent window for the given window.
    /// </summary>
    /// <returns>
    ///  The parent window or a null handle if the window is the topmost window.
    /// </returns>
    public static WindowHandle GetParent<T>(this T window) where T : IHandle<WindowHandle>
    {
        WindowHandle parent = TerraFXWindows.GetParent(window.Handle);
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
        int result = TerraFXWindows.IsGUIThread(convertToGuiIfFalse);
        return result != 0
            && !convertToGuiIfFalse | result != (int)WindowsError.ERROR_NOT_ENOUGH_MEMORY;
    }

    /// <summary>
    ///  Registers the given window class.
    /// </summary>
    public static unsafe Atom RegisterClass(WindowClassInfo windowClass)
    {
        Atom atom;
        using (WindowClassInfo.Marshaller marshaller = default)
        {
            marshaller.FillNative(out WNDCLASSEXW native, ref windowClass);
            atom = TerraFXWindows.RegisterClassExW(&native);
            if (!atom.IsValid)
            {
                Error.ThrowLastError();
            }
        }

        return atom;
    }

    /// <summary>
    ///  Unregisters the given class Atom.
    /// </summary>
    public static unsafe void UnregisterClass(Atom atom, ModuleInstance? module = null)
    {
        if (!TerraFXWindows.UnregisterClassW((ushort*)atom.ATOM, module ?? HINSTANCE.NULL))
        {
            Error.ThrowLastError();
        }
    }

    /// <summary>
    ///  Unregisters the given class name.
    /// </summary>
    public static unsafe void UnregisterClass(string className, ModuleInstance? module)
    {
        ArgumentNullException.ThrowIfNull(className);

        fixed (char* name = className)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.UnregisterClassW((ushort*)name, module ?? HINSTANCE.NULL),
                className);
        }
    }

    public static void DestroyWindow<T>(this T window) where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.DestroyWindow(window.Handle));

    public static unsafe WNDPROC GetWNDPROC<T>(this T window) where T : IHandle<WindowHandle>
        => (delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT>)window.GetWindowLong(WindowLong.WindowProcedure);

    public static nint GetWindowLong<T>(this T window, WindowLong index) where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        nint result = Environment.Is64BitProcess
            ? TerraFXWindows.GetWindowLongPtrW(window.Handle, (int)index)
            : TerraFXWindows.GetWindowLongW(window.Handle, (int)index);

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static nint SetWindowLong<T>(this T window, WindowLong index, nint value)
        where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        nint result = Environment.Is64BitProcess
            ? (IntPtr)TerraFXWindows.SetWindowLongPtrW(window.Handle, (int)index, value)
            : (IntPtr)TerraFXWindows.SetWindowLongW(window.Handle, (int)index, (int)value);

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

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
                result = TerraFXWindows.GetWindowTextW(window.Handle, (ushort*)c, buffer.Length);
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

    public static unsafe void SetWindowText<T>(this T window, string text) where T : IHandle<WindowHandle>
    {
        fixed (char* t = text)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.SetWindowTextW(window.Handle, (ushort*)t));
        }
    }

    public static nuint GetClassLong<T>(this T window, ClassLong index) where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        Error.SetLastError(WindowsError.NO_ERROR);

        nuint result = Environment.Is64BitProcess
            ? TerraFXWindows.GetClassLongPtrW(window.Handle, (int)index)
            : TerraFXWindows.GetClassLongW(window.Handle, (int)index);

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static nuint SetClassLong<T>(this T window, ClassLong index, nuint value)
        where T : IHandle<WindowHandle>
    {
        // Unfortunate, but this is necessary to tell if there is really an error
        // (Even though this is only documented on SetWindowLong, happens here too)
        Error.SetLastError(WindowsError.NO_ERROR);

        nuint result = Environment.Is64BitProcess
            ? TerraFXWindows.SetClassLongPtrW(window.Handle, (int)index, (nint)value)
            : TerraFXWindows.SetClassLongW(window.Handle, (int)index, (int)value);

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    /// <summary>
    ///  Sets the background brush for the window class. Returns the previous background brush.
    /// </summary>
    /// <param name="ownsHandle">
    ///  Whether or not the returned brush should own the handle. If true the brush handle
    ///  will be deleted when disposed.
    /// </param>
    public static unsafe BrushHandle SetClassBackgroundBrush<T>(
        this T window,
        BrushHandle value,
        bool ownsHandle = true) where T : IHandle<WindowHandle>
    {
        nuint result = SetClassLong(window, ClassLong.BackgroundBrush, (nuint)value.Handle);
        return new BrushHandle(new HBRUSH((void*)result), ownsHandle);
    }

    public static bool ShowWindow<T>(this T window, ShowWindowCommand command)
        where T : IHandle<WindowHandle>
        => TerraFXWindows.ShowWindow(window.Handle, (int)command);

    /// <summary>
    ///  Moves the window to the requested location. For main windows this is in screen coordinates. For child
    ///  windows this is relative to the client area of the parent window.
    /// </summary>
    public static void MoveWindow<T>(this T window, Rectangle position, bool repaint)
        where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(
            TerraFXWindows.MoveWindow(window.Handle, position.X, position.Y, position.Width, position.Height, repaint));

    /// <summary>
    ///  Dispatches sent messages, waiting for the next message in the calling thread's message queue.
    /// </summary>
    /// <param name="window">
    ///  Get messages for the specified window or all thread windows and thread messages if default.
    ///  If set to -1, will only return thread messages.
    /// </param>
    /// <returns>False when <see cref="MessageType.Quit"/> is returned.</returns>
    public static unsafe bool GetMessage(
        out WindowMessage message,
        WindowHandle window = default,
        MessageType minMessage = MessageType.Null,
        MessageType maxMessage = MessageType.Null)
    {
        fixed (WindowMessage* m = &message)
        {
            BOOL result = TerraFXWindows.GetMessageW((MSG*)m, window, (uint)minMessage, (uint)maxMessage);

            // One special case here is -1 for an error
            if (result == -1)
            {
                Error.ThrowLastError();
            }

            return result;
        }
    }

    public static unsafe bool PeekMessage(
        out WindowMessage message,
        WindowHandle window = default,
        uint minMessage = 0,
        uint maxMessage = 0,
        PeekMessageOptions options = PeekMessageOptions.NoRemove)
    {
        fixed (WindowMessage* m = &message)
        {
            return TerraFXWindows.PeekMessageW((MSG*)m, window, minMessage, maxMessage, (uint)options);
        }
    }

    public static unsafe bool TranslateMessage(ref WindowMessage message)
    {
        fixed (WindowMessage* m = &message)
        {
            return TerraFXWindows.TranslateMessage((MSG*)m);
        }
    }

    public static unsafe LResult DispatchMessage(ref WindowMessage message)
    {
        fixed (WindowMessage* m = &message)
        {
            return TerraFXWindows.DispatchMessageW((MSG*)m);
        }
    }

    public static LResult DefaultWindowProcedure<T>(
        this T window,
        MessageType message,
        WParam wParam,
        LParam lParam) where T : IHandle<WindowHandle>
        => TerraFXWindows.DefWindowProcW(window.Handle, (uint)message, wParam, lParam);

    public static void PostQuitMessage(int exitCode) => TerraFXWindows.PostQuitMessage(exitCode);

    /// <summary>
    ///  Returns the logical client coordinates of the given <paramref name="window"/>.
    /// </summary>
    public static unsafe Rectangle GetClientRectangle<T>(this T window)
        where T : IHandle<WindowHandle>
    {
        Unsafe.SkipInit(out Rect rect);
        Error.ThrowLastErrorIfFalse(TerraFXWindows.GetClientRect(window.Handle, (RECT*)&rect));
        return rect;
    }

    /// <summary>
    ///  Dimensions of the bounding rectangle of the specified <paramref name="window"/>
    ///  in screen coordinates relative to the upper-left corner.
    /// </summary>
    public static unsafe Rectangle GetWindowRectangle<T>(this T window) where T : IHandle<WindowHandle>
    {
        Unsafe.SkipInit(out Rect rect);
        Error.ThrowLastErrorIfFalse(TerraFXWindows.GetWindowRect(window.Handle, (RECT*)&rect));
        return rect;
    }

    public static void SetScrollRange<T>(this T window, ScrollBar scrollBar, int min, int max, bool redraw)
        where T : IHandle<WindowHandle>
    {
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.SetScrollRange(window.Handle, (int)scrollBar, min, max, redraw));
    }

    public static int SetScrollPosition<T>(this T window, ScrollBar scrollBar, int position, bool redraw)
        where T : IHandle<WindowHandle>
    {
        int result = TerraFXWindows.SetScrollPos(window.Handle, (int)scrollBar, position, redraw);

        // There appears to be a bug in the V6 common controls where they set ERROR_ACCESSDENIED. Clearing
        // LastError doesn't help. Skip error checking if we've set position 0.
        if (result == 0 && position != 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static unsafe int SetScrollInfo<T>(
        this T window,
        ScrollBar scrollBar,
        ref ScrollInfo scrollInfo,
        bool redraw) where T : IHandle<WindowHandle>
    {
        scrollInfo.Size = (uint)sizeof(ScrollInfo);
        fixed (ScrollInfo* si = &scrollInfo)
        {
            return TerraFXWindows.SetScrollInfo(window.Handle, (int)scrollBar, (SCROLLINFO*)si, redraw);
        }
    }

    public static int GetScrollPosition<T>(this T window, ScrollBar scrollBar) where T : IHandle<WindowHandle>
    {
        int result = TerraFXWindows.GetScrollPos(window.Handle, (int)scrollBar);
        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static unsafe void GetScrollInfo<T>(this T window, ScrollBar scrollBar, ref ScrollInfo scrollInfo)
        where T : IHandle<WindowHandle>
    {
        scrollInfo.Size = (uint)sizeof(ScrollInfo);
        fixed (ScrollInfo* si = &scrollInfo)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.GetScrollInfo(window.Handle, (int)scrollBar, (SCROLLINFO*)si));
        }
    }

    public static unsafe int ScrollWindow<T>(this T window, Point delta)
        where T : IHandle<WindowHandle>
    {
        int result = TerraFXWindows.ScrollWindowEx(
            window.Handle,
            delta.X,
            delta.Y,
            null,
            null,
            HRGN.NULL,
            null,
            (uint)(ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate));

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static unsafe int ScrollWindow<T>(this T window, Point delta, Rectangle scroll, Rectangle clip)
        where T : IHandle<WindowHandle>
    {
        Rect scrollRect = scroll;
        Rect clipRect = clip;

        int result = TerraFXWindows.ScrollWindowEx(
            window.Handle,
            delta.X,
            delta.Y,
            (RECT*)&scrollRect,
            (RECT*)&clipRect,
            HRGN.NULL,
            null,
            (int)(ScrollWindowFlags.Erase | ScrollWindowFlags.Invalidate));

        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static KeyboardType GetKeyboardType()
    {
        int result = TerraFXWindows.GetKeyboardType(0);
        if (result == 0)
        {
            Error.ThrowLastError();
        }

        return (KeyboardType)result;
    }

    public static int GetKeyboardSubType()
    {
        // Although not documented this API does not appear to clear last error
        Error.SetLastError(WindowsError.ERROR_SUCCESS);

        int result = TerraFXWindows.GetKeyboardType(1);
        if (result == 0)
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_SUCCESS);
        }

        return result;
    }

    public static int GetKeyboardFunctionKeyCount()
    {
        int result = TerraFXWindows.GetKeyboardType(2);
        if (result == 0)
        {
            Error.ThrowLastError();
        }

        return result;
    }

    public static KeyState GetKeyState(VirtualKey key) => (KeyState)TerraFXWindows.GetKeyState((int)key);

    public static unsafe string GetKeyNameText(LParam lParam)
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return checked((uint)TerraFXWindows.GetKeyNameTextW(lParam, (ushort*)b, (int)buffer.Length));
                }
            },
            ReturnSizeSemantics.BufferTruncates,

            // It is possible that there may be no name for a key,
            // in which case the api will return 0 with GetLastError of 0.
            shouldThrow: ErrorExtensions.Failed);
    }

    public static WindowHandle GetDialogItem<T>(this T window, int id) where T : IHandle<WindowHandle>
    {
        WindowHandle control = TerraFXWindows.GetDlgItem(window.Handle, id);
        if (control.IsInvalid)
        {
            Error.ThrowLastError();
        }

        return control;
    }

    public static WindowHandle SetCapture<T>(this T window) where T : IHandle<WindowHandle>
        => WindowsImports.SetCapture(window.Handle);

    public static void ReleaseCapture()
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.ReleaseCapture());

    public static unsafe TimerId SetTimer<T>(
        this T window,
        TimerId id,
        uint interval,
        TimerProcedure? callback = null,
        uint delayTolerance = 0) where T : IHandle<WindowHandle>
    {
        void* cb = callback is null ? null : (void*)Marshal.GetFunctionPointerForDelegate(callback);
        TimerId result = TerraFXWindows.SetCoalescableTimer(
            window.Handle,
            id,
            interval,
            (delegate* unmanaged<HWND, uint, nuint, uint, void>)cb,
            delayTolerance);

        if (result == TimerId.Null)
        {
            Error.ThrowLastError();
        }

        return result;
    }

    public static void KillTimer<T>(this T window, TimerId id) where T : IHandle<WindowHandle>
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.KillTimer(window.Handle, id));

    public static Color GetSystemColor(SystemColor systemColor)
        => new COLORREF(TerraFXWindows.GetSysColor((int)systemColor)).ToColor();

    /// <summary>
    ///  Gets the value for the given system metric.
    /// </summary>
    public static int GetSystemMetrics(SystemMetric metric) => TerraFXWindows.GetSystemMetrics((int)metric);

    public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok)
    {
        return MessageBox((WindowHandle)default, text, caption, type);
    }

    public static unsafe CommandId MessageBox<T>(
        this T owner,
        string text,
        string caption,
        MessageBoxType type = MessageBoxType.Ok)
        where T : IHandle<WindowHandle>
    {
        fixed (char* t = text)
        fixed (char* c = caption)
        {
            CommandId result = (CommandId)TerraFXWindows.MessageBoxExW(
                owner.Handle,
                (ushort*)t,
                (ushort*)c,
                (uint)type,
                wLanguageId: 0);

            if (result == CommandId.Error)
            {
                Error.ThrowLastError();
            }

            return result;
        }
    }

    public static unsafe WindowClassInfo GetClassInfo(this ModuleInstance instance, Atom atom)
    {
        WNDCLASSEXW wndClass;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetClassInfoExW(
                instance ?? HINSTANCE.NULL,
                (ushort*)(nint)atom,
                &wndClass));

        return wndClass;
    }

    public static unsafe WindowClassInfo GetClassInfo(this ModuleInstance instance, string className)
    {
        WNDCLASSEXW wndClass;

        fixed (char* c = className)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.GetClassInfoExW(
                instance ?? ModuleInstance.Null,
                (ushort*)c,
                &wndClass));
        }

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
        {
            Error.ThrowLastError(identifier.ToString());
        }

        // Null is not included in the result
        return new string(buffer, 0, result);
    }

    public static unsafe IconHandle LoadIcon(IconId id)
    {
        HICON handle = TerraFXWindows.LoadIconW(default, (ushort*)(uint)id);
        if (handle == HICON.NULL)
        {
            Error.ThrowLastError();
        }

        return new IconHandle(handle, ownsHandle: false);
    }

    public static unsafe IconHandle LoadIcon(string name, ModuleInstance module)
    {
        fixed (void* n = name)
        {
            HICON handle = TerraFXWindows.LoadIconW(module.Handle, (ushort*)n);

            if (handle == HICON.NULL)
                Error.ThrowLastError();

            return new IconHandle(handle, ownsHandle: false);
        }
    }

    public static MonitorHandle MonitorFromWindow<T>(
        this T window,
        MonitorOption option = MonitorOption.DefaultToNull) where T : IHandle<WindowHandle>
        => TerraFXWindows.MonitorFromWindow(window.Handle, (uint)option);

    public static unsafe MonitorHandle MonitorFromPoint(Point point, MonitorOption option = MonitorOption.DefaultToNull)
    {
        POINT p = *(POINT*)&point;
        return TerraFXWindows.MonitorFromPoint(p, (uint)option);
    }

    public static unsafe MonitorHandle MonitorFromRectangle(
        Rectangle rectangle,
        MonitorOption option = MonitorOption.DefaultToNull)
    {
        Rect rect = rectangle;
        return TerraFXWindows.MonitorFromRect((RECT*)&rect, (uint)option);
    }

    public static unsafe MonitorInfo GetMonitorInfo(this MonitorHandle monitor)
    {
        MonitorInfo info = MonitorInfo.Create();
        TerraFXWindows.GetMonitorInfoW(monitor, (MONITORINFO*)&info);
        return info;
    }

    public static unsafe ExtendedMonitorInfo GetExtendedMonitorInfo(this MonitorHandle monitor)
    {
        ExtendedMonitorInfo info = ExtendedMonitorInfo.Create();
        TerraFXWindows.GetMonitorInfoW(monitor, (MONITORINFO*)&info);
        return info;
    }

    public static unsafe DllVersionInfo GetCommonControlsVersion()
    {
        DllVersionInfo info = new() { Size = (uint)sizeof(DllVersionInfo) };
        WindowsImports.ComctlGetVersion(ref info).ThrowIfFailed();
        return info;
    }

    public static unsafe DpiAwarenessContext GetThreadDpiAwarenessContext()
        => TerraFXWindows.GetThreadDpiAwarenessContext();

    public static DpiAwarenessContext SetThreadDpiAwarenessContext(DpiAwarenessContext dpiContext)
        => TerraFXWindows.SetThreadDpiAwarenessContext(dpiContext);

    public static DpiAwareness GetThreadDpiAwareness()
        => GetAwarenessFromDpiAwarenessContext(GetThreadDpiAwarenessContext());

    public static DpiAwareness GetAwarenessFromDpiAwarenessContext(DpiAwarenessContext context)
        => (DpiAwareness)TerraFXWindows.GetAwarenessFromDpiAwarenessContext(context);

    public static uint GetDpiForSystem() => TerraFXWindows.GetDpiForSystem();

    public static bool SetProcessDpiAwarenessContext(DpiAwarenessContext context)
        => TerraFXWindows.SetProcessDpiAwarenessContext(context);

    public static uint GetDpiForWindow<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.GetDpiForWindow(window.Handle);

    /// <summary>
    ///  Converts the requested point size to height based on the DPI of the given window.
    /// </summary>
    public static int FontPointSizeToHeight<T>(this T window, int pointSize) where T : IHandle<WindowHandle>
        => TerraFXWindows.MulDiv(
            pointSize,
            (int)window.GetDpiForWindow(),
            72);

    /// <summary>
    ///  Enumerates child windows for the given <paramref name="parent"/>.
    /// </summary>
    /// <param name="callback">
    ///  The provided function will be passed child window handles. Return true to continue enumeration.
    /// </param>
    public static void EnumerateChildWindows(
        this WindowHandle parent,
        Func<WindowHandle, bool> callback)
    {
        using var enumerator = new ChildWindowEnumerator(parent, callback);
    }

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