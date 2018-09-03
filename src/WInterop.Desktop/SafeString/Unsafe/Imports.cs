// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.SafeString.Unsafe;

namespace WInterop.SafeString.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff563005.aspx
        [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static unsafe extern NTStatus RtlUpcaseUnicodeString(
            ref UNICODE_STRING DestinationString,
            ref UNICODE_STRING SourceString,
            BOOLEAN AllocateDestinationString);

        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff561903.aspx
        [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static unsafe extern void RtlFreeUnicodeString(
            ref UNICODE_STRING UnicodeString);
    }
}
