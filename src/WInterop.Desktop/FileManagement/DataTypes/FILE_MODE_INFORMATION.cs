// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.DataTypes
{
    // It isn't technically an enum, but it is much more usable to call it such
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545809.aspx
    //
    // There are a number of flags that map to other create file flags, notably:
    //
    //      FILE_RANDOM_ACCESS              ->      FILE_FLAG_RANDOM_ACCESS
    //      FILE_OPEN_FOR_BACKUP_INTENT     ->      FILE_FLAG_BACKUP_SEMANTICS
    //
    // There doesn't seem to be a way to see that these flags were set on the
    // the handle, although FO_RANDOM_ACCESS is a flag on the file handle.

    /// <summary>
    /// File mode information
    /// </summary>
    [Flags]
    public enum FILE_MODE_INFORMATION : uint
    {
        /// <summary>
        /// The internal match for FILE_FLAG_WRITE_THROUGH. Writes go directly to disk.
        /// </summary>
        FILE_WRITE_THROUGH = 0x00000002,

        /// <summary>
        /// The internal match for FILE_FLAG_SEQUENTIAL_SCAN.
        /// </summary>
        FILE_SEQUENTIAL_ONLY = 0x00000004,

        /// <summary>
        /// The internal match for FILE_FLAG_NO_BUFFERING.
        /// </summary>
        FILE_NO_INTERMEDIATE_BUFFERING = 0x00000008,

        /// <summary>
        /// The file is synchronous and alertable.
        /// </summary>
        FILE_SYNCHRONOUS_IO_ALERT = 0x00000010,

        /// <summary>
        /// The file is synchronous and not alertable. This is the state you get when
        /// you *don't* request FILE_FLAG_OVERLAPPED when creating a file handle.
        /// </summary>
        FILE_SYNCHRONOUS_IO_NONALERT = 0x00000020,

        /// <summary>
        /// The file is flagged for deletion when the last handle is closed. This is
        /// the match for FILE_FLAG_DELETE_ON_CLOSE.
        /// </summary>
        FILE_DELETE_ON_CLOSE = 0x00001000
    }
}
