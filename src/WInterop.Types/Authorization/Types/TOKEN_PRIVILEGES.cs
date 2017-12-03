// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379630.aspx">TOKEN_PRIVILEGES</a> structure.
    /// </summary>
    public struct TOKEN_PRIVILEGES
    {
        public uint PrivilegeCount;

        private LUID_AND_ATTRIBUTES _Privileges;

        public unsafe ReadOnlySpan<LUID_AND_ATTRIBUTES> Privileges
        {
            get
            {
                fixed (void* p = &_Privileges) { return new ReadOnlySpan<LUID_AND_ATTRIBUTES>(p, (int)PrivilegeCount); };
            }
        }
    }
}
