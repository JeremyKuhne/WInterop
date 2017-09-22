// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Globalization;

namespace WInterop
{
    public static class MoreCharSpanExtensions
    {
        public static bool EndsWithOrdinal(this ReadOnlySpan<char> buffer, ReadOnlySpan<char> value, bool ignoreCase = false)
        {
            if (value.Length == 0)
                return true;
            else if (buffer.Length == 0 || value.Length > buffer.Length)
                return false;

            if (buffer.Length != value.Length)
                buffer = buffer.Slice(buffer.Length - value.Length);

            return GlobalizationMethods.CompareStringOrdinal(buffer, value, ignoreCase) == 0;
        }
    }
}
