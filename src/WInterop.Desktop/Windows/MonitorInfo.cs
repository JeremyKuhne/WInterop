// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Gdi.Unsafe;

namespace WInterop.Windows
{
    /// <summary>
    /// Monitor information. [MONITORINFO]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MonitorInfo
    {
        private const uint MONITORINFOF_PRIMARY = 0x00000001;

        private readonly uint cbSize;
        private readonly RECT rcMonitor;
        private readonly RECT rcWork;
        private readonly uint dwFlags;

        public unsafe static MonitorInfo Create() => new MonitorInfo(sizeof(MonitorInfo));

        private MonitorInfo(int size)
        {
            this = default;
            cbSize = (uint)size;
        }

        /// <summary>
        /// The monitor rectangle.
        /// </summary>
        public Rectangle Monitor => rcMonitor;

        /// <summary>
        /// The work area rectangle.
        /// </summary>
        public Rectangle Work => rcWork;

        public bool IsPrimary => (dwFlags & MONITORINFOF_PRIMARY) != 0;
    }
}
