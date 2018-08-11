// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression
{
    // https://msdn.microsoft.com/en-us/library/ff797949.aspx

    /// <summary>
    /// Write callback for an FDI context.
    /// </summary>
    /// <param name="hf">Application defined handle to the open file.</param>
    /// <param name="pv">Buffer containing data to be written.</param>
    /// <param name="cb">Maximum number of bytes to be written.</param>
    /// <returns>Number of bytes written or -1 for an error.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint FNWRITE(
        IntPtr hf,
        byte* pv,
        uint cb);
}
