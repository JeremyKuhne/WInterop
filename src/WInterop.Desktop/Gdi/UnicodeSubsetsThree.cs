// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd374090.aspx
    [Flags]
    public enum UnicodeSubsetsThree : uint
    {
        CombiningHalfMarks              = 0b00000000_00000000_00000000_00000001,
        VerticalForms_CJKCompatForms    = 0b00000000_00000000_00000000_00000010,
        SmallFormVariants               = 0b00000000_00000000_00000000_00000100,
        ArabicPresentationFormsB        = 0b00000000_00000000_00000000_00001000,
        HalfwidthFullwidthForms         = 0b00000000_00000000_00000000_00010000,
        Specials                        = 0b00000000_00000000_00000000_00100000,
        Tibetan                         = 0b00000000_00000000_00000000_01000000,
        Syriac                          = 0b00000000_00000000_00000000_10000000,
        Thaana                          = 0b00000000_00000000_00000001_00000000,
        Sinhala                         = 0b00000000_00000000_00000010_00000000,
        Myanmar                         = 0b00000000_00000000_00000100_00000000,
        Ethiopic                        = 0b00000000_00000000_00001000_00000000,
        Cherokee                        = 0b00000000_00000000_00010000_00000000,
        UnifiedCanadianAboriginal       = 0b00000000_00000000_00100000_00000000,
        Ogham                           = 0b00000000_00000000_01000000_00000000,
        Runic                           = 0b00000000_00000000_10000000_00000000,
        Khmer                           = 0b00000000_00000001_00000000_00000000,
        Mongolian                       = 0b00000000_00000010_00000000_00000000,
        BraillePatterns                 = 0b00000000_00000100_00000000_00000000,
        YiSyllablesRadicals             = 0b00000000_00001000_00000000_00000000,
        TagalogHanunooBuhidTagbanwa     = 0b00000000_00010000_00000000_00000000,
        OldItalic                       = 0b00000000_00100000_00000000_00000000,
        Gothic                          = 0b00000000_01000000_00000000_00000000,
        Deseret                         = 0b00000000_10000000_00000000_00000000,
        MusicalSymbolsNotation          = 0b00000001_00000000_00000000_00000000,
        MathAlphanumericSymbols         = 0b00000010_00000000_00000000_00000000,
        PrivateUse                      = 0b00000100_00000000_00000000_00000000,
        VariationSelectors              = 0b00001000_00000000_00000000_00000000,
        Tags                            = 0b00010000_00000000_00000000_00000000,
        Limbu                           = 0b00100000_00000000_00000000_00000000,
        TaiLe                           = 0b01000000_00000000_00000000_00000000,
        NewTailLue                      = 0b10000000_00000000_00000000_00000000
    }
}
