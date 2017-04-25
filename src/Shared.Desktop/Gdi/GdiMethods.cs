// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using WInterop.Gdi.Types;
using WInterop.Support.Buffers;
using WInterop.Windows.Types;

namespace WInterop.Gdi
{
    public static partial class GdiMethods
    {
        public static int GetDeviceCapability(DeviceContext deviceContext, DeviceCapability capability)
        {
            return Imports.GetDeviceCaps(deviceContext, capability);
        }

        public unsafe static DeviceContext CreateDeviceContext(string driver, string device)
        {
            return Imports.CreateDCW(driver, device, null, null);
        }

        /// <summary>
        /// Get the device context for the client area of the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the entire screen.</param>
        public static DeviceContext GetDeviceContext(WindowHandle window)
        {
            return new WindowDeviceContext(window, Imports.GetDC(window));
        }

        /// <summary>
        /// Get the device context for the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the primary display monitor.</param>
        /// <returns>Returns a device context for the entire window, not just the client area.</returns>
        public static DeviceContext GetWindowDeviceContext(WindowHandle window)
        {
            return new WindowDeviceContext(window, Imports.GetWindowDC(window));
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
        public static GdiObjectHandle SelectObject(DeviceContext deviceContext, GdiObjectHandle @object)
        {
            IntPtr handle = Imports.SelectObject(deviceContext, @object);
            if (handle == IntPtr.Zero)
                return null;

            ObjectType type = Imports.GetObjectType(@object.DangerousGetHandle());
            if (type == ObjectType.OBJ_REGION)
            {
                return null;
            }

            return GdiObjectHandle.Create(handle, ownsHandle: false);
        }

        public static GdiObjectHandle GetStockObject(StockObject @object)
        {
            IntPtr handle = Imports.GetStockObject((int)@object);
            return GdiObjectHandle.Create(handle, ownsHandle: false);
        }

        public static BrushHandle GetStockBrush(StockBrush brush)
        {
            IntPtr handle = Imports.GetStockObject((int)brush);
            return new BrushHandle(handle, ownsHandle: false);
        }

        public static PenHandle GetStockPen(StockPen pen)
        {
            IntPtr handle = Imports.GetStockObject((int)pen);
            return new PenHandle(handle, ownsHandle: false);
        }

        public static BrushHandle CreateSolidBrush(COLORREF color)
        {
            return Imports.CreateSolidBrush(color);
        }

        public static BrushHandle CreateSolidBrush(byte red, byte green, byte blue)
        {
            return Imports.CreateSolidBrush(new COLORREF(red, green, blue));
        }

        public static bool UpdateWindow(WindowHandle window)
        {
            return Imports.UpdateWindow(window);
        }

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(WindowHandle window)
        {
            IntPtr handle = Imports.BeginPaint(window, out PAINTSTRUCT paintStruct);
            return new PaintDeviceContext(window, paintStruct, handle);
        }

        /// <summary>
        /// Calls BeginPaint and returns the created DeviceContext. Disposing the returned DeviceContext will call EndPaint.
        /// </summary>
        public static DeviceContext BeginPaint(WindowHandle window, out PAINTSTRUCT paintStruct)
        {
            IntPtr handle = Imports.BeginPaint(window, out paintStruct);
            return new PaintDeviceContext(window, paintStruct, handle);
        }

        public static bool TextOut(DeviceContext deviceContext, int x, int y, string text)
        {
            return Imports.TextOutW(deviceContext, x, y, text, text.Length);
        }

        public static unsafe int DrawText(DeviceContext deviceContext, string text, RECT rect, TextFormat format)
        {
            if ((format & TextFormat.DT_MODIFYSTRING) == 0)
            {
                // The string won't be changed, we can just pin
                fixed (char* c = text)
                {
                    return Imports.DrawTextW(deviceContext, c, text.Length, ref rect, format);
                }
            }

            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity((uint)(text.Length + 5));
                buffer.Append(text);
                return Imports.DrawTextW(deviceContext, buffer.CharPointer, (int)buffer.Length, ref rect, format);
            });
        }

        public static TextAlignment SetTextAlignment(DeviceContext deviceContext, TextAlignment alignment)
        {
            return Imports.SetTextAlign(deviceContext, alignment);
        }

        public static bool GetTextMetrics(DeviceContext deviceContext, out TEXTMETRIC metrics)
        {
            return Imports.GetTextMetricsW(deviceContext, out metrics);
        }

        public unsafe static bool InvalidateRectangle(WindowHandle window, RECT rect, bool erase)
        {
            return Imports.InvalidateRect(window, &rect, erase);
        }

        public unsafe static bool InvalidateRectangle(WindowHandle window, bool erase = true)
        {
            return Imports.InvalidateRect(window, null, erase);
        }

        public unsafe static bool MoveTo(DeviceContext deviceContext, int x, int y)
        {
            return Imports.MoveToEx(deviceContext, x, y, null);
        }

        public static bool LineTo(DeviceContext deviceContext, int x, int y)
        {
            return Imports.LineTo(deviceContext, x, y);
        }

        public static PolyFillMode GetPolyFillMode(DeviceContext deviceContext)
        {
            return Imports.GetPolyFillMode(deviceContext);
        }

        public static PolyFillMode SetPolyFillMode(DeviceContext deviceContext, PolyFillMode fillMode)
        {
            return Imports.SetPolyFillMode(deviceContext, fillMode);
        }

        public unsafe static bool Polygon(DeviceContext deviceContext, params POINT[] points)
        {
            fixed (POINT* p = points)
            {
                return Imports.Polygon(deviceContext, p, points.Length);
            }
        }

        public static bool Polyline(DeviceContext deviceContext, params POINT[] points)
        {
            return Imports.Polyline(deviceContext, points, points.Length);
        }

        public static bool Rectangle(DeviceContext deviceContext, int left, int top, int right, int bottom)
        {
            return Imports.Rectangle(deviceContext, left, top, right, bottom);
        }

        public static bool Rectangle(DeviceContext deviceContext, RECT rectangle)
        {
            return Imports.Rectangle(deviceContext, rectangle.left, rectangle.top, rectangle.right, rectangle.bottom);
        }

        public static bool Ellipse(DeviceContext deviceContext, int left, int top, int right, int bottom)
        {
            return Imports.Ellipse(deviceContext, left, top, right, bottom);
        }

        public static bool RoundRectangle(DeviceContext deviceContext, RECT rectangle, int cornerWidth, int cornerHeight)
        {
            return Imports.RoundRect(deviceContext, rectangle.left, rectangle.top, rectangle.right, rectangle.bottom, cornerWidth, cornerHeight);
        }

        public static bool RoundRectangle(DeviceContext deviceContext, int left, int top, int right, int bottom, int cornerWidth, int cornerHeight)
        {
            return Imports.RoundRect(deviceContext, left, top, right, bottom, cornerWidth, cornerHeight);
        }

        public static bool FillRectangle(DeviceContext deviceContext, RECT rectangle, BrushHandle brush)
        {
            return Imports.FillRect(deviceContext, ref rectangle, brush);
        }

        public unsafe static bool PolyBezier(DeviceContext deviceContext, params POINT[] points)
        {
            fixed (POINT* p = points)
            {
                return Imports.PolyBezier(deviceContext, p, (uint)points.Length);
            }
        }
    }
}
