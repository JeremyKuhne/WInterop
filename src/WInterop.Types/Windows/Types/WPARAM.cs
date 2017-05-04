// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows.Types
{
    public struct WPARAM
    {
        public UIntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);

        public WPARAM(UIntPtr value)
        {
            RawValue = value;
        }

        public WPARAM(ushort high, ushort low)
        {
            RawValue = (UIntPtr)Conversion.HighLowToInt(high, low);
        }

        public static implicit operator uint(WPARAM value)
        {
            return (uint)value.RawValue.ToUInt64();
        }

        public static implicit operator WPARAM(uint value)
        {
            return new WPARAM((UIntPtr)value);
        }

        public static explicit operator VirtualKey(WPARAM value)
        {
            return (VirtualKey)value.RawValue.ToUInt32();
        }

        public static explicit operator WPARAM(VirtualKey value)
        {
            return (uint)value;
        }

        public static explicit operator MouseKey(WPARAM value)
        {
            return (MouseKey)value.RawValue.ToUInt32();
        }

        public static explicit operator WPARAM(MouseKey value)
        {
            return (uint)value;
        }

        public override string ToString()
        {
            return RawValue.ToString();
        }
    }
}
