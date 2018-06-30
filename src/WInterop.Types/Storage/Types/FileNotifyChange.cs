// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage.Types
{
    [Flags]
    public enum FileNotifyChange
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365465.aspx

        /// <summary>
        /// Report any file name change. Includes renaming, creating, or deleting a file. [FILE_NOTIFY_CHANGE_FILE_NAME]
        /// </summary>
        FileName = 0x00000001,

        /// <summary>
        /// Report any directory name change. Includes creating or deleting a directory. [FILE_NOTIFY_CHANGE_DIR_NAME]
        /// </summary>
        DirectoryName = 0x00000002,

        /// <summary>
        /// Report any attribute change. [FILE_NOTIFY_CHANGE_ATTRIBUTES]
        /// </summary>
        Attributes = 0x00000004,

        /// <summary>
        /// Report any file size change. Only fires when changes are actually flushed to disk. [FILE_NOTIFY_CHANGE_SIZE]
        /// </summary>
        Size = 0x00000008,

        /// <summary>
        /// Report last write time changes. Only fires when changes are actually flushed to disk. [FILE_NOTIFY_CHANGE_LAST_WRITE]
        /// </summary>
        LastWrite = 0x00000010,

        /// <summary>
        /// Report last access time changes. [FILE_NOTIFY_CHANGE_LAST_ACCESS]
        /// </summary>
        LastAccess = 0x00000020,

        /// <summary>
        /// Report creation time changes. [FILE_NOTIFY_CHANGE_CREATION]
        /// </summary>
        Creation = 0x00000040,

        /// <summary>
        /// Report security descriptor changes. [FILE_NOTIFY_CHANGE_SECURITY]
        /// </summary>
        Security = 0x00000100
    }
}
