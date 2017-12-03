// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Contains information about group SIDs for an access token. Used with GetTokenInformation
    /// and AdjustTokenGroups.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379624.aspx"/>
    /// </summary>
    public struct TOKEN_GROUPS
    {
        public uint GroupCount;

        // This is an ANYSIZE_ARRAY
        public SID_AND_ATTRIBUTES Groups;
    }
}
