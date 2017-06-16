// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    [Flags]
    public enum PROPDESC_VIEW_FLAGS
    {
        PDVF_DEFAULT = 0,
        PDVF_CENTERALIGN = 0x1,
        PDVF_RIGHTALIGN = 0x2,
        PDVF_BEGINNEWGROUP = 0x4,
        PDVF_FILLAREA = 0x8,
        PDVF_SORTDESCENDING = 0x10,
        PDVF_SHOWONLYIFPRESENT = 0x20,
        PDVF_SHOWBYDEFAULT = 0x40,
        PDVF_SHOWINPRIMARYLIST = 0x80,
        PDVF_SHOWINSECONDARYLIST = 0x100,
        PDVF_HIDELABEL = 0x200,
        PDVF_HIDDEN = 0x800,
        PDVF_CANWRAP = 0x1000,
        PDVF_MASK_ALL = 0x1bff
    }
}
