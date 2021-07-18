// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;
using WInterop.Windows.Native;

namespace WInterop.Windows.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633577.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct WNDCLASSEX
    {
        public uint cbSize;
        public ClassStyle style;
        public IntPtr lpfnWndProc;
        public int cbClassExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public HICON hIcon;
        public HCURSOR hCursor;
        public HBRUSH hbrBackground;
        public char* lpszMenuName;
        public char* lpszClassName;
        public HICON hIconSm;
    }
}