﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    [Flags]
    public enum SHCOLSTATE : uint
    {
        SHCOLSTATE_DEFAULT = 0,
        SHCOLSTATE_TYPE_STR = 0x1,
        SHCOLSTATE_TYPE_INT = 0x2,
        SHCOLSTATE_TYPE_DATE = 0x3,
        SHCOLSTATE_TYPEMASK = 0xf,
        SHCOLSTATE_ONBYDEFAULT = 0x10,
        SHCOLSTATE_SLOW = 0x20,
        SHCOLSTATE_EXTENDED = 0x40,
        SHCOLSTATE_SECONDARYUI = 0x80,
        SHCOLSTATE_HIDDEN = 0x100,
        SHCOLSTATE_PREFER_VARCMP = 0x200,
        SHCOLSTATE_PREFER_FMTCMP = 0x400,
        SHCOLSTATE_NOSORTBYFOLDERNESS = 0x800,
        SHCOLSTATE_VIEWONLY = 0x10000,
        SHCOLSTATE_BATCHREAD = 0x20000,
        SHCOLSTATE_NO_GROUPBY = 0x40000,
        SHCOLSTATE_FIXED_WIDTH = 0x1000,
        SHCOLSTATE_NODPISCALE = 0x2000,
        SHCOLSTATE_FIXED_RATIO = 0x4000,
        SHCOLSTATE_DISPLAYMASK = 0xf000
    }
}
