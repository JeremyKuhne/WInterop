// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Security;

namespace WInterop.Network
{
    [DebuggerDisplay("{Name} {AccountType}")]
    public struct MemberInfo
    {
        public string Name;
        public SidNameUse AccountType;
    }
}
