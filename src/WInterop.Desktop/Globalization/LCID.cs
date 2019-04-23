// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    /// <summary>
    /// Locale Identifier (LCID)
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd373763.aspx
    public struct LCID
    {
        public static LCID Null => new LCID();

        public uint RawValue;

        public LCID(uint id)
        {
            RawValue = id;
        }

        public override bool Equals(object obj)
        {
            return obj is LCID other
                ? other.RawValue == RawValue
                : false;
        }

        public bool Equals(LCID other) => other.RawValue == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();

        public static bool operator ==(LCID a, LCID b) => a.RawValue == b.RawValue;

        public static bool operator !=(LCID a, LCID b) => a.RawValue != b.RawValue;

        public static implicit operator LCID(uint value) => new LCID(value);
    }
}
