// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Native
{
    // https://msdn.microsoft.com/en-us/library/ff797945.aspx

    /// <summary>
    /// Memory free callback for an FDI context.
    /// </summary>
    /// <param name="pv">Pointer to the memory block to free.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FNFREE(
        IntPtr pv);
}
