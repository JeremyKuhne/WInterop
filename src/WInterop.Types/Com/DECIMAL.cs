// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms221061.aspx
    // [MS-OAUT] https://msdn.microsoft.com/en-us/library/cc237603.aspx
    public struct DECIMAL
    {
        private const byte NegativeSignFlag = 0x80;
        private const int ManagedSignMask = unchecked((int)0x80000000);
        private const int ManagedScaleMask = 0x00FF0000;
        private const int ManagedScaleShift = 16;

        public ushort wReserved;
        public byte scale;
        public byte sign;
        public uint Hi32;
        public uint Lo32;
        public uint Mid32;

        public decimal ToDecimal()
        {
            return new decimal(
                lo: (int)Lo32,
                mid: (int)Mid32,
                hi: (int)Hi32,
                isNegative: sign == NegativeSignFlag,
                scale: scale);
        }

        public DECIMAL(decimal value)
        {
            wReserved = 0;
            int[] data = decimal.GetBits(value);
            Lo32 = (uint)data[0];
            Mid32 = (uint)data[1];
            Hi32 = (uint)data[2];
            scale = (byte)((data[3] & ManagedScaleMask) >> 16);
            sign = (data[3] & ManagedSignMask) == 0 ? (byte)0 : NegativeSignFlag;
        }
    }
}
