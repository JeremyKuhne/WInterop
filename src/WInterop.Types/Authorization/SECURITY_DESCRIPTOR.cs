// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379561.aspx
    public unsafe struct SECURITY_DESCRIPTOR
    {
        public byte Revision;
        public byte Sbz1;
        public ushort Control;
        public SID* Owner;
        public SID* Group;
        public ACL* Sacl;
        public ACL* Dacl;
    }
}
