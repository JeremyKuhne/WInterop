// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    using System;
    using Utility;

    public struct FileBasicInfo
    {
        public FileAttributes Attributes;
        public DateTime CreationTime;
        public DateTime LastAccessTime;
        public DateTime LastWriteTime;
        public DateTime ChangeTime;

        public FileBasicInfo(FILE_BASIC_INFO data)
        {
            Attributes = data.FileAttributes;
            CreationTime = Conversion.FileTimeToDateTime(data.CreationTime);
            LastAccessTime = Conversion.FileTimeToDateTime(data.LastAccessTime);
            LastWriteTime = Conversion.FileTimeToDateTime(data.LastWriteTime);
            ChangeTime = Conversion.FileTimeToDateTime(data.ChangeTime);
        }
    }
}

