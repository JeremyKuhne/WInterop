﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Devices;
using WInterop.Synchronization;

namespace WInterop.Devices
{
    public static partial class Devices
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363216.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool DeviceIoControl(
                SafeHandle hDevice,
                ControlCode dwIoControlCode,
                void* lpInBuffer,
                uint nInBufferSize,
                void* lpOutBuffer,
                uint nOutBufferSize,
                out uint lpBytesReturned,
                OVERLAPPED* lpOverlapped);
        }
    }
}
