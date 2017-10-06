// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// File access mode information.
    /// </summary>
    [Flags]
    public enum FileAccessMode : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545809.aspx

        // There are a number of flags that map to other create file flags, notably:
        //
        //      FILE_RANDOM_ACCESS              ->      FILE_FLAG_RANDOM_ACCESS
        //      FILE_OPEN_FOR_BACKUP_INTENT     ->      FILE_FLAG_BACKUP_SEMANTICS
        //
        // There doesn't seem to be a way to see that these flags were set on the
        // the handle, although FO_RANDOM_ACCESS is a flag on the file handle.

        /// <summary>
        /// Writes go directly to disk (FileFlags.WriteThrough was set [FILE_FLAG_WRITE_THROUGH]).
        /// [FILE_WRITE_THROUGH]
        /// </summary>
        WriteThrough = 0x00000002,

        /// <summary>
        /// Hints that the access for a file will be mostly sequential to optimize caching behavior
        /// (FileFlags.SequentialScan was set [FILE_FLAG_SEQUENTIAL_SCAN]). [FILE_SEQUENTIAL_ONLY]
        /// </summary>
        SequentialOnly = 0x00000004,

        /// <summary>
        /// Data writes skip the system cache (FileFlags.NoBuffering was set [FILE_FLAG_NO_BUFFERING]).
        /// [FILE_NO_INTERMEDIATE_BUFFERING]
        /// </summary>
        NoIntermediateBuffering = 0x00000008,

        /// <summary>
        /// The file is synchronous and alertable. [FILE_SYNCHRONOUS_IO_ALERT]
        /// </summary>
        SynchronousAlertable = 0x00000010,

        /// <summary>
        /// The file is synchronous and not alertable. This is the state you get when you *don't*
        /// request FileFlags.Overlapped [FILE_FLAG_OVERLAPPED] when creating a file handle.
        /// [FILE_SYNCHRONOUS_IO_NONALERT]
        /// </summary>
        SynchronousNotAlertable = 0x00000020,

        /// <summary>
        /// The file is flagged for deletion when the last handle is closed. This is
        /// the match for FileFlags.DeleteOnClose. (FILE_FLAG_DELETE_ON_CLOSE).
        /// </summary>
        DeleteOnClose = 0x00001000
    }
}
