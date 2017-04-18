// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379650.aspx
    // "Unable to retrieve" SIDs are ones that didn't succeed with CreateWellKnownSid on a non-domain joined Win10 box
    public enum WELL_KNOWN_SID_TYPE : uint
    {
        /// <summary>
        /// NULL SID (S-1-0-0) [SidTypeWellKnownGroup]
        /// </summary>
        WinNullSid = 0,

        /// <summary>
        /// Everyone (S-1-1-0) [SidTypeWellKnownGroup]
        /// </summary>
        WinWorldSid = 1,

        /// <summary>
        /// LOCAL (S-1-2-0) [SidTypeWellKnownGroup]
        /// </summary>
        WinLocalSid = 2,

        /// <summary>
        /// CREATOR OWNER (S-1-3-0) [SidTypeWellKnownGroup]
        /// </summary>
        WinCreatorOwnerSid = 3,

        /// <summary>
        /// CREATOR GROUP (S-1-3-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinCreatorGroupSid = 4,

        /// <summary>
        /// CREATOR OWNER SERVER (S-1-3-2) [SidTypeWellKnownGroup]
        /// </summary>
        WinCreatorOwnerServerSid = 5,

        /// <summary>
        /// CREATOR GROUP SERVER (S-1-3-3) [SidTypeWellKnownGroup]
        /// </summary>
        WinCreatorGroupServerSid = 6,

        /// <summary>
        /// NT Pseudo Domain (S-1-5) [SidTypeDomain]
        /// </summary>
        WinNtAuthoritySid = 7,

        /// <summary>
        /// DIALUP (S-1-5-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinDialupSid = 8,

        /// <summary>
        /// NETWORK (S-1-5-2) [SidTypeWellKnownGroup]
        /// </summary>
        WinNetworkSid = 9,

        /// <summary>
        /// BATCH (S-1-5-3) [SidTypeWellKnownGroup]
        /// </summary>
        WinBatchSid = 10,

        /// <summary>
        /// INTERACTIVE (S-1-5-4) [SidTypeWellKnownGroup]
        /// </summary>
        WinInteractiveSid = 11,

        /// <summary>
        /// SERVICE (S-1-5-6) [SidTypeWellKnownGroup]
        /// </summary>
        WinServiceSid = 12,

        /// <summary>
        /// ANONYMOUS LOGON (S-1-5-7) [SidTypeWellKnownGroup]
        /// </summary>
        WinAnonymousSid = 13,

        /// <summary>
        /// PROXY (S-1-5-8) [SidTypeWellKnownGroup]
        /// </summary>
        WinProxySid = 14,

        /// <summary>
        /// ENTERPRISE DOMAIN CONTROLLERS (S-1-5-9) [SidTypeWellKnownGroup]
        /// </summary>
        WinEnterpriseControllersSid = 15,

        /// <summary>
        /// SELF (S-1-5-10) [SidTypeWellKnownGroup]
        /// </summary>
        WinSelfSid = 16,

        /// <summary>
        /// Authenticated Users (S-1-5-11) [SidTypeWellKnownGroup]
        /// </summary>
        WinAuthenticatedUserSid = 17,

        /// <summary>
        /// RESTRICTED (S-1-5-12) [SidTypeWellKnownGroup]
        /// </summary>
        WinRestrictedCodeSid = 18,

        /// <summary>
        /// TERMINAL SERVER USER (S-1-5-13) [SidTypeWellKnownGroup]
        /// </summary>
        WinTerminalServerSid = 19,

        /// <summary>
        /// REMOTE INTERACTIVE LOGON (S-1-5-14) [SidTypeWellKnownGroup]
        /// </summary>
        WinRemoteLogonIdSid = 20,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinLogonIdsSid = 21,

        /// <summary>
        /// SYSTEM (S-1-5-18) [SidTypeWellKnownGroup]
        /// </summary>
        WinLocalSystemSid = 22,

        /// <summary>
        /// LOCAL SERVICE (S-1-5-19) [SidTypeWellKnownGroup]
        /// </summary>
        WinLocalServiceSid = 23,

        /// <summary>
        /// NETWORK SERVICE (S-1-5-20) [SidTypeWellKnownGroup]
        /// </summary>
        WinNetworkServiceSid = 24,

        /// <summary>
        /// BUILTIN (S-1-5-32) [SidTypeDomain]
        /// </summary>
        WinBuiltinDomainSid = 25,

        /// <summary>
        /// Administrators (S-1-5-32-544) [SidTypeAlias]
        /// </summary>
        WinBuiltinAdministratorsSid = 26,

        /// <summary>
        /// Users (S-1-5-32-545) [SidTypeAlias]
        /// </summary>
        WinBuiltinUsersSid = 27,

        /// <summary>
        /// Guests (S-1-5-32-546) [SidTypeAlias]
        /// </summary>
        WinBuiltinGuestsSid = 28,

        /// <summary>
        /// Power Users (S-1-5-32-547) [SidTypeAlias]
        /// </summary>
        WinBuiltinPowerUsersSid = 29,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinAccountOperatorsSid = 30,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinSystemOperatorsSid = 31,

        /// <summary>
        /// Unable to retrieve
        /// </summary>

        /// <summary>
        /// Backup Operators (S-1-5-32-551) [SidTypeAlias]
        /// </summary>
        WinBuiltinBackupOperatorsSid = 33,

        /// <summary>
        /// Replicator (S-1-5-32-552) [SidTypeAlias]
        /// </summary>
        WinBuiltinReplicatorSid = 34,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinPreWindows2000CompatibleAccessSid = 35,

        /// <summary>
        /// Remote Desktop Users (S-1-5-32-555) [SidTypeAlias]
        /// </summary>
        WinBuiltinRemoteDesktopUsersSid = 36,

        /// <summary>
        /// Network Configuration Operators (S-1-5-32-556) [SidTypeAlias]
        /// </summary>
        WinBuiltinNetworkConfigurationOperatorsSid = 37,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountAdministratorSid = 38,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountGuestSid = 39,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountKrbtgtSid = 40,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountDomainAdminsSid = 41,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountDomainUsersSid = 42,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountDomainGuestsSid = 43,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountComputersSid = 44,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountControllersSid = 45,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountCertAdminsSid = 46,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountSchemaAdminsSid = 47,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountEnterpriseAdminsSid = 48,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountPolicyAdminsSid = 49,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountRasAndIasServersSid = 50,

        /// <summary>
        /// NTLM Authentication (S-1-5-64-10) [SidTypeWellKnownGroup]
        /// </summary>
        WinNTLMAuthenticationSid = 51,

        /// <summary>
        /// Digest Authentication (S-1-5-64-21) [SidTypeWellKnownGroup]
        /// </summary>
        WinDigestAuthenticationSid = 52,

        /// <summary>
        /// SChannel Authentication (S-1-5-64-14) [SidTypeWellKnownGroup]
        /// </summary>
        WinSChannelAuthenticationSid = 53,

        /// <summary>
        /// This Organization (S-1-5-15) [SidTypeWellKnownGroup]
        /// </summary>
        WinThisOrganizationSid = 54,

        /// <summary>
        /// Other Organization (S-1-5-1000) [SidTypeWellKnownGroup]
        /// </summary>
        WinOtherOrganizationSid = 55,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinIncomingForestTrustBuildersSid = 56,

        /// <summary>
        /// Performance Monitor Users (S-1-5-32-558) [SidTypeAlias]
        /// </summary>
        WinBuiltinPerfMonitoringUsersSid = 57,

        /// <summary>
        /// Performance Log Users (S-1-5-32-559) [SidTypeAlias]
        /// </summary>
        WinBuiltinPerfLoggingUsersSid = 58,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinAuthorizationAccessSid = 59,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinTerminalServerLicenseServersSid = 60,

        /// <summary>
        /// Distributed COM Users (S-1-5-32-562) [SidTypeAlias]
        /// </summary>
        WinBuiltinDCOMUsersSid = 61,

        /// <summary>
        /// IIS_IUSRS (S-1-5-32-568) [SidTypeAlias]
        /// </summary>
        WinBuiltinIUsersSid = 62,

        /// <summary>
        /// IUSR (S-1-5-17) [SidTypeWellKnownGroup]
        /// </summary>
        WinIUserSid = 63,

        /// <summary>
        /// Cryptographic Operators (S-1-5-32-569) [SidTypeAlias]
        /// </summary>
        WinBuiltinCryptoOperatorsSid = 64,

        /// <summary>
        /// Untrusted Mandatory Level (S-1-16-0) [SidTypeLabel]
        /// </summary>
        WinUntrustedLabelSid = 65,

        /// <summary>
        /// Low Mandatory Level (S-1-16-4096) [SidTypeLabel]
        /// </summary>
        WinLowLabelSid = 66,

        /// <summary>
        /// Medium Mandatory Level (S-1-16-8192) [SidTypeLabel]
        /// </summary>
        WinMediumLabelSid = 67,

        /// <summary>
        /// High Mandatory Level (S-1-16-12288) [SidTypeLabel]
        /// </summary>
        WinHighLabelSid = 68,

        /// <summary>
        /// System Mandatory Level (S-1-16-16384) [SidTypeLabel]
        /// </summary>
        WinSystemLabelSid = 69,

        /// <summary>
        /// WRITE RESTRICTED (S-1-5-33) [SidTypeWellKnownGroup]
        /// </summary>
        WinWriteRestrictedCodeSid = 70,

        /// <summary>
        /// OWNER RIGHTS (S-1-3-4) [SidTypeWellKnownGroup]
        /// </summary>
        WinCreatorOwnerRightsSid = 71,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinCacheablePrincipalsGroupSid = 72,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinNonCacheablePrincipalsGroupSid = 73,

        /// <summary>
        /// ENTERPRISE READ-ONLY DOMAIN CONTROLLERS BETA (S-1-5-22) [SidTypeWellKnownGroup]
        /// </summary>
        WinEnterpriseReadonlyControllersSid = 74,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountReadonlyControllersSid = 75,

        /// <summary>
        /// Event Log Readers (S-1-5-32-573) [SidTypeAlias]
        /// </summary>
        WinBuiltinEventLogReadersGroup = 76,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinNewEnterpriseReadonlyControllersSid = 77,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinCertSvcDComAccessGroup = 78,

        /// <summary>
        /// Medium Plus Mandatory Level (S-1-16-8448) [SidTypeLabel]
        /// </summary>
        WinMediumPlusLabelSid = 79,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinLocalLogonSid = 80,

        /// <summary>
        /// CONSOLE LOGON (S-1-2-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinConsoleLogonSid = 81,

        /// <summary>
        /// This Organization Certificate (S-1-5-65-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinThisOrganizationCertificateSid = 82,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinApplicationPackageAuthoritySid = 83,

        /// <summary>
        /// ALL APPLICATION PACKAGES (S-1-15-2-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinBuiltinAnyPackageSid = 84,

        /// <summary>
        /// Your Internet connection (S-1-15-3-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityInternetClientSid = 85,

        /// <summary>
        /// Your Internet connection, including incoming connections from the Internet (S-1-15-3-2) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityInternetClientServerSid = 86,

        /// <summary>
        /// Your home or work networks (S-1-15-3-3) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityPrivateNetworkClientServerSid = 87,

        /// <summary>
        /// Your pictures library (S-1-15-3-4) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityPicturesLibrarySid = 88,

        /// <summary>
        /// Your videos library (S-1-15-3-5) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityVideosLibrarySid = 89,

        /// <summary>
        /// Your music library (S-1-15-3-6) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityMusicLibrarySid = 90,

        /// <summary>
        /// Your documents library (S-1-15-3-7) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityDocumentsLibrarySid = 91,

        /// <summary>
        /// Software and hardware certificates or a smart card (S-1-15-3-9) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilitySharedUserCertificatesSid = 92,

        /// <summary>
        /// Your Windows credentials (S-1-15-3-8) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityEnterpriseAuthenticationSid = 93,

        /// <summary>
        /// Removable storage (S-1-15-3-10) [SidTypeWellKnownGroup]
        /// </summary>
        WinCapabilityRemovableStorageSid = 94,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinRDSRemoteAccessServersSid = 95,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinRDSEndpointServersSid = 96,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinBuiltinRDSManagementServersSid = 97,

        /// <summary>
        /// USER MODE DRIVERS (S-1-5-84-0-0-0-0-0) [SidTypeWellKnownGroup]
        /// </summary>
        WinUserModeDriversSid = 98,

        /// <summary>
        /// Hyper-V Administrators (S-1-5-32-578) [SidTypeAlias]
        /// </summary>
        WinBuiltinHyperVAdminsSid = 99,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountCloneableControllersSid = 100,

        /// <summary>
        /// Access Control Assistance Operators (S-1-5-32-579) [SidTypeAlias]
        /// </summary>
        WinBuiltinAccessControlAssistanceOperatorsSid = 101,

        /// <summary>
        /// Remote Management Users (S-1-5-32-580) [SidTypeAlias]
        /// </summary>
        WinBuiltinRemoteManagementUsersSid = 102,

        /// <summary>
        /// Authentication authority asserted identity (S-1-18-1) [SidTypeWellKnownGroup]
        /// </summary>
        WinAuthenticationAuthorityAssertedSid = 103,

        /// <summary>
        /// Service asserted identity (S-1-18-2) [SidTypeWellKnownGroup]
        /// </summary>
        WinAuthenticationServiceAssertedSid = 104,

        /// <summary>
        /// Local account (S-1-5-113) [SidTypeWellKnownGroup]
        /// </summary>
        WinLocalAccountSid = 105,

        /// <summary>
        /// Local account and member of Administrators group (S-1-5-114) [SidTypeWellKnownGroup]
        /// </summary>
        WinLocalAccountAndAdministratorSid = 106,

        /// <summary>
        /// Unable to retrieve
        /// </summary>
        WinAccountProtectedUsersSid = 107
    }
}
