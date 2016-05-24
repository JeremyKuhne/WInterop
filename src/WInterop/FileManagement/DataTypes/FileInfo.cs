// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.FileManagement.DataTypes
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
            CreationTime = Conversion.FileTimeToDateTime(data.ftCreationTime);
            LastAccessTime = Conversion.FileTimeToDateTime(data.ftLastAccessTime);
            LastWriteTime = Conversion.FileTimeToDateTime(data.ftLastWriteTime);
            Size = Conversion.HighLowToLong(data.nFileSizeHigh, data.nFileSizeLow);
        }
    }
}
