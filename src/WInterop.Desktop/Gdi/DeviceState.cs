// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

[Flags]
public enum DeviceState : uint
{
    Active = 0x00000001,
    Attached = 0x00000002,
    PrimaryDevice = 0x00000004,
    MirroringDriver = 0x00000008,
    VgaCompatible = 0x00000010,
    Removable = 0x00000020,
    AccDriver = 0x00000040,
    UnsafeModesOn = 0x00080000,
    TsCompatible = 0x00200000,
    Disconnect = 0x02000000,
    Remote = 0x04000000,
    ModesPruned = 0x08000000,
}