﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa365740.aspx">WIN32_FIND_DATA</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public ref struct WIN32_FIND_DATA
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
        public ReadOnlySpan<char> cFileName => _cFileName.Buffer;
        public ReadOnlySpan<char> cAlternateFileName => _cAlternateFileName.Buffer;
    }
}
