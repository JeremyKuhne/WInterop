// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization.Types
{
    /// <summary>
    /// Language Identifier (LANGID)
    /// </summary>
    public struct LANGID
    {
        public static LANGID Null => new LANGID();

        public ushort RawValue;

        public LANGID(ushort id)
        {
            RawValue = id;
        }

        public override bool Equals(object obj)
        {
            return obj is LANGID other
                ? other.RawValue == RawValue
                : false;
        }

        public bool Equals(LANGID other) => other.RawValue == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();

        public static bool operator ==(LANGID a, LANGID b) => a.RawValue == b.RawValue;

        public static bool operator !=(LANGID a, LANGID b) => a.RawValue != b.RawValue;

        public static implicit operator LANGID(ushort value) => new LANGID(value);

    }
}
