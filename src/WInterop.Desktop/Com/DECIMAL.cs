// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms221061.aspx
// [MS-OAUT] https://docs.microsoft.com/openspecs/windows_protocols/ms-oaut/b5493025-e447-4109-93a8-ac29c48d018d
public struct DECIMAL
{
    private const byte NegativeSignFlag = 0x80;
    private const int ManagedSignMask = unchecked((int)0x80000000);
    private const int ManagedScaleMask = 0x00FF0000;
    private const int ManagedScaleShift = 16;

    public ushort Reserved;
    public byte Scale;
    public byte Sign;
    public uint Hi32;
    public uint Lo32;
    public uint Mid32;

    public decimal ToDecimal()
    {
        return new decimal(
            lo: (int)Lo32,
            mid: (int)Mid32,
            hi: (int)Hi32,
            isNegative: Sign == NegativeSignFlag,
            scale: Scale);
    }

    public DECIMAL(decimal value)
    {
        Reserved = 0;
        int[] data = decimal.GetBits(value);
        Lo32 = (uint)data[0];
        Mid32 = (uint)data[1];
        Hi32 = (uint)data[2];
        Scale = (byte)((data[3] & ManagedScaleMask) >> 16);
        Sign = (data[3] & ManagedSignMask) == 0 ? (byte)0 : NegativeSignFlag;
    }
}