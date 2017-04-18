// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Gdi.DataTypes;
using WInterop.Windows.DataTypes;

namespace WInterop.Gdi
{
    public static partial class GdiDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
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
        }

        public static int GetDeviceCapability(DeviceContext deviceContext, DeviceCapability capability)
        {
            return Direct.GetDeviceCaps(deviceContext, capability);
        }

        public unsafe static DeviceContext CreateDeviceContext(string driver, string device)
        {
            return Direct.CreateDC(driver, device, null, null);
        }

        /// <summary>
        /// Get the device context for the client area of the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the entire screen.</param>
        public static DeviceContext GetDeviceContext(WindowHandle window)
        {
            return Direct.GetDC(window);
        }

        /// <summary>
        /// Get the device context for the specified window.
        /// </summary>
        /// <param name="window">The window handle, or null for the primary display monitor.</param>
        /// <returns>Returns a device context for the entire window, not just the client area.</returns>
        public static DeviceContext GetWindowDeviceContext(WindowHandle window)
        {
            return Direct.GetWindowDC(window);
        }

        /// <summary>
        /// Enumerate display device info for the given device name.
        /// </summary>
        /// <param name="deviceName">The device to enumerate or null for all devices.</param>
        public static IEnumerable<DISPLAY_DEVICE> EnumerateDisplayDevices(string deviceName)
        {
            uint index = 0;
            DISPLAY_DEVICE device = new DISPLAY_DEVICE();
            device.cb = DISPLAY_DEVICE.s_size;
            while (Direct.EnumDisplayDevicesW(deviceName, index, ref device, 0))
            {
                yield return device;
                index++;
                device = new DISPLAY_DEVICE();
                device.cb = DISPLAY_DEVICE.s_size;
            }

            yield break;
        }

        public static IEnumerable<DEVMODE> EnumerateDisplaySettings(string deviceName, uint modeIndex = 0)
        {
            DEVMODE mode = new DEVMODE();
            mode.dmSize = DEVMODE.s_size;

            while (Direct.EnumDisplaySettingsW(deviceName, modeIndex, ref mode))
            {
                yield return mode;

                if (modeIndex == Defines.ENUM_CURRENT_SETTINGS || modeIndex == Defines.ENUM_REGISTRY_SETTINGS)
                    break;

                modeIndex++;
                mode = new DEVMODE();
                mode.dmSize = DEVMODE.s_size;
            }

            yield break;
        }
    }
}
