// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Devices.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562288.aspx
    public unsafe struct MOUNTMGR_MOUNT_POINTS
    {
        public uint Size;
        public uint NumberOfMountPoints;
        private MOUNTMGR_MOUNT_POINT _MountPoints;

        public unsafe ReadOnlySpan<MOUNTMGR_MOUNT_POINT> MountPoints
        {
            get
            {
                if (NumberOfMountPoints == 0)
                    return new ReadOnlySpan<MOUNTMGR_MOUNT_POINT>();

                fixed (MOUNTMGR_MOUNT_POINT* m = &_MountPoints)
                    return new ReadOnlySpan<MOUNTMGR_MOUNT_POINT>(m, (int)NumberOfMountPoints);
            }
        }
    }
}
