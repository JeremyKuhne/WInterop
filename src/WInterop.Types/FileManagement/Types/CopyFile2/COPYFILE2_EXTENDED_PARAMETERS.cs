// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COPYFILE2_EXTENDED_PARAMETERS
    {
        public uint dwSize;
        public CopyFileFlags dwCopyFlags;
        public unsafe int* pfCanel;
        public IntPtr pProgressRoutine;
        public IntPtr pvCallbackContext;
    }
}
