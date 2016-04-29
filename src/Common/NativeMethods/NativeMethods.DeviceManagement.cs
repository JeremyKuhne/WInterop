// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.DeviceManagement;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static partial class DeviceManagement
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363216.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DeviceIoControl(
                    IntPtr hDevice,
                    uint dwIoControlCode,
                    IntPtr lpInBuffer,
                    uint nInBufferSize,
                    IntPtr lpOutBuffer,
                    uint nOutBufferSize,
                    out uint lpBytesReturned,
                    IntPtr lpOverlapped);
            }

            // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH

            // https://msdn.microsoft.com/en-us/library/ms902086.aspx
            public static uint CTL_CODE(ControlCodeDeviceType deviceType, uint function, ControlCodeMethod method, ControlCodeAccess access)
            {
                return ((uint)deviceType << 16) | ((uint)access << 14) | (function << 2) | (uint)method;
            }
        }
    }
}
