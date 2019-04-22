// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Devices.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562285.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_DRIVE_LETTER_TARGET
    {
        public ushort DeviceNameLength;
        private char _DeviceName;
        public ReadOnlySpan<char> DeviceName => TrailingArray<char>.GetBufferInBytes(ref _DeviceName, DeviceNameLength);
    }
}
