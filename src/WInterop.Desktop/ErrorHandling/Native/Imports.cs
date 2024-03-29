﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Errors.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
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
}