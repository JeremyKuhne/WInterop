// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization;

namespace WInterop.SecurityManagement.Types
{
    // These live in NTSecAPI.h
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721916.
    [Flags]
    public enum PolicyAccessRights : uint
    {
        POLICY_VIEW_LOCAL_INFORMATION             = 0x00000001,
        POLICY_VIEW_AUDIT_INFORMATION             = 0x00000002,
        POLICY_GET_PRIVATE_INFORMATION            = 0x00000004,
        POLICY_TRUST_ADMIN                        = 0x00000008,
        POLICY_CREATE_ACCOUNT                     = 0x00000010,
        POLICY_CREATE_SECRET                      = 0x00000020,
        POLICY_CREATE_PRIVILEGE                   = 0x00000040,
        POLICY_SET_DEFAULT_QUOTA_LIMITS           = 0x00000080,
        POLICY_SET_AUDIT_REQUIREMENTS             = 0x00000100,
        POLICY_AUDIT_LOG_ADMIN                    = 0x00000200,
        POLICY_SERVER_ADMIN                       = 0x00000400,
        POLICY_LOOKUP_NAMES                       = 0x00000800,
        POLICY_NOTIFICATION                       = 0x00001000,
        POLICY_ALL_ACCESS = StandardAccessRights.Required
            | POLICY_VIEW_LOCAL_INFORMATION
            | POLICY_VIEW_AUDIT_INFORMATION
            | POLICY_GET_PRIVATE_INFORMATION
            | POLICY_TRUST_ADMIN
            | POLICY_CREATE_ACCOUNT
            | POLICY_CREATE_SECRET
            | POLICY_CREATE_PRIVILEGE
            | POLICY_SET_DEFAULT_QUOTA_LIMITS
            | POLICY_SET_AUDIT_REQUIREMENTS
            | POLICY_AUDIT_LOG_ADMIN
            | POLICY_SERVER_ADMIN
            | POLICY_LOOKUP_NAMES,
        POLICY_READ = StandardAccessRights.Read
            | POLICY_VIEW_AUDIT_INFORMATION
            | POLICY_GET_PRIVATE_INFORMATION,
        POLICY_WRITE = StandardAccessRights.Write
            | POLICY_TRUST_ADMIN
            | POLICY_CREATE_ACCOUNT
            | POLICY_CREATE_SECRET
            | POLICY_CREATE_PRIVILEGE
            | POLICY_SET_DEFAULT_QUOTA_LIMITS
            | POLICY_SET_AUDIT_REQUIREMENTS
            | POLICY_AUDIT_LOG_ADMIN
            | POLICY_SERVER_ADMIN,
        POLICY_EXECUTE = StandardAccessRights.Execute
            | POLICY_VIEW_LOCAL_INFORMATION
            | POLICY_LOOKUP_NAMES
    }
}
