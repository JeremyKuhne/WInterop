// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography.Types
{
    /// <summary>
    /// From wincrypt.h
    /// </summary>
    public enum StoreProvider : uint
    {
        CERT_STORE_PROV_MSG = 1,
        CERT_STORE_PROV_MEMORY = 2,
        CERT_STORE_PROV_FILE = 3,
        CERT_STORE_PROV_REG = 4,
        CERT_STORE_PROV_PKCS7 = 5,
        CERT_STORE_PROV_SERIALIZED = 6,
        CERT_STORE_PROV_FILENAME = 8,
        CERT_STORE_PROV_SYSTEM = 10,
        CERT_STORE_PROV_COLLECTION = 11,
        CERT_STORE_PROV_SYSTEM_REGISTRY = 13,
        CERT_STORE_PROV_PHYSICAL = 14,
        CERT_STORE_PROV_SMART_CARD = 15,
        CERT_STORE_PROV_LDAP = 16,
        CERT_STORE_PROV_PKCS12 = 17
    }
}