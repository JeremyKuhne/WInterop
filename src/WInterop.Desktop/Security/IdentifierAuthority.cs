// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
///  The top-level authority of a security identifier (SID). [SID_IDENTIFIER_AUTHORITY]
/// </summary>
/// <docs>
///   https://docs.microsoft.com/windows/win32/api/winnt/ns-winnt-sid_identifier_authority
///   https://docs.microsoft.com/openspecs/windows_protocols/ms-dtyp/c6ce4275-3d90-4890-ab3a-514745e4637e
///   https://docs.microsoft.com/openspecs/windows_protocols/ms-dtyp/81d92bba-d22b-4a8c-908a-554ab29148ab
/// </docs>
public readonly struct IdentifierAuthority : IEquatable<IdentifierAuthority>
{
    private readonly SID_IDENTIFIER_AUTHORITY _authority;

    /// <summary>
    ///  Null SID authority. Only valid with the NULL well-known SID (S-1-0-0). [SECURITY_NULL_SID_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority Null = new(0);

    /// <summary>
    ///  World SID authority. Only valid with the Everyone well-known SID (S-1-1-0). [SECURITY_WORLD_SID_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority World = new(1);

    /// <summary>
    ///  Local SID authority. Only valid with the Local well-known SID (S-1-2-0). [SECURITY_LOCAL_SID_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority Local = new(2);

    /// <summary>
    ///  Creator SID authority. Used with Creator Owner, Creator Group, and Creator Owner Server well-known SIDs.
    ///  (S-1-3-0, S-1-3-1, and S-1-3-2)
    ///
    ///  These SIDs are used as ACL placeholders and are replaced by user, group, and machine SIDs of the security
    ///  principal. [SECURITY_CREATOR_SID_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority Creator = new(3);

    // Not used
    // public static SID_IDENTIFIER_AUTHORITY SECURITY_NON_UNIQUE_AUTHORITY = new SID_IDENTIFIER_AUTHORITY(4);

    /// <summary>
    ///  Specifies the Windows NT operating system security subsystem SID authority. [SECURITY_NT_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority NT = new(5);

    /// <summary>
    ///  [SECURITY_RESOURCE_MANAGER_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority ResourceManager = new(9);

    /// <summary>
    ///  Specifies the application package authority. Defines application capabilities. [SECURITY_APP_PACKAGE_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority AppPackage = new(15);

    /// <summary>
    ///  Specified the mandatory label authority. Defines integrity levels. [SECURITY_MANDATORY_LABEL_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority MandatoryLabel = new(16);

    /// <summary>
    ///  Part of Server 2012+ Kerberos KDCs. [SECURITY_SCOPED_POLICY_ID_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority ScopedPolicy = new(17);

    /// <summary>
    ///  Authentication authority that asserts the client's identity. Part of Server 2012+ Kerberos KDCs.
    ///  [SECURITY_AUTHENTICATION_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority Authentication = new(18);

    /// <summary>
    ///  [SECURITY_PROCESS_TRUST_AUTHORITY]
    /// </summary>
    public static readonly IdentifierAuthority ProcessTrust = new(19);

    private IdentifierAuthority(byte knownAuthority) => InternalValue[5] = knownAuthority;

    internal IdentifierAuthority(SID_IDENTIFIER_AUTHORITY authority) => _authority = authority;

    private unsafe Span<byte> InternalValue
    {
        get
        {
            fixed (byte* b = _authority.Value)
            {
                return new(b, 6);
            }
        }
    }

    public ReadOnlySpan<byte> Value => InternalValue;

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

    public static bool operator ==(IdentifierAuthority left, IdentifierAuthority right)
        => left.Equals(right);

    public static bool operator !=(IdentifierAuthority left, IdentifierAuthority right)
        => !(left == right);
}