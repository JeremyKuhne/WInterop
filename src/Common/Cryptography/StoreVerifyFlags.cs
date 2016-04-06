// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography
{
    using System;

    /// <summary>
    /// From wincrypt.h
    /// </summary>
    [Flags]
    public enum StoreVerifyFlags : uint
    {
        CERT_STORE_SIGNATURE_FLAG = 0x00000001,
        CERT_STORE_TIME_VALIDITY_FLAG = 0x00000002,
        CERT_STORE_REVOCATION_FLAG = 0x00000004,
        CERT_STORE_NO_CRL_FLAG = 0x00010000,
        CERT_STORE_NO_ISSUER_FLAG = 0x00020000,
        CERT_STORE_BASE_CRL_FLAG = 0x00000100,
        CERT_STORE_DELTA_CRL_FLAG = 0x00000200
    }
}
