// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi.Native;
using WInterop.Support;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public static partial class Gdi
    {
        public static unsafe DeviceContext CreateDeviceContext(string driver, string device)
            => new DeviceContext(GdiImports.CreateDCW(driver, device, null, null), ownsHandle: true);

        /// <summary>
        ///  Creates a <see cref="DeviceContext"/> that covers all displays.
        /// </summary>
        public static unsafe DeviceContext CreateDesktopDeviceContext()
            => new DeviceContext(GdiImports.CreateDCW("DISPLAY", null, null, null), ownsHandle: true);

        /// <summary>
        ///  Returns an in memory device context that is compatible with the specified device.
        /// </summary>
        /// <param name="deviceContext">An existing device context or default for the application's current screen.</param>
        /// <returns>A 1 by 1 monochrome memory device context.</returns>
        public static DeviceContext CreateCompatibleDeviceContext(this in DeviceContext deviceContext)
            => new DeviceContext(GdiImports.CreateCompatibleDC(deviceContext), ownsHandle: true);

        /// <summary>
        ///  Gets a <paramref name="capability"/> for the given <paramref name="deviceContext"/>.
        /// </summary>
        public static int GetDeviceCapability(this in DeviceContext deviceContext, DeviceCapability capability)
            => GdiImports.GetDeviceCaps(deviceContext, capability);

        public static Size GetDeviceResolution(this in DeviceContext deviceContext)
            => new Size(
                deviceContext.GetDeviceCapability(DeviceCapability.HorzontalResolution),
                deviceContext.GetDeviceCapability(DeviceCapability.VerticalResolution));

        public static Size GetDesktopResolution(this in DeviceContext deviceContext)
            => new Size(
                deviceContext.GetDeviceCapability(DeviceCapability.DesktopHorizontalResolution),
                deviceContext.GetDeviceCapability(DeviceCapability.DesktopVerticalResolution));

        public static unsafe DeviceContext CreateInformationContext(string driver, string device)
            => new DeviceContext(GdiImports.CreateICW(driver, device, null, null), ownsHandle: true);

        /// <summary>
        ///  Get the device context for the client area of the specified window.
        /// </summary>
        public static DeviceContext GetDeviceContext(this in WindowHandle window)
            => new DeviceContext(GdiImports.GetDC(window), window);

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
            => new DeviceContext(GdiImports.GetWindowDC(window), window);

        public static BitmapHandle CreateCompatibleBitmap(this in DeviceContext context, Size size)
            => new BitmapHandle(GdiImports.CreateCompatibleBitmap(context, size.Width, size.Height));

        public static void BitBlit(
            this in DeviceContext source,
            in DeviceContext destination,
            Point sourceOrigin,
            Rectangle destinationBounds,
            RasterOperation operation)
        {
            Error.ThrowLastErrorIfFalse(
                GdiImports.BitBlt(
                    destination,
                    destinationBounds.X,
                    destinationBounds.Y,
                    destinationBounds.Width,
                    destinationBounds.Height,
                    source,
                    sourceOrigin.X,
                    sourceOrigin.Y,
                    operation));
        }

        public static StretchMode SetStretchBlitMode(this DeviceContext context, StretchMode mode)
        {
            StretchMode oldMode = GdiImports.SetStretchBltMode(context, mode);
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
            return GdiImports.StretchBlt(
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
                operation);
        }

        /// <summary>
        ///  Enumerate display device info for the given device name.
        /// </summary>
        /// <param name="deviceName">The device to enumerate or null for all devices.</param>
        public static IEnumerable<DisplayDevice> EnumerateDisplayDevices(string deviceName)
        {
            uint index = 0;
            DisplayDevice device = DisplayDevice.Create();

            while (GdiImports.EnumDisplayDevicesW(deviceName, index, ref device, 0))
            {
                yield return device;
                index++;
                device = DisplayDevice.Create();
            }

            yield break;
        }

        public static IEnumerable<DeviceMode> EnumerateDisplaySettings(string deviceName, uint modeIndex = 0)
        {
            DeviceMode mode = DeviceMode.Create();

            while (GdiImports.EnumDisplaySettingsW(deviceName, modeIndex, ref mode))
            {
                yield return mode;

                if (modeIndex == GdiDefines.ENUM_CURRENT_SETTINGS || modeIndex == GdiDefines.ENUM_REGISTRY_SETTINGS)
                    break;

                modeIndex++;
                mode = DeviceMode.Create();
            }

            yield break;
        }

        /// <summary>
        ///  Selects the given object into the specified device context.
        /// </summary>
        /// <returns>The previous object or null if failed OR null if the given object was a region.</returns>
        public static GdiObjectHandle SelectObject(this in DeviceContext context, GdiObjectHandle @object)
        {
            HGDIOBJ handle = GdiImports.SelectObject(context, @object);
            if (handle.IsInvalid)
                return default;

            ObjectType type = GdiImports.GetObjectType(@object);
            return type == ObjectType.Region ? default : new GdiObjectHandle(handle, ownsHandle: false);
        }

        public static bool UpdateWindow<T>(this T window) where T : IHandle<WindowHandle>
            => GdiImports.UpdateWindow(window.Handle);

        public static unsafe bool ValidateRectangle(this in WindowHandle window, ref Rectangle rectangle)
        {
            Rect rect = rectangle;
            return GdiImports.ValidateRect(window, &rect);
        }

        /// <summary>
        ///  Validates the entire Window.
        /// </summary>
        public static unsafe bool Validate(this in WindowHandle window)
            => GdiImports.ValidateRect(window, null);

        /// <summary>
        ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(this in WindowHandle window)
        {
            PAINTSTRUCT paintStruct = default;
            GdiImports.BeginPaint(window, ref paintStruct);
            return new DeviceContext(paintStruct, window);
        }

        /// <summary>
        ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(this in WindowHandle window, out Rectangle paintRectangle)
        {
            PAINTSTRUCT paintStruct = default;
            GdiImports.BeginPaint(window, ref paintStruct);
            paintRectangle = paintStruct.rcPaint;
            return new DeviceContext(paintStruct, window);
        }

        /// <summary>
        ///  Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(this in WindowHandle window, out PaintStruct paintStruct)
        {
            PAINTSTRUCT ps = default;
            GdiImports.BeginPaint(window, ref ps);
            paintStruct = ps;
            return new DeviceContext(ps, window);
        }

        public static unsafe bool InvalidateRectangle(this in WindowHandle window, Rectangle rectangle, bool erase)
        {
            Rect rect = rectangle;
            return GdiImports.InvalidateRect(window, &rect, erase);
        }

        public static unsafe bool Invalidate(this in WindowHandle window, bool erase = true)
            => GdiImports.InvalidateRect(window, null, erase);

        public static RegionHandle CreateEllipticRegion(Rectangle bounds)
            => new RegionHandle(GdiImports.CreateEllipticRgn(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom));

        public static RegionHandle CreateRectangleRegion(Rectangle rectangle)
            => new RegionHandle(GdiImports.CreateRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom));

        public static RegionType CombineRegion(
            this in RegionHandle destination,
            RegionHandle sourceOne,
            RegionHandle sourceTwo,
            CombineRegionMode mode)
            => GdiImports.CombineRgn(destination, sourceOne, sourceTwo, mode);

        public static RegionType SelectClippingRegion(this in DeviceContext context, RegionHandle region)
            => GdiImports.SelectClipRgn(context, region);

        public static Rectangle GetClipBox(this in DeviceContext context, out RegionType regionType)
        {
            regionType = GdiImports.GetClipBox(context, out Rect rect);
            return rect;
        }

        public static RegionHandle GetClippingRegion(this in DeviceContext context, out bool hasRegion)
        {
            HRGN hrgn = GdiImports.CreateRectRgn(0, 0, 0, 0);
            int result = GdiImports.GetClipRgn(context, hrgn);
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

        public static Point GetViewportOrigin(this in DeviceContext context, out bool success)
        {
            success = GdiImports.GetViewportOrgEx(context, out Point point);
            return point;
        }

        public static unsafe bool SetViewportOrigin(this in DeviceContext context, Point point)
            => GdiImports.SetViewportOrgEx(context, point.X, point.Y, null);

        public static Point GetWindowOrigin(this in DeviceContext context)
            => GetWindowOrigin(context, out _);

        public static Point GetWindowOrigin(this in DeviceContext context, out bool success)
        {
            success = GdiImports.GetWindowOrgEx(context, out Point point);
            return point;
        }

        public static unsafe bool SetWindowOrigin(this in DeviceContext context, Point point)
            => GdiImports.SetWindowOrgEx(context, point.X, point.Y, null);

        /// <summary>
        ///  Shared brush, handle doesn't need disposed.
        /// </summary>
        public static BrushHandle GetSystemColorBrush(SystemColor systemColor)
            => new BrushHandle(GdiImports.GetSysColorBrush(systemColor), ownsHandle: false);

        public static Color GetBackgroundColor(this in DeviceContext context)
            => GdiImports.GetBkColor(context);

        public static Color GetBrushColor(this in DeviceContext context)
            => GdiImports.GetDCBrushColor(context);

        public static Color SetBackgroundColor(this in DeviceContext context, Color color)
            => GdiImports.SetBkColor(context, color);

        public static Color SetBackgroundColor(this in DeviceContext context, SystemColor color)
            => GdiImports.SetBkColor(context, Windows.Windows.GetSystemColor(color));

        public static BackgroundMode SetBackgroundMode(this in DeviceContext context, BackgroundMode mode)
            => GdiImports.SetBkMode(context, mode);

        public static BackgroundMode GetBackgroundMode(this in DeviceContext context)
            => GdiImports.GetBkMode(context);

        public static PenMixMode SetRasterOperation(this in DeviceContext context, PenMixMode foregroundMixMode)
            => GdiImports.SetROP2(context, foregroundMixMode);

        public static PenMixMode GetRasterOperation(this in DeviceContext context)
            => GdiImports.GetROP2(context);

        public static bool ScreenToClient<T>(this T window, ref Point point) where T : IHandle<WindowHandle>
            => GdiImports.ScreenToClient(window.Handle, ref point);

        public static bool ScreenToClient<T>(this T window, ref Rectangle rectangle) where T : IHandle<WindowHandle>
        {
            Point location = rectangle.Location;
            bool result = GdiImports.ScreenToClient(window.Handle, ref location);
            rectangle.Location = location;
            return result;
        }

        public static bool ClientToScreen(this in WindowHandle window, ref Point point)
            => GdiImports.ClientToScreen(window, ref point);

        public static bool DeviceToLogical(this in DeviceContext context, params Point[] points)
            => DeviceToLogical(context, points.AsSpan());

        public static bool DeviceToLogical(this in DeviceContext context, ReadOnlySpan<Point> points)
            => GdiImports.DPtoLP(context, ref MemoryMarshal.GetReference(points), points.Length);

        public static unsafe bool LogicalToDevice(this in DeviceContext context, params Point[] points)
            => LogicalToDevice(context, points);

        public static unsafe bool LogicalToDevice(this in DeviceContext context, ReadOnlySpan<Point> points)
            => GdiImports.LPtoDP(context, ref MemoryMarshal.GetReference(points), points.Length);

        public static unsafe bool OffsetWindowOrigin(this in DeviceContext context, int x, int y)
            => GdiImports.OffsetWindowOrgEx(context, x, y, null);

        public static unsafe bool OffsetViewportOrigin(this in DeviceContext context, int x, int y)
            => GdiImports.OffsetViewportOrgEx(context, x, y, null);

        public static bool GetWindowExtents(this in DeviceContext context, out Size size)
            => GdiImports.GetWindowExtEx(context, out size);

        /// <summary>
        ///  Sets the logical ("window") dimensions of the device context.
        /// </summary>
        public static unsafe bool SetWindowExtents(this in DeviceContext context, Size size)
            => GdiImports.SetWindowExtEx(context, size.Width, size.Height, null);

        public static bool GetViewportExtents(this in DeviceContext context, out Size size)
            => GdiImports.GetViewportExtEx(context, out size);

        public static unsafe bool SetViewportExtents(this in DeviceContext context, Size size)
            => GdiImports.SetViewportExtEx(context, size.Width, size.Height, null);

        public static MappingMode GetMappingMode(this in DeviceContext context)
            => GdiImports.GetMapMode(context);

        public static MappingMode SetMappingMode(this in DeviceContext context, MappingMode mapMode)
            => GdiImports.SetMapMode(context, mapMode);

        public static Rectangle GetBoundsRect(this in DeviceContext context, bool reset = false)
        {
            GdiImports.GetBoundsRect(context, out Rect rect, reset ? BoundsState.Reset : default);
            return rect;
        }

        public static Rectangle GetBoundsRect(this in DeviceContext context, out BoundsState state, bool reset = false)
        {
            state = GdiImports.GetBoundsRect(context, out Rect rect, reset ? BoundsState.Reset : default);
            return rect;
        }

        public static unsafe Span<PaletteEntry> GetPaletteEntries(this in PaletteHandle palette)
        {
            uint count = GdiImports.GetPaletteEntries(palette, 0, 0, null);

            if (count == 0)
                return Span<PaletteEntry>.Empty;

            PaletteEntry[] entries = new PaletteEntry[count];

            fixed (PaletteEntry* pe = entries)
            {
                count = GdiImports.GetPaletteEntries(palette, 0, count, pe);
            }

            Debug.Assert(count == entries.Length);
            return new Span<PaletteEntry>(entries, 0, (int)count);
        }

        public static unsafe Span<PaletteEntry> GetSystemPaletteEntries(this in DeviceContext deviceContext)
        {
            uint count = GdiImports.GetSystemPaletteEntries(deviceContext, 0, 0, null);

            if (count == 0)
                return Span<PaletteEntry>.Empty;

            PaletteEntry[] entries = new PaletteEntry[count];

            fixed (PaletteEntry* pe = entries)
            {
                count = GdiImports.GetSystemPaletteEntries(deviceContext, 0, count, pe);
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
            => GdiImports.SetGraphicsMode(deviceContext, graphicsMode);

        public static GraphicsMode GetGraphicsMode(this in DeviceContext deviceContext)
            => GdiImports.GetGraphicsMode(deviceContext);

        /// <summary>
        ///  Set the transform for the given device context.
        /// </summary>
        /// <returns>
        ///  True if successful. SetWorldTransform doesn't set last error unless you try to pass a handle
        ///  that isn't a device context. The other failure case is that the graphics mode isn't set to
        ///  advanced.
        /// </returns>
        public static bool SetWorldTransform(this in DeviceContext deviceContext, ref Matrix3x2 transform)
            => GdiImports.SetWorldTransform(deviceContext, ref transform);

        public static bool GetWorldTransform(this in DeviceContext deviceContext, out Matrix3x2 transform)
            => GdiImports.GetWorldTransform(deviceContext, out transform);
    }
}