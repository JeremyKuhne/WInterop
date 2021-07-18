// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage.Native
{
    /// <summary>
    ///  Used to query names of files in a directory.
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540329.aspx"/></remarks>
    public struct FILE_NAMES_INFORMATION
    {
        public uint NextEntryOffset;
        public uint FileIndex;
        public uint FileNameLength;
        private char _FileName;
        public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(in _FileName, FileNameLength);
    }
}