// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545855.aspx">FILE_STANDARD_INFORMATION</a> structure.
    /// </summary>
    public struct FILE_STANDARD_INFORMATION
    {
        public ulong AllocationSize;
        public ulong EndOfFile;
        public uint NumberOfLinks;
        public BOOLEAN DeletePending;
        public BOOLEAN Directory;
    }
}
