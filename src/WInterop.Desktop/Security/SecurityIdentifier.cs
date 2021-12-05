// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Security;

/// <summary>
///  The binary form of a SID. In string format SIDs are in the form:
///  S-{revision}-{authority}[-{subauthority}...]
/// </summary>
/// <remarks>
///  <para>
///   Per the documentation you're not supposed to access the SID fields directly. Given that the struct
///   is publicly defined in the headers as above, it is unlikely to change. If you want to play safe
///   by MSDN use the Win32 methods for all data access.
///  </para>
///  <para>
///   For ease of interaction and lifetime we simply define the struct at it's max size. This allows
///   passing the struct around easily. It does eat up more memory than needed for the sub authorities,
///   but that seems a decent tradeoff as we don't expect to create a large number of these.
///  </para>
///  <para>
///   The size of <see cref="SecurityIdentifier"/> is 68 bytes.
///  </para>
/// </remarks>
/// <docs>
///  From winnt.h:
///
///         1   1   1   1   1   1
///         5   4   3   2   1   0   9   8   7   6   5   4   3   2   1   0
///      +---------------------------------------------------------------+
///      |      SubAuthorityCount        |Reserved1 (SBZ)|   Revision    |
///      +---------------------------------------------------------------+
///      |                   IdentifierAuthority[0]                      |
///      +---------------------------------------------------------------+
///      |                   IdentifierAuthority[1]                      |
///      +---------------------------------------------------------------+
///      |                   IdentifierAuthority[2]                      |
///      +---------------------------------------------------------------+
///      |                                                               |
///      +- -  -  -  -  -  -  -  SubAuthority[]  -  -  -  -  -  -  -  - -+
///      |                                                               |
///      +---------------------------------------------------------------+
///
///      typedef struct _SID
///      {
///          BYTE Revision;
///          BYTE SubAuthorityCount;
///          SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
///          DWORD SubAuthority[ANYSIZE_ARRAY];
///      } SID, *PISID;
///
///  System.Security.Principal.SecurityIdentifier allows copying to/from a byte array.
///  https://docs.microsoft.com/dotnet/api/system.security.principal.securityidentifier
///
///  SID
///  https://docs.microsoft.com/windows/win32/api/winnt/ns-winnt-sid
///
///  SID Components
///  https://docs.microsoft.com/windows/win32/secauthz/sid-components
///
///  (sizeof(SID) - sizeof(DWORD) + (SID_MAX_SUB_AUTHORITIES * sizeof(DWORD)))
///  private const uint SECURITY_MAX_SID_SIZE = 12 - 4 + (15 * 4);
/// </docs>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct SecurityIdentifier : IEquatable<SecurityIdentifier>
{
    private SID _sid;
    private fixed uint _subathorities[SID.SID_MAX_SUB_AUTHORITIES - 1];

    internal SecurityIdentifier(SID* sid)
    {
        this = default;
        _sid.Revision = sid->Revision;
        _sid.SubAuthorityCount = sid->SubAuthorityCount;
        _sid.IdentifierAuthority = sid->IdentifierAuthority;
        Unsafe.AsRef<SID>(sid).SubAuthorities().CopyTo(_sid.SubAuthorities());
    }

    public byte Revision => _sid.Revision;
    public IdentifierAuthority Authority => new(_sid.IdentifierAuthority);
    public ReadOnlySpan<uint> SubAuthorities => _sid.SubAuthorities();

    public bool Equals(SecurityIdentifier other) => _sid.Revision == other._sid.Revision
        && _sid.IdentifierAuthority.Equals(other._sid.IdentifierAuthority)
        && SubAuthorities.SequenceEqual(other.SubAuthorities);

    public bool Equals(SID* other) => _sid.Revision == other->Revision
        && _sid.IdentifierAuthority.Equals(other->IdentifierAuthority)
        && SubAuthorities.SequenceEqual(Unsafe.AsRef<SID>(other).SubAuthorities());

    public override bool Equals(object? obj)
        => obj is SecurityIdentifier identifier && Equals(identifier);

    public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right) => left.Equals(right);

    public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right) => !(left == right);

    public override int GetHashCode() => base.GetHashCode();
}