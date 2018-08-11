// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace WInterop.Security
{
    [DebuggerDisplay("{Privilege} : {Attributes}")]
    public struct PrivilegeSetting
    {
        public PrivilegeSetting(Privilege privilege, PrivilegeAttributes attributes)
        {
            Privilege = privilege;
            Attributes = attributes;
        }

        public Privilege Privilege;
        public PrivilegeAttributes Attributes;
    }
}
