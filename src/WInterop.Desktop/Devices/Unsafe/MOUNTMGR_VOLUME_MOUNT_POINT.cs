// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562295.aspx
    public readonly struct MOUNTMGR_VOLUME_MOUNT_POINT
    {
        public readonly ushort SourceVolumeNameOffset;
        public readonly ushort SourceVolumeNameLength;
        public readonly ushort TargetVolumeNameOffset;
        public readonly ushort TargetVolumeNameLength;
    }
}
