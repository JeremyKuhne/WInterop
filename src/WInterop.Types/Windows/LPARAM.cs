// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public struct LPARAM
    {
        public IntPtr Value;

        public ushort LowWord => Conversion.LowWord(Value);
        public ushort HighWord => Conversion.HighWord(Value);

        public LPARAM(IntPtr value) => Value = value;
        public LPARAM(short high, short low) => Value = (IntPtr)Conversion.HighLowToInt(high, low);
        public LPARAM(int high, int low) => Value = (IntPtr)Conversion.HighLowToInt(checked((short)high), checked((short)low));

        public static implicit operator int(LPARAM lParam) => (int)lParam.Value.ToInt64();
        public static explicit operator uint(LPARAM lParam) => (uint)lParam.Value.ToInt64();
        public static implicit operator LPARAM(int value) => new LPARAM((IntPtr)value);
        public static implicit operator LPARAM(IntPtr value) => new LPARAM(value);
        public static implicit operator IntPtr(LPARAM lParam) => lParam.Value;

        public unsafe static implicit operator void*(LPARAM lParam) => lParam.Value.ToPointer();
        public unsafe static implicit operator LPARAM (void* value) => new LPARAM((IntPtr)value);
        public static explicit operator WindowHandle(LPARAM lParam) => new HWND(lParam.Value);
        public static explicit operator LPARAM(WindowHandle value) => new LPARAM(value.HWND.Value);

        public override string ToString() => Value.ToString();
    }
}
