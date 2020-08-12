// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd317754.aspx
    [Flags]
    public enum CodePagesOem : uint
    {
        ReservedOem32                   = 0b00000000_00000000_00000000_00000001,
        ReservedOem33                   = 0b00000000_00000000_00000000_00000010,
        ReservedOem34                   = 0b00000000_00000000_00000000_00000100,
        ReservedOem35                   = 0b00000000_00000000_00000000_00001000,
        ReservedOem36                   = 0b00000000_00000000_00000000_00010000,
        ReservedOem37                   = 0b00000000_00000000_00000000_00100000,
        ReservedOem38                   = 0b00000000_00000000_00000000_01000000,
        ReservedOem39                   = 0b00000000_00000000_00000000_10000000,
        ReservedOem40                   = 0b00000000_00000000_00000001_00000000,
        ReservedOem41                   = 0b00000000_00000000_00000010_00000000,
        ReservedOem42                   = 0b00000000_00000000_00000100_00000000,
        ReservedOem43                   = 0b00000000_00000000_00001000_00000000,
        ReservedOem44                   = 0b00000000_00000000_00010000_00000000,
        ReservedOem45                   = 0b00000000_00000000_00100000_00000000,
        ReservedOem46                   = 0b00000000_00000000_01000000_00000000,

        /// <summary>
        ///  Code page 1258.
        /// </summary>
        Vietnamese                      = 0b00000000_00000000_10000000_00000000,

        /// <summary>
        ///  Code page 869.
        /// </summary>
        ModernGreek                     = 0b00000000_00000001_00000000_00000000,

        /// <summary>
        ///  Code page 866.
        /// </summary>
        Russian                         = 0b00000000_00000010_00000000_00000000,

        /// <summary>
        /// Code page 865.
        /// </summary>
        Nordic                          = 0b00000000_00000100_00000000_00000000,

        /// <summary>
        ///  Code page 864.
        /// </summary>
        Arabic                          = 0b00000000_00001000_00000000_00000000,

        /// <summary>
        /// Code page 863.
        /// </summary>
        CanadianFrench                  = 0b00000000_00010000_00000000_00000000,

        /// <summary>
        ///  Code page 862.
        /// </summary>
        Hebrew                          = 0b00000000_00100000_00000000_00000000,

        /// <summary>
        ///  Code page 861.
        /// </summary>
        Icelandic                       = 0b00000000_01000000_00000000_00000000,

        /// <summary>
        /// Code page 860.
        /// </summary>
        Portugese                       = 0b00000000_10000000_00000000_00000000,

        /// <summary>
        ///  Code page 857.
        /// </summary>
        Turkish                         = 0b00000001_00000000_00000000_00000000,

        /// <summary>
        /// Code page 855. (Primarily Russian)
        /// </summary>
        Cyrillic                        = 0b00000010_00000000_00000000_00000000,

        /// <summary>
        ///  Code page 852.
        /// </summary>
        Latin2                          = 0b00000100_00000000_00000000_00000000,

        /// <summary>
        ///  Code page 775.
        /// </summary>
        Baltic                          = 0b00001000_00000000_00000000_00000000,

        /// <summary>
        /// Code page 737. (Formerly 437G)
        /// </summary>
        Greek                           = 0b00010000_00000000_00000000_00000000,

        /// <summary>
        ///  Code pages 708 and 720. (ASMO 708)
        /// </summary>
        ArabicAsmo                      = 0b00100000_00000000_00000000_00000000,

        /// <summary>
        ///  Code page 850.
        /// </summary>
        MuiltilingualLatin              = 0b01000000_00000000_00000000_00000000,

        /// <summary>
        ///  Code page 437.
        /// </summary>
        US                              = 0b10000000_00000000_00000000_00000000
    }
}
