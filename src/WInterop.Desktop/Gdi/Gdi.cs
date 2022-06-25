// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using WInterop.Errors;
using WInterop.Support;
using WInterop.Windows;

namespace WInterop.Gdi;

public static partial class Gdi
{
    public static unsafe DeviceContext CreateDeviceContext(string driver, string device)
    {
        fixed (char* dr = driver)
        fixed (char* de = device)
        {
            return new(TerraFXWindows.CreateDCW((ushort*)dr, (ushort*)de, null, null), ownsHandle: true);
        }
    }

    /// <summary>
    ///  Creates a <see cref="DeviceContext"/> that covers all displays.
    /// </summary>
    public static unsafe DeviceContext CreateDesktopDeviceContext()
    {
        fixed (char* c = "DISPLAY")
        {
            return new(TerraFXWindows.CreateDCW((ushort*)c, null, null, null), ownsHandle: true);
        }
    }

    /// <summary>
    ///  Returns an in memory device context that is compatible with the specified device.
    /// </summary>
    /// <param name="deviceContext">An existing device context or default for the application's current screen.</param>
    /// <returns>A 1 by 1 monochrome memory device context.</returns>
    public static DeviceContext CreateCompatibleDeviceContext(this in DeviceContext deviceContext)
        => new(TerraFXWindows.CreateCompatibleDC(deviceContext), ownsHandle: true);

    /// <summary>
    ///  Gets a <paramref name="capability"/> for the given <paramref name="deviceContext"/>.
    /// </summary>
    public static int GetDeviceCapability(this in DeviceContext deviceContext, DeviceCapability capability)
        => TerraFXWindows.GetDeviceCaps(deviceContext, (int)capability);

    public static Size GetDeviceResolution(this in DeviceContext deviceContext)
        => new(
            deviceContext.GetDeviceCapability(DeviceCapability.HorzontalResolution),
            deviceContext.GetDeviceCapability(DeviceCapability.VerticalResolution));

    public static Size GetDesktopResolution(this in DeviceContext deviceContext)
        => new(
            deviceContext.GetDeviceCapability(DeviceCapability.DesktopHorizontalResolution),
            deviceContext.GetDeviceCapability(DeviceCapability.DesktopVerticalResolution));

    public static unsafe DeviceContext CreateInformationContext(string driver, string device)
    {
        fixed (char* dr = driver)
        fixed (char* de = device)
        {
            return new(TerraFXWindows.CreateICW((ushort*)dr, (ushort*)de, null, null), ownsHandle: true);
        }
    }

    /// <summary>
    ///  Get the device context for the client area of the specified window.
    /// </summary>
    public static DeviceContext GetDeviceContext(this in WindowHandle window)
        => new(TerraFXWindows.GetDC(window), window);

    /// <summary>
    ///  Get the device context for the screen.
    /// </summary>
    public static DeviceContext GetDeviceContext() => GetDeviceContext(default);

    /// <summary>
    ///  Get the device context for the specified window.
    /// </summary>
    /// <param name="window">The window handle, or null for the primary display monitor.</param>
    /// <returns>Returns a device context for the entire window, not just the client area.</returns>
    public static DeviceContext GetWindowDeviceContext(this in WindowHandle window)
        => new(TerraFXWindows.GetWindowDC(window), window);

    public static BitmapHandle CreateCompatibleBitmap(this in DeviceContext context, Size size)
        => new(TerraFXWindows.CreateCompatibleBitmap(context, size.Width, size.Height));

    public static void BitBlit(
        this in DeviceContext source,
        in DeviceContext destination,
        Point sourceOrigin,
        Rectangle destinationBounds,
        RasterOperation operation)
    {
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.BitBlt(
                destination,
                destinationBounds.X,
                destinationBounds.Y,
                destinationBounds.Width,
                destinationBounds.Height,
                source,
                sourceOrigin.X,
                sourceOrigin.Y,
                operation.Value));
    }

    public static StretchMode SetStretchBlitMode(this DeviceContext context, StretchMode mode)
    {
        StretchMode oldMode = (StretchMode)TerraFXWindows.SetStretchBltMode(context, (int)mode);
        if ((WindowsError)oldMode == WindowsError.ERROR_INVALID_PARAMETER)
            WindowsError.ERROR_INVALID_PARAMETER.Throw();
        return oldMode;
    }

    public static bool StretchBlit(
        this in DeviceContext source,
        in DeviceContext destination,
        Rectangle sourceBounds,
        Rectangle destinationBounds,
        RasterOperation operation)
    {
        return TerraFXWindows.StretchBlt(
            destination,
            destinationBounds.X,
            destinationBounds.Y,
            destinationBounds.Width,
            destinationBounds.Height,
            source,
            sourceBounds.X,
            sourceBounds.Y,
            sourceBounds.Width,
            sourceBounds.Height,
            operation.Value);
    }

    /// <summary>
    ///  Enumerate display device info for the given device name.
    /// </summary>
    /// <param name="deviceName">The device to enumerate or null for all devices.</param>
    public static IEnumerable<DisplayDevice> EnumerateDisplayDevices(string deviceName)
        => new DisplayDeviceEnumerable(deviceName);

    public static IEnumerable<DeviceMode> EnumerateDisplaySettings(string deviceName, uint modeIndex = 0)
        => new DisplaySettingsEnumerable(deviceName, modeIndex);

    /// <summary>
    ///  Selects the given object into the specified device context.
    /// </summary>
    /// <returns>The previous object or null if failed OR null if the given object was a region.</returns>
    public static GdiObjectHandle SelectObject(this in DeviceContext context, GdiObjectHandle @object)
    {
        HGDIOBJ handle = TerraFXWindows.SelectObject(context, @object);
        if (handle == HGDIOBJ.INVALID_VALUE)
            return default;

        ObjectType type = (ObjectType)TerraFXWindows.GetObjectType(@object);
        return type == ObjectType.Region ? default : new GdiObjectHandle(handle, ownsHandle: false);
    }

    public static bool UpdateWindow<T>(this T window) where T : IHandle<WindowHandle>
        => TerraFXWindows.UpdateWindow(window.Handle);

    public static unsafe bool ValidateRectangle(this in WindowHandle window, ref Rectangle rectangle)
    {
        Rect rect = rectangle;
        return TerraFXWindows.ValidateRect(window, (RECT*)&rect);
    }

    /// <summary>
    ///  Validates the entire Window.
    /// </summary>
    public static unsafe bool Validate(this in WindowHandle window)
        => TerraFXWindows.ValidateRect(window, null);

    /// <summary>
    ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
    /// </summary>
    public static unsafe DeviceContext BeginPaint(this in WindowHandle window)
    {
        PAINTSTRUCT paintStruct = default;
        TerraFXWindows.BeginPaint(window, &paintStruct);
        return new DeviceContext(paintStruct, window);
    }

    /// <summary>
    ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
    /// </summary>
    public static unsafe DeviceContext BeginPaint(this in WindowHandle window, out Rectangle paintRectangle)
    {
        PAINTSTRUCT paintStruct = default;
        TerraFXWindows.BeginPaint(window, &paintStruct);
        paintRectangle = paintStruct.rcPaint.ToRectangle();
        return new DeviceContext(paintStruct, window);
    }

    /// <summary>
    ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
    /// </summary>
    public static unsafe DeviceContext BeginPaint(this in WindowHandle window, out PaintStruct paintStruct)
    {
        PAINTSTRUCT ps = default;
        TerraFXWindows.BeginPaint(window, &ps);
        paintStruct = ps;
        return new DeviceContext(ps, window);
    }

    public static unsafe bool InvalidateRectangle(this in WindowHandle window, Rectangle rectangle, bool erase)
    {
        Rect rect = rectangle;
        return TerraFXWindows.InvalidateRect(window, (RECT*)&rect, erase);
    }

    public static unsafe bool Invalidate(this in WindowHandle window, bool erase = true)
        => TerraFXWindows.InvalidateRect(window, null, erase);

    public static RegionHandle CreateEllipticRegion(Rectangle bounds)
        => new(TerraFXWindows.CreateEllipticRgn(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom));

    public static RegionHandle CreateRectangleRegion(Rectangle rectangle)
        => new(TerraFXWindows.CreateRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom));

    public static RegionType CombineRegion(
        this in RegionHandle destination,
        RegionHandle sourceOne,
        RegionHandle sourceTwo,
        CombineRegionMode mode)
        => (RegionType)TerraFXWindows.CombineRgn(destination, sourceOne, sourceTwo, (int)mode);

    public static RegionType SelectClippingRegion(this in DeviceContext context, RegionHandle region)
        => (RegionType)TerraFXWindows.SelectClipRgn(context, region);

    public static unsafe Rectangle GetClipBox(this in DeviceContext context, out RegionType regionType)
    {
        Rect rect;
        regionType = (RegionType)TerraFXWindows.GetClipBox(context, (RECT*)&rect);
        return rect;
    }

    public static RegionHandle GetClippingRegion(this in DeviceContext context, out bool hasRegion)
    {
        HRGN hrgn = TerraFXWindows.CreateRectRgn(0, 0, 0, 0);
        int result = TerraFXWindows.GetClipRgn(context, hrgn);
        hasRegion = result switch
        {
            0 => false,
            1 => true,
            _ => throw new WInteropIOException("Failed to get the clipping region")
        };

        return new RegionHandle(hrgn);
    }

    public static Point GetViewportOrigin(this in DeviceContext context)
        => GetViewportOrigin(context, out _);

    public static unsafe Point GetViewportOrigin(this in DeviceContext context, out bool success)
    {
        Point point;
        success = TerraFXWindows.GetViewportOrgEx(context, (POINT*)&point);
        return point;
    }

    public static unsafe bool SetViewportOrigin(this in DeviceContext context, Point point)
        => TerraFXWindows.SetViewportOrgEx(context, point.X, point.Y, null);

    public static Point GetWindowOrigin(this in DeviceContext context)
        => GetWindowOrigin(context, out _);

    public static unsafe Point GetWindowOrigin(this in DeviceContext context, out bool success)
    {
        Point point;
        success = TerraFXWindows.GetWindowOrgEx(context, (POINT*)&point);
        return point;
    }

    public static unsafe bool SetWindowOrigin(this in DeviceContext context, Point point)
        => TerraFXWindows.SetWindowOrgEx(context, point.X, point.Y, null);

    /// <summary>
    ///  Shared brush, handle doesn't need disposed.
    /// </summary>
    public static BrushHandle GetSystemColorBrush(SystemColor systemColor)
        => new(TerraFXWindows.GetSysColorBrush((int)systemColor), ownsHandle: false);

    public static Color GetBackgroundColor(this in DeviceContext context)
        => TerraFXWindows.GetBkColor(context).ToColor();

    public static Color GetBrushColor(this in DeviceContext context)
        => TerraFXWindows.GetDCBrushColor(context).ToColor();

    public static Color SetBackgroundColor(this in DeviceContext context, Color color)
        => TerraFXWindows.SetBkColor(context, color.ToCOLORREF()).ToColor();

    public static Color SetBackgroundColor(this in DeviceContext context, SystemColor color)
        => TerraFXWindows.SetBkColor(context, TerraFXWindows.GetSysColor((int)color)).ToColor();

    public static BackgroundMode SetBackgroundMode(this in DeviceContext context, BackgroundMode mode)
        => (BackgroundMode)TerraFXWindows.SetBkMode(context, (int)mode);

    public static BackgroundMode GetBackgroundMode(this in DeviceContext context)
        => (BackgroundMode)TerraFXWindows.GetBkMode(context);

    public static PenMixMode SetRasterOperation(this in DeviceContext context, PenMixMode foregroundMixMode)
        => (PenMixMode)TerraFXWindows.SetROP2(context, (int)foregroundMixMode);

    public static PenMixMode GetRasterOperation(this in DeviceContext context)
        => (PenMixMode)TerraFXWindows.GetROP2(context);

    public static unsafe bool ScreenToClient<T>(this T window, ref Point point) where T : IHandle<WindowHandle>
    {
        fixed (Point* p = &point)
        {
            return TerraFXWindows.ScreenToClient(window.Handle, (POINT*)p);
        }
    }

    public static unsafe bool ScreenToClient<T>(this T window, ref Rectangle rectangle) where T : IHandle<WindowHandle>
    {
        Point location = rectangle.Location;
        bool result = TerraFXWindows.ScreenToClient(window.Handle, (POINT*)&location);
        rectangle.Location = location;
        return result;
    }

    public static unsafe bool ClientToScreen(this in WindowHandle window, ref Point point)
    {
        fixed (Point* p = &point)
        {
            return TerraFXWindows.ClientToScreen(window, (POINT*)p);
        }
    }

    public static bool DeviceToLogical(this in DeviceContext context, params Point[] points)
        => DeviceToLogical(context, points.AsSpan());

    public static unsafe bool DeviceToLogical(this in DeviceContext context, ReadOnlySpan<Point> points)
    {
        fixed (Point* p = points)
        {
            return TerraFXWindows.DPtoLP(context, (POINT*)p, points.Length);
        }
    }

    public static unsafe bool LogicalToDevice(this in DeviceContext context, params Point[] points)
        => LogicalToDevice(context, points);

    public static unsafe bool LogicalToDevice(this in DeviceContext context, ReadOnlySpan<Point> points)
    {
        fixed (Point* p = points)
        {
            return TerraFXWindows.LPtoDP(context, (POINT*)p, points.Length);
        }
    }

    public static unsafe bool OffsetWindowOrigin(this in DeviceContext context, int x, int y)
        => TerraFXWindows.OffsetWindowOrgEx(context, x, y, null);

    public static unsafe bool OffsetViewportOrigin(this in DeviceContext context, int x, int y)
        => TerraFXWindows.OffsetViewportOrgEx(context, x, y, null);

    public static unsafe bool GetWindowExtents(this in DeviceContext context, out Size size)
    {
        fixed (Size* s = &size)
        {
            return TerraFXWindows.GetWindowExtEx(context, (SIZE*)s);
        }
    }

    /// <summary>
    ///  Sets the logical ("window") dimensions of the device context.
    /// </summary>
    public static unsafe bool SetWindowExtents(this in DeviceContext context, Size size)
        => TerraFXWindows.SetWindowExtEx(context, size.Width, size.Height, null);

    public static unsafe bool GetViewportExtents(this in DeviceContext context, out Size size)
    {
        fixed (Size* s = &size)
        {
            return TerraFXWindows.GetViewportExtEx(context, (SIZE*)s);
        }
    }

    public static unsafe bool SetViewportExtents(this in DeviceContext context, Size size)
        => TerraFXWindows.SetViewportExtEx(context, size.Width, size.Height, null);

    public static MappingMode GetMappingMode(this in DeviceContext context)
        => (MappingMode)TerraFXWindows.GetMapMode(context);

    public static MappingMode SetMappingMode(this in DeviceContext context, MappingMode mapMode)
        => (MappingMode)TerraFXWindows.SetMapMode(context, (int)mapMode);

    public static unsafe Rectangle GetBoundsRect(this in DeviceContext context, bool reset = false)
    {
        Rect rect;
        TerraFXWindows.GetBoundsRect(context, (RECT*)&rect, reset ? (uint)BoundsState.Reset : default);
        return rect;
    }

    public static unsafe Rectangle GetBoundsRect(
        this in DeviceContext context,
        out BoundsState state,
        bool reset = false)
    {
        Rect rect;
        state = (BoundsState)TerraFXWindows.GetBoundsRect(
            context,
            (RECT*)&rect,
            reset ? (uint)BoundsState.Reset : default);
        return rect;
    }

    public static unsafe Span<PaletteEntry> GetPaletteEntries(this in PaletteHandle palette)
    {
        uint count = TerraFXWindows.GetPaletteEntries(palette, 0, 0, null);

        if (count == 0)
        {
            return Span<PaletteEntry>.Empty;
        }

        PaletteEntry[] entries = new PaletteEntry[count];

        fixed (PaletteEntry* pe = entries)
        {
            count = TerraFXWindows.GetPaletteEntries(palette, 0, count, (PALETTEENTRY*)pe);
        }

        Debug.Assert(count == entries.Length);
        return new Span<PaletteEntry>(entries, 0, (int)count);
    }

    public static unsafe Span<PaletteEntry> GetSystemPaletteEntries(this in DeviceContext deviceContext)
    {
        uint count = TerraFXWindows.GetSystemPaletteEntries(deviceContext, 0, 0, null);

        if (count == 0)
            return Span<PaletteEntry>.Empty;

        PaletteEntry[] entries = new PaletteEntry[count];

        fixed (PaletteEntry* pe = entries)
        {
            count = TerraFXWindows.GetSystemPaletteEntries(deviceContext, 0, count, (PALETTEENTRY*)pe);
        }

        Debug.Assert(count == entries.Length);
        return new Span<PaletteEntry>(entries, 0, (int)count);
    }

    /// <summary>
    ///  Sets the graphics mode for the given device context.
    /// </summary>
    /// <remarks>
    ///  You cannot set the mode to <see cref="GraphicsMode.Compatible"/> if the device context currently
    ///  has a non-identity transform.
    /// </remarks>
    public static GraphicsMode SetGraphicsMode(this in DeviceContext deviceContext, GraphicsMode graphicsMode)
        => (GraphicsMode)TerraFXWindows.SetGraphicsMode(deviceContext, (int)graphicsMode);

    public static GraphicsMode GetGraphicsMode(this in DeviceContext deviceContext)
        => (GraphicsMode)TerraFXWindows.GetGraphicsMode(deviceContext);

    /// <summary>
    ///  Set the transform for the given device context.
    /// </summary>
    /// <returns>
    ///  True if successful. SetWorldTransform doesn't set last error unless you try to pass a handle
    ///  that isn't a device context. The other failure case is that the graphics mode isn't set to
    ///  advanced.
    /// </returns>
    public static unsafe bool SetWorldTransform(this in DeviceContext deviceContext, ref Matrix3x2 transform)
    {
        fixed (Matrix3x2* t = &transform)
        {
            return TerraFXWindows.SetWorldTransform(deviceContext, (XFORM*)t);
        }
    }

    public static unsafe bool GetWorldTransform(this in DeviceContext deviceContext, out Matrix3x2 transform)
    {
        fixed (Matrix3x2* t = &transform)
        {
            return TerraFXWindows.GetWorldTransform(deviceContext, (XFORM*)t);
        }
    }
}