// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd374090.aspx
    [Flags]
    public enum UnicodeSubsetsOne : uint
    {
        BasicLatin                      = 0b00000000_00000000_00000000_00000001,
        Latin1Supplement                = 0b00000000_00000000_00000000_00000010,
        LatinExtendedA                  = 0b00000000_00000000_00000000_00000100,
        LatinExtendedB                  = 0b00000000_00000000_00000000_00001000,
        IPAPhoneticExtensions           = 0b00000000_00000000_00000000_00010000,
        SpacingToneModifier             = 0b00000000_00000000_00000000_00100000,
        CombiningDiacriticalMarks       = 0b00000000_00000000_00000000_01000000,
        GreekAndCoptic                  = 0b00000000_00000000_00000000_10000000,
        Coptic                          = 0b00000000_00000000_00000001_00000000,
        Cyrillic                        = 0b00000000_00000000_00000010_00000000,
        Armenian                        = 0b00000000_00000000_00000100_00000000,
        Hebrew                          = 0b00000000_00000000_00001000_00000000,
        Vai                             = 0b00000000_00000000_00010000_00000000,
        Arabic                          = 0b00000000_00000000_00100000_00000000,
        NKo                             = 0b00000000_00000000_01000000_00000000,
        Devanagari                      = 0b00000000_00000000_10000000_00000000,
        Bangla                          = 0b00000000_00000001_00000000_00000000,
        Gurmukhi                        = 0b00000000_00000010_00000000_00000000,
        Gujarati                        = 0b00000000_00000100_00000000_00000000,
        Odia                            = 0b00000000_00001000_00000000_00000000,
        Tamil                           = 0b00000000_00010000_00000000_00000000,
        Telugu                          = 0b00000000_00100000_00000000_00000000,
        Kannada                         = 0b00000000_01000000_00000000_00000000,
        Malayalam                       = 0b00000000_10000000_00000000_00000000,
        Thai                            = 0b00000001_00000000_00000000_00000000,
        Lao                             = 0b00000010_00000000_00000000_00000000,
        Georgian                        = 0b00000100_00000000_00000000_00000000,
        Balinese                        = 0b00001000_00000000_00000000_00000000,
        HangulJamo                      = 0b00010000_00000000_00000000_00000000,
        LatinExtendedAdditionalCD       = 0b00100000_00000000_00000000_00000000,
        GreekExtended                   = 0b01000000_00000000_00000000_00000000,
        Punctuation                     = 0b10000000_00000000_00000000_00000000
    }
}
