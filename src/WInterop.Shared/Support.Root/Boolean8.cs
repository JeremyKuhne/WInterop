// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    /// <summary>
    /// Using bool in a struct prevents the struct from being blittable.
    /// This allows structs to marshal more effectively. [BOOLEAN]
    /// </summary>
    /// <remarks>
    /// While a bool is stored as an 8 bit value in managed code it isn't considered blittable
    /// by parts of the CLR. You can pin with contained bool values directly, things like
    /// GCHandle refuse to.
    /// </remarks>
    public struct Boolean8
    {
        public byte RawValue;

        public Boolean8(bool b)
        {
            RawValue = b ? (byte)1 : (byte)0;
        }

        public Boolean8(byte value)
        {
            RawValue = value;
        }

        public bool IsTrue => RawValue != 0;

        public bool IsFalse => RawValue == 0;

        public static implicit operator bool(Boolean8 b) => b.IsTrue;

        public static implicit operator Boolean8(bool b) => new Boolean8(b);

        public override string ToString()
        {
            return IsTrue.ToString();
        }
    }
}
