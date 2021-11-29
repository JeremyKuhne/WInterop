// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows;

public class Window : IDisposable, IHandle<WindowHandle>, ILayoutHandler
{
    // Stash the delegate to keep it from being collected
    private readonly WindowProcedure _windowProcedure;
    private readonly WNDPROC _priorWindowProcedure;

    private readonly WindowClass _windowClass;
    private readonly GCHandle _gcHandle;
    private string? _text;

    private static readonly FontHandle s_defaultFont = CreateDefaultFont(default);

    public WindowHandle Handle { get; }

    public event WindowsMessageEvent? MessageHandler;

    public Window(
        WindowClass windowClass,
        Rectangle bounds,
        string? text = default,
        WindowStyles style = WindowStyles.Overlapped,
        ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
        bool isMainWindow = false,
        Window? parentWindow = default,
        IntPtr parameters = default,
        MenuHandle menuHandle = default)
    {
        _windowClass = windowClass;
        if (!_windowClass.IsRegistered)
        {
            _windowClass.Register();
        }

        _text = text;

        // Enable High DPI awareness if not enabled already. Requires Windows 10.
        if (Windows.GetThreadDpiAwareness() == DpiAwareness.Unaware
            && Windows.SetThreadDpiAwarenessContext(DpiAwarenessContext.PerMonitorV2).IsNull)
        {
            // Fall back from V2 if needed
            Windows.SetThreadDpiAwarenessContext(DpiAwarenessContext.PerMonitor);
        }

        Handle = _windowClass.CreateWindow(
            bounds,
            text,
            style,
            extendedStyle,
            isMainWindow,
            parentWindow?.Handle ?? default,
            parameters,
            menuHandle);

        if (parentWindow is null)
        {
            // Set up HDC for scaling
            Handle.SetGraphicsMode(GraphicsMode.Advanced);
            uint dpi = Handle.GetDpiForWindow();
            Matrix3x2 transform = Matrix3x2.CreateScale((dpi / 96.0f) * 5.0f);
            Handle.SetWorldTransform(ref transform);
        }

        if (Handle.GetFont(getSystemFontHandle: false).IsNull)
        {
            // Default system font is applied, use a nicer (ClearType) font
            Handle.SetFont(s_defaultFont);
        }

        _windowProcedure = WindowProcedure;
        _priorWindowProcedure = (WNDPROC)Handle.GetWindowLong(WindowLong.WindowProcedure);
        Handle.SetWindowLong(WindowLong.WindowProcedure, Marshal.GetFunctionPointerForDelegate(_windowProcedure));

        // Stash our managed pointer so we can find the Window from an HWND
        _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
        Handle.SetWindowLong(WindowLong.UserData, GCHandle.ToIntPtr(_gcHandle));
    }

    private static FontHandle CreateDefaultFont(WindowHandle window)
    {
        using var hdc = window.GetDeviceContext();
        return Gdi.Gdi.CreateFont(
            typeface: "Microsoft Sans Serif",
            height: window.FontPointSizeToHeight(11),
            quality: Quality.ClearTypeNatural);
    }

    protected virtual LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        var handlers = MessageHandler;
        if (handlers is not null)
        {
            foreach (var handler in handlers.GetInvocationList().OfType<WindowsMessageEvent>())
            {
                var result = handler(this, window, message, wParam, lParam);
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
        }

        switch (message)
        {
            case MessageType.SetText:
                // Update our cached text if necessary

                if (lParam.IsNull)
                {
                    _text = null;
                }
                else
                {
                    Message.SetText setText = new(lParam);
                    if (!setText.Text.Equals(_text, StringComparison.Ordinal))
                    {
                        _text = setText.Text.ToString();
                    }
                }

                // The default proc actually sets the text, so we shouldn't return from here
                break;

            case MessageType.DpiChanged:
                {
                    // Resize and reposition for the new DPI
                    Message.DpiChanged dpiChanged = new(wParam, lParam);

                    using var oldFont = s_defaultFont;
                    var newFont = Gdi.Gdi.CreateFont(
                        typeface: "Microsoft Sans Serif",
                        height: window.FontPointSizeToHeight(11),
                        quality: Quality.ClearTypeNatural);

                    window.SetFont(newFont);
                    window.EnumerateChildWindows(
                        (WindowHandle child, LParam param) =>
                        {
                            child.SetFont(newFont);
                            return true;
                        },
                        default);

                    window.MoveWindow(dpiChanged.SuggestedBounds, repaint: true);

                    break;
                }
        }

        return Windows.CallWindowProcedure(_priorWindowProcedure, window, message, wParam, lParam);
    }

    public string? Text
    {
        get => _text;
        set
        {
            Handle.SetWindowText(value!);
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Handle.SetWindowLong(WindowLong.WindowProcedure, (IntPtr)_priorWindowProcedure);
    }

    void ILayoutHandler.Layout(Rectangle bounds) => Handle.MoveWindow(bounds, repaint: true);

    public static implicit operator HWND(Window window) => window.Handle;
}