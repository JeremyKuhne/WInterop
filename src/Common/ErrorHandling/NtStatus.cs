// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    /// <summary>
    /// NtStatus defines.
    /// </summary>
    public static class NtStatus
    {
        // NTSTATUS values
        // https://msdn.microsoft.com/en-us/library/cc704588.aspx
        internal const int STATUS_SUCCESS = 0x00000000;

        /// <summary>
        /// {Buffer Overflow} The data was too large to fit into the specified buffer.
        /// </summary>
        internal const int STATUS_BUFFER_OVERFLOW = unchecked((int)0x80000005);

        /// <summary>
        /// The specified information record length does not match the length that is required for the specified information class.
        /// </summary>
        internal const int STATUS_INFO_LENGTH_MISMATCH = unchecked((int)0xC0000004);

        /// <summary>
        /// An invalid HANDLE was specified.
        /// </summary>
        internal const int STATUS_INVALID_HANDLE = unchecked((int)0xC0000008);

        /// <summary>
        /// An invalid parameter was passed to a service or function.
        /// </summary>
        internal const int STATUS_INVALID_PARAMETER = unchecked((int)0xC000000D);

        /// <summary>
        /// {Access Denied} A process has requested access to an object but has not been granted those access rights.
        /// </summary>
        internal const int STATUS_ACCESS_DENIED = unchecked((int)0xC0000022);

        /// <summary>
        /// {Buffer Too Small} The buffer is too small to contain the entry. No information has been written to the buffer.
        /// </summary>
        internal const int STATUS_BUFFER_TOO_SMALL = unchecked((int)0xC0000023);

    }
}
