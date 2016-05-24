// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.DataTypes
{
    [Flags]
    public enum FileFlags : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        NONE = 0x0,

        FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,

        FILE_FLAG_OPEN_NO_RECALL = 0x00100000,

        FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000,

        FILE_FLAG_SESSION_AWARE = 0x00800000,

        /// <summary>
        /// Allows accessing via POSIX semantics, notably not ignoring case.
        /// </summary>
        FILE_FLAG_POSIX_SEMANTICS = 0x01000000,

        FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,

        /// <summary>
        /// Flags the file to be deleted when the final handle is closed.
        /// </summary>
        FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,

        /// <summary>
        /// Hints that the access for a file will be mostly sequential to optimize caching behavior.
        /// </summary>
        FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,

        /// <summary>
        /// Hints that the access for a file will be mostly random to optimize caching behavior.
        /// Notably that the system shouldn't read ahead in anticipation of further reads.
        /// </summary>
        FILE_FLAG_RANDOM_ACCESS = 0x10000000,

        /// <summary>
        /// Causes data writes to skip the system cache and go straight to disk.
        /// </summary>
        /// <remarks>
        /// Using this flag requires reading and writing aligned on physical sectors
        /// and in sector sized chunks.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/cc644950.aspx
        /// 
        /// File metadata is always cached, so it must be flushed either explicitly
        /// or through the FILE_FLAG_WRITE_THROUGH flag.
        /// </remarks>
        FILE_FLAG_NO_BUFFERING = 0x20000000,

        /// <summary>
        /// Indicates that operations should potentially be asynchronous. If data is pending
        /// Read/WriteFile will return ERROR_IO_PENDING and will signal the event specified
        /// in the OVERLAPPED structure when complete.
        /// </summary>
        /// <remarks>
        /// In this state the system will not maintain a file pointer, you must pass the
        /// offset in the OVERLAPPED structure.
        /// 
        /// When this flag is *not* set, the file mode FILE_SYNCHRONOUS_IO_NONALERT is set,
        /// which is the file handle object flag FO_SYNCHRONOUS_IO without FO_ALERTABLE_IO.
        /// FILE_SYNCHRONOUS_IO_ALERT is FO_SYNCHRONOUS_IO with FO_ALTERTABLE_IO.
        /// 
        /// So when neither FILE_SYNCHRONOUS_IO_NONALERT or FILE_SYNCHRONOUS_IO_ALERT is set
        /// the object is overlapped (e.g. async is the "default" state).
        /// </remarks>
        FILE_FLAG_OVERLAPPED = 0x40000000,

        /// <summary>
        /// Causes writes to be immediately flushed to disk (e.g. FlushFileBuffers)
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa364218.aspx
        /// </summary>
        FILE_FLAG_WRITE_THROUGH = 0x80000000
    }
}
