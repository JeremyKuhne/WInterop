// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Storage;

namespace WInterop.Compression
{
    // https://msdn.microsoft.com/en-us/library/ff797948.aspx

    /// <summary>
    /// Seek callback for an FDI context.
    /// </summary>
    /// <param name="hf">Application defined handle to the open file.</param>
    /// <param name="dist">Number of bytes to move the file pointer.</param>
    /// <param name="seektype">Starting point for seeking.</param>
    /// <returns>Offset from the beginning of the file or -1 for an error.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FNSEEK(
        IntPtr hf,
        int dist,
        MoveMethod seektype);
}
