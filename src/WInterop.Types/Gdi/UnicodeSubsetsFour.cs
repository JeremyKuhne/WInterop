﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd374090.aspx
    [Flags]
    public enum UnicodeSubsetsFour : uint
    {
        Buginese                        = 0b00000000_00000000_00000000_00000001,
        Glagothic                       = 0b00000000_00000000_00000000_00000010,
        Tifinagh                        = 0b00000000_00000000_00000000_00000100,
        YijingHexagramSymbols           = 0b00000000_00000000_00000000_00001000,
        SyloticNagri                    = 0b00000000_00000000_00000000_00010000,
        LinearB_AegeanNumbers           = 0b00000000_00000000_00000000_00100000,
        AncientGreekNumbers             = 0b00000000_00000000_00000000_01000000,
        Ugaritic                        = 0b00000000_00000000_00000000_10000000,
        OldPersian                      = 0b00000000_00000000_00000001_00000000,
        Shavian                         = 0b00000000_00000000_00000010_00000000,
        Osmanya                         = 0b00000000_00000000_00000100_00000000,
        CypriotSyllabary                = 0b00000000_00000000_00001000_00000000,
        Khaoshthi                       = 0b00000000_00000000_00010000_00000000,
        TaiXuanJingSymbols              = 0b00000000_00000000_00100000_00000000,
        Cuneiform                       = 0b00000000_00000000_01000000_00000000,
        CountingRodNumerals             = 0b00000000_00000000_10000000_00000000,
        Sudanese                        = 0b00000000_00000001_00000000_00000000,
        Lepcha                          = 0b00000000_00000010_00000000_00000000,
        OlChiki                         = 0b00000000_00000100_00000000_00000000,
        Saurashtra                      = 0b00000000_00001000_00000000_00000000,
        KayahLi                         = 0b00000000_00010000_00000000_00000000,
        Rejang                          = 0b00000000_00100000_00000000_00000000,
        Cham                            = 0b00000000_01000000_00000000_00000000,
        AncientSymbols                  = 0b00000000_10000000_00000000_00000000,
        PhaistosDisc                    = 0b00000001_00000000_00000000_00000000,
        LycianCarianLydian              = 0b00000010_00000000_00000000_00000000,
        MahjongDominoTiles              = 0b00000100_00000000_00000000_00000000,
        LayoutHorizontalRightToLeft     = 0b00001000_00000000_00000000_00000000,
        LayoutVerticalThenHorizontal    = 0b00010000_00000000_00000000_00000000,
        LayoutVertialBottomToTop        = 0b00100000_00000000_00000000_00000000,
        ReservedA                       = 0b01000000_00000000_00000000_00000000,
        ReservedB                       = 0b10000000_00000000_00000000_00000000
    }
}
