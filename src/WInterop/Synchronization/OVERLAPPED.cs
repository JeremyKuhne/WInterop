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
    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        /// <summary>
        /// Status code for the request. STATUS_PENDING until completed.
        /// </summary>
        public UIntPtr Internal;

        /// <summary>
        /// Number of bytes written when completed without errors.
        /// </summary>
        public UIntPtr InternalHigh;

        // Offset as defined here is not technically part of the definition, adding for easier use.
        // OffsetLot is defined as Offset in the specs
        // public uint OffsetLow;
        // public uint OffsetHigh;

        /// <summary>
        /// The offset to read from if the file supports seeking. Must be zero otherwise.
        /// </summary>
        public ulong Offset;

        // Ignoring this union of offset as it is reserved.
        // public IntPtr Pointer;

        /// <summary>
        /// The event that will be signaled when the operation is completed. Must be zero or a valid CreateEvent handle.
        /// Should be a manual reset event.
        /// </summary>
        public IntPtr hEvent;
    }
}
