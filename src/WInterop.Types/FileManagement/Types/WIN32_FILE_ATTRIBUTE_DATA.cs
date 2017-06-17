// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365739.aspx">WIN32_FILE_ATTRIBUTE_DATA</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public struct WIN32_FILE_ATTRIBUTE_DATA
    {
        // By forcing the packing to 4 we can use ulongs and get the same layout
        // as the actual definition

        public FileAttributes dwFileAttributes;
        public LongFileTime ftCreationTime;
        public LongFileTime ftLastAccessTime;
        public LongFileTime ftLastWriteTime;
        public ulong nFileSize;
    }
}
