// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum FileTypes : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb761366.aspx

        /// <summary>
        /// Only read/write files (default). (DDL_READWRITE)
        /// </summary>
        ReadWrite = 0x0000,

        /// <summary>
        /// Includes read-only files. (DDL_READONLY)
        /// </summary>
        ReadOnly = 0x0001,

        /// <summary>
        /// Includes hidden files. (DDL_HIDDEN)
        /// </summary>
        Hidden = 0x0002,

        /// <summary>
        /// Includes system files. (DDL_SYSTEM)
        /// </summary>
        System = 0x0004,

        /// <summary>
        /// Includes subdirectories. (DDL_DIRECTORY)
        /// </summary>
        Directory = 0x0010,

        /// <summary>
        /// Includes archived files. (DDL_ARCHIVE)
        /// </summary>
        Archive = 0x0020,

        /// <summary>
        /// Use PostMessage to send messages instead of SendMessage. (DDL_POSTMSGS)
        /// </summary>
        PostMessages = 0x2000,

        /// <summary>
        /// Adds all mapped drives to the list. (DDL_DRIVES)
        /// </summary>
        Drives = 0x4000,

        /// <summary>
        /// Don't include read/write files. (DDL_EXCLUSIVE)
        /// </summary>
        Exclusive = 0x8000
    }
}
