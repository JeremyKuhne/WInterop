// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// The top-level authority of a security identifier (SID).
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379598.aspx"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/dd302645.aspx"/>
    /// <seealso cref="https://msdn.microsoft.com/en-us/library/cc980032.aspx"/>
    /// </remarks>
    public struct SID_IDENTIFIER_AUTHORITY
    {
        private FixedByte.Size6 _Value;
        public Span<byte> Value => _Value.Buffer;

        /// <summary>
        /// Null SID authority. Only valid with the NULL well-known SID (S-1-0-0). [SECURITY_NULL_SID_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY NULL = new SID_IDENTIFIER_AUTHORITY(0);

        /// <summary>
        /// World SID authority. Only valid with the Everyone well-known SID (S-1-1-0). [SECURITY_WORLD_SID_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY WORLD = new SID_IDENTIFIER_AUTHORITY(1);

        /// <summary>
        /// Local SID authority. Only valid with the Local well-known SID (S-1-2-0). [SECURITY_LOCAL_SID_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY LOCAL = new SID_IDENTIFIER_AUTHORITY(2);

        /// <summary>
        /// Creator SID authority. Used with Creator Owner, Creator Group, and Creator Owner Server well-known SIDs.
        /// (S-1-3-0, S-1-3-1, and S-1-3-2)
        /// 
        /// These SIDs are used as ACL placeholders and are replaced by user, group, and machine SIDs of the security
        /// principal. [SECURITY_CREATOR_SID_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY CREATOR = new SID_IDENTIFIER_AUTHORITY(3);

        // Not used
        // public static SID_IDENTIFIER_AUTHORITY SECURITY_NON_UNIQUE_AUTHORITY = new SID_IDENTIFIER_AUTHORITY(4);

        /// <summary>
        /// Specifies the Windows NT operating system security subsystem SID authority. [SECURITY_NT_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY NT = new SID_IDENTIFIER_AUTHORITY(5);

        /// <summary>
        /// [SECURITY_RESOURCE_MANAGER_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY RESOURCE_MANAGER = new SID_IDENTIFIER_AUTHORITY(9);

        /// <summary>
        /// Specifies the application package authority. Defines application capabilities. [SECURITY_APP_PACKAGE_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY APP_PACKAGE = new SID_IDENTIFIER_AUTHORITY(15);

        /// <summary>
        /// Specified the mandatory label authority. Defines integrity levels. [SECURITY_MANDATORY_LABEL_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY MANDATORY_LABEL = new SID_IDENTIFIER_AUTHORITY(16);

        /// <summary>
        /// Part of Server 2012+ Kerberos KDCs. [SECURITY_SCOPED_POLICY_ID_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY SCOPED_POLICY_ID = new SID_IDENTIFIER_AUTHORITY(17);

        /// <summary>
        /// Authentication authority that asserts the client's identity. Part of Server 2012+ Kerberos KDCs.
        /// [SECURITY_AUTHENTICATION_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY AUTHENTICATION = new SID_IDENTIFIER_AUTHORITY(18);

        /// <summary>
        /// [SECURITY_PROCESS_TRUST_AUTHORITY]
        /// </summary>
        public static SID_IDENTIFIER_AUTHORITY PROCESS_TRUST = new SID_IDENTIFIER_AUTHORITY(19);

        private SID_IDENTIFIER_AUTHORITY(byte knownAuthority)
        {
            Value[5] = knownAuthority;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case SID_IDENTIFIER_AUTHORITY authority:
                    return Equals(authority);
                case byte[] bytes:
                    return Value.SequenceEqual(new ReadOnlySpan<byte>(bytes));
                default:
                    return false;
            }
        }

        public bool Equals(SID_IDENTIFIER_AUTHORITY other) => Value.SequenceEqual(other.Value);

        public override int GetHashCode() => base.GetHashCode();
    }
}
