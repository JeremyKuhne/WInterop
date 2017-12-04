// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Synchronization.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms684342.aspx">OVERLAPPED</a> structure.
    /// </summary>
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

        /// <summary>
        /// The offset to read from if the file supports seeking. Must be zero otherwise.
        /// </summary>
        public LowHighUlong Offset;

        // Ignoring this union of offset as it is reserved.
        // public IntPtr Pointer;

        /// <summary>
        /// The event that will be signaled when the operation is completed. Must be zero or a valid CreateEvent handle.
        /// Should be a manual reset event.
        /// </summary>
        public IntPtr hEvent;
    }
}
