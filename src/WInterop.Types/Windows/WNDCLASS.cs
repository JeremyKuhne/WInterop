// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Windows.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633577.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct WNDCLASS
    {
        public WindowClassStyle style;
        public IntPtr lpfnWndProc;
        public int cbClassExtra;
        public int cbWndExtra;

        // SafeHandles can't be marshalled *from* native code
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;

        public IntPtr lpszClassName;
     }
}