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
    public struct BOOL
    {
        public uint RawValue;

        public BOOL(bool b)
        {
            RawValue = b ? 1u : 0u;
        }

        public BOOL(uint value)
        {
            RawValue = value;
        }

        public BOOL(int value)
        {
            RawValue = (uint)value;
        }

        public bool IsTrue => RawValue != 0;

        public bool IsFalse => RawValue == 0;

        public static implicit operator bool(BOOL b) => b.IsTrue;

        public static implicit operator BOOL(bool b) => new BOOL(b);

        public override string ToString()
        {
            return IsTrue.ToString();
        }
    }
}
