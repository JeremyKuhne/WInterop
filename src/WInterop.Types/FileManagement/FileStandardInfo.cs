// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.DataTypes
{
    public struct FileStandardInfo
    {
        public ulong AllocationSize;
        public ulong EndOfFile;
        public uint NumberOfLinks;
        public bool DeletePending;
        public bool Directory;

        public FileStandardInfo(FILE_STANDARD_INFO data)
        {
            AllocationSize = data.AllocationSize;
            EndOfFile = data.EndOfFile;
            NumberOfLinks = data.NumberOfLinks;
            DeletePending = data.DeletePending != 0;
            Directory = data.Directory != 0;
        }
    }
}
