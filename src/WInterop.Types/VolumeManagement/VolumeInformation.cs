// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.VolumeManagement.DataTypes
{
    public struct VolumeInformation
    {
        public string RootPathName;
        public string VolumeName;
        public uint VolumeSerialNumber;
        public uint MaximumComponentLength;
        public FileSystemFeature FileSystemFlags;
        public string FileSystemName;
    }
}
