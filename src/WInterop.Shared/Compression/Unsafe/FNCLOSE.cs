// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/ff797930.aspx

    /// <summary>
    /// File close callback for an FDI context.
    /// </summary>
    /// <param name="hf">Application defined handle to the open file.</param>
    /// <returns>0 for success, -1 for error.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FNCLOSE(
        IntPtr hf);
}
