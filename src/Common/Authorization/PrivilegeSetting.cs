// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Privilege} : {State}")]
    public struct PrivilegeSetting
    {
        internal PrivilegeSetting(string privilege, PrivilegeState state)
        {
            Privileges p;
            if (!Enum.TryParse(privilege, out p))
            {
                p = Privileges.UnknownPrivilege;
            }
            Privilege = p;
            State = state;
        }

        public Privileges Privilege;
        public PrivilegeState State;
    }
}
