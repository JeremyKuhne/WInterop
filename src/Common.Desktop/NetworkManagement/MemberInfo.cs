// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.NetworkManagement
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name} {AccountType}")]
    public struct MemberInfo
    {
        public string Name;
        public SID_NAME_USE AccountType;
    }
}
