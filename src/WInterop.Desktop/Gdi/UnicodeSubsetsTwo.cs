// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd374090.aspx
    [Flags]
    public enum UnicodeSubsetsTwo : uint
    {
        SuperscriptsSubscripts          = 0b00000000_00000000_00000000_00000001,
        CurrencySymbols                 = 0b00000000_00000000_00000000_00000010,
        SymbolCombiningDiacritical      = 0b00000000_00000000_00000000_00000100,
        LetterlikeSymbols               = 0b00000000_00000000_00000000_00001000,
        NumberForms                     = 0b00000000_00000000_00000000_00010000,
        Arrows                          = 0b00000000_00000000_00000000_00100000,
        MathSymbolsOperators            = 0b00000000_00000000_00000000_01000000,
        Technical                       = 0b00000000_00000000_00000000_10000000,
        ControlPictures                 = 0b00000000_00000000_00000001_00000000,
        OCR                             = 0b00000000_00000000_00000010_00000000,
        EnclosedAlphanumerics           = 0b00000000_00000000_00000100_00000000,
        BoxDrawing                      = 0b00000000_00000000_00001000_00000000,
        BlockElements                   = 0b00000000_00000000_00010000_00000000,
        GeometricShapes                 = 0b00000000_00000000_00100000_00000000,
        MiscSymbols                     = 0b00000000_00000000_01000000_00000000,
        Dingbats                        = 0b00000000_00000000_10000000_00000000,
        CJKSymbolsPunctuation           = 0b00000000_00000001_00000000_00000000,
        Hiragana                        = 0b00000000_00000010_00000000_00000000,
        Katakana                        = 0b00000000_00000100_00000000_00000000,
        Bopomofo                        = 0b00000000_00001000_00000000_00000000,
        HangulCompatibilityJamo         = 0b00000000_00010000_00000000_00000000,
        Phags_pa                        = 0b00000000_00100000_00000000_00000000,
        CJKEnclosed_Months              = 0b00000000_01000000_00000000_00000000,
        CJKCompatibility                = 0b00000000_10000000_00000000_00000000,
        HangulSyllables                 = 0b00000001_00000000_00000000_00000000,
        NonPlane0                       = 0b00000010_00000000_00000000_00000000,
        Phonecian                       = 0b00000100_00000000_00000000_00000000,
        CJKRadicals_UnifiedIdeographs   = 0b00001000_00000000_00000000_00000000,
        PrivateUse                      = 0b00010000_00000000_00000000_00000000,
        CJKStrokes_CompatIdeographs     = 0b00100000_00000000_00000000_00000000,
        AlpabeticPresentationForms      = 0b01000000_00000000_00000000_00000000,
        ArabicPresentationFormsA        = 0b10000000_00000000_00000000_00000000
    }
}
