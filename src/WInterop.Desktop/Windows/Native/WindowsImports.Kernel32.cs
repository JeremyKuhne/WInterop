// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows.Native;

public static partial class WindowsImports
{
    // https://msdn.microsoft.com/library/windows/desktop/ms679277.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool Beep(
        uint dwFreq,
        uint dwDurations);

    // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-muldiv
    [SuppressGCTransition]
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern int MulDiv(
        int nNumber,
        int nNumerator,
        int nDenominator);
}