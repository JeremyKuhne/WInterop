// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows
{
    public struct LPARAM
    {
        public IntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);

        public LPARAM(IntPtr value) => RawValue = value;
        public LPARAM(short high, short low) => RawValue = (IntPtr)Conversion.HighLowToInt(high, low);
        public LPARAM(int high, int low) => RawValue = (IntPtr)Conversion.HighLowToInt(checked((short)high), checked((short)low));

        public static implicit operator int(LPARAM value) => (int)value.RawValue.ToInt64();
        public static explicit operator uint(LPARAM value) => (uint)value.RawValue.ToInt64();
        public static implicit operator LPARAM(int value) => new LPARAM((IntPtr)value);
        public static implicit operator LPARAM(IntPtr value) => new LPARAM(value);
        public static implicit operator IntPtr(LPARAM value) => value.RawValue;

        public unsafe static implicit operator void*(LPARAM value) => value.RawValue.ToPointer();
        public unsafe static implicit operator LPARAM (void* value) => new LPARAM((IntPtr)value);
        public static explicit operator WindowHandle(LPARAM value) => value.RawValue;
        public static explicit operator LPARAM(WindowHandle value) => new LPARAM(value.HWND);

        public override string ToString() => RawValue.ToString();
    }
}
