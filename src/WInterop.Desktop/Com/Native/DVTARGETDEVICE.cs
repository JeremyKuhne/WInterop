// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native
{
    // https://docs.microsoft.com/windows/desktop/api/objidl/ns-objidl-tagdvtargetdevice
    public struct DVTARGETDEVICE
    {
        public uint tdSize;
        public ushort tdDriverNameOffset;
        public ushort tdDeviceNameOffset;
        public ushort tdPortNameOffset;
        public ushort tdExtDevmodeOffset;

        // This is a trailing array of byte (i.e. BYTE tdData[1])-
        public byte tdData;
    }
}
