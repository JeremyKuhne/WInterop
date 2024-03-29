﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Windows;

public struct LParam
{
    public nint Value;

    public ushort LowWord => Conversion.LowWord(Value);
    public ushort HighWord => Conversion.HighWord(Value);

    public LParam(nint value) => Value = value;
    public LParam(short high, short low) => Value = Conversion.HighLowToInt(high, low);
    public LParam(int high, int low) => Value = Conversion.HighLowToInt(checked((short)high), checked((short)low));

    public bool IsNull => Value == 0;

    public static implicit operator int(LParam value) => (int)value.Value;
    public static explicit operator uint(LParam value) => (uint)value.Value;
    public static implicit operator LParam(int value) => new(value);
    public static implicit operator LParam(nint value) => new(value);
    public static implicit operator nint(LParam value) => value.Value;
    public static implicit operator LParam((int High, int Low) value) => new(value.High, value.Low);
    public static implicit operator LPARAM(LParam value) => new(value);
    public static implicit operator LParam(LPARAM value) => new(value);

    public static unsafe implicit operator void*(LParam value) => (void*)value.Value;
    public static unsafe implicit operator LParam(void* value) => new((nint)value);
    public static unsafe explicit operator WindowHandle(LParam value) => new HWND((void*)value.Value);
    public static unsafe explicit operator LParam(WindowHandle value) => new((nint)value.HWND.Value);

    public override string ToString() => Value.ToString();
}