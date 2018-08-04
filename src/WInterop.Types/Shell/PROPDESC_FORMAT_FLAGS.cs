// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    [Flags]
    public enum PROPDESC_FORMAT_FLAGS
    {
        PDFF_DEFAULT = 0,
        PDFF_PREFIXNAME = 0x1,
        PDFF_FILENAME = 0x2,
        PDFF_ALWAYSKB = 0x4,
        PDFF_RESERVED_RIGHTTOLEFT = 0x8,
        PDFF_SHORTTIME = 0x10,
        PDFF_LONGTIME = 0x20,
        PDFF_HIDETIME = 0x40,
        PDFF_SHORTDATE = 0x80,
        PDFF_LONGDATE = 0x100,
        PDFF_HIDEDATE = 0x200,
        PDFF_RELATIVEDATE = 0x400,
        PDFF_USEEDITINVITATION = 0x800,
        PDFF_READONLY = 0x1000,
        PDFF_NOAUTOREADINGORDER = 0x2000
    }
}
