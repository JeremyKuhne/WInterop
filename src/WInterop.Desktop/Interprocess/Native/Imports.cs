// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace WInterop.Interprocess.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://docs.microsoft.com/windows/win32/api/namedpipeapi/nf-namedpipeapi-createpipe
    [DllImport(Libraries.Kernel32, ExactSpelling = true, SetLastError = true)]
    public static extern unsafe bool CreatePipe(
        out SafeFileHandle hReadPipe,
        out SafeFileHandle hWritePipe,
        SECURITY_ATTRIBUTES* lpPipeAttributes,
        uint nSize);
}