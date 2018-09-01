// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Windows.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633577.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct WNDCLASS
    {
        public ClassStyle style;
        public IntPtr lpfnWndProc;
        public int cbClassExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public char* lpszMenuName;
        public char* lpszClassName;
     }
}