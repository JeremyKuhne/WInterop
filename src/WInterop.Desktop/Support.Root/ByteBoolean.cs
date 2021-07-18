// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    /// <summary>
    ///  Using bool in a struct prevents the struct from being blittable. This allows structs to marshal more
    ///  effectively.
    /// </summary>
    /// <native>[BOOLEAN]</native>
    /// <remarks>
    ///  While a bool is stored as an 8 bit value in managed code it isn't considered blittable by parts of the CLR.
    ///  You can pin with contained bool values directly, things like GCHandle refuse to.
    /// </remarks>
    public readonly struct ByteBoolean
    {
        public byte RawValue { get; }

        public ByteBoolean(bool b) => RawValue = b ? (byte)1 : (byte)0;

        public ByteBoolean(byte value) => RawValue = value;

        public readonly bool IsTrue => RawValue != 0;

        public readonly bool IsFalse => RawValue == 0;

        public static implicit operator bool(ByteBoolean b) => b.IsTrue;

        public static implicit operator ByteBoolean(bool b) => new ByteBoolean(b);

        public override string ToString() => IsTrue.ToString();
    }
}