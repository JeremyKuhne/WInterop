// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363788.aspx">BY_HANDLE_FILE_INFORMATION</a> structure.
    /// </summary>
    public readonly struct ByHandleFileInformation
    {
        public readonly AllFileAttributes FileAttributes;
        public readonly FileTime CreationTime;
        public readonly FileTime LastAccessTime;
        public readonly FileTime LastWriteTime;
        public readonly uint VolumeSerialNumber;
        public readonly HighLowUlong FileSize;
        public readonly uint NumberOfLinks;
        public readonly HighLowUlong FileIndex;
    }
}
