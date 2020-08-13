// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    ///  [TOKEN_STATISTICS]
    /// </summary>
    public struct TokenStatistics
    {
        public LUID TokenId;
        public LUID AuthenticationId;
        public long ExpirationTime;
        public TokenType TokenType;
        public ImpersonationLevel ImpersonationLevel;

        /// <summary>
        ///  Bytes allocated for storing the DACL and primary identifier.
        /// </summary>
        public uint DynamicCharged;

        /// <summary>
        ///  Allocated bytes not in use.
        /// </summary>
        public uint DynamicAvailable;

        public uint GroupCount;
        public uint PrivilegeCount;

        /// <summary>
        ///  Luid that changes every time the token is modified.
        /// </summary>
        public LUID ModifiedId;
    }
}
