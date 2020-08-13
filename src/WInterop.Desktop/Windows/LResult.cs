// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows
{
    public struct LResult
    {
        public IntPtr Value;

        public ushort LowWord => Conversion.LowWord(Value);
        public ushort HighWord => Conversion.HighWord(Value);

        public LResult(IntPtr value) => Value = value;

        public static implicit operator LResult(int value) => new LResult((IntPtr)value);
        public static implicit operator int(LResult value) => (int)value.Value.ToInt64();
        public static implicit operator LResult(uint value) => new LResult((IntPtr)value);
        public static implicit operator uint(LResult value) => (uint)value.Value.ToInt64();
        public static implicit operator LResult(IntPtr value) => new LResult(value);
        public static implicit operator IntPtr(LResult value) => value.Value;
    }
}
