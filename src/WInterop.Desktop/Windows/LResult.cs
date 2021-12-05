// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Windows;

public struct LResult
{
    public nint Value;

    public ushort LowWord => Conversion.LowWord(Value);
    public ushort HighWord => Conversion.HighWord(Value);

    public LResult(nint value) => Value = value;

    public static implicit operator LResult(int value) => new((IntPtr)value);
    public static implicit operator int(LResult value) => (int)value.Value;
    public static implicit operator LResult(uint value) => new((IntPtr)value);
    public static implicit operator uint(LResult value) => (uint)value.Value;
    public static implicit operator LResult(nint value) => new(value);
    public static implicit operator nint(LResult value) => value.Value;
    public static unsafe implicit operator void*(LResult value) => (void*)value.Value;
    public static unsafe implicit operator LResult(void* value) => new((nint)value);
}