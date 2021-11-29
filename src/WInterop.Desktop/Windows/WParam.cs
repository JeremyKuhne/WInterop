// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Windows;

public struct WParam
{
    public nuint Value;

    public ushort LowWord => Conversion.LowWord(Value);
    public ushort HighWord => Conversion.HighWord(Value);

    public WParam(nuint value) => Value = value;
    public WParam(ushort high, ushort low) => Value = Conversion.HighLowToInt(high, low);

    public bool IsNull => Value == 0;

    public static implicit operator WParam(UIntPtr value) => new(value);
    public static implicit operator nuint(WParam value) => value.Value;
    public static implicit operator uint(WParam value) => (uint)value.Value;
    public static implicit operator WParam(uint value) => new(value);

    // We make these explicit as we want to encourage keeping signed/unsigned alignment
    public static unsafe explicit operator WParam(nint value) => new((nuint)value);
    public static unsafe explicit operator nint(WParam value) => new IntPtr((nint)value.Value);
    public static explicit operator int(WParam value) => (int)(uint)value;
    public static explicit operator WParam(int value) => new((nuint)value);
    public static explicit operator char(WParam value) => (char)(uint)value;
    public static explicit operator WParam(char value) => new(value);

    public static unsafe implicit operator void*(WParam value) => (void*)value.Value;
    public static unsafe implicit operator WParam(void* value) => new((nuint)value);

    public static explicit operator VirtualKey(WParam value) => (VirtualKey)value.Value;
    public static explicit operator WParam(VirtualKey value) => (uint)value;
    public static explicit operator MouseKey(WParam value) => (MouseKey)value.Value;
    public static explicit operator WParam(MouseKey value) => (uint)value;

    public override string ToString() => Value.ToString();
}