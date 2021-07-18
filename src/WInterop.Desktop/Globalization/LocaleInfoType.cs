// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd464799.aspx
    public enum LocaleInfoType : uint
    {
        LOCALE_ILANGUAGE              = 0x00000001,   // language id, LOCALE_SNAME preferred
        LOCALE_SABBREVLANGNAME        = 0x00000003,   // arbitrary abbreviated language name, LOCALE_SISO639LANGNAME preferred
        LOCALE_IDIALINGCODE           = 0x00000005,   // country/region dialing code, example: en-US and en-CA return 1.
        LOCALE_ICOUNTRY               = 0x00000005,   // Deprecated synonym for LOCALE_IDIALINGCODE, use LOCALE_SISO3166CTRYNAME to query for a region identifier.
        LOCALE_SABBREVCTRYNAME        = 0x00000007,   // arbitrary abbreviated country/region name, LOCALE_SISO3166CTRYNAME preferred
        LOCALE_IGEOID                 = 0x0000005B,   // geographical location id, eg "244"
        LOCALE_IDEFAULTLANGUAGE       = 0x00000009,   // default language id, deprecated
        LOCALE_IDEFAULTCOUNTRY        = 0x0000000A,   // default country/region code, deprecated
        LOCALE_IDEFAULTCODEPAGE       = 0x0000000B,   // default oem code page (use of Unicode is recommended instead)
        LOCALE_IDEFAULTANSICODEPAGE   = 0x00001004,   // default ansi code page (use of Unicode is recommended instead)
        LOCALE_IDEFAULTMACCODEPAGE    = 0x00001011,   // default mac code page (use of Unicode is recommended instead)
        LOCALE_SLIST                  = 0x0000000C,   // list item separator, eg "," for "1,2,3,4"
        LOCALE_IMEASURE               = 0x0000000D,   // 0 = metric, 1 = US measurement system
        LOCALE_SDECIMAL               = 0x0000000E,   // decimal separator, eg "." for 1,234.00
        LOCALE_STHOUSAND              = 0x0000000F,   // thousand separator, eg "," for 1,234.00
        LOCALE_SGROUPING              = 0x00000010,   // digit grouping, eg "3;0" for 1,000,000
        LOCALE_IDIGITS                = 0x00000011,   // number of fractional digits eg 2 for 1.00
        LOCALE_ILZERO                 = 0x00000012,   // leading zeros for decimal, 0 for .97, 1 for 0.97
        LOCALE_INEGNUMBER             = 0x00001010,   // negative number mode, 0-4, see documentation
        LOCALE_SNATIVEDIGITS          = 0x00000013,   // native digits for 0-9, eg "0123456789"
        LOCALE_SCURRENCY              = 0x00000014,   // local monetary symbol, eg "$"
        LOCALE_SINTLSYMBOL            = 0x00000015,   // intl monetary symbol, eg "USD"
        LOCALE_SMONDECIMALSEP         = 0x00000016,   // monetary decimal separator, eg "." for $1,234.00
        LOCALE_SMONTHOUSANDSEP        = 0x00000017,   // monetary thousand separator, eg "," for $1,234.00
        LOCALE_SMONGROUPING           = 0x00000018,   // monetary grouping, eg "3;0" for $1,000,000.00
        LOCALE_ICURRDIGITS            = 0x00000019,   // # local monetary digits, eg 2 for $1.00
        LOCALE_IINTLCURRDIGITS        = 0x0000001A,   // # intl monetary digits, eg 2 for $1.00
        LOCALE_ICURRENCY              = 0x0000001B,   // positive currency mode, 0-3, see documenation
        LOCALE_INEGCURR               = 0x0000001C,   // negative currency mode, 0-15, see documentation
        LOCALE_SDATE                  = 0x0000001D,   // date separator (derived from LOCALE_SSHORTDATE, use that instead)
        LOCALE_STIME                  = 0x0000001E,   // time separator (derived from LOCALE_STIMEFORMAT, use that instead)
        LOCALE_SSHORTDATE             = 0x0000001F,   // short date format string, eg "MM/dd/yyyy"
        LOCALE_SLONGDATE              = 0x00000020,   // long date format string, eg "dddd, MMMM dd, yyyy"
        LOCALE_STIMEFORMAT            = 0x00001003,   // time format string, eg "HH:mm:ss"
        LOCALE_IDATE                  = 0x00000021,   // short date format ordering (derived from LOCALE_SSHORTDATE, use that instead)
        LOCALE_ILDATE                 = 0x00000022,   // long date format ordering (derived from LOCALE_SLONGDATE, use that instead)
        LOCALE_ITIME                  = 0x00000023,   // time format specifier (derived from LOCALE_STIMEFORMAT, use that instead)
        LOCALE_ITIMEMARKPOSN          = 0x00001005,   // time marker position (derived from LOCALE_STIMEFORMAT, use that instead)
        LOCALE_ICENTURY               = 0x00000024,   // century format specifier (short date, LOCALE_SSHORTDATE is preferred)
        LOCALE_ITLZERO                = 0x00000025,   // leading zeros in time field (derived from LOCALE_STIMEFORMAT, use that instead)
        LOCALE_IDAYLZERO              = 0x00000026,   // leading zeros in day field (short date, LOCALE_SSHORTDATE is preferred)
        LOCALE_IMONLZERO              = 0x00000027,   // leading zeros in month field (short date, LOCALE_SSHORTDATE is preferred)
        LOCALE_SAM                    = 0x00000028,   // AM designator, eg "AM"
        LOCALE_S1159                  = LOCALE_SAM,
        LOCALE_SPM                    = 0x00000029,   // PM designator, eg "PM"
        LOCALE_S2359                  = LOCALE_SPM,
        LOCALE_ICALENDARTYPE          = 0x00001009,   // type of calendar specifier, eg CAL_GREGORIAN
        LOCALE_IOPTIONALCALENDAR      = 0x0000100B,   // additional calendar types specifier, eg CAL_GREGORIAN_US
        LOCALE_IFIRSTDAYOFWEEK        = 0x0000100C,   // first day of week specifier, 0-6, 0=Monday, 6=Sunday
        LOCALE_IFIRSTWEEKOFYEAR       = 0x0000100D,   // first week of year specifier, 0-2, see documentation
        LOCALE_SDAYNAME1              = 0x0000002A,   // long name for Monday
        LOCALE_SDAYNAME2              = 0x0000002B,   // long name for Tuesday
        LOCALE_SDAYNAME3              = 0x0000002C,   // long name for Wednesday
        LOCALE_SDAYNAME4              = 0x0000002D,   // long name for Thursday
        LOCALE_SDAYNAME5              = 0x0000002E,   // long name for Friday
        LOCALE_SDAYNAME6              = 0x0000002F,   // long name for Saturday
        LOCALE_SDAYNAME7              = 0x00000030,   // long name for Sunday
        LOCALE_SABBREVDAYNAME1        = 0x00000031,   // abbreviated name for Monday
        LOCALE_SABBREVDAYNAME2        = 0x00000032,   // abbreviated name for Tuesday
        LOCALE_SABBREVDAYNAME3        = 0x00000033,   // abbreviated name for Wednesday
        LOCALE_SABBREVDAYNAME4        = 0x00000034,   // abbreviated name for Thursday
        LOCALE_SABBREVDAYNAME5        = 0x00000035,   // abbreviated name for Friday
        LOCALE_SABBREVDAYNAME6        = 0x00000036,   // abbreviated name for Saturday
        LOCALE_SABBREVDAYNAME7        = 0x00000037,   // abbreviated name for Sunday
        LOCALE_SMONTHNAME1            = 0x00000038,   // long name for January
        LOCALE_SMONTHNAME2            = 0x00000039,   // long name for February
        LOCALE_SMONTHNAME3            = 0x0000003A,   // long name for March
        LOCALE_SMONTHNAME4            = 0x0000003B,   // long name for April
        LOCALE_SMONTHNAME5            = 0x0000003C,   // long name for May
        LOCALE_SMONTHNAME6            = 0x0000003D,   // long name for June
        LOCALE_SMONTHNAME7            = 0x0000003E,   // long name for July
        LOCALE_SMONTHNAME8            = 0x0000003F,   // long name for August
        LOCALE_SMONTHNAME9            = 0x00000040,   // long name for September
        LOCALE_SMONTHNAME10           = 0x00000041,   // long name for October
        LOCALE_SMONTHNAME11           = 0x00000042,   // long name for November
        LOCALE_SMONTHNAME12           = 0x00000043,   // long name for December
        LOCALE_SMONTHNAME13           = 0x0000100E,   // long name for 13th month (if exists)
        LOCALE_SABBREVMONTHNAME1      = 0x00000044,   // abbreviated name for January
        LOCALE_SABBREVMONTHNAME2      = 0x00000045,   // abbreviated name for February
        LOCALE_SABBREVMONTHNAME3      = 0x00000046,   // abbreviated name for March
        LOCALE_SABBREVMONTHNAME4      = 0x00000047,   // abbreviated name for April
        LOCALE_SABBREVMONTHNAME5      = 0x00000048,   // abbreviated name for May
        LOCALE_SABBREVMONTHNAME6      = 0x00000049,   // abbreviated name for June
        LOCALE_SABBREVMONTHNAME7      = 0x0000004A,   // abbreviated name for July
        LOCALE_SABBREVMONTHNAME8      = 0x0000004B,   // abbreviated name for August
        LOCALE_SABBREVMONTHNAME9      = 0x0000004C,   // abbreviated name for September
        LOCALE_SABBREVMONTHNAME10     = 0x0000004D,   // abbreviated name for October
        LOCALE_SABBREVMONTHNAME11     = 0x0000004E,   // abbreviated name for November
        LOCALE_SABBREVMONTHNAME12     = 0x0000004F,   // abbreviated name for December
        LOCALE_SABBREVMONTHNAME13     = 0x0000100F,   // abbreviated name for 13th month (if exists)
        LOCALE_SPOSITIVESIGN          = 0x00000050,   // positive sign, eg ""
        LOCALE_SNEGATIVESIGN          = 0x00000051,   // negative sign, eg "-"
        LOCALE_IPOSSIGNPOSN           = 0x00000052,   // positive sign position (derived from INEGCURR)
        LOCALE_INEGSIGNPOSN           = 0x00000053,   // negative sign position (derived from INEGCURR)
        LOCALE_IPOSSYMPRECEDES        = 0x00000054,   // mon sym precedes pos amt (derived from ICURRENCY)
        LOCALE_IPOSSEPBYSPACE         = 0x00000055,   // mon sym sep by space from pos amt (derived from ICURRENCY)
        LOCALE_INEGSYMPRECEDES        = 0x00000056,   // mon sym precedes neg amt (derived from INEGCURR)
        LOCALE_INEGSEPBYSPACE         = 0x00000057,   // mon sym sep by space from neg amt (derived from INEGCURR)
        LOCALE_FONTSIGNATURE          = 0x00000058,   // font signature
        LOCALE_SISO639LANGNAME        = 0x00000059,   // ISO abbreviated language name, eg "en"
        LOCALE_SISO3166CTRYNAME       = 0x0000005A,   // ISO abbreviated country/region name, eg "US"
        LOCALE_IDEFAULTEBCDICCODEPAGE = 0x00001012,   // default ebcdic code page (use of Unicode is recommended instead)
        LOCALE_IPAPERSIZE             = 0x0000100A,   // 1 = letter, 5 = legal, 8 = a3, 9 = a4
        LOCALE_SENGCURRNAME           = 0x00001007,   // english name of currency, eg "Euro"
        LOCALE_SNATIVECURRNAME        = 0x00001008,   // native name of currency, eg "euro"
        LOCALE_SYEARMONTH             = 0x00001006,   // year month format string, eg "MM/yyyy"
        LOCALE_SSORTNAME              = 0x00001013,   // sort name, usually "", eg "Dictionary" in UI Language
        LOCALE_IDIGITSUBSTITUTION     = 0x00001014,   // 0 = context, 1 = none, 2 = national
        LOCALE_SNAME                  = 0x0000005c,   // locale name (ie: en-us)
        LOCALE_SDURATION              = 0x0000005d,   // time duration format, eg "hh:mm:ss"
        LOCALE_SKEYBOARDSTOINSTALL    = 0x0000005e,   // Used internally, see GetKeyboardLayoutName() function
        LOCALE_SSHORTESTDAYNAME1      = 0x00000060,   // Shortest day name for Monday
        LOCALE_SSHORTESTDAYNAME2      = 0x00000061,   // Shortest day name for Tuesday
        LOCALE_SSHORTESTDAYNAME3      = 0x00000062,   // Shortest day name for Wednesday
        LOCALE_SSHORTESTDAYNAME4      = 0x00000063,   // Shortest day name for Thursday
        LOCALE_SSHORTESTDAYNAME5      = 0x00000064,   // Shortest day name for Friday
        LOCALE_SSHORTESTDAYNAME6      = 0x00000065,   // Shortest day name for Saturday
        LOCALE_SSHORTESTDAYNAME7      = 0x00000066,   // Shortest day name for Sunday
        LOCALE_SISO639LANGNAME2       = 0x00000067,   // 3 character ISO abbreviated language name, eg "eng"
        LOCALE_SISO3166CTRYNAME2      = 0x00000068,   // 3 character ISO country/region name, eg "USA"
        LOCALE_SNAN                   = 0x00000069,   // Not a Number, eg "NaN"
        LOCALE_SPOSINFINITY           = 0x0000006a,   // + Infinity, eg "infinity"
        LOCALE_SNEGINFINITY           = 0x0000006b,   // - Infinity, eg "-infinity"
        LOCALE_SSCRIPTS               = 0x0000006c,   // Typical scripts in the locale: ; delimited script codes, eg "Latn;"
        LOCALE_SPARENT                = 0x0000006d,   // Fallback name for resources, eg "en" for "en-US"
        LOCALE_SCONSOLEFALLBACKNAME   = 0x0000006e,   // Fallback name for within the console for Unicode Only locales, eg "en" for bn-IN
        LOCALE_IREADINGLAYOUT         = 0x00000070,   // Returns one of the following 4 reading layout values:
                                                      // 0 - Left to right (eg en-US)
                                                      // 1 - Right to left (eg arabic locales)
                                                      // 2 - Vertical top to bottom with columns to the left and also left to right (ja-JP locales)
                                                      // 3 - Vertical top to bottom with columns proceeding to the right
        LOCALE_INEUTRAL               = 0x00000071,   // Returns 0 for specific cultures, 1 for neutral cultures.
        LOCALE_INEGATIVEPERCENT       = 0x00000074,   // Returns 0-11 for the negative percent format
        LOCALE_IPOSITIVEPERCENT       = 0x00000075,   // Returns 0-3 for the positive percent formatIPOSITIVEPERCENT
        LOCALE_SPERCENT               = 0x00000076,   // Returns the percent symbol
        LOCALE_SPERMILLE              = 0x00000077,   // Returns the permille (U+2030) symbol
        LOCALE_SMONTHDAY              = 0x00000078,   // Returns the preferred month/day format
        LOCALE_SSHORTTIME             = 0x00000079,   // Returns the preferred short time format (ie: no seconds, just h:mm)
        LOCALE_SOPENTYPELANGUAGETAG   = 0x0000007a,   // Open type language tag, eg: "latn" or "dflt"
        LOCALE_SSORTLOCALE            = 0x0000007b,   // Name of locale to use for sorting/collation/casing behavior.
        LOCALE_SRELATIVELONGDATE      = 0x0000007c,   // Long date without year, day of week, month, date, eg: for lock screen
        LOCALE_SSHORTESTAM            = 0x0000007e,   // Shortest AM designator, eg "A"
        LOCALE_SSHORTESTPM            = 0x0000007f    // Shortest PM designator, eg "P"
    }
}