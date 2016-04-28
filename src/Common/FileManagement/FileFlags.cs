// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement
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
        FILE_FLAG_POSIX_SEMANTICS = 0x01000000,
        FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,
        FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,
        FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,
        FILE_FLAG_RANDOM_ACCESS = 0x10000000,
        FILE_FLAG_NO_BUFFERING = 0x20000000,
        FILE_FLAG_OVERLAPPED = 0x40000000,
        FILE_FLAG_WRITE_THROUGH = 0x80000000
    }
}
