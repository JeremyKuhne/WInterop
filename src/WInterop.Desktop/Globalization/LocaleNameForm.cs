// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd464799.aspx
    public enum LocaleNameForm : uint
    {
        LOCALE_SLOCALIZEDDISPLAYNAME  = 0x00000002,   // localized name of locale, eg "German (Germany)" in UI language
        LOCALE_SENGLISHDISPLAYNAME    = 0x00000072,   // Display name (language + country/region usually) in English, eg "German (Germany)"
        LOCALE_SNATIVEDISPLAYNAME     = 0x00000073,   // Display name in native locale language, eg "Deutsch (Deutschland)
        LOCALE_SLOCALIZEDLANGUAGENAME = 0x0000006f,   // Language Display Name for a language, eg "German" in UI language
        LOCALE_SENGLISHLANGUAGENAME   = 0x00001001,   // English name of language, eg "German"
        LOCALE_SNATIVELANGUAGENAME    = 0x00000004,   // native name of language, eg "Deutsch"
        LOCALE_SLOCALIZEDCOUNTRYNAME  = 0x00000006,   // localized name of country/region, eg "Germany" in UI language
        LOCALE_SENGLISHCOUNTRYNAME    = 0x00001002,   // English name of country/region, eg "Germany"
        LOCALE_SNATIVECOUNTRYNAME     = 0x00000008    // native name of country/region, eg "Deutschland"
    }
}
