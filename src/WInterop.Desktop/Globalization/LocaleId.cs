// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    /// <summary>
    ///  Locale Identifier [LCID]
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/intl/locale-identifiers</docs>
    public struct LocaleId
    {
        public static LocaleId Null => default;

        public uint RawValue;

        public LocaleId(uint id) => RawValue = id;

        public override bool Equals(object? obj) => obj is LocaleId other && other.RawValue == RawValue;

        public bool Equals(LocaleId other) => other.RawValue == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();

        public static bool operator ==(LocaleId a, LocaleId b) => a.RawValue == b.RawValue;

        public static bool operator !=(LocaleId a, LocaleId b) => a.RawValue != b.RawValue;

        public static implicit operator LocaleId(uint value) => new LocaleId(value);
    }
}
