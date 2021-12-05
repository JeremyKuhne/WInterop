// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
///  Information type for Get/SetTokenInfomration.
/// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379626.aspx">TOKEN_INFORMATION_CLASS</a> enumeration.
///  [TOKEN_INFORMATION_CLASS]
/// </summary>
public enum TokenInformation : uint
{
    /// <summary>
    ///  Gets the user account for the given token.
    ///  [TokenUser]
    /// </summary>
    User = TOKEN_INFORMATION_CLASS.TokenUser,

    /// <summary>
    ///  The groups for the given token. [TokenGroups]
    /// </summary>
    Groups = TOKEN_INFORMATION_CLASS.TokenGroups,

    /// <summary>
    ///  Privileges for the given token. [TokenPrivileges]
    /// </summary>
    Privileges = TOKEN_INFORMATION_CLASS.TokenPrivileges,

    /// <summary>
    ///  Default owner for newly created objects. [TokenOwner]
    /// </summary>
    Owner = TOKEN_INFORMATION_CLASS.TokenOwner,

    /// <summary>
    ///  Primary group for newly created objects. [TokenPrimaryGroup]
    /// </summary>
    PrimaryGroup = TOKEN_INFORMATION_CLASS.TokenPrimaryGroup,

    /// <summary>
    ///  [TokenDefaultDacl]
    /// </summary>
    DefaultDacl = TOKEN_INFORMATION_CLASS.TokenDefaultDacl,

    /// <summary>
    ///  [TokenSource]
    /// </summary>
    Source = TOKEN_INFORMATION_CLASS.TokenSource,

    /// <summary>
    ///  [TokenType]
    /// </summary>
    Type = TOKEN_INFORMATION_CLASS.TokenType,

    /// <summary>
    ///  [TokenImpersonationLevel]
    /// </summary>
    ImpersonationLevel = TOKEN_INFORMATION_CLASS.TokenImpersonationLevel,

    /// <summary>
    ///  [TokenStatistics]
    /// </summary>
    Statistics = TOKEN_INFORMATION_CLASS.TokenStatistics,

    /// <summary>
    ///  [TokenRestrictedSids]
    /// </summary>
    RestrictedSids = TOKEN_INFORMATION_CLASS.TokenRestrictedSids,

    /// <summary>
    ///  [TokenSessionId]
    /// </summary>
    SessionId = TOKEN_INFORMATION_CLASS.TokenSessionId,

    /// <summary>
    ///  [TokenGroupsAndPrivileges]
    /// </summary>
    GroupsAndPrivileges = TOKEN_INFORMATION_CLASS.TokenGroupsAndPrivileges,

    /// <summary>
    ///  [TokenSessionReference]
    /// </summary>
    SessionReference = TOKEN_INFORMATION_CLASS.TokenSessionReference,

    /// <summary>
    ///  [TokenSandBoxInert]
    /// </summary>
    SandBoxInert = TOKEN_INFORMATION_CLASS.TokenSandBoxInert,

    /// <summary>
    ///  [TokenAuditPolicy]
    /// </summary>
    AuditPolicy = TOKEN_INFORMATION_CLASS.TokenAuditPolicy,

    /// <summary>
    ///  [TokenOrigin]
    /// </summary>
    Origin = TOKEN_INFORMATION_CLASS.TokenOrigin,

    /// <summary>
    ///  [TokenElevationType]
    /// </summary>
    ElevationType = TOKEN_INFORMATION_CLASS.TokenElevationType,

    /// <summary>
    ///  [TokenLinkedToken]
    /// </summary>
    LinkedToken = TOKEN_INFORMATION_CLASS.TokenLinkedToken,

    /// <summary>
    ///  [TokenElevation]
    /// </summary>
    Elevation = TOKEN_INFORMATION_CLASS.TokenElevation,

    /// <summary>
    ///  [TokenHasRestrictions]
    /// </summary>
    HasRestrictions = TOKEN_INFORMATION_CLASS.TokenHasRestrictions,

    /// <summary>
    ///  [TokenAccessInformation]
    /// </summary>
    AccessInformation = TOKEN_INFORMATION_CLASS.TokenAccessInformation,

    /// <summary>
    ///  [TokenVirtualizationAllowed]
    /// </summary>
    VirtualizationAllowed = TOKEN_INFORMATION_CLASS.TokenVirtualizationAllowed,

    /// <summary>
    ///  [TokenVirtualizationEnabled]
    /// </summary>
    VirtualizationEnabled = TOKEN_INFORMATION_CLASS.TokenVirtualizationEnabled,

    /// <summary>
    ///  [TokenIntegrityLevel]
    /// </summary>
    IntegrityLevel = TOKEN_INFORMATION_CLASS.TokenIntegrityLevel,

    /// <summary>
    ///  [TokenUIAccess]
    /// </summary>
    UIAccess = TOKEN_INFORMATION_CLASS.TokenUIAccess,

    /// <summary>
    ///  [TokenMandatoryPolicy]
    /// </summary>
    MandatoryPolicy = TOKEN_INFORMATION_CLASS.TokenMandatoryPolicy,

    /// <summary>
    ///  [TokenLogonSid]
    /// </summary>
    LogonSid = TOKEN_INFORMATION_CLASS.TokenLogonSid,

    /// <summary>
    ///  [TokenIsAppContainer]
    /// </summary>
    IsAppContainer = TOKEN_INFORMATION_CLASS.TokenIsAppContainer,

    /// <summary>
    ///  [TokenCapabilities]
    /// </summary>
    Capabilities = TOKEN_INFORMATION_CLASS.TokenCapabilities,

    /// <summary>
    ///  [TokenAppContainerSid]
    /// </summary>
    AppContainerSid = TOKEN_INFORMATION_CLASS.TokenAppContainerSid,

    /// <summary>
    ///  [TokenAppContainerNumber]
    /// </summary>
    AppContainerNumber = TOKEN_INFORMATION_CLASS.TokenAppContainerNumber,

    /// <summary>
    ///  [TokenUserClaimAttributes]
    /// </summary>
    UserClaimAttributes = TOKEN_INFORMATION_CLASS.TokenUserClaimAttributes,

    /// <summary>
    ///  [TokenDeviceClaimAttributes]
    /// </summary>
    DeviceClaimAttributes = TOKEN_INFORMATION_CLASS.TokenDeviceClaimAttributes,

    /// <summary>
    ///  [TokenRestrictedUserClaimAttributes]
    /// </summary>
    RestrictedUserClaimAttributes = TOKEN_INFORMATION_CLASS.TokenRestrictedUserClaimAttributes,

    /// <summary>
    ///  [TokenRestrictedDeviceClaimAttributes]
    /// </summary>
    RestrictedDeviceClaimAttributes = TOKEN_INFORMATION_CLASS.TokenRestrictedDeviceClaimAttributes,

    /// <summary>
    ///  [TokenDeviceGroups]
    /// </summary>
    DeviceGroups = TOKEN_INFORMATION_CLASS.TokenDeviceGroups,

    /// <summary>
    ///  [TokenRestrictedDeviceGroups]
    /// </summary>
    RestrictedDeviceGroups = TOKEN_INFORMATION_CLASS.TokenRestrictedDeviceGroups,

    /// <summary>
    ///  [TokenSecurityAttributes]
    /// </summary>
    SecurityAttributes = TOKEN_INFORMATION_CLASS.TokenSecurityAttributes,

    /// <summary>
    ///  [TokenIsRestricted]
    /// </summary>
    IsRestricted = TOKEN_INFORMATION_CLASS.TokenIsRestricted
}