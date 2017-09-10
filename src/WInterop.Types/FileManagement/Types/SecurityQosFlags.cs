// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.FileManagement.Types
{
    [Flags]
    public enum SecurityQosFlags : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// [SECURITY_SQOS_PRESENT]
        /// </summary>
        QosPresent = 0x00100000,

        /// <summary>
        /// [SECURITY_ANONYMOUS]
        /// </summary>
        Anonymous = (SECURITY_IMPERSONATION_LEVEL.SecurityAnonymous << 16),

        /// <summary>
        /// [SECURITY_IDENTIFICATION]
        /// </summary>
        Identification = (SECURITY_IMPERSONATION_LEVEL.SecurityIdentification << 16),

        /// <summary>
        /// [SECURITY_IMPERSONATION]
        /// </summary>
        Impersonation = (SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation << 16),

        /// <summary>
        /// [SECURITY_DELEGATION]
        /// </summary>
        Delegations = (SECURITY_IMPERSONATION_LEVEL.SecurityDelegation << 16),

        /// <summary>
        /// [SECURITY_CONTEXT_TRACKING]
        /// </summary>
        ContextTracking = 0x00040000,

        /// <summary>
        /// [SECURITY_EFFECTIVE_ONLY]
        /// </summary>
        EffectiveOnly = 0x00080000
    }
}
