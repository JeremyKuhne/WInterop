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
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365152.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true, SetLastError = true)]
    public static extern unsafe bool CreatePipe(
        out SafeFileHandle hReadPipe,
        out SafeFileHandle hWritePipe,
        SECURITY_ATTRIBUTES* lpPipeAttributes,
        uint nSize);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365150.aspx
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
    public static extern unsafe PipeHandle CreateNamedPipeW(
        string lpName,
        uint dwOpenMode,
        uint dwPipeMode,
        uint nMaxInstances,
        uint nOutBufferSize,
        uint nInBufferSize,
        uint nDefaultTimeOut,
        SECURITY_ATTRIBUTES* lpSecurityAttributes);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365147.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe SafeMailslotHandle CreateMailslotW(
        string lpName,
        uint nMaxMessageSize,
        uint lReadTimeout,
        SECURITY_ATTRIBUTES* lpSecurityAttributes);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365435.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool GetMailslotInfo(
        SafeMailslotHandle hMailslot,
        uint* lpMaxMessageSize,
        uint* lpNextSize,
        uint* lpMessageCount,
        uint* lpReadTimeout);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365786.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool SetMailslotInfo(
        SafeMailslotHandle hMailslot,
        uint lReadTimeout);
}