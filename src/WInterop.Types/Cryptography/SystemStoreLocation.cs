// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography.DataTypes
{
    /// <summary>
    /// From wincrypt.h
    /// </summary>
    public enum SystemStoreLocation : uint
    {
        //  Registry: HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE
        CERT_SYSTEM_STORE_CURRENT_USER = SystemStoreLocationId.CERT_SYSTEM_STORE_CURRENT_USER_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,
        CERT_SYSTEM_STORE_LOCAL_MACHINE = SystemStoreLocationId.CERT_SYSTEM_STORE_LOCAL_MACHINE_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,
        //  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services
        CERT_SYSTEM_STORE_CURRENT_SERVICE = SystemStoreLocationId.CERT_SYSTEM_STORE_CURRENT_SERVICE_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,
        CERT_SYSTEM_STORE_SERVICES = SystemStoreLocationId.CERT_SYSTEM_STORE_SERVICES_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,
        //  Registry: HKEY_USERS
        CERT_SYSTEM_STORE_USERS = SystemStoreLocationId.CERT_SYSTEM_STORE_USERS_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

        //  Registry: HKEY_CURRENT_USER\Software\Policies\Microsoft\SystemCertificates
        CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY = SystemStoreLocationId.CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,
        //  Registry: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\SystemCertificates
        CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY = SystemStoreLocationId.CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

        //  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\EnterpriseCertificates
        CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE = SystemStoreLocationId.CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT
    }
}
