// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows.Native;

public static partial class WindowsImports
{
    // https://docs.microsoft.com/windows/win32/api/utilapiset/nf-utilapiset-beep
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool Beep(
        uint dwFreq,
        uint dwDurations);
}