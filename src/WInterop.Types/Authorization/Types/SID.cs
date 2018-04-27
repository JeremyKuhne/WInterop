﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.Types
{

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
    /// The binary form of a SID. In string format SIDs are in the form:
    /// S-{revision}-{authority}[-{subauthority}...]
    /// </summary>
    /// <remarks>
    /// Per the documentation you're not supposed to access the SID fields directly. Given that the struct
    /// is publicly defined in the headers as above, it is unlikely to change. If you want to play safe
    /// by MSDN use the Win32 methods for all data access.
    /// 
    /// For ease of interaction and lifetime we simply define the struct at it's max size. This allows
    /// passing the struct around easily. It does eat up more memory than needed for the sub authorities,
    /// but that seems a decent tradeoff as we don't expect to create a large number of these. This does
    /// also require using <see cref="CopyFromNative"/> to get SIDs allocated by Windows.
    /// 
    /// The size of SID is 68 bytes.
    /// </remarks>
    public readonly struct SID
    {
        public readonly byte Revision;
        public readonly byte SubAuthorityCount;
        public readonly IdentifierAuthority IdentifierAuthority;

        // Also known as Relative Identifiers
        private readonly SubAuthorities _subAuthorities;

        // TODO: For some reason this doesn't give the right bits back
        // public ReadOnlySpan<uint> SubAuthority => _subAuthorities.GetAuthorities(SubAuthorityCount);

        /// <summary>
        /// Use this to copy from a native buffer, as the defined SID will likely not have
        /// a full set of SubAuthorities.
        /// </summary>
        public unsafe SID(SID* native)
        {
            Revision = native->Revision;
            SubAuthorityCount = native->SubAuthorityCount;
            IdentifierAuthority = native->IdentifierAuthority;
            _subAuthorities.CopyAuthorities(in native->_subAuthorities, SubAuthorityCount);
        }

        private struct SubAuthorities
        {
            // From winnt.h
            private const int SID_MAX_SUB_AUTHORITIES = 15;

            private unsafe fixed uint _authorities[SID_MAX_SUB_AUTHORITIES];

            private unsafe Span<uint> GetAuthorities(int count)
            {
                fixed (uint* a = _authorities)
                {
                    return new Span<uint>(a, count);
                }
            }

            public unsafe void CopyAuthorities(in SubAuthorities source, int count)
            {
                if (count == 0) return;

                source.GetAuthorities(count).CopyTo(GetAuthorities(count));
            }
        }
    }
}
