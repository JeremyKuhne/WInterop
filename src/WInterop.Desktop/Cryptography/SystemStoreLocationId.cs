// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography
{
    /// <summary>
    /// From wincrypt.h, used by crypto <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa376058.aspx">CertEnumSystemStore</a> function.
    /// </summary>
    public enum SystemStoreLocationId : uint
    {
        /// <summary>
        /// Registry: HKEY_CURRENT_USER [CERT_SYSTEM_STORE_CURRENT_USER_ID]
        /// </summary>
        CurrentUser = 1,

        /// <summary>
        /// Registry: HKEY_LOCAL_MACHINE [CERT_SYSTEM_STORE_LOCAL_MACHINE_ID]
        /// </summary>
        LocalMachine = 2,

        /// <summary>
        /// Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services [CERT_SYSTEM_STORE_CURRENT_SERVICE_ID]
        /// </summary>
        CurrentService = 4,

        /// <summary>
        /// Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services [CERT_SYSTEM_STORE_SERVICES_ID]
        /// </summary>
        Services = 5,

        /// <summary>
        /// Registry: HKEY_USERS [CERT_SYSTEM_STORE_USERS_ID]
        /// </summary>
        Users = 6,

        /// <summary>
        /// Registry: HKEY_CURRENT_USER\Software\Policies\Microsoft\SystemCertificates
        /// [CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID]
        /// </summary>
        CurrentUserGroupPolicy = 7,

        /// <summary>
        /// Registry: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\SystemCertificates
        /// [CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID]
        /// </summary>
        LocalMachineGroupPolicy = 8,

        /// <summary>
        /// Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\EnterpriseCertificates
        /// [CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID]
        /// </summary>
        LocalMachineEnterprise = 9
    }
}
