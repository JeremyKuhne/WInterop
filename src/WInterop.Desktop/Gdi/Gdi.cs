// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;
using WInterop.Support;
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
        public static DeviceContext CreateCompatibleDeviceContext(in DeviceContext context)
            => new DeviceContext(Imports.CreateCompatibleDC(context), ownsHandle: true);

        public static int GetDeviceCapability(in DeviceContext context, DeviceCapability capability) => Imports.GetDeviceCaps(context, capability);

        public unsafe static DeviceContext CreateInformationContext(string driver, string device)
            => new DeviceContext(Imports.CreateICW(driver, device, null, null), ownsHandle: true);

        /// <summary>
        /// Get the device context for the client area of the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the entire screen.</param>
        public static DeviceContext GetDeviceContext(WindowHandle window)
        {
            return new DeviceContext(Imports.GetDC(window), window);
        }

        /// <summary>
        /// Get the device context for the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the primary display monitor.</param>
        /// <returns>Returns a device context for the entire window, not just the client area.</returns>
        public static DeviceContext GetWindowDeviceContext(WindowHandle window)
        {
            return new DeviceContext(Imports.GetWindowDC(window), window);
        }

        public static BitmapHandle CreateCompatibleBitmap(in DeviceContext context, Size size) => 
            new BitmapHandle(Imports.CreateCompatibleBitmap(context, size.Width, size.Height));

        public static void BitBlt(
            in DeviceContext source,
            in DeviceContext destination,
            Point sourceOrigin,
            Rectangle destinationRect,
            RasterOperation operation)
        {
            if (!Imports.BitBlt(
                destination,
                destinationRect.X,
                destinationRect.Y,
                destinationRect.Width,
                destinationRect.Height,
                source,
                sourceOrigin.X,
                sourceOrigin.Y,
                operation))
            {
                throw Errors.GetIoExceptionForLastError();
            }
        }


        /// <summary>
        /// Enumerate display device info for the given device name.
        /// </summary>
        /// <param name="deviceName">The device to enumerate or null for all devices.</param>
        public static IEnumerable<DISPLAY_DEVICE> EnumerateDisplayDevices(string deviceName)
        {
            uint index = 0;
            DISPLAY_DEVICE device = new DISPLAY_DEVICE()
            {
                cb = DISPLAY_DEVICE.s_size
            };

            while (Imports.EnumDisplayDevicesW(deviceName, index, ref device, 0))
            {
                yield return device;
                index++;
                device = new DISPLAY_DEVICE()
                {
                    cb = DISPLAY_DEVICE.s_size
                };
            }

            yield break;
        }

        public static IEnumerable<DEVMODE> EnumerateDisplaySettings(string deviceName, uint modeIndex = 0)
        {
            DEVMODE mode = new DEVMODE()
            {
                dmSize = DEVMODE.s_size
            };

            while (Imports.EnumDisplaySettingsW(deviceName, modeIndex, ref mode))
            {
                yield return mode;

                if (modeIndex == GdiDefines.ENUM_CURRENT_SETTINGS || modeIndex == GdiDefines.ENUM_REGISTRY_SETTINGS)
                    break;

                modeIndex++;
                mode = new DEVMODE()
                {
                    dmSize = DEVMODE.s_size
                };
            }

            yield break;
        }

        /// <summary>
        /// Selects the given object into the specified device context.
        /// </summary>
        /// <returns>The previous object or null if failed OR null if the given object was a region.</returns>
        public static GdiObjectHandle SelectObject(in DeviceContext context, GdiObjectHandle @object)
        {
            HGDIOBJ handle = Imports.SelectObject(context, @object);
            if (handle.IsInvalid)
                return default;

            ObjectType type = Imports.GetObjectType(@object);
            if (type == ObjectType.Region)
                return default;

            return new GdiObjectHandle(handle, ownsHandle: false);
        }


        public static bool UpdateWindow(WindowHandle window) => Imports.UpdateWindow(window);

        public static bool ValidateRectangle(WindowHandle window, ref Rectangle rectangle)
        {
            RECT rect = rectangle;
            return Imports.ValidateRect(window, ref rect);
        }

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(WindowHandle window)
        {
            return new DeviceContext(Imports.BeginPaint(window, out PAINTSTRUCT paintStruct), window, paintStruct);
        }

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(WindowHandle window, out PaintStruct paintStruct)
        {
            HDC hdc = Imports.BeginPaint(window, out PAINTSTRUCT ps);
            paintStruct = ps;
            return new DeviceContext(hdc, window, in ps);
        }


        public unsafe static bool InvalidateRectangle(WindowHandle window, Rectangle rectangle, bool erase)
        {
            RECT rect = rectangle;
            return Imports.InvalidateRect(window, &rect, erase);
        }

        public unsafe static bool Invalidate(WindowHandle window, bool erase = true)
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

        public static RegionType CombineRegion(RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode)
        {
            return Imports.CombineRgn(destination, sourceOne, sourceTwo, mode);
        }

        public static RegionType SelectClippingRegion(in DeviceContext context, RegionHandle region)
        {
            return Imports.SelectClipRgn(context, region);
        }

        public static unsafe bool SetViewportOrigin(in DeviceContext context, int x, int y)
        {
            return Imports.SetViewportOrgEx(context, x, y, null);
        }

        public static unsafe bool SetWindowOrigin(in DeviceContext context, int x, int y)
        {
            return Imports.SetWindowOrgEx(context, x, y, null);
        }

        /// <summary>
        /// Shared brush, handle doesn't need disposed.
        /// </summary>
        public static BrushHandle GetSystemColorBrush(SystemColor systemColor)
        {
            return new BrushHandle(Imports.GetSysColorBrush(systemColor), ownsHandle: false);
        }

        public static Color GetBackgroundColor(in DeviceContext context)
        {
            return Imports.GetBkColor(context);
        }

        public static Color GetBrushColor(in DeviceContext context)
        {
            return Imports.GetDCBrushColor(context);
        }

        public static Color SetBackgroundColor(in DeviceContext context, COLORREF color)
        {
            return Imports.SetBkColor(context, color);
        }

        public static BackgroundMode SetBackgroundMode(in DeviceContext context, BackgroundMode mode)
        {
            return Imports.SetBkMode(context, mode);
        }

        public static BackgroundMode GetBackgroundMode(in DeviceContext context)
        {
            return Imports.GetBkMode(context);
        }

        public static PenMixMode SetRasterOperation(in DeviceContext context, PenMixMode foregroundMixMode)
        {
            return Imports.SetROP2(context, foregroundMixMode);
        }

        public static PenMixMode GetRasterOperation(in DeviceContext context)
        {
            return Imports.GetROP2(context);
        }


        public static bool ScreenToClient(WindowHandle window, ref Point point)
        {
            return Imports.ScreenToClient(window, ref point);
        }

        public static bool ClientToScreen(WindowHandle window, ref Point point)
        {
            return Imports.ClientToScreen(window, ref point);
        }

        public static bool DeviceToLogical(in DeviceContext context, params Point[] points)
        {
            return DeviceToLogical(context, points.AsSpan());
        }

        public static bool DeviceToLogical(in DeviceContext context, ReadOnlySpan<Point> points)
        {
            return Imports.DPtoLP(context, ref MemoryMarshal.GetReference(points), points.Length);
        }

        public unsafe static bool LogicalToDevice(in DeviceContext context, params Point[] points)
        {
            return LogicalToDevice(context, points);
        }

        public unsafe static bool LogicalToDevice(in DeviceContext context, ReadOnlySpan<Point> points)
        {
            return Imports.LPtoDP(context, ref MemoryMarshal.GetReference(points), points.Length);
        }

        public unsafe static bool OffsetWindowOrigin(in DeviceContext context, int x, int y)
        {
            return Imports.OffsetWindowOrgEx(context, x, y, null);
        }

        public unsafe static bool OffsetViewportOrigin(in DeviceContext context, int x, int y)
        {
            return Imports.OffsetViewportOrgEx(context, x, y, null);
        }

        public unsafe static bool SetWindowExtents(in DeviceContext context, int x, int y)
        {
            return Imports.SetWindowExtEx(context, x, y, null);
        }

        public unsafe static bool SetViewportExtents(in DeviceContext context, int x, int y)
        {
            return Imports.SetViewportExtEx(context, x, y, null);
        }

        public static MapMode SetMapMode(in DeviceContext context, MapMode mapMode)
        {
            return Imports.SetMapMode(context, mapMode);
        }

        public static Rectangle GetClipBox(in DeviceContext context, out RegionType complexity)
        {
            complexity = Imports.GetClipBox(context, out RECT rect);
            return rect;
        }
    }
}
