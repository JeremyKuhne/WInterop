// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Cryptography.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376059.aspx
    [return: MarshalAs(UnmanagedType.Bool)]
    public delegate bool CertEnumSystemStoreCallback(
        IntPtr pvSystemStore,
        uint dwFlags,
        IntPtr pStoreInfo,
        IntPtr pvReserved,
        IntPtr pvArg);
}
