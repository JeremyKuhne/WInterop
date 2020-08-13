// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    ///  Access control entry type.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374919.aspx"/>
    /// </remarks>
    public enum AceType : byte
    {
        /// <summary>
        ///  [ACCESS_ALLOWED_ACE_TYPE]
        /// </summary>
        AccessAllowed = 0x0,

        /// <summary>
        ///  [ACCESS_DENIED_ACE_TYPE]
        /// </summary>
        AccessDenied = 0x1,

        /// <summary>
        ///  [SYSTEM_AUDIT_ACE_TYPE]
        /// </summary>
        SystemAudit = 0x2,

        /// <summary>
        ///  [SYSTEM_ALARM_ACE_TYPE]
        /// </summary>
        /// <remarks>
        ///  Max type for V2.
        /// </remarks>
        SystemAlarm = 0x3,

        /// <summary>
        ///  [ACCESS_ALLOWED_COMPOUND_ACE_TYPE]
        /// </summary>
        /// <remarks>
        ///  Max type for V3.
        /// </remarks>
        AccessAllowedCompound = 0x4,

        /// <summary>
        ///  [ACCESS_ALLOWED_OBJECT_ACE_TYPE]
        /// </summary>
        AccessAllowedObject = 0x5,

        /// <summary>
        ///  [ACCESS_DENIED_OBJECT_ACE_TYPE]
        /// </summary>
        AccessDeniedObject = 0x6,

        /// <summary>
        ///  [SYSTEM_AUDIT_OBJECT_ACE_TYPE]
        /// </summary>
        SystemAuditObject = 0x7,

        /// <summary>
        ///  [SYSTEM_ALARM_OBJECT_ACE_TYPE]
        /// </summary>
        /// <remarks>
        ///  Max type for V4.
        /// </remarks>
        SystemAlarmObject = 0x8,

        /// <summary>
        ///  [ACCESS_ALLOWED_CALLBACK_ACE_TYPE]
        /// </summary>
        AccessAllowedCallback = 0x9,

        /// <summary>
        ///  [ACCESS_DENIED_CALLBACK_ACE_TYPE]
        /// </summary>
        AccessDeniedCallback = 0xA,

        /// <summary>
        ///  [ACCESS_ALLOWED_CALLBACK_OBJECT_ACE_TYPE]
        /// </summary>
        AccessAllowedCallbackObject = 0xB,

        /// <summary>
        ///  [ACCESS_DENIED_CALLBACK_OBJECT_ACE_TYPE]
        /// </summary>
        AccessDeniedCallbackObject = 0xC,

        /// <summary>
        ///  [SYSTEM_AUDIT_CALLBACK_ACE_TYPE]
        /// </summary>
        SystemAuditCallback = 0xD,

        /// <summary>
        ///  [SYSTEM_ALARM_CALLBACK_ACE_TYPE]
        /// </summary>
        SystemAlarmCallback = 0xE,

        /// <summary>
        ///  [SYSTEM_AUDIT_CALLBACK_OBJECT_ACE_TYPE]
        /// </summary>
        SystemAuditCallbackObject = 0xF,

        /// <summary>
        ///  [SYSTEM_ALARM_CALLBACK_OBJECT_ACE_TYPE]
        /// </summary>
        SystemAlarmCallbackObject = 0x10,

        /// <summary>
        ///  [SYSTEM_MANDATORY_LABEL_ACE_TYPE]
        /// </summary>
        SystemMandatoryLabel = 0x11,

        /// <summary>
        ///  [SYSTEM_RESOURCE_ATTRIBUTE_ACE_TYPE]
        /// </summary>
        SystemResourceAttribute = 0x12,

        /// <summary>
        ///  [SYSTEM_SCOPED_POLICY_ID_ACE_TYPE]
        /// </summary>
        SystemScopedPolicyId = 0x13,

        /// <summary>
        ///  [SYSTEM_PROCESS_TRUST_LABEL_ACE_TYPE]
        /// </summary>
        SystemProcessTrustLabel = 0x14,

        /// <summary>
        ///  [SYSTEM_ACCESS_FILTER_ACE_TYPE]
        /// </summary>
        SystemAccessFilter = 0x15
    }
}
