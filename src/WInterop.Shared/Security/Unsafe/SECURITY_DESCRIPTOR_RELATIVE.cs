// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Unsafe
{
    public unsafe readonly struct SECURITY_DESCRIPTOR_RELATIVE
    {
        public readonly byte Revision;
        public readonly byte Sbz1;
        public readonly SECURITY_DESCRIPTOR_CONTROL Control;
        public readonly uint Owner;
        public readonly uint Group;
        public readonly uint Sacl;
        public readonly uint Dacl;
    }
}
