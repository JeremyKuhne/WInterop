// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379561.aspx
    public unsafe readonly struct SECURITY_DESCRIPTOR
    {
        public readonly byte Revision;
        public readonly byte Sbz1;
        public readonly SECURITY_DESCRIPTOR_CONTROL Control;
        public readonly SID* Owner;
        public readonly SID* Group;
        public readonly ACL* Sacl;
        public readonly ACL* Dacl;
    }
}
