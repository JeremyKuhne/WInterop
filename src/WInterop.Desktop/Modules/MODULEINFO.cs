// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Modules
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684229.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct MODULEINFO
    {
        /// <summary>
        /// Load address of the module- equivalent to HMODULE's value.
        /// </summary>
        public IntPtr lpBaseOfDll;

        /// <summary>
        /// Size of the linear space that the module occupies, in bytes. From the PE header.
        /// </summary>
        public uint SizeOfImage;

        /// <summary>
        /// The entry point for the module. From the PE header.
        /// </summary>
        public IntPtr EntryPoint;
    }
}
