// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Native
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545817.aspx">FILE_NAME_INFORMATION</a> structure.
    ///  Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364388.aspx">FILE_NAME_INFO</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_NAME_INFORMATION
    {
        public uint FileNameLength;
        private char _FileName;
        public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(ref _FileName, FileNameLength);
    }
}
