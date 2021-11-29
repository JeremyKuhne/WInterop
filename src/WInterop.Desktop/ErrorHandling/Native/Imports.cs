// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Errors.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679355.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern ErrorMode GetErrorMode();

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679355.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern ErrorMode GetThreadErrorMode();

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680621.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern ErrorMode SetErrorMode(
        ErrorMode uMode);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680621.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetThreadErrorMode(
        ErrorMode dwNewMode,
        out ErrorMode lpOldMode);

    // https://docs.microsoft.com/windows/win32/api/winternl/nf-winternl-rtlntstatustodoserror
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern uint RtlNtStatusToDosError(NTStatus Status);

    // LsaNtStatusToWinError is another entry point to RtlNtStatusToDosError
    // https://docs.microsoft.com/windows/win32/api/ntsecapi/nf-ntsecapi-lsantstatustowinerror
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern uint LsaNtStatusToWinError(NTStatus Status);

    // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-formatmessage
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern uint FormatMessageW(
        FormatMessageFlags dwFlags,
        IntPtr lpSource,
        uint dwMessageId,
        // LANGID or 0 for auto lookup
        uint dwLanguageId,
        IntPtr lpBuffer,
        // Size is in chars
        uint nSize,
        string[]? Arguments);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680627.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern void SetLastError(
        WindowsError dwErrCode);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679360.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern WindowsError GetLastError();
}