// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd145132.aspx
public enum CharacterSet : byte
{
    /// <summary>
    ///  [ANSI_CHARSET]
    /// </summary>
    Ansi = 0,

    /// <summary>
    ///  [DEFAULT_CHARSET]
    /// </summary>
    Default = 1,

    /// <summary>
    ///  [SYMBOL_CHARSET]
    /// </summary>
    Symbol = 2,

    /// <summary>
    ///  [MAC_CHARSET]
    /// </summary>
    Mac = 77,

    /// <summary>
    ///  [SHIFTJIS_CHARSET]
    /// </summary>
    ShiftJis = 128,

    /// <summary>
    ///  [HANGEUL_CHARSET]
    /// </summary>
    Hangeul = 129,

    /// <summary>
    ///  [HANGUL_CHARSET]
    /// </summary>
    Hangul = 129,

    /// <summary>
    ///  [GB2312_CHARSET]
    /// </summary>
    GB2312 = 134,

    /// <summary>
    ///  [CHINESEBIG5_CHARSET]
    /// </summary>
    ChineseBig5 = 136,

    /// <summary>
    ///  [OEM_CHARSET]
    /// </summary>
    Oem = 255,

    /// <summary>
    ///  [JOHAB_CHARSET]
    /// </summary>
    Johab = 130,

    /// <summary>
    ///  [HEBREW_CHARSET]
    /// </summary>
    Hebrew = 177,

    /// <summary>
    ///  [ARABIC_CHARSET]
    /// </summary>
    Arabic = 178,

    /// <summary>
    ///  [GREEK_CHARSET]
    /// </summary>
    Greek = 161,

    /// <summary>
    ///  [TURKISH_CHARSET]
    /// </summary>
    Turkish = 162,

    /// <summary>
    ///  [VIETNAMESE_CHARSET]
    /// </summary>
    Vietnamese = 163,

    /// <summary>
    ///  [BALTIC_CHARSET]
    /// </summary>
    Baltic = 186,

    /// <summary>
    ///  [THAI_CHARSET]
    /// </summary>
    Thai = 222,

    /// <summary>
    ///  [EASTEUROPE_CHARSET]
    /// </summary>
    EasternEurope = 238,

    /// <summary>
    ///  [RUSSIAN_CHARSET]
    /// </summary>
    Russian = 204
}