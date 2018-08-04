// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550671.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct IO_STATUS_BLOCK
    {
        /// <summary>
        /// Status
        /// </summary>
        public IO_STATUS Status;

        /// <summary>
        /// Request dependent value.
        /// </summary>
        public IntPtr Information;

        // This isn't an actual Windows type, it is a union within IO_STATUS_BLOCK. We *have* to separate it out as
        // the size of IntPtr varies by architecture and we can't specify the size at compile time to offset the
        // Information pointer in the status block.
        [StructLayout(LayoutKind.Explicit)]
        public struct IO_STATUS
        {
            /// <summary>
            /// The completion status, either STATUS_SUCCESS if the operation was completed successfully or
            /// some other informational, warning, or error status.
            /// </summary>
            [FieldOffset(0)]
            public NTSTATUS Status;

            /// <summary>
            /// Reserved for internal use.
            /// </summary>
            [FieldOffset(0)]
            public IntPtr Pointer;
        }
    }
}
