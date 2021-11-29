// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices;

/// <summary>
///  Control code used with IO control. [CTL_CODE]
/// </summary>
public struct ControlCode
{
    // https://msdn.microsoft.com/en-us/library/ms902086.aspx
    // https://social.technet.microsoft.com/wiki/contents/articles/24653.decoding-io-control-codes-ioctl-fsctl-and-deviceiocodes-with-table-of-known-values.aspx

    private const uint TransferTypeMask = 0b0000_0000_0000_0000_0000_0000_0000_0011;
    private const uint FunctionCodeMask = 0b0000_0000_0000_0000_0001_1111_1111_1100;
    private const uint CustomMask = 0b0000_0000_0000_0000_0010_0000_0000_0000;
    private const uint RequiredAccessMask = 0b0000_0000_0000_0000_1100_0000_0000_0000;
    private const uint DeviceTypeMask = 0b0111_1111_1111_1111_0000_0000_0000_0000;
    private const uint CommonMask = 0b1000_0000_0000_0000_0000_0000_0000_0000;

    public uint Value;

    public ControlCode(uint value)
    {
        Value = value;
    }

    public ControlCode(ControlCodeDeviceType deviceType, uint function, ControlCodeMethod method, ControlCodeAccess access)
    {
        Value = ((uint)deviceType << 16) | ((uint)access << 14) | (function << 2) | (uint)method;
    }

    public ControlCodeDeviceType DeviceType => (ControlCodeDeviceType)((Value & DeviceTypeMask) >> 16);
    public ControlCodeAccess RequiredAccess => (ControlCodeAccess)((Value & RequiredAccessMask) >> 14);
    public ushort FunctionCode => (ushort)((Value & FunctionCodeMask) >> 2);
    public ControlCodeMethod TransferType => (ControlCodeMethod)(Value & TransferTypeMask);

    public static implicit operator ControlCode(ControlCodes.MountManager code) => new ControlCode((uint)code);
    public static implicit operator ControlCode(ControlCodes.MountDevice code) => new ControlCode((uint)code);
    public static implicit operator ControlCode(ControlCodes.FileSystem code) => new ControlCode((uint)code);

    public override string ToString() => $"{DeviceType}, Function Code 0x{FunctionCode:X4}, {RequiredAccess}, {TransferType}";
}