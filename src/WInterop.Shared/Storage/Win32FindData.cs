// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage
{
    /// <summary>
    /// [WIN32_FIND_DATA]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/minwinbase/ns-minwinbase-_win32_find_dataa"/></msdn>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public ref struct Win32FindData
    {
        public FileAttributes dwFileAttributes;
        public FileTime ftCreationTime;
        public FileTime ftLastAccessTime;
        public FileTime ftLastWriteTime;
        public HighLowUlong nFileSize;
        public ReparseTag dwReserved0;
        public uint dwReserved1;
        private FixedString.Size260 _cFileName;
        private FixedString.Size14 _cAlternateFileName;
        public ReadOnlySpan<char> cFileName => _cFileName.Buffer;
        public ReadOnlySpan<char> cAlternateFileName => _cAlternateFileName.Buffer;
    }
}
