// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization
{
    /// <summary>
    /// Security information type to be set or queried. [SECURITY_INFORMATION]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379573.aspx"/>
    /// </remarks>
    [Flags]
    public enum SecurityInformation : uint
    {
        /// <summary>
        /// Primary group identifier of the object being referenced. [OWNER_SECURITY_INFORMATION]
        /// </summary>
        /// <remarks>
        /// <see cref="StandardAccessRights.ReadControl"/> right required to query.
        /// <see cref="StandardAccessRights.WriteOwner"/> right required to set.
        /// </remarks>
        Owner = 0x00000001,

        /// <summary>
        /// [GROUP_SECURITY_INFORMATION]
        /// </summary>
        Group = 0x00000002,

        /// <summary>
        /// [DACL_SECURITY_INFORMATION]
        /// </summary>
        Dacl = 0x00000004,

        /// <summary>
        /// [SACL_SECURITY_INFORMATION]
        /// </summary>
        Sacl = 0x00000008,

        /// <summary>
        /// [LABEL_SECURITY_INFORMATION]
        /// </summary>
        Label = 0x00000010,

        /// <summary>
        /// The resource properties of the object being referenced. The resource properties are
        /// stored in SYSTEM_RESOURCE_ATTRIBUTE_ACE types in the SACL of the security descriptor.
        /// [ATTRIBUTE_SECURITY_INFORMATION]
        /// </summary>
        Attribute = 0x00000020,

        /// <summary>
        /// [SCOPE_SECURITY_INFORMATION]
        /// </summary>
        Scope = 0x00000040,

        /// <summary>
        /// [PROCESS_TRUST_LABEL_SECURITY_INFORMATION]
        /// </summary>
        ProcessTrustLabel = 0x00000080,

        /// <summary>
        /// [ACCESS_FILTER_SECURITY_INFORMATION]
        /// </summary>
        AccessFilter = 0x00000100,

        /// <summary>
        /// [BACKUP_SECURITY_INFORMATION]
        /// </summary>
        BackupSecurity = 0x00010000,

        /// <summary>
        /// [PROTECTED_DACL_SECURITY_INFORMATION]
        /// </summary>
        ProtectedDacl = 0x80000000,

        /// <summary>
        /// [PROTECTED_SACL_SECURITY_INFORMATION]
        /// </summary>
        ProtectedSacl = 0x40000000,

        /// <summary>
        /// [UNPROTECTED_DACL_SECURITY_INFORMATION]
        /// </summary>
        UnprotectedDacl = 0x20000000,

        /// <summary>
        /// [UNPROTECTED_SACL_SECURITY_INFORMATION]
        /// </summary>
        UnprotectedSacl = 0x10000000,
    }
}
