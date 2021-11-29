// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Cryptography;

// https://msdn.microsoft.com/en-us/library/windows/desktop/aa376061.aspx
[return: MarshalAs(UnmanagedType.Bool)]
public delegate bool CertEnumSystemStoreLocationCallback(
    IntPtr pvszStoreLocations,
    uint dwFlags,
    IntPtr pvReserved,
    IntPtr pvArg);