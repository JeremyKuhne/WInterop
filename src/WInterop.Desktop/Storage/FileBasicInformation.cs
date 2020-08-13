// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545762.aspx">FILE_BASIC_INFORMATION</a> structure.
    ///  Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364217.aspx">FILE_BASIC_INFO</a> structure.
    /// </summary>
    public readonly struct FileBasicInformation
    {
        public readonly LongFileTime CreationTime;
        public readonly LongFileTime LastAccessTime;
        public readonly LongFileTime LastWriteTime;
        public readonly LongFileTime ChangeTime;
        public readonly AllFileAttributes FileAttributes;
    }
}
