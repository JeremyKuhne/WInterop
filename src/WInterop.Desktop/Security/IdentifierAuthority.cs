// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    /// <summary>
    /// The top-level authority of a security identifier (SID). [SID_IDENTIFIER_AUTHORITY]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379598.aspx"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/dd302645.aspx"/>
    /// <seealso cref="https://msdn.microsoft.com/en-us/library/cc980032.aspx"/>
    /// </remarks>
    public readonly struct IdentifierAuthority : IEquatable<IdentifierAuthority>
    {
        private readonly FixedByte.Size6 _Value;
        public ReadOnlySpan<byte> Value => _Value.Buffer;

        /// <summary>
        /// Null SID authority. Only valid with the NULL well-known SID (S-1-0-0). [SECURITY_NULL_SID_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority Null = new IdentifierAuthority(0);

        /// <summary>
        /// World SID authority. Only valid with the Everyone well-known SID (S-1-1-0). [SECURITY_WORLD_SID_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority World = new IdentifierAuthority(1);

        /// <summary>
        /// Local SID authority. Only valid with the Local well-known SID (S-1-2-0). [SECURITY_LOCAL_SID_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority Local = new IdentifierAuthority(2);

        /// <summary>
        /// Creator SID authority. Used with Creator Owner, Creator Group, and Creator Owner Server well-known SIDs.
        /// (S-1-3-0, S-1-3-1, and S-1-3-2)
        ///
        /// These SIDs are used as ACL placeholders and are replaced by user, group, and machine SIDs of the security
        /// principal. [SECURITY_CREATOR_SID_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority Creator = new IdentifierAuthority(3);

        // Not used
        // public static SID_IDENTIFIER_AUTHORITY SECURITY_NON_UNIQUE_AUTHORITY = new SID_IDENTIFIER_AUTHORITY(4);

        /// <summary>
        /// Specifies the Windows NT operating system security subsystem SID authority. [SECURITY_NT_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority NT = new IdentifierAuthority(5);

        /// <summary>
        /// [SECURITY_RESOURCE_MANAGER_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority ResourceManager = new IdentifierAuthority(9);

        /// <summary>
        /// Specifies the application package authority. Defines application capabilities. [SECURITY_APP_PACKAGE_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority AppPackage = new IdentifierAuthority(15);

        /// <summary>
        /// Specified the mandatory label authority. Defines integrity levels. [SECURITY_MANDATORY_LABEL_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority MandatoryLabel = new IdentifierAuthority(16);

        /// <summary>
        /// Part of Server 2012+ Kerberos KDCs. [SECURITY_SCOPED_POLICY_ID_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority ScopedPolicy = new IdentifierAuthority(17);

        /// <summary>
        /// Authentication authority that asserts the client's identity. Part of Server 2012+ Kerberos KDCs.
        /// [SECURITY_AUTHENTICATION_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority Authentication = new IdentifierAuthority(18);

        /// <summary>
        /// [SECURITY_PROCESS_TRUST_AUTHORITY]
        /// </summary>
        public static IdentifierAuthority ProcessTrust = new IdentifierAuthority(19);

        private IdentifierAuthority(byte knownAuthority)
        {
            _Value.Buffer[5] = knownAuthority;
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                IdentifierAuthority authority => Equals(authority),
                byte[] bytes => Value.SequenceEqual(new ReadOnlySpan<byte>(bytes)),
                _ => false,
            };
        }

        public bool Equals(IdentifierAuthority other) => Value.SequenceEqual(other.Value);

        public override int GetHashCode() => base.GetHashCode();
    }
}
