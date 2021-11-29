// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.CRT.Types;

namespace WInterop.Compression.Native;

// https://msdn.microsoft.com/en-us/library/ff797946.aspx

/// <summary>
///  Read callback for an FDI context.
/// </summary>
/// <param name="pszFile">Name of the file.</param>
/// <param name="oflag">Type of operations allowed.</param>
/// <param name="pmode">Permission mode.</param>
/// <returns>Application defined handle to the open file, or -1 for an error.</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
public delegate IntPtr FNOPEN(
    string pszFile,
    FCNTL oflag,
    pmode pmode);