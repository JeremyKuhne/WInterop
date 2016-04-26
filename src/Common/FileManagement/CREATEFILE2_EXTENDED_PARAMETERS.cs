// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    using System;
    using System.Runtime.InteropServices;

    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449426.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CREATEFILE2_EXTENDED_PARAMETERS
    {
        public uint dwSize;
        public FileAttributes dwFileAttributes;
        public FileFlags dwFileFlags;
        public SecurityQosFlags dwSecurityQosFlags;
        public IntPtr lpSecurityAttributes;
        public IntPtr hTemplateFile;
    }
}
