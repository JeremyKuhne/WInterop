// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public static partial class Gdi
    {
        public unsafe static DeviceContext CreateDeviceContext(string driver, string device)
            => new DeviceContext(Imports.CreateDCW(driver, device, null, null), ownsHandle: true);

        /// <summary>
        /// Returns an in memory device context that is compatible with the specified device.
        /// </summary>
        /// <param name="context">An existing device context or new DeviceContext() for the application's current screen.</param>
        /// <returns>A 1 by 1 monochrome memory device context.</returns>
        public static DeviceContext CreateCompatibleDeviceContext(this in DeviceContext context)
            => new DeviceContext(Imports.CreateCompatibleDC(context), ownsHandle: true);

        public static int GetDeviceCapability(this in DeviceContext context, DeviceCapability capability) => Imports.GetDeviceCaps(context, capability);

        public unsafe static DeviceContext CreateInformationContext(string driver, string device)
            => new DeviceContext(Imports.CreateICW(driver, device, null, null), ownsHandle: true);

        /// <summary>
        /// Get the device context for the client area of the specified window.
        /// </summary>
        public static DeviceContext GetDeviceContext(this in WindowHandle window)
            => new DeviceContext(Imports.GetDC(window), window);

        /// <summary>
        /// Get the device context for the screen.
        /// </summary>
        public static DeviceContext GetDeviceContext() => new WindowHandle().GetDeviceContext();

        /// <summary>
        /// Get the device context for the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the primary display monitor.</param>
        /// <returns>Returns a device context for the entire window, not just the client area.</returns>
        public static DeviceContext GetWindowDeviceContext(this in WindowHandle window)
        {
            return new DeviceContext(Imports.GetWindowDC(window), window);
        }

        public static BitmapHandle CreateCompatibleBitmap(this in DeviceContext context, Size size) => 
            new BitmapHandle(Imports.CreateCompatibleBitmap(context, size.Width, size.Height));

        public static void BitBlit(
            this in DeviceContext source,
            in DeviceContext destination,
            Point sourceOrigin,
            Rectangle destinationBounds,
            RasterOperation operation)
        {
            if (!Imports.BitBlt(
                destination,
                destinationBounds.X,
                destinationBounds.Y,
                destinationBounds.Width,
                destinationBounds.Height,
                source,
                sourceOrigin.X,
                sourceOrigin.Y,
                operation))
            {
                throw Error.GetIoExceptionForLastError();
            }
        }

        public static StretchMode SetStretchBlitMode(this DeviceContext context, StretchMode mode)
        {
            StretchMode oldMode = Imports.SetStretchBltMode(context, mode);
            if ((WindowsError)oldMode == WindowsError.ERROR_INVALID_PARAMETER)
                throw Error.GetIoExceptionForError(WindowsError.ERROR_INVALID_PARAMETER);
            return oldMode;
        }

        public static bool StretchBlit(
            this in DeviceContext source,
            in DeviceContext destination,
            Rectangle sourceBounds,
            Rectangle destinationBounds,
            RasterOperation operation)
        {
            return Imports.StretchBlt(
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
        /// Enumerate display device info for the given device name.
        /// </summary>
        /// <param name="deviceName">The device to enumerate or null for all devices.</param>
        public static IEnumerable<DisplayDevice> EnumerateDisplayDevices(string deviceName)
        {
            uint index = 0;
            DisplayDevice device = DisplayDevice.Create();

            while (Imports.EnumDisplayDevicesW(deviceName, index, ref device, 0))
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

            while (Imports.EnumDisplaySettingsW(deviceName, modeIndex, ref mode))
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
        /// Selects the given object into the specified device context.
        /// </summary>
        /// <returns>The previous object or null if failed OR null if the given object was a region.</returns>
        public static GdiObjectHandle SelectObject(this in DeviceContext context, GdiObjectHandle @object)
        {
            HGDIOBJ handle = Imports.SelectObject(context, @object);
            if (handle.IsInvalid)
                return default;

            ObjectType type = Imports.GetObjectType(@object);
            if (type == ObjectType.Region)
                return default;

            return new GdiObjectHandle(handle, ownsHandle: false);
        }

        public static bool UpdateWindow(this in WindowHandle window) => Imports.UpdateWindow(window);

        public unsafe static bool ValidateRectangle(this in WindowHandle window, ref Rectangle rectangle)
        {
            RECT rect = rectangle;
            return Imports.ValidateRect(window, &rect);
        }

        /// <summary>
        /// Validates the entire Window.
        /// </summary>
        public unsafe static bool Validate(this in WindowHandle window)
            => Imports.ValidateRect(window, null);

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(this in WindowHandle window)
        {
            return new DeviceContext(Imports.BeginPaint(window, out PAINTSTRUCT paintStruct), window, paintStruct);
        }

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(this in WindowHandle window, out PaintStruct paintStruct)
        {
            HDC hdc = Imports.BeginPaint(window, out PAINTSTRUCT ps);
            paintStruct = ps;
            return new DeviceContext(hdc, window, in ps);
        }

        public unsafe static bool InvalidateRectangle(this in WindowHandle window, Rectangle rectangle, bool erase)
        {
            RECT rect = rectangle;
            return Imports.InvalidateRect(window, &rect, erase);
        }

        public unsafe static bool Invalidate(this in WindowHandle window, bool erase = true)
        {
            return Imports.InvalidateRect(window, null, erase);
        }

        public static RegionHandle CreateEllipticRegion(int left, int top, int right, int bottom)
        {
            return new RegionHandle(Imports.CreateEllipticRgn(left, top, right, bottom));
        }

        public static RegionHandle CreateRectangleRegion(int left, int top, int right, int bottom)
        {
            return new RegionHandle(Imports.CreateRectRgn(left, top, right, bottom));
        }

        public static RegionType CombineRegion(this in RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode)
        {
            return Imports.CombineRgn(destination, sourceOne, sourceTwo, mode);
        }

        public static RegionType SelectClippingRegion(this in DeviceContext context, RegionHandle region)
        {
            return Imports.SelectClipRgn(context, region);
        }

        public static unsafe bool SetViewportOrigin(this in DeviceContext context, Point point)
        {
            return Imports.SetViewportOrgEx(context, point.X, point.Y, null);
        }

        public static unsafe bool SetWindowOrigin(this in DeviceContext context, Point point)
        {
            return Imports.SetWindowOrgEx(context, point.X, point.Y, null);
        }

        /// <summary>
        /// Shared brush, handle doesn't need disposed.
        /// </summary>
        public static BrushHandle GetSystemColorBrush(SystemColor systemColor)
        {
            return new BrushHandle(Imports.GetSysColorBrush(systemColor), ownsHandle: false);
        }

        public static Color GetBackgroundColor(this in DeviceContext context)
        {
            return Imports.GetBkColor(context);
        }

        public static Color GetBrushColor(this in DeviceContext context)
        {
            return Imports.GetDCBrushColor(context);
        }

        public static Color SetBackgroundColor(this in DeviceContext context, Color color) => Imports.SetBkColor(context, color);
        public static Color SetBackgroundColor(this in DeviceContext context, SystemColor color)
            => Imports.SetBkColor(context, Windows.Windows.GetSystemColor(color));


        public static BackgroundMode SetBackgroundMode(this in DeviceContext context, BackgroundMode mode)
        {
            return Imports.SetBkMode(context, mode);
        }

        public static BackgroundMode GetBackgroundMode(this in DeviceContext context)
        {
            return Imports.GetBkMode(context);
        }

        public static PenMixMode SetRasterOperation(this in DeviceContext context, PenMixMode foregroundMixMode)
        {
            return Imports.SetROP2(context, foregroundMixMode);
        }

        public static PenMixMode GetRasterOperation(this in DeviceContext context)
        {
            return Imports.GetROP2(context);
        }

        public static bool ScreenToClient(this in WindowHandle window, ref Point point)
        {
            return Imports.ScreenToClient(window, ref point);
        }

        public static bool ClientToScreen(this in WindowHandle window, ref Point point)
        {
            return Imports.ClientToScreen(window, ref point);
        }

        public static bool DeviceToLogical(this in DeviceContext context, params Point[] points)
        {
            return DeviceToLogical(context, points.AsSpan());
        }

        public static bool DeviceToLogical(this in DeviceContext context, ReadOnlySpan<Point> points)
        {
            return Imports.DPtoLP(context, ref MemoryMarshal.GetReference(points), points.Length);
        }

        public unsafe static bool LogicalToDevice(this in DeviceContext context, params Point[] points)
        {
            return LogicalToDevice(context, points);
        }

        public unsafe static bool LogicalToDevice(this in DeviceContext context, ReadOnlySpan<Point> points)
        {
            return Imports.LPtoDP(context, ref MemoryMarshal.GetReference(points), points.Length);
        }

        public unsafe static bool OffsetWindowOrigin(this in DeviceContext context, int x, int y)
        {
            return Imports.OffsetWindowOrgEx(context, x, y, null);
        }

        public unsafe static bool OffsetViewportOrigin(this in DeviceContext context, int x, int y)
        {
            return Imports.OffsetViewportOrgEx(context, x, y, null);
        }

        /// <summary>
        /// Sets the logical ("window") dimensions of the device context.
        /// </summary>
        public unsafe static bool SetWindowExtents(this in DeviceContext context, Size size)
            => Imports.SetWindowExtEx(context, size.Width, size.Height, null);

        public unsafe static bool SetViewportExtents(this in DeviceContext context, Size size)
            => Imports.SetViewportExtEx(context, size.Width, size.Height, null);

        public static MappingMode SetMappingMode(this in DeviceContext context, MappingMode mapMode)
            => Imports.SetMapMode(context, mapMode);

        public static Rectangle GetClipBox(this in DeviceContext context, out RegionType complexity)
        {
            complexity = Imports.GetClipBox(context, out RECT rect);
            return rect;
        }
    }
}
