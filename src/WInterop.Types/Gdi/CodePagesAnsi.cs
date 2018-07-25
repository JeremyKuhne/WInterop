// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd317754.aspx
    [Flags]
    public enum CodePagesAnsi : uint
    {
        /// <summary>
        /// Code page 1252.
        /// </summary>
        Latin1                          = 0b00000000_00000000_00000000_00000001,

        /// <summary>
        /// Code page 1250.
        /// </summary>
        Latin2_CentralEurope            = 0b00000000_00000000_00000000_00000010,

        /// <summary>
        /// Code page 1251.
        /// </summary>
        Cyrillic                        = 0b00000000_00000000_00000000_00000100,

        /// <summary>
        /// Code page 1253.
        /// </summary>
        Greek                           = 0b00000000_00000000_00000000_00001000,

        /// <summary>
        /// Code page 1254.
        /// </summary>
        Turkish                         = 0b00000000_00000000_00000000_00010000,

        /// <summary>
        /// Code page 1255.
        /// </summary>
        Hebrew                          = 0b00000000_00000000_00000000_00100000,

        /// <summary>
        /// Code page 1256.
        /// </summary>
        Arabic                          = 0b00000000_00000000_00000000_01000000,

        /// <summary>
        /// Code page 1257.
        /// </summary>
        Baltic                          = 0b00000000_00000000_00000000_10000000,

        /// <summary>
        /// Code page 1258.
        /// </summary>
        Vietnamese                      = 0b00000000_00000000_00000001_00000000,

        ReservedAnsi09                  = 0b00000000_00000000_00000010_00000000,
        ReservedAnsi10                  = 0b00000000_00000000_00000100_00000000,
        ReservedAnsi11                  = 0b00000000_00000000_00001000_00000000,
        ReservedAnsi12                  = 0b00000000_00000000_00010000_00000000,
        ReservedAnsi13                  = 0b00000000_00000000_00100000_00000000,
        ReservedAnsi14                  = 0b00000000_00000000_01000000_00000000,
        ReservedAnsi15                  = 0b00000000_00000000_10000000_00000000,

        /// <summary>
        /// Code page 874.
        /// </summary>
        Thai                            = 0b00000000_00000001_00000000_00000000,

        /// <summary>
        /// Code page 932. (Shift-JIS)
        /// </summary>
        Japanese                        = 0b00000000_00000010_00000000_00000000,

        /// <summary>
        /// Code page 936.
        /// </summary>
        SimplifiedChinese               = 0b00000000_00000100_00000000_00000000,

        /// <summary>
        /// Code page 949. (Hangul TongHabHyung Code)
        /// </summary>
        KoreanUnifiedHangul             = 0b00000000_00001000_00000000_00000000,

        /// <summary>
        /// Code page 950.
        /// </summary>
        TraditionalChinese              = 0b00000000_00010000_00000000_00000000,

        /// <summary>
        /// Code page 1361.
        /// </summary>
        KoreanJohab                     = 0b00000000_00100000_00000000_00000000,

        ReservedAlternate22             = 0b00000000_01000000_00000000_00000000,
        ReservedAlternate23             = 0b00000000_10000000_00000000_00000000,
        ReservedAlternate24             = 0b00000001_00000000_00000000_00000000,
        ReservedAlternate25             = 0b00000010_00000000_00000000_00000000,
        ReservedAlternate26             = 0b00000100_00000000_00000000_00000000,
        ReservedAlternate27             = 0b00001000_00000000_00000000_00000000,
        ReservedAlternate28             = 0b00010000_00000000_00000000_00000000,
        ReservedAlternate29             = 0b00100000_00000000_00000000_00000000,
        ReservedSystem30                = 0b01000000_00000000_00000000_00000000,
        ReservedSystem31                = 0b10000000_00000000_00000000_00000000
    }
}

















