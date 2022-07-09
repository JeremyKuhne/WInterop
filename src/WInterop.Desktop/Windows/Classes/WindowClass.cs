// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules;

namespace WInterop.Windows;

public class WindowClass : IDisposable
{
    // Stash the delegate to keep it from being collected
    private readonly WindowProcedure _windowProcedure;
    private string _className;
    private WindowClassInfo? _windowClass;
    private bool _disposedValue;

    public Atom Atom { get; private set; }
    public WindowHandle MainWindow { get; private set; }
    public ModuleInstance ModuleInstance { get; }

    public unsafe WindowClass(
        string? className = default,
        ModuleInstance? moduleInstance = default,
        ClassStyle classStyle = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
        BrushHandle backgroundBrush = default,
        IconHandle icon = default,
        IconHandle smallIcon = default,
        CursorHandle cursor = default,
        string? menuName = null,
        int menuId = 0,
        int classExtraBytes = 0,
        int windowExtraBytes = 0)
    {
        // Handle default values
        className ??= Guid.NewGuid().ToString();

        if (backgroundBrush == default)
            backgroundBrush = SystemColor.Window;
        else if (backgroundBrush == BrushHandle.NoBrush)
            backgroundBrush = default;

        if (icon == default)
            icon = IconId.Application;
        else if (icon == IconHandle.NoIcon)
            icon = default;

        if (cursor == default)
            cursor = CursorId.Arrow;
        else if (cursor == CursorHandle.NoCursor)
            cursor = default;

        moduleInstance ??= Modules.Modules.GetExeModuleHandle();

        if (menuId != 0 && menuName is not null)
            throw new ArgumentException($"Can't set both {nameof(menuName)} and {nameof(menuId)}.");

        _windowProcedure = WindowProcedure;
        ModuleInstance = moduleInstance;

        _className = className;
        _windowClass = new WindowClassInfo(_windowProcedure)
        {
            ClassName = className,
            Style = classStyle,
            ClassExtraBytes = classExtraBytes,
            WindowExtraBytes = windowExtraBytes,
            Instance = moduleInstance,
            Icon = icon,
            Cursor = cursor,
            Background = backgroundBrush,
            SmallIcon = smallIcon,
            MenuId = menuId,
            MenuName = menuName
        };
    }

    public WindowClass(string registeredClassName)
    {
        _windowProcedure = WindowProcedure;
        _className = registeredClassName;
        ModuleInstance = ModuleInstance.Null;
    }

    public bool IsRegistered => Atom.IsValid || ModuleInstance == ModuleInstance.Null;

    /// <summary>
    ///  Registers this <see cref="WindowClass"/> so that instances can be created.
    /// </summary>
    public unsafe WindowClass Register()
    {
        if (_windowClass is not null && !IsRegistered)
        {
            Atom = Windows.RegisterClass(_windowClass);
        }

        return this;
    }

    /// <summary>
    ///  Creates an instance of this <see cref="WindowClass"/>.
    /// </summary>
    /// <param name="bounds">
    ///  Pass <see cref="Windows.DefaultBounds"/> for the default size.
    /// </param>
    /// <param name="windowName">
    ///  The text for the title bar when using <see cref="WindowStyles.Caption"/> or <see cref="WindowStyles.Overlapped"/>.
    ///  For buttons, checkboxes, and other static controls this is the text of the control or a resource reference.
    /// </param>
    /// <param name="isMainWindow">
    ///  Set this to indicate that this is the main window for the application and closing it should terminate the message loop.
    /// </param>
    public virtual WindowHandle CreateWindow(
        Rectangle bounds,
        string? windowName = null,
        WindowStyles style = WindowStyles.Overlapped,
        ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
        bool isMainWindow = false,
        WindowHandle parentWindow = default,
        IntPtr parameters = default,
        MenuHandle menuHandle = default)
    {
        if (!IsRegistered)
            throw new InvalidOperationException("Window class must be registered before using.");

        if (isMainWindow && !MainWindow.IsNull)
            throw new ArgumentException("Main window has already been set.", nameof(isMainWindow));

        if (bounds == default)
            bounds = Windows.DefaultBounds;

        WindowHandle window = Atom.IsValid
            ? Windows.CreateWindow(
                Atom,
                windowName,
                style,
                extendedStyle,
                bounds,
                parentWindow,
                menuHandle,
                parameters)
            : Windows.CreateWindow(
                _className,
                windowName,
                style,
                extendedStyle,
                bounds,
                parentWindow,
                menuHandle,
                parameters);

        if (!Atom.IsValid)
        {
            Atom = (ushort)window.GetClassLong(ClassLong.Atom);
        }

        if (isMainWindow)
        {
            MainWindow = window;
        }

        return window;
    }

    protected virtual LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Destroy:
                if (window == MainWindow)
                    Windows.PostQuitMessage(0);
                return 0;
        }

        return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    ~WindowClass()
    {
        if (!_disposedValue)
        {
            _disposedValue = true;
            Dispose(disposing: false);
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (!_disposedValue)
        {
            _disposedValue = true;
            Dispose(disposing: true);
        }
    }
}