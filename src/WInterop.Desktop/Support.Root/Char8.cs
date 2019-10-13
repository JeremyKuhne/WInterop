// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    /// <summary>
    /// Used for blitting CHAR and UCHAR. [CHAR] [UCHAR]
    /// </summary>
    public readonly struct Char8
    {
        public byte Value { get; }

        public Char8(byte value) => Value = value;

        public static implicit operator char(Char8 c) => (char)c.Value;
    }
}
