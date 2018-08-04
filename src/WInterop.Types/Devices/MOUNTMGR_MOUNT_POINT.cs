// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562286.aspx
    public struct MOUNTMGR_MOUNT_POINT
    {
        public uint SymbolicLinkNameOffset;
        public ushort SymbolicLinkNameLength;
        public uint UniqueIdOffset;
        public ushort UniqueIdLength;
        public uint DeviceNameOffset;
        public ushort DeviceNameLength;
    }
}
