// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    using System;
    using System.Runtime.InteropServices;
    using ComTypes = System.Runtime.InteropServices.ComTypes;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365739.aspx">WIN32_FILE_ATTRIBUTE_DATA</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIN32_FILE_ATTRIBUTE_DATA
    {
        public FileAttributes dwFileAttributes;
        public ComTypes.FILETIME ftCreationTime;
        public ComTypes.FILETIME ftLastAccessTime;
        public ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
    }
}
