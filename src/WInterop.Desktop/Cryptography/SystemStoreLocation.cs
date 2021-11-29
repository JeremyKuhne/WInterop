// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography;

/// <summary>
///  From wincrypt.h
/// </summary>
public enum SystemStoreLocation : uint
{
    /// <summary>
    ///  Registry: HKEY_CURRENT_USER [CERT_SYSTEM_STORE_CURRENT_USER]
    /// </summary>
    CurrentUser = SystemStoreLocationId.CurrentUser << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_LOCAL_MACHINE [CERT_SYSTEM_STORE_LOCAL_MACHINE]
    /// </summary>
    LocalMachine = SystemStoreLocationId.LocalMachine << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services [CERT_SYSTEM_STORE_CURRENT_SERVICE]
    /// </summary>
    CurrentService = SystemStoreLocationId.CurrentService << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Services [CERT_SYSTEM_STORE_SERVICES]
    /// </summary>
    Services = SystemStoreLocationId.Services << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_USERS [CERT_SYSTEM_STORE_USERS]
    /// </summary>
    Users = SystemStoreLocationId.Users << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_CURRENT_USER\Software\Policies\Microsoft\SystemCertificates
    ///  [CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY]
    /// </summary>
    CurrentUserGroupPolicy = SystemStoreLocationId.CurrentUserGroupPolicy << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\SystemCertificates
    ///  [CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY]
    /// </summary>
    LocalMachineGroupPolicy = SystemStoreLocationId.LocalMachineGroupPolicy << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT,

    /// <summary>
    ///  Registry: HKEY_LOCAL_MACHINE\Software\Microsoft\EnterpriseCertificates
    ///  [CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE]
    /// </summary>
    LocalMachineEnterprise = SystemStoreLocationId.LocalMachineEnterprise << CryptoDefines.CERT_SYSTEM_STORE_LOCATION_SHIFT
}