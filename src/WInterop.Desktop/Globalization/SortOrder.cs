// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    public enum SortOrder : byte
    {
        SORT_DEFAULT                     = 0x0,
        SORT_INVARIANT_MATH              = 0x1,
        SORT_JAPANESE_XJIS               = 0x0,
        SORT_JAPANESE_RADICALSTROKE      = 0x4,
        SORT_CHINESE_BIG5                = 0x0,
        SORT_CHINESE_PRCP                = 0x0,
        SORT_CHINESE_PRC                 = 0x2,
        SORT_CHINESE_BOPOMOFO            = 0x3,
        SORT_CHINESE_RADICALSTROKE       = 0x4,
        SORT_KOREAN_KSC                  = 0x0,
        SORT_GERMAN_PHONE_BOOK           = 0x1,
        SORT_HUNGARIAN_DEFAULT           = 0x0,
        SORT_HUNGARIAN_TECHNICAL         = 0x1,
        SORT_GEORGIAN_TRADITIONAL        = 0x0,
        SORT_GEORGIAN_MODERN             = 0x1
    }
}