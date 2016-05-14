// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Synchronization
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms684342.aspx">OVERLAPPED</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct OVERLAPPED
    {
        [FieldOffset(0)] public uint Internal;
        [FieldOffset(4)] public uint InternalHigh;
        // Defined as Offset in the specs
        [FieldOffset(8)] public uint OffsetLow;
        [FieldOffset(12)] public uint OffsetHigh;
        // Not technically part of the definition, adding for easier use.
        [FieldOffset(8)] public ulong Offset;
        [FieldOffset(8)] public IntPtr Pointer;
        [FieldOffset(16)] public IntPtr hEvent;
    }
}
