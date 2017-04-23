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
            public static extern DeviceContext GetWindowDC(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd144871.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern DeviceContext GetDC(
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

            // https://msdn.microsoft.com/en-us/library/dd183533.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern GdiObject GetStockObject(
                StockObject stockObject);

            // https://msdn.microsoft.com/en-us/library/dd183518.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BrushHandle CreateSolidBrush(
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd162957.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern IntPtr SelectObject(
                DeviceContext hdc,
                GdiObject hgdiobj);

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
            public static extern DeviceContext BeginPaint(
                WindowHandle hwnd,
                out PAINTSTRUCT lpPaint);

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
            return Imports.GetDC(window);
        }

        /// <summary>
        /// Get the device context for the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the primary display monitor.</param>
        /// <returns>Returns a device context for the entire window, not just the client area.</returns>
        public static DeviceContext GetWindowDeviceContext(WindowHandle window)
        {
            return Imports.GetWindowDC(window);
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

        public static BrushHandle GetStockBrush(StockBrush brush)
        {
            IntPtr handle = Imports.GetStockObject((int)brush);
            return new BrushHandle(handle, ownsHandle: false);
        }

        public static bool UpdateWindow(WindowHandle window)
        {
            return Imports.UpdateWindow(window);
        }

        public static DeviceContext BeginPaint(WindowHandle window, out PAINTSTRUCT paintStruct)
        {
            return Imports.BeginPaint(window, out paintStruct);
        }

        public static void EndPaint(WindowHandle window, ref PAINTSTRUCT paintStruct)
        {
            Imports.EndPaint(window, ref paintStruct);
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
    }
}
