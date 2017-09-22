// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545817.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_NAME_INFORMATION
    {
        public uint FileNameLength;
        private TrailingString _FileName;
        public ReadOnlySpan<char> FileName => _FileName.GetBuffer(FileNameLength);
    }
}
