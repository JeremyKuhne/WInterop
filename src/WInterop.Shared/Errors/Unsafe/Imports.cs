// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using Internal = WInterop.Support.Internal;

namespace WInterop.Errors.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>

    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public static extern uint LsaNtStatusToWinError(NTSTATUS Status);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679351.aspx
        public static uint FormatMessageW(
            FormatMessageFlags dwFlags,
            IntPtr lpSource,
            uint dwMessageId,
            // LANGID or 0 for auto lookup
            uint dwLanguageId,
            IntPtr lpBuffer,
            // Size is in chars
            uint nSize,
            string[] Arguments) => Internal.Imports.FormatMessageW(dwFlags, lpSource, dwMessageId, dwLanguageId, lpBuffer, nSize, Arguments);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680627.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern void SetLastError(
            WindowsError dwErrCode);
    }
}
