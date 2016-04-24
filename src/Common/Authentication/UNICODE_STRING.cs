// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authentication
{
    using System.Runtime.InteropServices;

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380518.aspx
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UNICODE_STRING
    {
        /// <summary>
        /// Length, in bytes, not including the the null, if any.
        /// </summary>
        public ushort Length;

        /// <summary>
        /// Max size of the buffer in bytes
        /// </summary>
        public ushort MaximumLength;

        public char* Buffer;
    }
}
