// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    public struct FileInfo
    {
        public FileAttributes Attributes;
        public DateTime CreationTime;
        public DateTime LastAccessTime;
        public DateTime LastWriteTime;
        public ulong Size;

        public FileInfo(WIN32_FILE_ATTRIBUTE_DATA data)
        {
            Attributes = data.dwFileAttributes;
            CreationTime = data.ftCreationTime.ToDateTimeUtc().ToLocalTime();
            LastAccessTime = data.ftLastAccessTime.ToDateTimeUtc().ToLocalTime();
            LastWriteTime = data.ftLastWriteTime.ToDateTimeUtc().ToLocalTime();
            Size = data.nFileSize;
        }
    }
}
