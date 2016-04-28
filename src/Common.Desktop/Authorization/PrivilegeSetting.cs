// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace WInterop.Authorization
{
    [DebuggerDisplay("{Privilege} : {Attributes}")]
    public struct PrivilegeSetting
    {
        internal PrivilegeSetting(string privilege, PrivilegeAttributes attributes)
        {
            Privileges p;
            if (!Enum.TryParse(privilege, out p))
            {
                p = Privileges.UnknownPrivilege;
            }
            Privilege = p;
            Attributes = attributes;
        }

        public Privileges Privilege;
        public PrivilegeAttributes Attributes;
    }
}
