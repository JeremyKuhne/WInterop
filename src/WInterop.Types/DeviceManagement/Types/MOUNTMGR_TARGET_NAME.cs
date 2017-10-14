// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DeviceManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562289.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_TARGET_NAME
    {
        public ushort DeviceNameLength;
        private TrailingString _DeviceName;
        public ReadOnlySpan<char> DeviceName => _DeviceName.GetBuffer(DeviceNameLength);
    }
}
