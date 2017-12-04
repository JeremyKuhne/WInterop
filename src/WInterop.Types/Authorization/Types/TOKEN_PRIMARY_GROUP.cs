// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Gets/Sets the primary group for objects created by a given access token.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379629.aspx"/>
    /// </summary>
    public unsafe struct TOKEN_PRIMARY_GROUP
    {
        public SID* PrimaryGroup;
    }
}
