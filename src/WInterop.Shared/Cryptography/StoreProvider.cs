// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography
{
    /// <summary>
    /// From wincrypt.h
    /// </summary>
    public enum StoreProvider : uint
    {
        /// <summary>
        /// [CERT_STORE_PROV_MSG]
        /// </summary>
        Message = 1,

        /// <summary>
        /// [CERT_STORE_PROV_MEMORY]
        /// </summary>
        Memory = 2,

        /// <summary>
        /// [CERT_STORE_PROV_FILE]
        /// </summary>
        File = 3,

        /// <summary>
        /// [CERT_STORE_PROV_REG]
        /// </summary>
        Registry = 4,

        /// <summary>
        /// [CERT_STORE_PROV_PKCS7]
        /// </summary>
        Pkcs7 = 5,

        /// <summary>
        /// [CERT_STORE_PROV_SERIALIZED]
        /// </summary>
        Serialized = 6,

        /// <summary>
        /// [CERT_STORE_PROV_FILENAME]
        /// </summary>
        Filename = 8,

        /// <summary>
        /// [CERT_STORE_PROV_SYSTEM]
        /// </summary>
        System = 10,

        /// <summary>
        /// [CERT_STORE_PROV_COLLECTION]
        /// </summary>
        Collection = 11,

        /// <summary>
        /// [CERT_STORE_PROV_SYSTEM_REGISTRY]
        /// </summary>
        SystemRegistry = 13,

        /// <summary>
        /// [CERT_STORE_PROV_PHYSICAL]
        /// </summary>
        Physical = 14,

        /// <summary>
        /// [CERT_STORE_PROV_SMART_CARD]
        /// </summary>
        SmartCard = 15,

        /// <summary>
        /// [CERT_STORE_PROV_LDAP]
        /// </summary>
        Ldap = 16,

        /// <summary>
        /// [CERT_STORE_PROV_PKCS12]
        /// </summary>
        Pkcs12 = 17
    }
}