// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Synchronization
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms684342.aspx">OVERLAPPED</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        public IntPtr Internal;
        public IntPtr InternalHigh;
        public uint Offset;
        public uint OffsetHigh;
        public IntPtr hEvent;
    }
}
