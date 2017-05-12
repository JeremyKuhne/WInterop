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

        public WPARAM(UIntPtr value) => RawValue = value;
        public WPARAM(ushort high, ushort low) => RawValue = (UIntPtr)Conversion.HighLowToInt(high, low);

        public static implicit operator WPARAM(UIntPtr value) => new WPARAM(value);
        public static implicit operator UIntPtr(WPARAM value) => value.RawValue;

        // We make these explicit as we want to encourage keeping signed/unsigned alignment
        unsafe public static explicit operator WPARAM(IntPtr value) => new WPARAM(new UIntPtr(value.ToPointer()));
        unsafe public static explicit operator IntPtr(WPARAM value) => new IntPtr(value.RawValue.ToPointer());

        public static implicit operator uint(WPARAM value) => (uint)value.RawValue.ToUInt64();
        public static implicit operator WPARAM(uint value) => new WPARAM((UIntPtr)value);

        public static explicit operator VirtualKey(WPARAM value) => (VirtualKey)value.RawValue.ToUInt32();
        public static explicit operator WPARAM(VirtualKey value) => (uint)value;
        public static explicit operator MouseKey(WPARAM value) => (MouseKey)value.RawValue.ToUInt32();
        public static explicit operator WPARAM(MouseKey value) => (uint)value;

        public override string ToString()
        {
            return RawValue.ToString();
        }
    }
}
