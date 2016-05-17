// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles;
using WInterop.Utility;

namespace WInterop.FileManagement
{
    public class FindResult
    {
        public string OriginalPath { get; private set; }
        public string FileName { get; private set; }
        public string AlternateFileName { get; private set; }
        public IntPtr FindHandle { get; private set; }
        public FileAttributes Attributes { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime LastAccess { get; private set; }
        public DateTime LastWrite { get; private set; }
        public ulong Length { get; private set; }

        public FindResult(IntPtr handle, WIN32_FIND_DATA findData, string originalPath)
        {
            OriginalPath = originalPath;
            FindHandle = handle;
            FileName = findData.cFileName;
            AlternateFileName = findData.cAlternateFileName;
            Attributes = findData.dwFileAttributes;

            Creation = Conversion.FileTimeToDateTime(findData.ftCreationTime);
            LastAccess = Conversion.FileTimeToDateTime(findData.ftLastAccessTime);
            LastWrite = Conversion.FileTimeToDateTime(findData.ftLastWriteTime);
            Length = Conversion.HighLowToLong(findData.nFileSizeHigh, findData.nFileSizeLow);
        }
    }
}
