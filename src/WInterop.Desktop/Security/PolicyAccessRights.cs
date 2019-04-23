// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    // These live in NTSecAPI.h
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721916.
    [Flags]
    public enum PolicyAccessRights : uint
    {
        /// <summary>
        /// [POLICY_VIEW_LOCAL_INFORMATION]
        /// </summary>
        ViewLocalInformation = 0x00000001,

        /// <summary>
        /// [POLICY_VIEW_AUDIT_INFORMATION]
        /// </summary>
        ViewAuditInformation = 0x00000002,

        /// <summary>
        /// [POLICY_GET_PRIVATE_INFORMATION]
        /// </summary>
        GetPrivateInformation = 0x00000004,

        /// <summary>
        /// [POLICY_TRUST_ADMIN]
        /// </summary>
        TrustAdmin = 0x00000008,

        /// <summary>
        /// [POLICY_CREATE_ACCOUNT]
        /// </summary>
        CreateAccount = 0x00000010,

        /// <summary>
        /// [POLICY_CREATE_SECRET]
        /// </summary>
        CreateSecret = 0x00000020,

        /// <summary>
        /// [POLICY_CREATE_PRIVILEGE]
        /// </summary>
        CreatePrivilege = 0x00000040,

        /// <summary>
        /// [POLICY_SET_DEFAULT_QUOTA_LIMITS]
        /// </summary>
        SetDefaultQuotaLimits = 0x00000080,

        /// <summary>
        /// [POLICY_SET_AUDIT_REQUIREMENTS]
        /// </summary>
        SetAuditRequirements = 0x00000100,

        /// <summary>
        /// [POLICY_AUDIT_LOG_ADMIN]
        /// </summary>
        AuditLogAdmin = 0x00000200,

        /// <summary>
        /// [POLICY_SERVER_ADMIN]
        /// </summary>
        ServerAdmin = 0x00000400,

        /// <summary>
        /// [POLICY_LOOKUP_NAMES]
        /// </summary>
        LookupNames = 0x00000800,

        /// <summary>
        /// [POLICY_NOTIFICATION]
        /// </summary>
        Notification = 0x00001000,

        /// <summary>
        /// [POLICY_ALL_ACCESS]
        /// </summary>
        AllAccess = StandardAccessRights.Required
            | ViewLocalInformation
            | ViewAuditInformation
            | GetPrivateInformation
            | TrustAdmin
            | CreateAccount
            | CreateSecret
            | CreatePrivilege
            | SetDefaultQuotaLimits
            | SetAuditRequirements
            | AuditLogAdmin
            | ServerAdmin
            | LookupNames,

        /// <summary>
        /// [POLICY_READ]
        /// </summary>
        Read = StandardAccessRights.Read
            | ViewAuditInformation
            | GetPrivateInformation,

        /// <summary>
        /// [POLICY_WRITE]
        /// </summary>
        Write = StandardAccessRights.Write
            | TrustAdmin
            | CreateAccount
            | CreateSecret
            | CreatePrivilege
            | SetDefaultQuotaLimits
            | SetAuditRequirements
            | AuditLogAdmin
            | ServerAdmin,

        /// <summary>
        /// [POLICY_EXECUTE]
        /// </summary>
        Execute = StandardAccessRights.Execute
            | ViewLocalInformation
            | LookupNames
    }
}
