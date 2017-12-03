// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Information type for Get/SetTokenInfomration.
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379626.aspx">TOKEN_INFORMATION_CLASS</a> enumeration.
    /// [TOKEN_INFORMATION_CLASS]
    /// </summary>
    public enum TokenInformation : uint
    {
        /// <summary>
        /// Gets the user account for the given token.
        /// [TokenUser]
        /// </summary>
        User = 1,

        /// <summary>
        /// [TokenGroups]
        /// </summary>
        Groups,

        /// <summary>
        /// [TokenPrivileges]
        /// </summary>
        TokenPrivileges,

        /// <summary>
        /// [TokenOwner]
        /// </summary>
        TokenOwner,

        /// <summary>
        /// [TokenPrimaryGroup]
        /// </summary>
        TokenPrimaryGroup,

        /// <summary>
        /// [TokenDefaultDacl]
        /// </summary>
        TokenDefaultDacl,

        /// <summary>
        /// [TokenSource]
        /// </summary>
        TokenSource,

        /// <summary>
        /// [TokenType]
        /// </summary>
        TokenType,

        /// <summary>
        /// [TokenImpersonationLevel]
        /// </summary>
        TokenImpersonationLevel,

        /// <summary>
        /// [TokenStatistics]
        /// </summary>
        TokenStatistics,

        /// <summary>
        /// [TokenRestrictedSids]
        /// </summary>
        TokenRestrictedSids,

        /// <summary>
        /// [TokenSessionId]
        /// </summary>
        TokenSessionId,

        /// <summary>
        /// [TokenGroupsAndPrivileges]
        /// </summary>
        TokenGroupsAndPrivileges,

        /// <summary>
        /// [TokenSessionReference]
        /// </summary>
        TokenSessionReference,

        /// <summary>
        /// [TokenSandBoxInert]
        /// </summary>
        TokenSandBoxInert,

        /// <summary>
        /// [TokenAuditPolicy]
        /// </summary>
        TokenAuditPolicy,

        /// <summary>
        /// [TokenOrigin]
        /// </summary>
        TokenOrigin,

        /// <summary>
        /// [TokenElevationType]
        /// </summary>
        TokenElevationType,

        /// <summary>
        /// [TokenLinkedToken]
        /// </summary>
        TokenLinkedToken,

        /// <summary>
        /// [TokenElevation]
        /// </summary>
        TokenElevation,

        /// <summary>
        /// [TokenHasRestrictions]
        /// </summary>
        TokenHasRestrictions,

        /// <summary>
        /// [TokenAccessInformation]
        /// </summary>
        TokenAccessInformation,

        /// <summary>
        /// [TokenVirtualizationAllowed]
        /// </summary>
        TokenVirtualizationAllowed,

        /// <summary>
        /// [TokenVirtualizationEnabled]
        /// </summary>
        TokenVirtualizationEnabled,

        /// <summary>
        /// [TokenIntegrityLevel]
        /// </summary>
        TokenIntegrityLevel,

        /// <summary>
        /// [TokenUIAccess]
        /// </summary>
        TokenUIAccess,

        /// <summary>
        /// [TokenMandatoryPolicy]
        /// </summary>
        TokenMandatoryPolicy,

        /// <summary>
        /// [TokenLogonSid]
        /// </summary>
        TokenLogonSid,

        /// <summary>
        /// [TokenIsAppContainer]
        /// </summary>
        TokenIsAppContainer,

        /// <summary>
        /// [TokenCapabilities]
        /// </summary>
        TokenCapabilities,

        /// <summary>
        /// [TokenAppContainerSid]
        /// </summary>
        TokenAppContainerSid,

        /// <summary>
        /// [TokenAppContainerNumber]
        /// </summary>
        TokenAppContainerNumber,

        /// <summary>
        /// [TokenUserClaimAttributes]
        /// </summary>
        TokenUserClaimAttributes,

        /// <summary>
        /// [TokenDeviceClaimAttributes]
        /// </summary>
        TokenDeviceClaimAttributes,

        /// <summary>
        /// [TokenRestrictedUserClaimAttributes]
        /// </summary>
        TokenRestrictedUserClaimAttributes,

        /// <summary>
        /// [TokenRestrictedDeviceClaimAttributes]
        /// </summary>
        TokenRestrictedDeviceClaimAttributes,

        /// <summary>
        /// [TokenDeviceGroups]
        /// </summary>
        TokenDeviceGroups,

        /// <summary>
        /// [TokenRestrictedDeviceGroups]
        /// </summary>
        TokenRestrictedDeviceGroups,

        /// <summary>
        /// [TokenSecurityAttributes]
        /// </summary>
        TokenSecurityAttributes,

        /// <summary>
        /// [TokenIsRestricted]
        /// </summary>
        TokenIsRestricted
    }
}
