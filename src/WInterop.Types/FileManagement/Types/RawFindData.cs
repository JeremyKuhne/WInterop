// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Used for processing and filtering find results.
    /// </summary>
    public struct RawFindData
    {
        public ReadOnlySpan<char> FileName;
        public string Directory;
        public FileAttributes FileAttributes;
        public ulong FileSize;
        public long RawCreationTimeUtc;
        public long RawLastAccessTimeUtc;
        public long RawLastWriteTimeUtc;

        public DateTime CreationTimeUtc => DateTime.FromFileTimeUtc(RawCreationTimeUtc);
        public DateTime LastAccessTimeUtc => DateTime.FromFileTimeUtc(RawLastAccessTimeUtc);
        public DateTime LastWriteTimeUtc => DateTime.FromFileTimeUtc(RawLastWriteTimeUtc);
    }
}
