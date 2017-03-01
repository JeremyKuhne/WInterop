// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Support.Buffers;

namespace WInterop.Authorization.DataTypes
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
    /// by MSDN use the methods for all data access. (TODO: Not implemented yet.)
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct SID
    {
        // From winnt.h
        private const int SID_MAX_SUB_AUTHORITIES = 15;

        public byte Revision;
        public byte SubAuthorityCount;
        public SID_IDENTIFIER_AUTHORITY IdentifierAuthority;

        // Also known as Relative Identifiers
        public unsafe fixed uint SubAuthority[SID_MAX_SUB_AUTHORITIES];
    }
}
