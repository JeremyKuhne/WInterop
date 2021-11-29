// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography;

// https://msdn.microsoft.com/en-us/library/windows/desktop/aa376059.aspx
public delegate bool CertEnumSystemStoreCallback(
    IntPtr pvSystemStore,
    uint dwFlags,
    IntPtr pStoreInfo,
    IntPtr pvReserved,
    IntPtr pvArg);