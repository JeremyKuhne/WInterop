// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
///  Well known security identifiers in Windows. [WELL_KNOWN_SID_TYPE]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/winnt/ne-winnt-well_known_sid_type
/// </docs>
public enum WellKnownSID : uint
{
    // Info was populated by using CreateWellKnownSid on a non-domain joined Win10 box.
    // As such domain only accounts don't have full information yet.

    /// <summary>
    ///  NULL SID (S-1-0-0) [WinNullSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Null = WELL_KNOWN_SID_TYPE.WinNullSid,

    /// <summary>
    ///  Everyone (S-1-1-0) [WinWorldSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    World = WELL_KNOWN_SID_TYPE.WinWorldSid,

    /// <summary>
    ///  LOCAL (S-1-2-0) [WinLocalSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Local = WELL_KNOWN_SID_TYPE.WinLocalSid,

    /// <summary>
    ///  CREATOR OWNER (S-1-3-0) [WinCreatorOwnerSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    CreatorOwner = WELL_KNOWN_SID_TYPE.WinCreatorOwnerSid,

    /// <summary>
    ///  CREATOR GROUP (S-1-3-1) [WinCreatorGroupSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    CreatorGroup = WELL_KNOWN_SID_TYPE.WinCreatorGroupSid,

    /// <summary>
    ///  CREATOR OWNER SERVER (S-1-3-2) [WinCreatorOwnerServerSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    CreatorOwnerServer = WELL_KNOWN_SID_TYPE.WinCreatorOwnerServerSid,

    /// <summary>
    ///  CREATOR GROUP SERVER (S-1-3-3) [WinCreatorGroupServerSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    CreatorGroupServer = WELL_KNOWN_SID_TYPE.WinCreatorGroupServerSid,

    /// <summary>
    ///  NT Pseudo Domain (S-1-5) [WinNtAuthoritySid] <see cref="SidNameUse.Domain"/>
    /// </summary>
    NTAuthority = WELL_KNOWN_SID_TYPE.WinNtAuthoritySid,

    /// <summary>
    ///  DIALUP (S-1-5-1) [WinDialupSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Dialup = WELL_KNOWN_SID_TYPE.WinDialupSid,

    /// <summary>
    ///  NETWORK (S-1-5-2) [WinNetworkSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Network = WELL_KNOWN_SID_TYPE.WinNetworkSid,

    /// <summary>
    ///  BATCH (S-1-5-3) [WinBatchSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Batch = WELL_KNOWN_SID_TYPE.WinBatchSid,

    /// <summary>
    ///  INTERACTIVE (S-1-5-4) [WinInteractiveSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Interactive = WELL_KNOWN_SID_TYPE.WinInteractiveSid,

    /// <summary>
    ///  SERVICE (S-1-5-6) [WinServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Service = WELL_KNOWN_SID_TYPE.WinServiceSid,

    /// <summary>
    ///  ANONYMOUS LOGON (S-1-5-7) [WinAnonymousSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Anonymous = WELL_KNOWN_SID_TYPE.WinAnonymousSid,

    /// <summary>
    ///  PROXY (S-1-5-8) [WinProxySid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Proxy = WELL_KNOWN_SID_TYPE.WinProxySid,

    /// <summary>
    ///  ENTERPRISE DOMAIN CONTROLLERS (S-1-5-9) [WinEnterpriseControllersSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    EnterpriseControllers = WELL_KNOWN_SID_TYPE.WinEnterpriseControllersSid,

    /// <summary>
    ///  SELF (S-1-5-10) [WinSelfSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    Self = WELL_KNOWN_SID_TYPE.WinSelfSid,

    /// <summary>
    ///  Authenticated Users (S-1-5-11) [WinAuthenticatedUserSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    AuthenticatedUsers = WELL_KNOWN_SID_TYPE.WinAuthenticatedUserSid,

    /// <summary>
    ///  RESTRICTED (S-1-5-12) [WinRestrictedCodeSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    RestrictedCode = WELL_KNOWN_SID_TYPE.WinRestrictedCodeSid,

    /// <summary>
    ///  TERMINAL SERVER USER (S-1-5-13) [WinTerminalServerSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    TerminalServerUser = WELL_KNOWN_SID_TYPE.WinTerminalServerSid,

    /// <summary>
    ///  REMOTE INTERACTIVE LOGON (S-1-5-14) [WinRemoteLogonIdSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    RemoteLogon = WELL_KNOWN_SID_TYPE.WinRemoteLogonIdSid,

    /// <summary>
    ///  [WinLogonIdsSid]
    /// </summary>
    Logon = WELL_KNOWN_SID_TYPE.WinLogonIdsSid,

    /// <summary>
    ///  SYSTEM (S-1-5-18) [WinLocalSystemSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    LocalSystem = WELL_KNOWN_SID_TYPE.WinLocalSystemSid,

    /// <summary>
    ///  LOCAL SERVICE (S-1-5-19) [WinLocalServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    LocalService = WELL_KNOWN_SID_TYPE.WinLocalServiceSid,

    /// <summary>
    ///  NETWORK SERVICE (S-1-5-20) [WinNetworkServiceSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    NetworkService = WELL_KNOWN_SID_TYPE.WinNetworkServiceSid,

    /// <summary>
    ///  BUILTIN (S-1-5-32) [WinBuiltinDomainSid] <see cref="SidNameUse.Domain"/>
    /// </summary>
    Domain = WELL_KNOWN_SID_TYPE.WinBuiltinDomainSid,

    /// <summary>
    ///  Administrators (S-1-5-32-544) [WinBuiltinAdministratorsSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    Administrators = WELL_KNOWN_SID_TYPE.WinBuiltinAdministratorsSid,

    /// <summary>
    ///  Users (S-1-5-32-545) [WinBuiltinUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    Users = WELL_KNOWN_SID_TYPE.WinBuiltinUsersSid,

    /// <summary>
    ///  Guests (S-1-5-32-546) [WinBuiltinGuestsSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    Guests = WELL_KNOWN_SID_TYPE.WinBuiltinGuestsSid,

    /// <summary>
    ///  Power Users (S-1-5-32-547) [WinBuiltinPowerUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    PowerUsers = WELL_KNOWN_SID_TYPE.WinBuiltinPowerUsersSid,

    /// <summary>
    ///  [WinBuiltinAccountOperatorsSid]
    /// </summary>
    AccountOperators = WELL_KNOWN_SID_TYPE.WinBuiltinAccountOperatorsSid,

    /// <summary>
    ///  [WinBuiltinSystemOperatorsSid]
    /// </summary>
    SystemOperators = WELL_KNOWN_SID_TYPE.WinBuiltinSystemOperatorsSid,

    /// <summary>
    ///  Backup Operators (S-1-5-32-551) [WinBuiltinBackupOperatorsSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    BackupOperatorsGroup = WELL_KNOWN_SID_TYPE.WinBuiltinBackupOperatorsSid,

    /// <summary>
    ///  Replicator (S-1-5-32-552) [WinBuiltinReplicatorSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    Replicator = WELL_KNOWN_SID_TYPE.WinBuiltinReplicatorSid,

    /// <summary>
    ///  [WinBuiltinPreWindows2000CompatibleAccessSid]
    /// </summary>
    PreWindows2000CompatibleAccess = WELL_KNOWN_SID_TYPE.WinBuiltinPreWindows2000CompatibleAccessSid,

    /// <summary>
    ///  Remote Desktop Users (S-1-5-32-555) [WinBuiltinRemoteDesktopUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    RemoteDesktopUsers = WELL_KNOWN_SID_TYPE.WinBuiltinRemoteDesktopUsersSid,

    /// <summary>
    ///  Network Configuration Operators (S-1-5-32-556) [WinBuiltinNetworkConfigurationOperatorsSid]
    /// <see cref="SidNameUse.Alias"/>
    /// </summary>
    NetworkConfigurationOperators = WELL_KNOWN_SID_TYPE.WinBuiltinNetworkConfigurationOperatorsSid,

    /// <summary>
    ///  [WinAccountAdministratorSid]
    /// </summary>
    AccountAdministrator = WELL_KNOWN_SID_TYPE.WinAccountAdministratorSid,

    /// <summary>
    ///  [WinAccountGuestSid]
    /// </summary>
    Guest = WELL_KNOWN_SID_TYPE.WinAccountGuestSid,

    /// <summary>
    ///  [WinAccountKrbtgtSid]
    /// </summary>
    KerberosTargetGroup = WELL_KNOWN_SID_TYPE.WinAccountKrbtgtSid,

    /// <summary>
    ///  [WinAccountDomainAdminsSid]
    /// </summary>
    DomainAdmins = WELL_KNOWN_SID_TYPE.WinAccountDomainAdminsSid,

    /// <summary>
    ///  [WinAccountDomainUsersSid]
    /// </summary>
    DomainUsers = WELL_KNOWN_SID_TYPE.WinAccountDomainUsersSid,

    /// <summary>
    ///  [WinAccountDomainGuestsSid]
    /// </summary>
    DomainGuests = WELL_KNOWN_SID_TYPE.WinAccountDomainGuestsSid,

    /// <summary>
    ///  [WinAccountComputersSid]
    /// </summary>
    Computers = WELL_KNOWN_SID_TYPE.WinAccountComputersSid,

    /// <summary>
    ///  [WinAccountControllersSid]
    /// </summary>
    Controllers = WELL_KNOWN_SID_TYPE.WinAccountControllersSid,

    /// <summary>
    ///  [WinAccountCertAdminsSid]
    /// </summary>
    CertificateAdmins = WELL_KNOWN_SID_TYPE.WinAccountCertAdminsSid,

    /// <summary>
    ///  [WinAccountSchemaAdminsSid]
    /// </summary>
    SchemaAdmins = WELL_KNOWN_SID_TYPE.WinAccountSchemaAdminsSid,

    /// <summary>
    ///  [WinAccountEnterpriseAdminsSid]
    /// </summary>
    EnterpriseAdmins = WELL_KNOWN_SID_TYPE.WinAccountEnterpriseAdminsSid,

    /// <summary>
    ///  [WinAccountPolicyAdminsSid]
    /// </summary>
    PolicyAdmins = WELL_KNOWN_SID_TYPE.WinAccountPolicyAdminsSid,

    /// <summary>
    ///  [WinAccountRasAndIasServersSid]
    /// </summary>
    RasAndIasServers = WELL_KNOWN_SID_TYPE.WinAccountRasAndIasServersSid,

    /// <summary>
    ///  NTLM Authentication (S-1-5-64-10) [WinNTLMAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    NTLMAuthentication = WELL_KNOWN_SID_TYPE.WinNTLMAuthenticationSid,

    /// <summary>
    ///  Digest Authentication (S-1-5-64-21) [WinDigestAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    DigestAuthentication = WELL_KNOWN_SID_TYPE.WinDigestAuthenticationSid,

    /// <summary>
    ///  SChannel Authentication (S-1-5-64-14) [WinSChannelAuthenticationSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    SChannelAuthentication = WELL_KNOWN_SID_TYPE.WinSChannelAuthenticationSid,

    /// <summary>
    ///  This Organization (S-1-5-15) [WinThisOrganizationSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    ThisOrganization = WELL_KNOWN_SID_TYPE.WinThisOrganizationSid,

    /// <summary>
    ///  Other Organization (S-1-5-1000) [WinOtherOrganizationSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    OtherOrganization = WELL_KNOWN_SID_TYPE.WinOtherOrganizationSid,

    /// <summary>
    ///  [WinBuiltinIncomingForestTrustBuildersSid]
    /// </summary>
    IncomingForestTrustBuilders = WELL_KNOWN_SID_TYPE.WinBuiltinIncomingForestTrustBuildersSid,

    /// <summary>
    ///  Performance Monitor Users (S-1-5-32-558) [WinBuiltinPerfMonitoringUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    PerfMonitoringUsers = WELL_KNOWN_SID_TYPE.WinBuiltinPerfMonitoringUsersSid,

    /// <summary>
    ///  Performance Log Users (S-1-5-32-559) [WinBuiltinPerfLoggingUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    PerfLoggingUsers = WELL_KNOWN_SID_TYPE.WinBuiltinPerfLoggingUsersSid,

    /// <summary>
    ///  [WinBuiltinAuthorizationAccessSid]
    /// </summary>
    AuthorizationAccess = WELL_KNOWN_SID_TYPE.WinBuiltinAuthorizationAccessSid,

    /// <summary>
    ///  [WinBuiltinTerminalServerLicenseServersSid]
    /// </summary>
    TerminalServerLicenseServers = WELL_KNOWN_SID_TYPE.WinBuiltinTerminalServerLicenseServersSid,

    /// <summary>
    ///  Distributed COM Users (S-1-5-32-562) [WinBuiltinDCOMUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    DCOMUsers = WELL_KNOWN_SID_TYPE.WinBuiltinDCOMUsersSid,

    /// <summary>
    ///  IIS_IUSRS (S-1-5-32-568) [WinBuiltinIUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    IISUsers = WELL_KNOWN_SID_TYPE.WinBuiltinIUsersSid,

    /// <summary>
    ///  IUSR (S-1-5-17) [WinIUserSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    IISUser = WELL_KNOWN_SID_TYPE.WinIUserSid,

    /// <summary>
    ///  Cryptographic Operators (S-1-5-32-569) [WinBuiltinCryptoOperatorsSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    CryptoOperators = WELL_KNOWN_SID_TYPE.WinBuiltinCryptoOperatorsSid,

    /// <summary>
    ///  Untrusted Mandatory Level (S-1-16-0) [WinUntrustedLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    UntrustedLabel = WELL_KNOWN_SID_TYPE.WinUntrustedLabelSid,

    /// <summary>
    ///  Low Mandatory Level (S-1-16-4096) [WinLowLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    LowLabel = WELL_KNOWN_SID_TYPE.WinLowLabelSid,

    /// <summary>
    ///  Medium Mandatory Level (S-1-16-8192) [WinMediumLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    MediumLabel = WELL_KNOWN_SID_TYPE.WinMediumLabelSid,

    /// <summary>
    ///  High Mandatory Level (S-1-16-12288) [WinHighLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    HighLabel = WELL_KNOWN_SID_TYPE.WinHighLabelSid,

    /// <summary>
    ///  System Mandatory Level (S-1-16-16384) [WinSystemLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    SystemLabel = WELL_KNOWN_SID_TYPE.WinSystemLabelSid,

    /// <summary>
    ///  WRITE RESTRICTED (S-1-5-33) [WinWriteRestrictedCodeSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    WriteRestrictedCode = WELL_KNOWN_SID_TYPE.WinWriteRestrictedCodeSid,

    /// <summary>
    ///  OWNER RIGHTS (S-1-3-4) [WinCreatorOwnerRightsSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    CreatorOwnerRights = WELL_KNOWN_SID_TYPE.WinCreatorOwnerRightsSid,

    /// <summary>
    ///  [WinCacheablePrincipalsGroupSid]
    /// </summary>
    CacheablePrincipals = WELL_KNOWN_SID_TYPE.WinCacheablePrincipalsGroupSid,

    /// <summary>
    ///  [WinNonCacheablePrincipalsGroupSid]
    /// </summary>
    NonCacheablePrincipals = WELL_KNOWN_SID_TYPE.WinNonCacheablePrincipalsGroupSid,

    /// <summary>
    ///  ENTERPRISE READ-ONLY DOMAIN CONTROLLERS BETA (S-1-5-22) [WinEnterpriseReadonlyControllersSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    EnterpriseReadonlyControllers = WELL_KNOWN_SID_TYPE.WinEnterpriseReadonlyControllersSid,

    /// <summary>
    ///  [WinAccountReadonlyControllersSid]
    /// </summary>
    ReadonlyControllers = WELL_KNOWN_SID_TYPE.WinAccountReadonlyControllersSid,

    /// <summary>
    ///  Event Log Readers (S-1-5-32-573) [WinBuiltinEventLogReadersGroup] <see cref="SidNameUse.Alias"/>
    /// </summary>
    EventLogReaders = WELL_KNOWN_SID_TYPE.WinBuiltinEventLogReadersGroup,

    /// <summary>
    ///  [WinNewEnterpriseReadonlyControllersSid]
    /// </summary>
    NewEnterpriseReadonlyControllers = WELL_KNOWN_SID_TYPE.WinNewEnterpriseReadonlyControllersSid,

    /// <summary>
    ///  [WinBuiltinCertSvcDComAccessGroup]
    /// </summary>
    CertSvcDComAccess = WELL_KNOWN_SID_TYPE.WinBuiltinCertSvcDComAccessGroup,

    /// <summary>
    ///  Medium Plus Mandatory Level (S-1-16-8448) [WinMediumPlusLabelSid] <see cref="SidNameUse.Label"/>
    /// </summary>
    MediumPlusLabel = WELL_KNOWN_SID_TYPE.WinMediumPlusLabelSid,

    /// <summary>
    ///  [WinLocalLogonSid]
    /// </summary>
    LocalLogon = WELL_KNOWN_SID_TYPE.WinLocalLogonSid,

    /// <summary>
    ///  CONSOLE LOGON (S-1-2-1) [WinConsoleLogonSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    ConsoleLogon = WELL_KNOWN_SID_TYPE.WinConsoleLogonSid,

    /// <summary>
    ///  This Organization Certificate (S-1-5-65-1) [WinThisOrganizationCertificateSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    ThisOrganizationCertificate = WELL_KNOWN_SID_TYPE.WinThisOrganizationCertificateSid,

    /// <summary>
    ///  [WinApplicationPackageAuthoritySid]
    /// </summary>
    ApplicationPackageAuthority = WELL_KNOWN_SID_TYPE.WinApplicationPackageAuthoritySid,

    /// <summary>
    ///  ALL APPLICATION PACKAGES (S-1-15-2-1) [WinBuiltinAnyPackageSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    AllApplicationPackages = WELL_KNOWN_SID_TYPE.WinBuiltinAnyPackageSid,

    /// <summary>
    ///  Your Internet connection (S-1-15-3-1) [WinCapabilityInternetClientSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    InternetClientCapability = WELL_KNOWN_SID_TYPE.WinCapabilityInternetClientSid,

    /// <summary>
    ///  Your Internet connection, including incoming connections from the Internet (S-1-15-3-2)
    ///  [WinCapabilityInternetClientServerSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    InternetClientServerCapability = WELL_KNOWN_SID_TYPE.WinCapabilityInternetClientServerSid,

    /// <summary>
    ///  Your home or work networks (S-1-15-3-3) [WinCapabilityPrivateNetworkClientServerSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    PrivateNetworkClientServerCapability = WELL_KNOWN_SID_TYPE.WinCapabilityPrivateNetworkClientServerSid,

    /// <summary>
    ///  Your pictures library (S-1-15-3-4) [WinCapabilityPicturesLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    PicturesLibraryCapability = WELL_KNOWN_SID_TYPE.WinCapabilityPicturesLibrarySid,

    /// <summary>
    ///  Your videos library (S-1-15-3-5) [WinCapabilityVideosLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    VideosLibraryCapability = WELL_KNOWN_SID_TYPE.WinCapabilityVideosLibrarySid,

    /// <summary>
    ///  Your music library (S-1-15-3-6) [WinCapabilityMusicLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    MusicLibraryCapability = WELL_KNOWN_SID_TYPE.WinCapabilityMusicLibrarySid,

    /// <summary>
    ///  Your documents library (S-1-15-3-7) [WinCapabilityDocumentsLibrarySid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    DocumentsLibraryCapability = WELL_KNOWN_SID_TYPE.WinCapabilityDocumentsLibrarySid,

    /// <summary>
    ///  Software and hardware certificates or a smart card (S-1-15-3-9) [WinCapabilitySharedUserCertificatesSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    SharedUserCertificatesCapability = WELL_KNOWN_SID_TYPE.WinCapabilitySharedUserCertificatesSid,

    /// <summary>
    ///  Your Windows credentials (S-1-15-3-8) [WinCapabilityEnterpriseAuthenticationSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    EnterpriseAuthenticationCapability = WELL_KNOWN_SID_TYPE.WinCapabilityEnterpriseAuthenticationSid,

    /// <summary>
    ///  Removable storage (S-1-15-3-10) [WinCapabilityRemovableStorageSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    RemovableStorageCapability = WELL_KNOWN_SID_TYPE.WinCapabilityRemovableStorageSid,

    /// <summary>
    ///  [WinBuiltinRDSRemoteAccessServersSid]
    /// </summary>
    RDSRemoteAccessServers = WELL_KNOWN_SID_TYPE.WinBuiltinRDSRemoteAccessServersSid,

    /// <summary>
    ///  [WinBuiltinRDSEndpointServersSid]
    /// </summary>
    RDSEndpointServers = WELL_KNOWN_SID_TYPE.WinBuiltinRDSEndpointServersSid,

    /// <summary>
    ///  [WinBuiltinRDSManagementServersSid]
    /// </summary>
    RDSManagementServers = WELL_KNOWN_SID_TYPE.WinBuiltinRDSManagementServersSid,

    /// <summary>
    ///  USER MODE DRIVERS (S-1-5-84-0-0-0-0-0) [WinUserModeDriversSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    UserModeDrivers = WELL_KNOWN_SID_TYPE.WinUserModeDriversSid,

    /// <summary>
    ///  Hyper-V Administrators (S-1-5-32-578) [WinBuiltinHyperVAdminsSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    HyperVAdmins = WELL_KNOWN_SID_TYPE.WinBuiltinHyperVAdminsSid,

    /// <summary>
    ///  [WinAccountCloneableControllersSid]
    /// </summary>
    AccountCloneableControllers = WELL_KNOWN_SID_TYPE.WinAccountCloneableControllersSid,

    /// <summary>
    ///  Access Control Assistance Operators (S-1-5-32-579) [WinBuiltinAccessControlAssistanceOperatorsSid]
    /// <see cref="SidNameUse.Alias"/>
    /// </summary>
    AccessControlAssistanceOperators = WELL_KNOWN_SID_TYPE.WinBuiltinAccessControlAssistanceOperatorsSid,

    /// <summary>
    ///  Remote Management Users (S-1-5-32-580) [WinBuiltinRemoteManagementUsersSid] <see cref="SidNameUse.Alias"/>
    /// </summary>
    RemoteManagementUsers = WELL_KNOWN_SID_TYPE.WinBuiltinRemoteManagementUsersSid,

    /// <summary>
    ///  Authentication authority asserted identity (S-1-18-1) [WinAuthenticationAuthorityAssertedSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    AuthenticationAuthorityAsserted = WELL_KNOWN_SID_TYPE.WinAuthenticationAuthorityAssertedSid,

    /// <summary>
    ///  Service asserted identity (S-1-18-2) [WinAuthenticationServiceAssertedSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    AuthenticationServiceAsserted = WELL_KNOWN_SID_TYPE.WinAuthenticationServiceAssertedSid,

    /// <summary>
    ///  Local account (S-1-5-113) [WinLocalAccountSid] <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    LocalAccount = WELL_KNOWN_SID_TYPE.WinLocalAccountSid,

    /// <summary>
    ///  Local account and member of Administrators group (S-1-5-114) [WinLocalAccountAndAdministratorSid]
    /// <see cref="SidNameUse.WellKnownGroup"/>
    /// </summary>
    LocalAccountAndAdministrator = WELL_KNOWN_SID_TYPE.WinLocalAccountAndAdministratorSid,

    /// <summary>
    ///  [WinAccountProtectedUsersSid]
    /// </summary>
    ProtectedUsers = WELL_KNOWN_SID_TYPE.WinAccountProtectedUsersSid
}