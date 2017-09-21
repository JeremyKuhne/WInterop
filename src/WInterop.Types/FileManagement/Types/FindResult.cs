// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Support;

namespace WInterop.FileManagement.Types
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
        public ulong AllocatedBytes { get; private set; }
        public ReparseTag ReparseTag { get; private set; }

        public FindResult(ref WIN32_FIND_DATA findData, string directory)
        {
            Directory = directory;
            FileName = findData.cFileName.ToNullTerminatedString();
            AlternateFileName = findData.cAlternateFileName.ToNullTerminatedString();
            Attributes = findData.dwFileAttributes;

            CreationUtc = findData.ftCreationTime.ToDateTimeUtc();
            LastAccessUtc = findData.ftLastAccessTime.ToDateTimeUtc();
            LastWriteUtc = findData.ftLastWriteTime.ToDateTimeUtc();
            Length = findData.nFileSize;
            ReparseTag = findData.dwReserved0;
        }

        public unsafe FindResult(FILE_DIRECTORY_INFORMATION* info, string directory)
        {
            Directory = directory;
            FileName = info->FileName.Value;
            Attributes = info->FileAttributes;
            CreationUtc = info->CreationTime.ToDateTimeUtc();
            LastAccessUtc = info->LastAccessTime.ToDateTimeUtc();
            LastWriteUtc = info->ChangeTime.ToDateTimeUtc();
            Length = (uint)info->EndOfFile;
            AllocatedBytes = (uint)info->AllocationSize;
        }

        public unsafe FindResult(FILE_FULL_DIR_INFORMATION* info, string directory)
        {
            Directory = directory;
            FileName = info->FileName.GetValue(info->FileNameLength);
            Attributes = info->FileAttributes;
            CreationUtc = info->CreationTime.ToDateTimeUtc();
            LastAccessUtc = info->LastAccessTime.ToDateTimeUtc();
            LastWriteUtc = info->ChangeTime.ToDateTimeUtc();
            Length = (uint)info->EndOfFile;
            AllocatedBytes = (uint)info->AllocationSize;
            if ((info->FileAttributes & FileAttributes.ReparsePoint) != 0)
                ReparseTag = (ReparseTag)info->EaSize;
        }

        private string DebuggerDisplay => Paths.Combine(Directory, FileName);
    }
}
