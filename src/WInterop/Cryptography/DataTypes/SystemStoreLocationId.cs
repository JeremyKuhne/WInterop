// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography.DataTypes
{
    /// <summary>
    /// From wincrypt.h, used by crypto <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa376058.aspx">CertEnumSystemStore</a> function.
    /// </summary>
    public enum SystemStoreLocationId : uint
    {
        //  Registry: HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE
        CERT_SYSTEM_STORE_CURRENT_USER_ID = 1,
        CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2,
        //  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services
        CERT_SYSTEM_STORE_CURRENT_SERVICE_ID = 4,
        CERT_SYSTEM_STORE_SERVICES_ID = 5,
        //  Registry: HKEY_USERS
        CERT_SYSTEM_STORE_USERS_ID = 6,

        //  Registry: HKEY_CURRENT_USER\Software\Policies\Microsoft\SystemCertificates
        CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID = 7,
        //  Registry: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\SystemCertificates
        CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID = 8,

        //  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\EnterpriseCertificates
        CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID = 9
    }
}
