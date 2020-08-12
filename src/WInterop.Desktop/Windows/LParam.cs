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
    public struct LParam
    {
        public IntPtr Value;

        public ushort LowWord => Conversion.LowWord(Value);
        public ushort HighWord => Conversion.HighWord(Value);

        public LParam(IntPtr value) => Value = value;
        public LParam(short high, short low) => Value = (IntPtr)Conversion.HighLowToInt(high, low);
        public LParam(int high, int low) => Value = (IntPtr)Conversion.HighLowToInt(checked((short)high), checked((short)low));

        public static implicit operator int(LParam lParam) => (int)lParam.Value.ToInt64();
        public static explicit operator uint(LParam lParam) => (uint)lParam.Value.ToInt64();
        public static implicit operator LParam(int value) => new LParam((IntPtr)value);
        public static implicit operator LParam(IntPtr value) => new LParam(value);
        public static implicit operator IntPtr(LParam lParam) => lParam.Value;

        public static unsafe implicit operator void* (LParam lParam) => lParam.Value.ToPointer();
        public static unsafe implicit operator LParam(void* value) => new LParam((IntPtr)value);
        public static explicit operator WindowHandle(LParam lParam) => new HWND(lParam.Value);
        public static explicit operator LParam(WindowHandle value) => new LParam(value.HWND.Value);

        public override string ToString() => Value.ToString();
    }
}
