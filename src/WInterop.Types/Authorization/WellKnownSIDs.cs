// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// Well known security identifiers in Windows. [WELL_KNOWN_SID_TYPE]
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379650.aspx"/>
    /// </summary>
    public enum WellKnownSID : uint
    {
        // Info was populated by using CreateWellKnownSid on a non-domain joined Win10 box.
        // As such domain only accounts don't have full information yet.

        /// <summary>
        /// NULL SID (S-1-0-0) [WinNullSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Null = 0,

        /// <summary>
        /// Everyone (S-1-1-0) [WinWorldSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        World = 1,

        /// <summary>
        /// LOCAL (S-1-2-0) [WinLocalSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Local = 2,

        /// <summary>
        /// CREATOR OWNER (S-1-3-0) [WinCreatorOwnerSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        CreatorOwner = 3,

        /// <summary>
        /// CREATOR GROUP (S-1-3-1) [WinCreatorGroupSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        CreatorGroup = 4,

        /// <summary>
        /// CREATOR OWNER SERVER (S-1-3-2) [WinCreatorOwnerServerSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        CreatorOwnerServer = 5,

        /// <summary>
        /// CREATOR GROUP SERVER (S-1-3-3) [WinCreatorGroupServerSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        CreatorGroupServer = 6,

        /// <summary>
        /// NT Pseudo Domain (S-1-5) [WinNtAuthoritySid] <see cref="SidNameUse.Domain"/>
        /// </summary>
        NTAuthority = 7,

        /// <summary>
        /// DIALUP (S-1-5-1) [WinDialupSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Dialup = 8,

        /// <summary>
        /// NETWORK (S-1-5-2) [WinNetworkSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Network = 9,

        /// <summary>
        /// BATCH (S-1-5-3) [WinBatchSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Batch = 10,

        /// <summary>
        /// INTERACTIVE (S-1-5-4) [WinInteractiveSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Interactive = 11,

        /// <summary>
        /// SERVICE (S-1-5-6) [WinServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Service = 12,

        /// <summary>
        /// ANONYMOUS LOGON (S-1-5-7) [WinAnonymousSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Anonymous = 13,

        /// <summary>
        /// PROXY (S-1-5-8) [WinProxySid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Proxy = 14,

        /// <summary>
        /// ENTERPRISE DOMAIN CONTROLLERS (S-1-5-9) [WinEnterpriseControllersSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        EnterpriseControllers = 15,

        /// <summary>
        /// SELF (S-1-5-10) [WinSelfSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        Self = 16,

        /// <summary>
        /// Authenticated Users (S-1-5-11) [WinAuthenticatedUserSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        AuthenticatedUsers = 17,

        /// <summary>
        /// RESTRICTED (S-1-5-12) [WinRestrictedCodeSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        RestrictedCode = 18,

        /// <summary>
        /// TERMINAL SERVER USER (S-1-5-13) [WinTerminalServerSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        TerminalServerUser = 19,

        /// <summary>
        /// REMOTE INTERACTIVE LOGON (S-1-5-14) [WinRemoteLogonIdSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        RemoteLogon = 20,

        /// <summary>
        /// [WinLogonIdsSid]
        /// </summary>
        Logon = 21,

        /// <summary>
        /// SYSTEM (S-1-5-18) [WinLocalSystemSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        LocalSystem = 22,

        /// <summary>
        /// LOCAL SERVICE (S-1-5-19) [WinLocalServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        LocalService = 23,

        /// <summary>
        /// NETWORK SERVICE (S-1-5-20) [WinNetworkServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        NetworkService = 24,

        /// <summary>
        /// BUILTIN (S-1-5-32) [WinBuiltinDomainSid] <see cref="SidNameUse.Domain"/>
        /// </summary>
        Domain = 25,

        /// <summary>
        /// Administrators (S-1-5-32-544) [WinBuiltinAdministratorsSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        Administrators = 26,

        /// <summary>
        /// Users (S-1-5-32-545) [WinBuiltinUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        Users = 27,

        /// <summary>
        /// Guests (S-1-5-32-546) [WinBuiltinGuestsSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        Guests = 28,

        /// <summary>
        /// Power Users (S-1-5-32-547) [WinBuiltinPowerUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        PowerUsers = 29,

        /// <summary>
        /// [WinBuiltinAccountOperatorsSid]
        /// </summary>
        AccountOperators = 30,

        /// <summary>
        /// [WinBuiltinSystemOperatorsSid]
        /// </summary>
        SystemOperators = 31,

        /// <summary>
        /// Backup Operators (S-1-5-32-551) [WinBuiltinBackupOperatorsSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        BackupOperatorsGroup = 33,

        /// <summary>
        /// Replicator (S-1-5-32-552) [WinBuiltinReplicatorSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        Replicator = 34,

        /// <summary>
        /// [WinBuiltinPreWindows2000CompatibleAccessSid]
        /// </summary>
        PreWindows2000CompatibleAccess = 35,

        /// <summary>
        /// Remote Desktop Users (S-1-5-32-555) [WinBuiltinRemoteDesktopUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        RemoteDesktopUsers = 36,

        /// <summary>
        /// Network Configuration Operators (S-1-5-32-556) [WinBuiltinNetworkConfigurationOperatorsSid]
        /// <see cref="SidNameUse.Alias"/>
        /// </summary>
        NetworkConfigurationOperators = 37,

        /// <summary>
        /// [WinAccountAdministratorSid]
        /// </summary>
        AccountAdministrator = 38,

        /// <summary>
        /// [WinAccountGuestSid]
        /// </summary>
        Guest = 39,

        /// <summary>
        /// [WinAccountKrbtgtSid]
        /// </summary>
        KerberosTargetGroup = 40,

        /// <summary>
        /// [WinAccountDomainAdminsSid]
        /// </summary>
        DomainAdmins = 41,

        /// <summary>
        /// [WinAccountDomainUsersSid]
        /// </summary>
        DomainUsers = 42,

        /// <summary>
        /// [WinAccountDomainGuestsSid]
        /// </summary>
        DomainGuests = 43,

        /// <summary>
        /// [WinAccountComputersSid]
        /// </summary>
        Computers = 44,

        /// <summary>
        /// [WinAccountControllersSid]
        /// </summary>
        Controllers = 45,

        /// <summary>
        /// [WinAccountCertAdminsSid]
        /// </summary>
        CertAdmins = 46,

        /// <summary>
        /// [WinAccountSchemaAdminsSid]
        /// </summary>
        SchemaAdmins = 47,

        /// <summary>
        /// [WinAccountEnterpriseAdminsSid]
        /// </summary>
        EnterpriseAdmins = 48,

        /// <summary>
        /// [WinAccountPolicyAdminsSid]
        /// </summary>
        PolicyAdmins = 49,

        /// <summary>
        /// [WinAccountRasAndIasServersSid]
        /// </summary>
        RasAndIasServers = 50,

        /// <summary>
        /// NTLM Authentication (S-1-5-64-10) [WinNTLMAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        NTLMAuthentication = 51,

        /// <summary>
        /// Digest Authentication (S-1-5-64-21) [WinDigestAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        DigestAuthentication = 52,

        /// <summary>
        /// SChannel Authentication (S-1-5-64-14) [WinSChannelAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        SChannelAuthentication = 53,

        /// <summary>
        /// This Organization (S-1-5-15) [WinThisOrganizationSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        ThisOrganization = 54,

        /// <summary>
        /// Other Organization (S-1-5-1000) [WinOtherOrganizationSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        OtherOrganization = 55,

        /// <summary>
        /// [WinBuiltinIncomingForestTrustBuildersSid]
        /// </summary>
        IncomingForestTrustBuilders = 56,

        /// <summary>
        /// Performance Monitor Users (S-1-5-32-558) [WinBuiltinPerfMonitoringUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        PerfMonitoringUsers = 57,

        /// <summary>
        /// Performance Log Users (S-1-5-32-559) [WinBuiltinPerfLoggingUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        PerfLoggingUsers = 58,

        /// <summary>
        /// [WinBuiltinAuthorizationAccessSid]
        /// </summary>
        AuthorizationAccess = 59,

        /// <summary>
        /// [WinBuiltinTerminalServerLicenseServersSid]
        /// </summary>
        TerminalServerLicenseServers = 60,

        /// <summary>
        /// Distributed COM Users (S-1-5-32-562) [WinBuiltinDCOMUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        DCOMUsers = 61,

        /// <summary>
        /// IIS_IUSRS (S-1-5-32-568) [WinBuiltinIUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        IISUsers = 62,

        /// <summary>
        /// IUSR (S-1-5-17) [WinIUserSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        IISUser = 63,

        /// <summary>
        /// Cryptographic Operators (S-1-5-32-569) [WinBuiltinCryptoOperatorsSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        CryptoOperators = 64,

        /// <summary>
        /// Untrusted Mandatory Level (S-1-16-0) [WinUntrustedLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        UntrustedLabel = 65,

        /// <summary>
        /// Low Mandatory Level (S-1-16-4096) [WinLowLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        LowLabel = 66,

        /// <summary>
        /// Medium Mandatory Level (S-1-16-8192) [WinMediumLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        MediumLabel = 67,

        /// <summary>
        /// High Mandatory Level (S-1-16-12288) [WinHighLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        HighLabel = 68,

        /// <summary>
        /// System Mandatory Level (S-1-16-16384) [WinSystemLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        SystemLabel = 69,

        /// <summary>
        /// WRITE RESTRICTED (S-1-5-33) [WinWriteRestrictedCodeSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        WriteRestrictedCode = 70,

        /// <summary>
        /// OWNER RIGHTS (S-1-3-4) [WinCreatorOwnerRightsSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        CreatorOwnerRights = 71,

        /// <summary>
        /// [WinCacheablePrincipalsGroupSid]
        /// </summary>
        CacheablePrincipals = 72,

        /// <summary>
        /// [WinNonCacheablePrincipalsGroupSid]
        /// </summary>
        NonCacheablePrincipals = 73,

        /// <summary>
        /// ENTERPRISE READ-ONLY DOMAIN CONTROLLERS BETA (S-1-5-22) [WinEnterpriseReadonlyControllersSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        EnterpriseReadonlyControllers = 74,

        /// <summary>
        /// [WinAccountReadonlyControllersSid]
        /// </summary>
        ReadonlyControllers = 75,

        /// <summary>
        /// Event Log Readers (S-1-5-32-573) [WinBuiltinEventLogReadersGroup] <see cref="SidNameUse.Alias"/>
        /// </summary>
        EventLogReaders = 76,

        /// <summary>
        /// [WinNewEnterpriseReadonlyControllersSid]
        /// </summary>
        NewEnterpriseReadonlyControllers = 77,

        /// <summary>
        /// [WinBuiltinCertSvcDComAccessGroup]
        /// </summary>
        CertSvcDComAccess = 78,

        /// <summary>
        /// Medium Plus Mandatory Level (S-1-16-8448) [WinMediumPlusLabelSid] <see cref="SidNameUse.Label"/>
        /// </summary>
        MediumPlusLabel = 79,

        /// <summary>
        /// [WinLocalLogonSid]
        /// </summary>
        LocalLogon = 80,

        /// <summary>
        /// CONSOLE LOGON (S-1-2-1) [WinConsoleLogonSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        ConsoleLogon = 81,

        /// <summary>
        /// This Organization Certificate (S-1-5-65-1) [WinThisOrganizationCertificateSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        ThisOrganizationCertificate = 82,

        /// <summary>
        /// [WinApplicationPackageAuthoritySid]
        /// </summary>
        ApplicationPackageAuthority = 83,

        /// <summary>
        /// ALL APPLICATION PACKAGES (S-1-15-2-1) [WinBuiltinAnyPackageSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        AllApplicationPackages = 84,

        /// <summary>
        /// Your Internet connection (S-1-15-3-1) [WinCapabilityInternetClientSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        InternetClientCapability = 85,

        /// <summary>
        /// Your Internet connection, including incoming connections from the Internet (S-1-15-3-2)
        /// [WinCapabilityInternetClientServerSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        InternetClientServerCapability = 86,

        /// <summary>
        /// Your home or work networks (S-1-15-3-3) [WinCapabilityPrivateNetworkClientServerSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        PrivateNetworkClientServerCapability = 87,

        /// <summary>
        /// Your pictures library (S-1-15-3-4) [WinCapabilityPicturesLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        PicturesLibraryCapability = 88,

        /// <summary>
        /// Your videos library (S-1-15-3-5) [WinCapabilityVideosLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        VideosLibraryCapability = 89,

        /// <summary>
        /// Your music library (S-1-15-3-6) [WinCapabilityMusicLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        MusicLibraryCapability = 90,

        /// <summary>
        /// Your documents library (S-1-15-3-7) [WinCapabilityDocumentsLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        DocumentsLibraryCapability = 91,

        /// <summary>
        /// Software and hardware certificates or a smart card (S-1-15-3-9) [WinCapabilitySharedUserCertificatesSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        SharedUserCertificatesCapability = 92,

        /// <summary>
        /// Your Windows credentials (S-1-15-3-8) [WinCapabilityEnterpriseAuthenticationSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        EnterpriseAuthenticationCapability = 93,

        /// <summary>
        /// Removable storage (S-1-15-3-10) [WinCapabilityRemovableStorageSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        RemovableStorageCapability = 94,

        /// <summary>
        /// [WinBuiltinRDSRemoteAccessServersSid]
        /// </summary>
        RDSRemoteAccessServers = 95,

        /// <summary>
        /// [WinBuiltinRDSEndpointServersSid]
        /// </summary>
        RDSEndpointServers = 96,

        /// <summary>
        /// [WinBuiltinRDSManagementServersSid]
        /// </summary>
        RDSManagementServers = 97,

        /// <summary>
        /// USER MODE DRIVERS (S-1-5-84-0-0-0-0-0) [WinUserModeDriversSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        UserModeDrivers = 98,

        /// <summary>
        /// Hyper-V Administrators (S-1-5-32-578) [WinBuiltinHyperVAdminsSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        HyperVAdmins = 99,

        /// <summary>
        /// [WinAccountCloneableControllersSid]
        /// </summary>
        AccountCloneableControllers = 100,

        /// <summary>
        /// Access Control Assistance Operators (S-1-5-32-579) [WinBuiltinAccessControlAssistanceOperatorsSid]
        /// <see cref="SidNameUse.Alias"/>
        /// </summary>
        AccessControlAssistanceOperators = 101,

        /// <summary>
        /// Remote Management Users (S-1-5-32-580) [WinBuiltinRemoteManagementUsersSid] <see cref="SidNameUse.Alias"/>
        /// </summary>
        RemoteManagementUsers = 102,

        /// <summary>
        /// Authentication authority asserted identity (S-1-18-1) [WinAuthenticationAuthorityAssertedSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        AuthenticationAuthorityAsserted = 103,

        /// <summary>
        /// Service asserted identity (S-1-18-2) [WinAuthenticationServiceAssertedSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        AuthenticationServiceAsserted = 104,

        /// <summary>
        /// Local account (S-1-5-113) [WinLocalAccountSid] <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        LocalAccount = 105,

        /// <summary>
        /// Local account and member of Administrators group (S-1-5-114) [WinLocalAccountAndAdministratorSid]
        /// <see cref="SidNameUse.WellKnownGroup"/>
        /// </summary>
        LocalAccountAndAdministrator = 106,

        /// <summary>
        /// [WinAccountProtectedUsersSid]
        /// </summary>
        ProtectedUsers = 107
    }
}
