// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Cryptography.Types
{
    /// <summary>
    ///  From wincrypt.h
    /// </summary>
    [Flags]
    public enum StoreVerifyFlags : uint
    {
        /// <summary>
        ///  [CERT_STORE_SIGNATURE_FLAG]
        /// </summary>
        Signature = 0x00000001,

        /// <summary>
        ///  [CERT_STORE_TIME_VALIDITY_FLAG]
        /// </summary>
        TimeValidity = 0x00000002,

        /// <summary>
        ///  [CERT_STORE_REVOCATION_FLAG]
        /// </summary>
        Revocation = 0x00000004,

        /// <summary>
        ///  [CERT_STORE_NO_CRL_FLAG]
        /// </summary>
        NoCrl = 0x00010000,

        /// <summary>
        ///  [CERT_STORE_NO_ISSUER_FLAG]
        /// </summary>
        NoIssuer = 0x00020000,

        /// <summary>
        ///  [CERT_STORE_BASE_CRL_FLAG]
        /// </summary>
        BaseCrl = 0x00000100,

        /// <summary>
        ///  [CERT_STORE_DELTA_CRL_FLAG]
        /// </summary>
        DeltaCrl = 0x00000200
    }
}