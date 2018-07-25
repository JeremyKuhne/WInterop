// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd145132.aspx
    public enum CharacterSet : byte
    {
        ANSI_CHARSET           = 0,
        DEFAULT_CHARSET        = 1,
        SYMBOL_CHARSET         = 2,
        MAC_CHARSET            = 77,
        SHIFTJIS_CHARSET       = 128,
        HANGEUL_CHARSET        = 129,
        HANGUL_CHARSET         = 129,
        GB2312_CHARSET         = 134,
        CHINESEBIG5_CHARSET    = 136,
        OEM_CHARSET            = 255,
        JOHAB_CHARSET          = 130,
        HEBREW_CHARSET         = 177,
        ARABIC_CHARSET         = 178,
        GREEK_CHARSET          = 161,
        TURKISH_CHARSET        = 162,
        VIETNAMESE_CHARSET     = 163,
        BALTIC_CHARSET         = 186,
        THAI_CHARSET           = 222,
        EASTEUROPE_CHARSET     = 238,
        RUSSIAN_CHARSET        = 204
    }
}
