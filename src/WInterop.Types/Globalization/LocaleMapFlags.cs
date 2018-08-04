// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    public enum LocaleMapFlags : uint
    {
        /// <summary>
        /// Ignore case. [NORM_IGNORECASE]
        /// </summary>
        IgnoreCase = 0x00000001,

        /// <summary>
        /// Ignore nonspacing characters. [NORM_IGNORENONSPACE]
        /// </summary>
        IgnoreNonSpace =  0x00000002,

        /// <summary>
        /// Ignore symbols and punctuation. [NORM_IGNORESYMBOLS]
        /// </summary>
        IgnoreSymbols =  0x00000004,

        /// <summary>
        /// Sort digits as numbers. [SORT_DIGITSASNUMBERS]
        /// </summary>
        SortDigitsAsNumbers = 0x00000008,

        /// <summary>
        /// Ignore case as linguistically appropriate. [LINGUISTIC_IGNORECASE]
        /// </summary>
        LinguisticIgnoreCase = 0x00000010,

        /// <summary>
        /// Ignore nonspacing characters as linguistically appropriate. [LINGUISTIC_IGNOREDIACRITIC]
        /// </summary>
        LinguisticIgnoreDiacritic = 0x00000020,

        /// <summary>
        /// Map all characters to lower case (if the locale supports it). [LCMAP_LOWERCASE]
        /// </summary>
        LowerCase = 0x00000100,

        /// <summary>
        /// Map all characters to upper case (if the locale supports it). [LCMAP_UPPERCASE]
        /// </summary>
        UpperCase = 0x00000200,

        /// <summary>
        /// Map first characters of major words to upper case (if the locale supports it). [LCMAP_TITLECASE]
        /// </summary>
        TitleCase = 0x00000300,

        /// <summary>
        /// Produce a normalized sort key. [LCMAP_SORTKEY]
        /// </summary>
        SortKey = 0x00000400,

        /// <summary>
        /// Use byte reversal. [LCMAP_BYTEREV]
        /// </summary>
        ByteReversal = 0x00000800,

        /// <summary>
        /// Treat punctuation the same as symbols. [SORT_STRINGSORT]
        /// </summary>
        StringSort = 0x00001000,  // use string sort method

        /// <summary>
        /// Do not differentiate between hiragana and katakana characters. [NORM_IGNOREKANATYPE]
        /// </summary>
        IgnoreKanaType = 0x00010000,

        /// <summary>
        /// Ignore the difference between half-width and full-width characters. [NORM_IGNOREWIDTH]
        /// </summary>
        IgnoreWidth = 0x00020000,

        /// <summary>
        /// Map katakana to hiragana. [LCMAP_HIRAGANA]
        /// </summary>
        Hiragana = 0x00100000,

        /// <summary>
        /// Map hirigana to katakana. [LCMAP_KATAKANA]
        /// </summary>
        Katakana = 0x00200000,

        /// <summary>
        /// Use narrow characters where applicable. [LCMAP_HALFWIDTH]
        /// </summary>
        Halfwidth = 0x00400000,

        /// <summary>
        /// Use Unicode (wide) characters where applicable. [LCMAP_FULLWIDTH]
        /// </summary>
        FullWidth = 0x00800000,

        /// <summary>
        /// Use linguistic rules for casing, instead of (the default) file system rules.
        /// Used only with LowerCase and UpperCase flags. [LCMAP_LINGUISTIC_CASING]
        /// </summary>
        LinguisticCasingChange = 0x01000000,

        /// <summary>
        /// Map traditional Chinese characters to simplified Chinese characters. [LCMAP_SIMPLIFIED_CHINESE]
        /// </summary>
        SimplifiedChinese = 0x02000000,

        /// <summary>
        /// Map simplified Chinese characters to traditional Chinese characters. [LCMAP_TRADITIONAL_CHINESE]
        /// </summary>
        TraditionalChinese = 0x04000000,

        /// <summary>
        /// Use linguistic rules for casing, instead of (the default) file system rules. [NORM_LINGUISTIC_CASING]
        /// </summary>
        LinguisticCasing = 0x08000000,

        /// <summary>
        /// Returns the sort handle that then can be passed back as the sortHandle argument instead of a locale name.
        /// [LCMAP_SORTHANDLE]
        /// </summary>
        /// <remarks>
        /// Introduced in Windows 8.
        /// </remarks>
        SortHandle = 0x20000000,

        /// <summary>
        /// Returns a hash of the given string. [LCMAP_HASH]
        /// </summary>
        /// <remarks>
        /// Introduced in Windows 8.
        /// </remarks>
        Hash = 0x00040000
    }
}
