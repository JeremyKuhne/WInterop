// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                return null;

            return GdiObjectHandle.Create(handle, ownsHandle: false);
        }

        public static GdiObjectHandle GetStockObject(StockObject @object)
        {
            IntPtr handle = Imports.GetStockObject((int)@object);
            return GdiObjectHandle.Create(handle, ownsHandle: false);
        }

        public static BrushHandle GetStockBrush(StockBrush brush)
        {
            return Imports.GetStockObject((int)brush);
        }

        public static PenHandle GetStockPen(StockPen pen)
        {
            return Imports.GetStockObject((int)pen);
        }

        public static FontHandle GetStockFont(StockFont font)
        {
            return Imports.GetStockObject((int)font);
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

        public static unsafe bool TextOut(DeviceContext deviceContext, int x, int y, string text)
        {
            fixed (char* s = text)
                return Imports.TextOutW(deviceContext, x, y, s, text.Length);
        }

        public static unsafe bool TextOut(DeviceContext deviceContext, int x, int y, char* text, int length)
        {
            return Imports.TextOutW(deviceContext, x, y, text, length);
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

        public unsafe static bool Invalidate(WindowHandle window, bool erase = true)
        {
            return Imports.InvalidateRect(window, null, erase);
        }

        public unsafe static bool SetPixel(DeviceContext deviceContext, int x, int y, COLORREF color)
        {
            return Imports.SetPixelV(deviceContext, x, y, color);
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

        public static RegionHandle CreateEllipticRegion(int left, int top, int right, int bottom)
        {
            return Imports.CreateEllipticRgn(left, top, right, bottom);
        }

        public static RegionHandle CreateRectangleRegion(int left, int top, int right, int bottom)
        {
            return Imports.CreateRectRgn(left, top, right, bottom);
        }

        public static RegionType CombineRegion(RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode)
        {
            return Imports.CombineRgn(destination, sourceOne, sourceTwo, mode);
        }

        public static RegionType SelectClippingRegion(DeviceContext deviceContext, RegionHandle region)
        {
            return Imports.SelectClipRgn(deviceContext, region);
        }

        public static unsafe void SetViewportOrigin(DeviceContext deviceContext, int x, int y)
        {
            Imports.SetViewportOrgEx(deviceContext, x, y, null);
        }

        public static BackgroundMode SetBackgroundMode(DeviceContext deviceContext, BackgroundMode mode)
        {
            return Imports.SetBkMode(deviceContext, mode);
        }

        public static BackgroundMode GetBackgroundMode(DeviceContext deviceContext)
        {
            return Imports.GetBkMode(deviceContext);
        }

        public static FontHandle CreateFont(
            int height,
            int width,
            int escapement,
            int orientation,
            FontWeight weight,
            bool italic,
            bool underline,
            bool strikeout,
            CharacterSet characterSet,
            OutputPrecision outputPrecision,
            ClippingPrecision clippingPrecision,
            Quality quality,
            PitchAndFamily pitchAndFamily,
            string typeface)
        {
            return Imports.CreateFontW(height, width, escapement, orientation, weight, italic, underline, strikeout, (uint)characterSet,
                (uint)outputPrecision, (uint)clippingPrecision, (uint)quality, (uint)pitchAndFamily, typeface);
        }

        private static int EnumerateFontCallback(
            ref ENUMLOGFONTEXDV fontAttributes,
            ref NEWTEXTMETRICEX textMetrics,
            FontTypes fontType,
            LPARAM lParam)
        {
            var info = (List<FontInformation>)GCHandle.FromIntPtr(lParam).Target;
            info.Add(new FontInformation { FontType = fontType, TextMetrics = textMetrics, FontAttributes = fontAttributes });
            return 1;
        }

        public static IEnumerable<FontInformation> EnumerateFontFamilies(DeviceContext deviceContext, CharacterSet characterSet, string faceName)
        {
            LOGFONT logFont = new LOGFONT
            {
                lfCharSet = characterSet,
                FaceName = faceName
            };

            List<FontInformation> info = new List<FontInformation>();
            GCHandle gch = GCHandle.Alloc(info, GCHandleType.Normal);
            try
            {
                int result = Imports.EnumFontFamiliesExW(deviceContext, ref logFont, EnumerateFontCallback, GCHandle.ToIntPtr(gch), 0);
            }
            finally
            {
                gch.Free();
            }

            return info;
        }

        public static bool ScreenToClient(WindowHandle window, ref POINT point)
        {
            return Imports.ScreenToClient(window, ref point);
        }

        public static bool ClientToScreen(WindowHandle window, ref POINT point)
        {
            return Imports.ClientToScreen(window, ref point);
        }

        public unsafe static bool DeviceToLogical(DeviceContext deviceContext, params POINT[] points)
        {
            fixed (POINT* p = points)
                return Imports.DPtoLP(deviceContext, p, points.Length);
        }

        public unsafe static bool LogicalToDevice(DeviceContext deviceContext, params POINT[] points)
        {
            fixed (POINT* p = points)
                return Imports.LPtoDP(deviceContext, p, points.Length);
        }
    }
}
