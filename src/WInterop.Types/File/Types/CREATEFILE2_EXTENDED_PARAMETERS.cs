// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.File.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449426.aspx
    public ref struct CREATEFILE2_EXTENDED_PARAMETERS
    {
        public uint dwSize;
        public FileAttributes dwFileAttributes;
        public FileFlags dwFileFlags;
        public SecurityQosFlags dwSecurityQosFlags;
        public unsafe SECURITY_ATTRIBUTES* lpSecurityAttributes;
        public IntPtr hTemplateFile;
    }
}
