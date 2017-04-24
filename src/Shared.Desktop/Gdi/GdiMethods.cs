// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;
using WInterop.Support.Buffers;
using WInterop.Windows.Types;

namespace WInterop.Gdi
{
    public static partial class GdiMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/dd144877.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetDeviceCaps(
                DeviceContext hdc,
                DeviceCapability nIndex);

            // https://msdn.microsoft.com/en-us/library/dd144947.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetWindowDC(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd144871.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetDC(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd183490.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern DeviceContext CreateDC(
                string lpszDriver,
                string lpszDevice,
                string lpszOutput,
                DEVMODE* lpInitData);

            // https://msdn.microsoft.com/en-us/library/dd183533.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool DeleteDC(
                IntPtr hdc);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162920.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ReleaseDC(
                WindowHandle hWnd,
                IntPtr hdc);

            // https://msdn.microsoft.com/en-us/library/dd183533.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern GdiObjectHandle GetStockObject(
                StockObject stockObject);

            // https://msdn.microsoft.com/en-us/library/dd183518.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BrushHandle CreateSolidBrush(
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd162957.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern IntPtr SelectObject(
                DeviceContext hdc,
                GdiObjectHandle hgdiobj);

            // https://msdn.microsoft.com/en-us/library/dd183539.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool DeleteObject(
                IntPtr hObject);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162609.aspx
            [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
            public static extern bool EnumDisplayDevicesW(
                string lpDevice,
                uint iDevNum,
                ref DISPLAY_DEVICE lpDisplayDevice,
                uint dwFlags);

            // https://msdn.microsoft.com/en-us/library/dd162611.aspx
            [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
            public static extern bool EnumDisplaySettingsW(
                string lpszDeviceName,
                uint iModeNum,
                ref DEVMODE lpDevMode);

            // https://msdn.microsoft.com/en-us/library/dd183485.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BitmapHandle CreateBitmap(
                int nWidth,
                int nHeight,
                uint cPlanes,
                uint cBitsPerPel,
                IntPtr lpvBits);

            // https://msdn.microsoft.com/en-us/library/dd144905.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern ObjectType GetObjectType(
                IntPtr h);

            // https://msdn.microsoft.com/en-us/library/dd144925.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern IntPtr GetStockObject(
                int fnObject);

            // https://msdn.microsoft.com/en-us/library/dd145167.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool UpdateWindow(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd183362.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr BeginPaint(
                WindowHandle hwnd,
                out PAINTSTRUCT lpPaint);

            // https://msdn.microsoft.com/en-us/library/dd145002.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public unsafe static extern bool InvalidateRect(
                WindowHandle hWnd,
                RECT* lpRect,
                bool bErase);

            // https://msdn.microsoft.com/en-us/library/dd162598.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool EndPaint(
                WindowHandle hwnd,
                [In] ref PAINTSTRUCT lpPaint);

            // https://msdn.microsoft.com/en-us/library/dd162498.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static unsafe extern int DrawTextW(
                DeviceContext hDC,
                char* lpchText,
                int nCount,
                ref RECT lpRect,
                TextFormat uFormat);

            // https://msdn.microsoft.com/en-us/library/dd145133.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool TextOutW(
                DeviceContext hdc,
                int nXStart,
                int nYStart,
                string lpString,
                int cchString);

            // https://msdn.microsoft.com/en-us/library/dd144941.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool GetTextMetricsW(
                DeviceContext hdc,
                out TEXTMETRIC lptm);

            // https://msdn.microsoft.com/en-us/library/dd145091.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern TextAlignment SetTextAlign(
                DeviceContext hdc,
                TextAlignment fMode);

            // https://msdn.microsoft.com/en-us/library/dd144932.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern TextAlignment GetTextAlign(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd183354.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool AngleArc(
                DeviceContext hdc,
                int X,
                int Y,
                uint dwRadius,
                float eStartAngle,
                float eSweepAngle);

            // https://msdn.microsoft.com/en-us/library/dd183357.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Arc(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nXStartArc,
                int nYStartArc,
                int nXEndArc,
                int nYEndArc);

            // https://msdn.microsoft.com/en-us/library/dd183358.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool ArcTo(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nXRadial1,
                int nYRadial1,
                int nXRadial2,
                int nYRadial2);

            // https://msdn.microsoft.com/en-us/library/dd144848.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern ArcDirection GetArcDirection(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd145029.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool LineTo(
                DeviceContext hdc,
                int nXEnd,
                int nYEnd);

            // https://msdn.microsoft.com/en-us/library/dd145069.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool MoveToEx(
                DeviceContext hdc,
                int X,
                int Y,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd144910.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PolyFillMode GetPolyFillMode(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd145080.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PolyFillMode SetPolyFillMode(
                DeviceContext hdc,
                PolyFillMode iPolyFillMode);

            // https://msdn.microsoft.com/en-us/library/dd162811.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolyBezier(
                DeviceContext hdc,
                POINT[] lppt,
                uint cPoints);

            // https://msdn.microsoft.com/en-us/library/dd162812.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolyBezierTo(
                DeviceContext hdc,
                POINT[] lppt,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162813.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolyDraw(
                DeviceContext hdc,
                POINT[] lppt,
                PointType[] lpbTypes,
                int cCount);

            // https://msdn.microsoft.com/en-us/library/dd162815.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Polyline(
                DeviceContext hdc,
                POINT[] lppt,
                int cPoints);

            // https://msdn.microsoft.com/en-us/library/dd162816.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolylineTo(
                DeviceContext hdc,
                POINT[] lppt,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162819.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolyPolyline(
                DeviceContext hdc,
                POINT[] lppt,
                uint[] lpdwPolyPoints,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162961.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern ArcDirection SetArcDirection(
                DeviceContext hdc,
                ArcDirection ArcDirection);

            // https://msdn.microsoft.com/en-us/library/dd183428.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Chord(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nXRadial1,
                int nYRadial1,
                int nXRadial2,
                int nYRadial2);

            // https://msdn.microsoft.com/en-us/library/dd162510(v=vs.85).aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Ellipse(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162719.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool FillRect(
                DeviceContext hDC,
                [In] ref RECT lprc,
                BrushHandle hbr);

            // https://msdn.microsoft.com/en-us/library/dd144838.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool FrameRect(
                DeviceContext hDC,
                [In] ref RECT lprc,
                BrushHandle hbr);

            // https://msdn.microsoft.com/en-us/library/dd145007.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool InvertRect(
                DeviceContext hDC,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd162799.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Pie(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nXRadial1,
                int nYRadial1,
                int nXRadial2,
                int nYRadial2);

            // https://msdn.microsoft.com/en-us/library/dd162814.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Polygon(
                DeviceContext hdc,
                POINT[] lpPoints,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd162818.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PolyPolygon(
                DeviceContext hdc,
                POINT[] lpPoints,
                int[] lpPolyCounts,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd162898.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Rectangle(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162944.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool RoundRect(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidth,
                int nHeight);
        }

        public static int GetDeviceCapability(DeviceContext deviceContext, DeviceCapability capability)
        {
            return Imports.GetDeviceCaps(deviceContext, capability);
        }

        public unsafe static DeviceContext CreateDeviceContext(string driver, string device)
        {
            return Imports.CreateDC(driver, device, null, null);
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

        public unsafe static bool InvalidateRectangle(WindowHandle window, bool erase)
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

        public static bool Polygon(DeviceContext deviceContext, params POINT[] points)
        {
            return Imports.Polygon(deviceContext, points, points.Length);
        }

        public static bool Polyline(DeviceContext deviceContext, params POINT[] points)
        {
            return Imports.Polyline(deviceContext, points, points.Length);
        }

        public static bool Rectangle(DeviceContext deviceContext, int left, int top, int right, int bottom)
        {
            return Imports.Rectangle(deviceContext, left, top, right, bottom);
        }

        public static bool Ellipse(DeviceContext deviceContext, int left, int top, int right, int bottom)
        {
            return Imports.Ellipse(deviceContext, left, top, right, bottom);
        }

        public static bool RoundRectangle(DeviceContext deviceContext, int left, int top, int right, int bottom, int cornerWidth, int cornerHeight)
        {
            return Imports.RoundRect(deviceContext, left, top, right, bottom, cornerWidth, cornerHeight);
        }

        public static bool PolyBezier(DeviceContext deviceContext, params POINT[] points)
        {
            return Imports.PolyBezier(deviceContext, points, (uint)points.Length);
        }
    }
}
