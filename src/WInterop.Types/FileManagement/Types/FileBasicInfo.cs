// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
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
            CreationTime = data.CreationTime.ToDateTimeUtc().ToLocalTime();
            LastAccessTime = data.LastAccessTime.ToDateTimeUtc().ToLocalTime();
            LastWriteTime = data.LastWriteTime.ToDateTimeUtc().ToLocalTime();
            ChangeTime = data.ChangeTime.ToDateTimeUtc().ToLocalTime();
        }
    }
}

