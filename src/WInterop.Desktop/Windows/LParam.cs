// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public struct LParam
    {
        public nint Value;

        public ushort LowWord => Conversion.LowWord(Value);
        public ushort HighWord => Conversion.HighWord(Value);

        public LParam(nint value) => Value = value;
        public LParam(short high, short low) => Value = Conversion.HighLowToInt(high, low);
        public LParam(int high, int low) => Value = Conversion.HighLowToInt(checked((short)high), checked((short)low));

        public bool IsNull => Value == 0;

        public static implicit operator int(LParam lParam) => (int)lParam.Value;
        public static explicit operator uint(LParam lParam) => (uint)lParam.Value;
        public static implicit operator LParam(int value) => new(value);
        public static implicit operator LParam(nint value) => new(value);
        public static implicit operator nint(LParam lParam) => lParam.Value;
        public static implicit operator LParam((int High, int Low) value) => new(value.High, value.Low);

        public static unsafe implicit operator void*(LParam lParam) => (void*)lParam.Value;
        public static unsafe implicit operator LParam(void* value) => new((nint)value);
        public static explicit operator WindowHandle(LParam lParam) => new HWND(lParam.Value);
        public static explicit operator LParam(WindowHandle value) => new(value.HWND.Value);

        public override string ToString() => Value.ToString();
    }
}