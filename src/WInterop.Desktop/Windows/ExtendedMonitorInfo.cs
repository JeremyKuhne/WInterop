using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WInterop.Windows
{
    /// <summary>
    /// Extended monitor information. [MONITORINFOEX]
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct ExtendedMonitorInfo
    {
        private const int CCHDEVICENAME = 32;

        private MonitorInfo _info;
        private FixedString.Size32 _szDevice;

        public unsafe static ExtendedMonitorInfo Create() => new ExtendedMonitorInfo(sizeof(ExtendedMonitorInfo));

        private ExtendedMonitorInfo(int size)
        {
            _info = new MonitorInfo(size);
        }

        /// <summary>
        /// The monitor rectangle.
        /// </summary>
        public Rectangle Monitor => _info.Monitor;

        /// <summary>
        /// The work area rectangle.
        /// </summary>
        public Rectangle Work => _info.Work;

        public bool IsPrimary => _info.IsPrimary;

        public ReadOnlySpan<char> DeviceName => _szDevice.Buffer.SliceAtNull();
    }
}
