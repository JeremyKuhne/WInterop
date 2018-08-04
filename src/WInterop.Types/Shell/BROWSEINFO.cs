// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Windows;

namespace WInterop.Shell
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct BROWSEINFO
    {
        public WindowHandle hwndOwner;
        public ItemIdList pidlRoot;
        public char* pszDisplayName;
        public char* lpszTitle;
        public BrowseInfoFlags ulFlags;
        public IntPtr lpfn;
        public LPARAM lParam;
        public int iImage;
    }
}
