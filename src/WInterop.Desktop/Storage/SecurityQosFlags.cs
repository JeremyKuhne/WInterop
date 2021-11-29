// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Security;

namespace WInterop.Storage;

/// <summary>
///  Security Quality of Service flags for CreateFile.
/// </summary>
[Flags]
public enum SecurityQosFlags : uint
{
    /// <summary>
    ///  Not technically a defined attribute, added here for convenience.
    /// </summary>
    None = 0x0,

    /// <summary>
    ///  States that security quality of service options are present. [SECURITY_SQOS_PRESENT]
    /// </summary>
    QosPresent = 0x00100000,

    /// <summary>
    ///  [SECURITY_ANONYMOUS]
    /// </summary>
    Anonymous = ImpersonationLevel.Anonymous << 16,

    /// <summary>
    ///  [SECURITY_IDENTIFICATION]
    /// </summary>
    Identification = ImpersonationLevel.Identification << 16,

    /// <summary>
    ///  [SECURITY_IMPERSONATION]
    /// </summary>
    Impersonation = ImpersonationLevel.Impersonation << 16,

    /// <summary>
    ///  [SECURITY_DELEGATION]
    /// </summary>
    Delegations = ImpersonationLevel.Delegation << 16,

    /// <summary>
    ///  [SECURITY_CONTEXT_TRACKING]
    /// </summary>
    ContextTracking = 0x00040000,

    /// <summary>
    ///  [SECURITY_EFFECTIVE_ONLY]
    /// </summary>
    EffectiveOnly = 0x00080000
}