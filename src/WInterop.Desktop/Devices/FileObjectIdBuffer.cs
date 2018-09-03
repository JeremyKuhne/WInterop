// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Devices
{
    /// <summary>
    /// [FILE_OBJECTID_BUFFER]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364393.aspx
    [StructLayout(LayoutKind.Explicit)]
    public struct FileObjectIdBuffer
    {
        [FieldOffset(0)]
        public FixedByte.Size16 ObjectId;

        [FieldOffset(16)]
        public FixedByte.Size16 BirthVolumeId;

        [FieldOffset(32)]
        public FixedByte.Size16 BirthObjectId;

        [FieldOffset(48)]
        public FixedByte.Size16 DomainId;

        [FieldOffset(16)]
        public FixedByte.Size48 ExtendedInfo;
    }
}
