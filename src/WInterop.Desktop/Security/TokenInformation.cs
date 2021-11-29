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
    User = 1,

    /// <summary>
    ///  The groups for the given token. [TokenGroups]
    /// </summary>
    Groups,

    /// <summary>
    ///  Privileges for the given token. [TokenPrivileges]
    /// </summary>
    Privileges,

    /// <summary>
    ///  Default owner for newly created objects. [TokenOwner]
    /// </summary>
    Owner,

    /// <summary>
    ///  Primary group for newly created objects. [TokenPrimaryGroup]
    /// </summary>
    PrimaryGroup,

    /// <summary>
    ///  [TokenDefaultDacl]
    /// </summary>
    DefaultDacl,

    /// <summary>
    ///  [TokenSource]
    /// </summary>
    Source,

    /// <summary>
    ///  [TokenType]
    /// </summary>
    Type,

    /// <summary>
    ///  [TokenImpersonationLevel]
    /// </summary>
    ImpersonationLevel,

    /// <summary>
    ///  [TokenStatistics]
    /// </summary>
    Statistics,

    /// <summary>
    ///  [TokenRestrictedSids]
    /// </summary>
    RestrictedSids,

    /// <summary>
    ///  [TokenSessionId]
    /// </summary>
    SessionId,

    /// <summary>
    ///  [TokenGroupsAndPrivileges]
    /// </summary>
    GroupsAndPrivileges,

    /// <summary>
    ///  [TokenSessionReference]
    /// </summary>
    SessionReference,

    /// <summary>
    ///  [TokenSandBoxInert]
    /// </summary>
    SandBoxInert,

    /// <summary>
    ///  [TokenAuditPolicy]
    /// </summary>
    AuditPolicy,

    /// <summary>
    ///  [TokenOrigin]
    /// </summary>
    Origin,

    /// <summary>
    ///  [TokenElevationType]
    /// </summary>
    ElevationType,

    /// <summary>
    ///  [TokenLinkedToken]
    /// </summary>
    LinkedToken,

    /// <summary>
    ///  [TokenElevation]
    /// </summary>
    Elevation,

    /// <summary>
    ///  [TokenHasRestrictions]
    /// </summary>
    HasRestrictions,

    /// <summary>
    ///  [TokenAccessInformation]
    /// </summary>
    AccessInformation,

    /// <summary>
    ///  [TokenVirtualizationAllowed]
    /// </summary>
    VirtualizationAllowed,

    /// <summary>
    ///  [TokenVirtualizationEnabled]
    /// </summary>
    VirtualizationEnabled,

    /// <summary>
    ///  [TokenIntegrityLevel]
    /// </summary>
    IntegrityLevel,

    /// <summary>
    ///  [TokenUIAccess]
    /// </summary>
    UIAccess,

    /// <summary>
    ///  [TokenMandatoryPolicy]
    /// </summary>
    MandatoryPolicy,

    /// <summary>
    ///  [TokenLogonSid]
    /// </summary>
    LogonSid,

    /// <summary>
    ///  [TokenIsAppContainer]
    /// </summary>
    IsAppContainer,

    /// <summary>
    ///  [TokenCapabilities]
    /// </summary>
    Capabilities,

    /// <summary>
    ///  [TokenAppContainerSid]
    /// </summary>
    AppContainerSid,

    /// <summary>
    ///  [TokenAppContainerNumber]
    /// </summary>
    AppContainerNumber,

    /// <summary>
    ///  [TokenUserClaimAttributes]
    /// </summary>
    UserClaimAttributes,

    /// <summary>
    ///  [TokenDeviceClaimAttributes]
    /// </summary>
    DeviceClaimAttributes,

    /// <summary>
    ///  [TokenRestrictedUserClaimAttributes]
    /// </summary>
    RestrictedUserClaimAttributes,

    /// <summary>
    ///  [TokenRestrictedDeviceClaimAttributes]
    /// </summary>
    RestrictedDeviceClaimAttributes,

    /// <summary>
    ///  [TokenDeviceGroups]
    /// </summary>
    DeviceGroups,

    /// <summary>
    ///  [TokenRestrictedDeviceGroups]
    /// </summary>
    RestrictedDeviceGroups,

    /// <summary>
    ///  [TokenSecurityAttributes]
    /// </summary>
    SecurityAttributes,

    /// <summary>
    ///  [TokenIsRestricted]
    /// </summary>
    IsRestricted
}