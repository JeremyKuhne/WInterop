// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement.Types
{
#pragma warning disable IDE1006
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365740.aspx">WIN32_FIND_DATA</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIN32_FIND_DATA
    {
        public FileAttributes dwFileAttributes;
        public FILETIME ftCreationTime;
        public FILETIME ftLastAccessTime;
        public FILETIME ftLastWriteTime;
        public HighLowUlong nFileSize;
        public ReparseTag dwReserved0;
        public uint dwReserved1;
        private FixedString.Size260 _cFileName;
        private FixedString.Size14 _cAlternateFileName;
        public Span<char> cFileName => _cFileName.Buffer;
        public Span<char> cAlternateFileName => _cAlternateFileName.Buffer;
    }
#pragma warning restore IDE1006
}
