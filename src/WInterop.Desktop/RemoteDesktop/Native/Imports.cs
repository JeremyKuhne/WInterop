// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.RemoteDesktop.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://msdn.microsoft.com/en-us/library/aa382990.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool ProcessIdToSessionId(
        uint dwProcessId,
        out uint pSessionId);

    // https://msdn.microsoft.com/en-us/library/aa383835.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern uint WTSGetActiveConsoleSessionId();
}