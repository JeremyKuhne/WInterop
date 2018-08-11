// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Handles
{
    // The full struct isn't officially documented, names may be wrong.
    //
    //  http://msdn.microsoft.com/en-us/library/windows/hardware/ff551944.aspx
    //      typedef struct _PUBLIC_OBJECT_BASIC_INFORMATION
    //      {
    //          ULONG Attributes;
    //          ACCESS_MASK GrantedAccess;
    //          ULONG HandleCount;
    //          ULONG PointerCount;
    //          ULONG Reserved[10];    // reserved for internal use
    //      } PUBLIC_OBJECT_BASIC_INFORMATION, *PPUBLIC_OBJECT_BASIC_INFORMATION;
    //
    // ACCESS_MASK
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx
    //
    public struct OBJECT_BASIC_INFORMATION
    {
        public ObjectAttributes Attributes;
        public uint GrantedAccess;
        public uint HandleCount;
        public uint PointerCount;
        public uint PagedPoolCharge;
        public uint NonPagedPoolCharge;
        public uint Reserved1;
        public uint Reserved2;
        public uint Reserved3;
        public uint NameInfoSize;
        public uint TypeInfoSize;
        public uint SecurityDescriptorSize;
        public long CreationTime;
    }
}
