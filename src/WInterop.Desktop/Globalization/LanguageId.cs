// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    /// <summary>
    ///  Language Identifier [LANGID]
    /// </summary>
    /// <remarks>
    ///  <see cref="https://docs.microsoft.com/en-us/windows/win32/intl/language-identifiers"/>
    /// </remarks>
    public struct LanguageId
    {
        public static LanguageId Null => default;

        public ushort RawValue;

        public LanguageId(ushort id)
        {
            RawValue = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is LanguageId other
                ? other.RawValue == RawValue
                : false;
        }

        public bool Equals(LanguageId other) => other.RawValue == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();

        public static bool operator ==(LanguageId a, LanguageId b) => a.RawValue == b.RawValue;

        public static bool operator !=(LanguageId a, LanguageId b) => a.RawValue != b.RawValue;

        public static implicit operator LanguageId(ushort value) => new LanguageId(value);
    }
}
