// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Support;

namespace WInterop.File.Types
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FindResult
    {
        public string Directory { get; private set; }
        public string FileName { get; private set; }
        public string AlternateFileName { get; private set; }
        public FileAttributes Attributes { get; private set; }
        public DateTimeOffset CreationUtc { get; private set; }
        public DateTimeOffset LastAccessUtc { get; private set; }
        public DateTimeOffset LastWriteUtc { get; private set; }
        public ulong Length { get; private set; }
        public ulong AllocatedBytes { get; private set; }
        public ReparseTag ReparseTag { get; private set; }

        public FindResult(ref RawFindData findData)
        {
            Directory = findData.Directory;
            FileName = findData.FileName.CreateString();
            Attributes = findData.FileAttributes;
            CreationUtc = findData.CreationTimeUtc;
            LastAccessUtc = findData.LastAccessTimeUtc;
            LastWriteUtc = findData.LastWriteTimeUtc;
            Length = (uint)findData.FileSize;
        }

        private string DebuggerDisplay => Paths.Combine(Directory, FileName);
    }
}
