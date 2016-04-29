// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Authentication;

namespace WInterop.Handles
{
    // The full struct isn't officially documented, names may be wrong.
    //
    //  https://msdn.microsoft.com/en-us/library/windows/hardware/ff551947.aspx
    //      typedef struct __PUBLIC_OBJECT_TYPE_INFORMATION
    //      {
    //          UNICODE_STRING TypeName;
    //          ULONG Reserved[22];    // reserved for internal use
    //      } PUBLIC_OBJECT_TYPE_INFORMATION, *PPUBLIC_OBJECT_TYPE_INFORMATION;
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct OBJECT_TYPE_INFORMATION
    {
        public UNICODE_STRING TypeName;

        // All below are not officially documented, names may be incorrect

        public uint TotalNumberOfObjects;
        public uint TotalNumberOfHandles;
        public uint TotalPagedPoolUsage;
        public uint TotalNonPagedPoolUsage;
        public uint TotalNamePoolUsage;
        public uint TotalHandleTableUsage;

        // HighWater is the peak value
        public uint HighWaterNumberOfObjects;
        public uint HighWaterNumberOfHandles;
        public uint HighWaterPagedPoolUsage;
        public uint HighWaterNonPagedPoolUsage;
        public uint HighWaterNamePoolUsage;
        public uint HighWaterHandleTableUsage;

        /// <summary>
        /// Attributes that can't be used on instances
        /// </summary>
        public ObjectAttributes InvalidAttributes;

        /// <summary>
        /// Mapping of generic access rights (read/write/execute/all) to the type's specific rights
        /// </summary>
        public GENERIC_MAPPING GenericMapping;

        /// <summary>
        /// Types of access a thread can request when opening a handle to an object of this type
        /// (read, write, terminate, suspend, etc.)
        /// </summary>
        public uint ValidAccessMask;
        public byte SecurityRequired;
        public byte MaintainHandleCount;
        public byte TypeIndex;
        public byte ReservedByte;

        /// <summary>
        /// Instances are allocated from paged or non-paged (0) memory
        /// </summary>
        public uint PoolType;
        public uint DefaultPagedPoolCharge;
        public uint DefaultNonPagedPoolCharge;
    }
}
