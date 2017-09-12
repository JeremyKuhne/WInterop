// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    public class FindResult
    {
        public string Directory { get; private set; }
        public string FileName { get; private set; }
        public string AlternateFileName { get; private set; }
        public FileAttributes Attributes { get; private set; }
        public DateTime CreationUtc { get; private set; }
        public DateTime LastAccessUtc { get; private set; }
        public DateTime LastWriteUtc { get; private set; }
        public ulong Length { get; private set; }
        public ReparseTag ReparseTag { get; private set; }

        public FindResult(ref WIN32_FIND_DATA findData, string directory)
        {
            Directory = directory;
            FileName = findData.cFileName;
            AlternateFileName = findData.cAlternateFileName;
            Attributes = findData.dwFileAttributes;

            CreationUtc = findData.ftCreationTime.ToDateTimeUtc();
            LastAccessUtc = findData.ftLastAccessTime.ToDateTimeUtc();
            LastWriteUtc = findData.ftLastWriteTime.ToDateTimeUtc();
            Length = findData.nFileSize;
            ReparseTag = (ReparseTag)findData.dwReserved0;
        }
    }
}
