// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows
{
    public struct WParam
    {
        public UIntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);

        public WParam(UIntPtr value) => RawValue = value;
        public WParam(ushort high, ushort low) => RawValue = (UIntPtr)Conversion.HighLowToInt(high, low);

        public static implicit operator WParam(UIntPtr value) => new WParam(value);
        public static implicit operator UIntPtr(WParam value) => value.RawValue;
        public static implicit operator uint(WParam value) => (uint)value.RawValue.ToUInt64();
        public static implicit operator WParam(uint value) => new WParam((UIntPtr)value);

        // We make these explicit as we want to encourage keeping signed/unsigned alignment
        public unsafe static explicit operator WParam(IntPtr value) => new WParam(new UIntPtr(value.ToPointer()));
        public unsafe static explicit operator IntPtr(WParam value) => new IntPtr(value.RawValue.ToPointer());
        public static explicit operator int(WParam value) => (int)(uint)value;
        public static explicit operator WParam(int value) => new WParam((UIntPtr)value);
        public static explicit operator char(WParam value) => (char)(uint)value;
        public static explicit operator WParam(char value) => new WParam((UIntPtr)value);

        public unsafe static implicit operator void* (WParam value) => value.RawValue.ToPointer();
        public unsafe static implicit operator WParam(void* value) => new WParam((UIntPtr)value);

        public static explicit operator VirtualKey(WParam value) => (VirtualKey)value.RawValue.ToUInt32();
        public static explicit operator WParam(VirtualKey value) => (uint)value;
        public static explicit operator MouseKey(WParam value) => (MouseKey)value.RawValue.ToUInt32();
        public static explicit operator WParam(MouseKey value) => (uint)value;

        public override string ToString()
        {
            return RawValue.ToString();
        }
    }
}
