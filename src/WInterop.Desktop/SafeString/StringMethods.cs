// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.SafeString.Native;

namespace WInterop.SafeString
{
    public static partial class StringMethods
    {
        public static unsafe void ToUpperInvariant(ref UNICODE_STRING value)
        {
            Imports.RtlUpcaseUnicodeString(ref value, ref value, false).ThrowIfFailed();
        }
    }
}