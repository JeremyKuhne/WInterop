// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement.Types
{
    public struct RawFindData
    {
        public NativeString FileName;
        public FileAttributes FileAttributes;
        public ulong FileSize;
        public DateTime CreationTimeUtc;
        public DateTime LastAccessTimeUtc;
        public DateTime LastWriteTimeUtc;
    }
}
