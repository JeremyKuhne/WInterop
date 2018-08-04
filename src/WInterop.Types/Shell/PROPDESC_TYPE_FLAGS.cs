// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    [Flags]
    public enum PROPDESC_TYPE_FLAGS : uint
    {
        PDTF_DEFAULT = 0,
        PDTF_MULTIPLEVALUES = 0x1,
        PDTF_ISINNATE = 0x2,
        PDTF_ISGROUP = 0x4,
        PDTF_CANGROUPBY = 0x8,
        PDTF_CANSTACKBY = 0x10,
        PDTF_ISTREEPROPERTY = 0x20,
        PDTF_INCLUDEINFULLTEXTQUERY = 0x40,
        PDTF_ISVIEWABLE = 0x80,
        PDTF_ISQUERYABLE = 0x100,
        PDTF_CANBEPURGED = 0x200,
        PDTF_SEARCHRAWVALUE = 0x400,
        PDTF_DONTCOERCEEMPTYSTRINGS = 0x800,
        PDTF_ALWAYSINSUPPLEMENTALSTORE = 0x1000,
        PDTF_ISSYSTEMPROPERTY = 0x80000000,
        PDTF_MASK_ALL = 0x80001fff
    }
}
