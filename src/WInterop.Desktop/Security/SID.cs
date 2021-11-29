// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Security;

// From winnt.h:
//
//         1   1   1   1   1   1
//         5   4   3   2   1   0   9   8   7   6   5   4   3   2   1   0
//      +---------------------------------------------------------------+
//      |      SubAuthorityCount        |Reserved1 (SBZ)|   Revision    |
//      +---------------------------------------------------------------+
//      |                   IdentifierAuthority[0]                      |
//      +---------------------------------------------------------------+
//      |                   IdentifierAuthority[1]                      |
//      +---------------------------------------------------------------+
//      |                   IdentifierAuthority[2]                      |
//      +---------------------------------------------------------------+
//      |                                                               |
//      +- -  -  -  -  -  -  -  SubAuthority[]  -  -  -  -  -  -  -  - -+
//      |                                                               |
//      +---------------------------------------------------------------+
//
//      typedef struct _SID
//      {
//          BYTE Revision;
//          BYTE SubAuthorityCount;
//          SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
//          DWORD SubAuthority[ANYSIZE_ARRAY];
//      } SID, *PISID;
//
// System.Security.Principal.SecurityIdentifier allows copying to/from a byte array.
// https://msdn.microsoft.com/en-us/library/system.security.principal.securityidentifier.aspx

// SID
// https://msdn.microsoft.com/en-us/library/windows/desktop/aa379594.aspx

// SID Components
// https://msdn.microsoft.com/en-us/library/windows/desktop/aa379597.aspx

// (sizeof(SID) - sizeof(DWORD) + (SID_MAX_SUB_AUTHORITIES * sizeof(DWORD)))
// private const uint SECURITY_MAX_SID_SIZE = 12 - 4 + (15 * 4);

/// <summary>
///  The binary form of a SID. In string format SIDs are in the form:
///  S-{revision}-{authority}[-{subauthority}...]
/// </summary>
/// <remarks>
///  Per the documentation you're not supposed to access the SID fields directly. Given that the struct
///  is publicly defined in the headers as above, it is unlikely to change. If you want to play safe
///  by MSDN use the Win32 methods for all data access.
///
///  For ease of interaction and lifetime we simply define the struct at it's max size. This allows
///  passing the struct around easily. It does eat up more memory than needed for the sub authorities,
///  but that seems a decent tradeoff as we don't expect to create a large number of these. This does
///  also require using <see cref="SID(SID*)"/> to get SIDs allocated by Windows.
///
///  The size of SID is 68 bytes.
/// </remarks>
public readonly struct SID
{
    public readonly byte Revision;
    public readonly byte SubAuthorityCount;
    public readonly IdentifierAuthority IdentifierAuthority;

    // Also known as Relative Identifiers
    private readonly SubAuthorityArray _subAuthorities;
    public ReadOnlySpan<uint> SubAuthorities => _subAuthorities.GetAuthorities(SubAuthorityCount);

    // From winnt.h
    private const int SID_MAX_SUB_AUTHORITIES = 15;

    /// <summary>
    ///  Use this to copy from a native buffer, as the defined SID will likely not have a full set of SubAuthorities.
    /// </summary>
    public unsafe SID(SID* native)
    {
        this = default;
        Revision = native->Revision;
        SubAuthorityCount = native->SubAuthorityCount;
        IdentifierAuthority = native->IdentifierAuthority;
        _subAuthorities.CopyAuthorities(in native->_subAuthorities, SubAuthorityCount);
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct SubAuthorityArray
    {
        // Hackjob for now. As of C# 7.3 there is no way to mark a fixed array as readonly.
        // We want the SID to be truly readonly so it can be passed as in to methods without
        // C# creating an implicit copy.

        private readonly uint _subauthority1;
        private readonly uint _subauthority2;
        private readonly uint _subauthority3;
        private readonly uint _subauthority4;
        private readonly uint _subauthority5;
        private readonly uint _subauthority6;
        private readonly uint _subauthority7;
        private readonly uint _subauthority8;
        private readonly uint _subauthority9;
        private readonly uint _subauthority10;
        private readonly uint _subauthority11;
        private readonly uint _subauthority12;
        private readonly uint _subauthority13;
        private readonly uint _subauthority14;
        private readonly uint _subauthority15;

        public unsafe Span<uint> GetAuthorities(int count)
        {
            fixed (uint* a = &_subauthority1)
            {
                return new Span<uint>(a, count);
            }
        }

        public unsafe void CopyAuthorities(in SubAuthorityArray source, int count)
        {
            if (count == 0) return;

            source.GetAuthorities(count).CopyTo(GetAuthorities(count));
        }
    }

    public bool Equals(in SID other)
        => Revision == other.Revision
            && IdentifierAuthority.Equals(other.IdentifierAuthority)
            && SubAuthorityCount == other.SubAuthorityCount
            && SubAuthorities.SequenceEqual(other.SubAuthorities);

    public unsafe bool Equals(SID* other)
        => other != null
            && Revision == other->Revision
            && IdentifierAuthority.Equals(other->IdentifierAuthority)
            && SubAuthorityCount == other->SubAuthorityCount
            && SubAuthorities.SequenceEqual(other->SubAuthorities);
}