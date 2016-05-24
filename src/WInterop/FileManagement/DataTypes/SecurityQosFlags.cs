// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.DataTypes;

namespace WInterop.FileManagement.DataTypes
{
    [Flags]
    public enum SecurityQosFlags : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        NONE = 0x0,

        SECURITY_SQOS_PRESENT = 0x00100000,
        SECURITY_ANONYMOUS = (SECURITY_IMPERSONATION_LEVEL.SecurityAnonymous << 16),
        SECURITY_IDENTIFICATION = (SECURITY_IMPERSONATION_LEVEL.SecurityIdentification << 16),
        SECURITY_IMPERSONATION = (SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation << 16),
        SECURITY_DELEGATION = (SECURITY_IMPERSONATION_LEVEL.SecurityDelegation << 16),
        SECURITY_CONTEXT_TRACKING = 0x00040000,
        SECURITY_EFFECTIVE_ONLY = 0x00080000
    }
}
