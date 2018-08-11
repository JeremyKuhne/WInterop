// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    public struct FileStandardInfo
    {
        public ulong AllocationSize;
        public ulong EndOfFile;
        public uint NumberOfLinks;
        public bool DeletePending;
        public bool Directory;

        public FileStandardInfo(FILE_STANDARD_INFORMATION data)
        {
            AllocationSize = data.AllocationSize;
            EndOfFile = data.EndOfFile;
            NumberOfLinks = data.NumberOfLinks;
            DeletePending = data.DeletePending;
            Directory = data.Directory;
        }
    }
}
