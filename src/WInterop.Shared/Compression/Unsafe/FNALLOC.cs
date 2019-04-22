// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Native
{
    // https://msdn.microsoft.com/en-us/library/ff797929.aspx

    /// <summary>
    /// Memory allocator callback for an FDI context.
    /// </summary>
    /// <param name="cb">Number of bytes to allocate.</param>
    /// <returns>Pointer to the allocated space or null if out of memory.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr FNALLOC(
        uint cb);
}
