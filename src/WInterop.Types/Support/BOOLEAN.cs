// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support
{
    /// <summary>
    /// Using bool in a struct prevents the struct from being blittable.
    /// This allows structs to marshal more effectively.
    /// </summary>
    public struct BOOLEAN
    {
        public byte RawValue;

        public BOOLEAN(bool b)
        {
            RawValue = b ? (byte)1 : (byte)0;
        }

        public BOOLEAN(byte value)
        {
            RawValue = value;
        }

        public bool IsTrue => RawValue != 0;

        public bool IsFalse => RawValue == 0;

        public static implicit operator bool(BOOLEAN b) => b.IsTrue;

        public static implicit operator BOOLEAN(bool b) => new BOOLEAN(b);

        public override string ToString()
        {
            return IsTrue.ToString();
        }
    }
}
