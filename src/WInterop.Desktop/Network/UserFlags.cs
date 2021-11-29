// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Network;

[Flags]
public enum UserFlags : uint
{
    /// <summary>
    ///  [UF_SCRIPT]
    /// </summary>
    Script = 0x0001,

    /// <summary>
    ///  [UF_ACCOUNTDISABLE]
    /// </summary>
    AccountDisable = 0x0002,

    /// <summary>
    ///  [UF_HOMEDIR_REQUIRED]
    /// </summary>
    HomeDirectoryRequired = 0x0008,

    /// <summary>
    ///  [UF_LOCKOUT]
    /// </summary>
    Lockout = 0x0010,

    /// <summary>
    ///  [UF_PASSWD_NOTREQD]
    /// </summary>
    PasswordNotRequired = 0x0020,

    /// <summary>
    ///  [UF_PASSWD_CANT_CHANGE]
    /// </summary>
    PasswordCantChange = 0x0040,

    /// <summary>
    ///  [UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED]
    /// </summary>
    EncryptedTextPasswordAllowed = 0x0080,

    /// <summary>
    ///  [UF_TEMP_DUPLICATE_ACCOUNT]
    /// </summary>
    TempDuplicateAccount = 0x0100,

    /// <summary>
    ///  [UF_NORMAL_ACCOUNT]
    /// </summary>
    NormalAccount = 0x0200,

    /// <summary>
    ///  [UF_INTERDOMAIN_TRUST_ACCOUNT]
    /// </summary>
    InterdomainTrustAccount = 0x0800,

    /// <summary>
    ///  [UF_WORKSTATION_TRUST_ACCOUNT]
    /// </summary>
    WorkstationTrustAccount = 0x1000,

    /// <summary>
    ///  [UF_SERVER_TRUST_ACCOUNT]
    /// </summary>
    ServerTrustAccount = 0x2000,

    /// <summary>
    ///  [UF_DONT_EXPIRE_PASSWD]
    /// </summary>
    DontExpirePassword = 0x10000,

    /// <summary>
    ///  [UF_MNS_LOGON_ACCOUNT]
    /// </summary>
    MnsLogonAccount = 0x20000,

    /// <summary>
    ///  [UF_SMARTCARD_REQUIRED]
    /// </summary>
    SmartcardRequired = 0x40000,

    /// <summary>
    ///  [UF_TRUSTED_FOR_DELEGATION]
    /// </summary>
    TrustedForDelegation = 0x80000,

    /// <summary>
    ///  [UF_NOT_DELEGATED]
    /// </summary>
    NotDelegated = 0x100000,

    /// <summary>
    ///  [UF_USE_DES_KEY_ONLY]
    /// </summary>
    UseDesKeyOnly = 0x200000,

    /// <summary>
    ///  [UF_DONT_REQUIRE_PREAUTH]
    /// </summary>
    DontRequirePreauthorization = 0x400000,

    /// <summary>
    ///  [UF_PASSWORD_EXPIRED]
    /// </summary>
    PasswordExpired = 0x800000,

    /// <summary>
    ///  [UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION]
    /// </summary>
    TrustedToAuthenticateForDelegation = 0x1000000,

    /// <summary>
    ///  [UF_NO_AUTH_DATA_REQUIRED]
    /// </summary>
    NoAuthorizationDataRequired = 0x2000000,

    /// <summary>
    ///  [UF_PARTIAL_SECRETS_ACCOUNT]
    /// </summary>
    PartialSecretsAccount = 0x4000000,

    /// <summary>
    ///  [UF_USE_AES_KEYS]
    /// </summary>
    UseAesKeys = 0x8000000
}