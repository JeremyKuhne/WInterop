// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// File types returned by the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx">GetFileType</a> API.
    /// </summary>
    public enum FileType : uint
    {
        FILE_TYPE_UNKNOWN = 0x0000,
        FILE_TYPE_DISK = 0x0001,
        FILE_TYPE_CHAR = 0x0002,
        FILE_TYPE_PIPE = 0x0003,
        // Unused
        // FILE_TYPE_REMOTE = 0x8000
    }
}
