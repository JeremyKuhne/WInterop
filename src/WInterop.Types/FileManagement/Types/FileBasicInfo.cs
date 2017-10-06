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
        public DateTime CreationTimeUtc;
        public DateTime LastAccessTimeUtc;
        public DateTime LastWriteTimeUtc;
        public DateTime ChangeTimeUtc;

        public FileBasicInfo(FILE_BASIC_INFORMATION data)
        {
            Attributes = data.FileAttributes;
            CreationTimeUtc = data.CreationTime.ToDateTimeUtc();
            LastAccessTimeUtc = data.LastAccessTime.ToDateTimeUtc();
            LastWriteTimeUtc = data.LastWriteTime.ToDateTimeUtc();
            ChangeTimeUtc = data.ChangeTime.ToDateTimeUtc();
        }
    }
}

