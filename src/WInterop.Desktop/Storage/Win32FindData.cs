// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage
{
    /// <summary>
    ///  [WIN32_FIND_DATA]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/minwinbase/ns-minwinbase-_win32_find_dataa"/></msdn>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct Win32FindData
    {
        public readonly AllFileAttributes FileAttributes;
        public readonly FileTime CreationTime;
        public readonly FileTime LastAccessTime;
        public readonly FileTime LastWriteTime;
        public readonly HighLowUlong FileSize;
        public readonly ReparseTag ReparseTag; // dwReserved0
        public readonly uint Reserved1;
        private readonly FixedString.Size260 _cFileName;
        private readonly FixedString.Size14 _cAlternateFileName;
        public ReadOnlySpan<char> FileName => _cFileName.Buffer;
        public ReadOnlySpan<char> AlternateFileName => _cAlternateFileName.Buffer;
    }
}