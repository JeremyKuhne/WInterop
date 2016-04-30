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
        public const int STATUS_SUCCESS = 0x00000000;

        /// <summary>
        /// Returned by enumeration APIs to indicate more information is available to successive calls.
        /// </summary>
        public const int STATUS_MORE_ENTRIES = 0x00000105;

        /// <summary>
        /// {Buffer Overflow} The data was too large to fit into the specified buffer.
        /// </summary>
        public const int STATUS_BUFFER_OVERFLOW = unchecked((int)0x80000005);

        /// <summary>
        /// The specified information record length does not match the length that is required for the specified information class.
        /// </summary>
        public const int STATUS_INFO_LENGTH_MISMATCH = unchecked((int)0xC0000004);

        /// <summary>
        /// An invalid HANDLE was specified.
        /// </summary>
        public const int STATUS_INVALID_HANDLE = unchecked((int)0xC0000008);

        /// <summary>
        /// An invalid parameter was passed to a service or function.
        /// </summary>
        public const int STATUS_INVALID_PARAMETER = unchecked((int)0xC000000D);

        /// <summary>
        /// {Access Denied} A process has requested access to an object but has not been granted those access rights.
        /// </summary>
        public const int STATUS_ACCESS_DENIED = unchecked((int)0xC0000022);

        /// <summary>
        /// {Buffer Too Small} The buffer is too small to contain the entry. No information has been written to the buffer.
        /// </summary>
        public const int STATUS_BUFFER_TOO_SMALL = unchecked((int)0xC0000023);

        /// <summary>
        /// The object name is invalid.
        /// </summary>
        public const int STATUS_OBJECT_NAME_INVALID = unchecked((int)0xC0000033);

        /// <summary>
        /// The object name is not found.
        /// </summary>
        public const int STATUS_OBJECT_NAME_NOT_FOUND = unchecked((int)0xC0000034);

        /// <summary>
        /// The object name already exists.
        /// </summary>
        public const int STATUS_OBJECT_NAME_COLLISION = unchecked((int)0xC0000035);

        /// <summary>
        /// The object path component was not a directory object.
        /// </summary>
        public const int STATUS_OBJECT_PATH_INVALID = unchecked((int)0xC0000039);

        /// <summary>
        /// {Path Not Found} The path does not exist.
        /// </summary>
        public const int STATUS_OBJECT_PATH_NOT_FOUND = unchecked((int)0xC000003A);

        /// <summary>
        /// The object path component was not a directory object.
        /// </summary>
        public const int STATUS_OBJECT_PATH_SYNTAX_BAD = unchecked((int)0xC000003B);

        /// <summary>
        /// Insufficient system resources exist to complete the API.
        /// </summary>
        public const int STATUS_INSUFFICIENT_RESOURCES = unchecked((int)0xC000009A);
    }
}
