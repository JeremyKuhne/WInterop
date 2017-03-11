// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.DataTypes
{
    [Flags]
    public enum DeviceState : uint
    {
        DISPLAY_DEVICE_ACTIVE = 0x00000001,
        DISPLAY_DEVICE_ATTACHED = 0x00000002,
        DISPLAY_DEVICE_PRIMARY_DEVICE = 0x00000004,
        DISPLAY_DEVICE_MIRRORING_DRIVER = 0x00000008,
        DISPLAY_DEVICE_VGA_COMPATIBLE = 0x00000010,
        DISPLAY_DEVICE_REMOVABLE = 0x00000020,
        DISPLAY_DEVICE_ACC_DRIVER = 0x00000040,
        DISPLAY_DEVICE_UNSAFE_MODES_ON = 0x00080000,
        DISPLAY_DEVICE_TS_COMPATIBLE = 0x00200000,
        DISPLAY_DEVICE_DISCONNECT = 0x02000000,
        DISPLAY_DEVICE_REMOTE = 0x04000000,
        DISPLAY_DEVICE_MODESPRUNED = 0x08000000,
    }
}
