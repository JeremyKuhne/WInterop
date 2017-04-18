// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Handles.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550671.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct IO_STATUS_BLOCK
    {
        IO_STATUS Status;
        IntPtr Information;
    }
}
