// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Modules
{
    /// <summary>
    ///  [MODULEINFO]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684229.aspx
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ModuleInfo
    {
        /// <summary>
        ///  Load address of the module- equivalent to HMODULE's value.
        /// </summary>
        public readonly IntPtr BaseOfDll;

        /// <summary>
        ///  Size of the linear space that the module occupies, in bytes. From the PE header.
        /// </summary>
        public readonly uint SizeOfImage;

        /// <summary>
        ///  The entry point for the module. From the PE header.
        /// </summary>
        public readonly IntPtr EntryPoint;
    }
}