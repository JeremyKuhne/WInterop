// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183499.aspx
    public enum FontWeight : int
    {
        FW_DONTCARE        = 0,
        FW_THIN            = 100,
        FW_EXTRALIGHT      = 200,
        FW_LIGHT           = 300,
        FW_NORMAL          = 400,
        FW_MEDIUM          = 500,
        FW_SEMIBOLD        = 600,
        FW_BOLD            = 700,
        FW_EXTRABOLD       = 800,
        FW_HEAVY           = 900,

        FW_ULTRALIGHT      = FW_EXTRALIGHT,
        FW_REGULAR         = FW_NORMAL,
        FW_DEMIBOLD        = FW_SEMIBOLD,
        FW_ULTRABOLD       = FW_EXTRABOLD,
        FW_BLACK           = FW_HEAVY
    }
}
