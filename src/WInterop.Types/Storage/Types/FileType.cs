// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// File types returned by the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx">GetFileType</a> API.
    /// </summary>
    public enum FileType : uint
    {
        /// <summary>
        /// File type is unknown. [FILE_TYPE_UNKNOWN]
        /// </summary>
        Unknown = 0x0000,

        /// <summary>
        /// File type is as disk file. [FILE_TYPE_DISK]
        /// </summary>
        Disk = 0x0001,

        /// <summary>
        /// Character file, such as an LPT device or a console. [FILE_TYPE_CHAR]
        /// </summary>
        Character = 0x0002,

        /// <summary>
        /// A pipe or socket. [FILE_TYPE_PIPE]
        /// </summary>
        Pipe = 0x0003,

        // Unused
        // FILE_TYPE_REMOTE = 0x8000
    }
}
