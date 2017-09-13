// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.SafeString.Types;
using WInterop.Support;

namespace WInterop.SafeString
{
    public static partial class StringMethods
    {
        public static unsafe void ToUpperInvariant(ref UNICODE_STRING value)
        {
            NTSTATUS status = Imports.RtlUpcaseUnicodeString(
                (UNICODE_STRING*)Structs.AddressOf(ref value), (UNICODE_STRING*)Structs.AddressOf(ref value), false);

            if (!ErrorMacros.NT_SUCCESS(status))
                ErrorMethods.GetIoExceptionForNTStatus(status);
        }
    }
}
