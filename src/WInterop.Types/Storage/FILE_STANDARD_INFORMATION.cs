// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545855.aspx">FILE_STANDARD_INFORMATION</a> structure.
    /// Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364401.aspx">FILE_STANDARD_INFO</a> structure.
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
