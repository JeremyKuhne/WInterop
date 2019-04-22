// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native
{
    public struct FORMATETC
    {
        public ushort cfFormat;
        public unsafe DVTARGETDEVICE* ptd;
        public uint dwAspect;
        public int lindex;
        public uint tymed;
    }
}
