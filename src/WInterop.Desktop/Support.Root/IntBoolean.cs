// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    /// <summary>
    ///  Using bool in a struct prevents the struct from being blittable.
    ///  This allows structs to marshal more effectively.
    /// </summary>
    /// <native>[BOOL]</native>
    public readonly struct IntBoolean
    {
        public int RawValue { get; }

        public IntBoolean(bool b) => RawValue = b ? 1 : 0;
        public IntBoolean(int value) => RawValue = value;

        public readonly bool IsTrue => RawValue != 0;
        public readonly bool IsFalse => RawValue == 0;

        public static implicit operator bool(IntBoolean b) => b.IsTrue;
        public static implicit operator IntBoolean(bool b) => new IntBoolean(b);
        public static implicit operator uint(IntBoolean b) => (uint)b.RawValue;
        public static implicit operator IntBoolean(uint b) => new IntBoolean((int)b);

        public override string ToString() => IsTrue.ToString();
    }
}
