// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security.Unsafe
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379630.aspx">TOKEN_PRIVILEGES</a> structure.
    /// </summary>
    public struct TOKEN_PRIVILEGES
    {
        public uint PrivilegeCount;

        // This is an ANYSIZE_ARRAY
        private LuidAndAttributes _Privileges;

        unsafe public ReadOnlySpan<LuidAndAttributes> Privileges
        {
            get { fixed (LuidAndAttributes* p = &_Privileges) { return new ReadOnlySpan<LuidAndAttributes>(p, (int)PrivilegeCount); } }
        }
    }
}
