// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    public struct FileBasicInfo
    {
        public FileAttributes Attributes;
        public DateTimeOffset CreationTimeUtc;
        public DateTimeOffset LastAccessTimeUtc;
        public DateTimeOffset LastWriteTimeUtc;
        public DateTimeOffset ChangeTimeUtc;

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

