// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562295.aspx
    public struct MOUNTMGR_VOLUME_MOUNT_POINT
    {
        public ushort SourceVolumeNameOffset;
        public ushort SourceVolumeNameLength;
        public ushort TargetVolumeNameOffset;
        public ushort TargetVolumeNameLength;
    }
}
