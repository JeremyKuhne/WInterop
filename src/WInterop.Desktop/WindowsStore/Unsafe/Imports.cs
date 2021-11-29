// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.WindowsStore.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh920918.aspx
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern WindowsError GetCurrentApplicationUserModelId(
        ref uint applicationUserModelIdLength,
        SafeHandle applicationUserModelId);
}