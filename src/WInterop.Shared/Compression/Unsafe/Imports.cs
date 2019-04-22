// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/bb432271.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr FDICreate(
            FNALLOC pfnalloc,
            FNFREE pfnfree,
            FNOPEN pfnopen,
            FNREAD pfnread,
            FNWRITE pfnwrite,
            FNCLOSE pfnclose,
            FNSEEK pfnseek,
            int cpuType,
            ref ExtractResult perf);

        // https://msdn.microsoft.com/en-us/library/bb432272.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool FDIDestroy(
            IntPtr hfdi);

        // https://msdn.microsoft.com/en-us/library/bb432273.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool FDIIsCabinet(
            FdiHandle hfdi,
            IntPtr hf,
            ref CabinetInfo pfdici);

        // https://msdn.microsoft.com/en-us/library/bb432270.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern bool FDICopy(
            FdiHandle hfdi,
            string pszCabinet,
            string pszCabPath,
            int flags,
            FNFDINOTIFY pfnfdin,
            IntPtr pfnfdid,
            IntPtr pvUser);
    }
}
