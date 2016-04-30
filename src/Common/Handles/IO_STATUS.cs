// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Handles
{
    // This isn't an actual Windows type, we have to separate it out as the size of IntPtr varies by architecture
    // and we can't specify the size at compile time to offset the Information pointer in the status block.
    [StructLayout(LayoutKind.Explicit)]
    public struct IO_STATUS
    {
        [FieldOffset(0)]
        int Status;

        [FieldOffset(0)]
        IntPtr Pointer;
    }
}
