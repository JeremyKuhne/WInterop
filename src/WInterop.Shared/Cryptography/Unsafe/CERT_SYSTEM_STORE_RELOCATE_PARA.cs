// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Cryptography.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa377575.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct CERT_SYSTEM_STORE_RELOCATE_PARA
    {
        /// <summary>
        /// Can be HKEY hKeyBase
        /// </summary>
        public IntPtr pvBase;

        /// <summary>
        /// Can be LPCSTR pszSystemStore or LPCWSTR pwszSystemStore
        /// </summary>
        public IntPtr pvSystemStore;
    }
}
