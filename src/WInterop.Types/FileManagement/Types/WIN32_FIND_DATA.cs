// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365740.aspx">WIN32_FIND_DATA</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public struct WIN32_FIND_DATA
    {
        // We're forcing the packing to 4 to allow us to use longs directly here

        public FileAttributes dwFileAttributes;
        public LongFileTime ftCreationTime;
        public LongFileTime ftLastAccessTime;
        public LongFileTime ftLastWriteTime;
        public ulong nFileSize;
        public uint dwReserved0;
        public uint dwReserved1;
        public FixedString.Size260 cFileName;
        public FixedString.Size14 cAlternateFileName;
    }
}
