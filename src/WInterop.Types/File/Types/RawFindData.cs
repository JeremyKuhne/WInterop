// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.File.Types
{
    /// <summary>
    /// Used for processing and filtering find results.
    /// </summary>
    public unsafe ref struct RawFindData
    {
        private FILE_FULL_DIR_INFORMATION* _info;
        private string _directory;

        public RawFindData(FILE_FULL_DIR_INFORMATION* info, string directory)
        {
            _info = info;
            _directory = directory;
        }

        public ReadOnlySpan<char> FileName => _info->FileName;
        public string Directory => _directory;
        public FileAttributes FileAttributes => _info->FileAttributes;
        public ulong FileSize => (ulong)_info->AllocationSize;

        public DateTimeOffset CreationTimeUtc => _info->CreationTime.ToDateTimeUtc();
        public DateTimeOffset LastAccessTimeUtc => _info->LastAccessTime.ToDateTimeUtc();
        public DateTimeOffset LastWriteTimeUtc => _info->LastWriteTime.ToDateTimeUtc();
    }
}
