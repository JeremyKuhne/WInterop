﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/aa379307.aspx">PRIVILEGE_SET</a> structure.
    /// </summary>
    public struct PRIVILEGE_SET
    {
        public uint PrivilegeCount;
        public uint Control;
        public LUID_AND_ATTRIBUTES Privilege;
    }
}
