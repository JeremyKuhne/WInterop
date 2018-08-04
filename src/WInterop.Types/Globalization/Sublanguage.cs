// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Globalization
{
    public enum Sublanguage : byte
    {
        SUBLANG_NEUTRAL                             = 0x00,    // language neutral
        SUBLANG_DEFAULT                             = 0x01,    // user default
        SUBLANG_SYS_DEFAULT                         = 0x02,    // system default
        SUBLANG_CUSTOM_DEFAULT                      = 0x03,    // default custom language/locale
        SUBLANG_CUSTOM_UNSPECIFIED                  = 0x04,    // custom language/locale
        SUBLANG_UI_CUSTOM_DEFAULT                   = 0x05,    // Default custom MUI language/locale
        SUBLANG_AFRIKAANS_SOUTH_AFRICA              = 0x01,    // Afrikaans (South Africa) = 0x0436 af-ZA
        SUBLANG_ALBANIAN_ALBANIA                    = 0x01,    // Albanian (Albania) = 0x041c sq-AL
        SUBLANG_ALSATIAN_FRANCE                     = 0x01,    // Alsatian (France) = 0x0484
        SUBLANG_AMHARIC_ETHIOPIA                    = 0x01,    // Amharic (Ethiopia) = 0x045e
        SUBLANG_ARABIC_SAUDI_ARABIA                 = 0x01,    // Arabic (Saudi Arabia)
        SUBLANG_ARABIC_IRAQ                         = 0x02,    // Arabic (Iraq)
        SUBLANG_ARABIC_EGYPT                        = 0x03,    // Arabic (Egypt)
        SUBLANG_ARABIC_LIBYA                        = 0x04,    // Arabic (Libya)
        SUBLANG_ARABIC_ALGERIA                      = 0x05,    // Arabic (Algeria)
        SUBLANG_ARABIC_MOROCCO                      = 0x06,    // Arabic (Morocco)
        SUBLANG_ARABIC_TUNISIA                      = 0x07,    // Arabic (Tunisia)
        SUBLANG_ARABIC_OMAN                         = 0x08,    // Arabic (Oman)
        SUBLANG_ARABIC_YEMEN                        = 0x09,    // Arabic (Yemen)
        SUBLANG_ARABIC_SYRIA                        = 0x0a,    // Arabic (Syria)
        SUBLANG_ARABIC_JORDAN                       = 0x0b,    // Arabic (Jordan)
        SUBLANG_ARABIC_LEBANON                      = 0x0c,    // Arabic (Lebanon)
        SUBLANG_ARABIC_KUWAIT                       = 0x0d,    // Arabic (Kuwait)
        SUBLANG_ARABIC_UAE                          = 0x0e,    // Arabic (U.A.E)
        SUBLANG_ARABIC_BAHRAIN                      = 0x0f,    // Arabic (Bahrain)
        SUBLANG_ARABIC_QATAR                        = 0x10,    // Arabic (Qatar)
        SUBLANG_ARMENIAN_ARMENIA                    = 0x01,    // Armenian (Armenia) = 0x042b hy-AM
        SUBLANG_ASSAMESE_INDIA                      = 0x01,    // Assamese (India) = 0x044d
        SUBLANG_AZERI_LATIN                         = 0x01,    // Azeri (Latin) - for Azerbaijani, SUBLANG_AZERBAIJANI_AZERBAI
        SUBLANG_AZERI_CYRILLIC                      = 0x02,    // Azeri (Cyrillic) - for Azerbaijani, SUBLANG_AZERBAIJANI_AZER
        SUBLANG_AZERBAIJANI_AZERBAIJAN_LATIN        = 0x01,    // Azerbaijani (Azerbaijan, Latin)
        SUBLANG_AZERBAIJANI_AZERBAIJAN_CYRILLIC     = 0x02,    // Azerbaijani (Azerbaijan, Cyrillic)
        SUBLANG_BANGLA_INDIA                        = 0x01,    // Bangla (India)
        SUBLANG_BANGLA_BANGLADESH                   = 0x02,    // Bangla (Bangladesh)
        SUBLANG_BASHKIR_RUSSIA                      = 0x01,    // Bashkir (Russia) = 0x046d ba-RU
        SUBLANG_BASQUE_BASQUE                       = 0x01,    // Basque (Basque) = 0x042d eu-ES
        SUBLANG_BELARUSIAN_BELARUS                  = 0x01,    // Belarusian (Belarus) = 0x0423 be-BY
        SUBLANG_BENGALI_INDIA                       = 0x01,    // Bengali (India) - Note some prefer SUBLANG_BANGLA_INDIA
        SUBLANG_BENGALI_BANGLADESH                  = 0x02,    // Bengali (Bangladesh) - Note some prefer SUBLANG_BANGLA_BANGL
        SUBLANG_BOSNIAN_BOSNIA_HERZEGOVINA_LATIN    = 0x05,    // Bosnian (Bosnia and Herzegovina - Latin) = 0x141a bs-BA-Latn
        SUBLANG_BOSNIAN_BOSNIA_HERZEGOVINA_CYRILLIC = 0x08,    // Bosnian (Bosnia and Herzegovina - Cyrillic) = 0x201a bs-BA-Cyr
        SUBLANG_BRETON_FRANCE                       = 0x01,    // Breton (France) = 0x047e
        SUBLANG_BULGARIAN_BULGARIA                  = 0x01,    // Bulgarian (Bulgaria) = 0x0402
        SUBLANG_CATALAN_CATALAN                     = 0x01,    // Catalan (Catalan) = 0x0403
        SUBLANG_CENTRAL_KURDISH_IRAQ                = 0x01,    // Central Kurdish (Iraq) = 0x0492 ku-Arab-IQ
        SUBLANG_CHEROKEE_CHEROKEE                   = 0x01,    // Cherokee (Cherokee) = 0x045c chr-Cher-US
        SUBLANG_CHINESE_TRADITIONAL                 = 0x01,    // Chinese (Taiwan) = 0x0404 zh-TW
        SUBLANG_CHINESE_SIMPLIFIED                  = 0x02,    // Chinese (PR China) = 0x0804 zh-CN
        SUBLANG_CHINESE_HONGKONG                    = 0x03,    // Chinese (Hong Kong S.A.R., P.R.C.) = 0x0c04 zh-HK
        SUBLANG_CHINESE_SINGAPORE                   = 0x04,    // Chinese (Singapore) = 0x1004 zh-SG
        SUBLANG_CHINESE_MACAU                       = 0x05,    // Chinese (Macau S.A.R.) = 0x1404 zh-MO
        SUBLANG_CORSICAN_FRANCE                     = 0x01,    // Corsican (France) = 0x0483
        SUBLANG_CZECH_CZECH_REPUBLIC                = 0x01,    // Czech (Czech Republic) = 0x0405
        SUBLANG_CROATIAN_CROATIA                    = 0x01,    // Croatian (Croatia)
        SUBLANG_CROATIAN_BOSNIA_HERZEGOVINA_LATIN   = 0x04,    // Croatian (Bosnia and Herzegovina - Latin) = 0x101a hr-BA
        SUBLANG_DANISH_DENMARK                      = 0x01,    // Danish (Denmark) = 0x0406
        SUBLANG_DARI_AFGHANISTAN                    = 0x01,    // Dari (Afghanistan)
        SUBLANG_DIVEHI_MALDIVES                     = 0x01,    // Divehi (Maldives) = 0x0465 div-MV
        SUBLANG_DUTCH                               = 0x01,    // Dutch
        SUBLANG_DUTCH_BELGIAN                       = 0x02,    // Dutch (Belgian)
        SUBLANG_ENGLISH_US                          = 0x01,    // English (USA)
        SUBLANG_ENGLISH_UK                          = 0x02,    // English (UK)
        SUBLANG_ENGLISH_AUS                         = 0x03,    // English (Australian)
        SUBLANG_ENGLISH_CAN                         = 0x04,    // English (Canadian)
        SUBLANG_ENGLISH_NZ                          = 0x05,    // English (New Zealand)
        SUBLANG_ENGLISH_EIRE                        = 0x06,    // English (Irish)
        SUBLANG_ENGLISH_SOUTH_AFRICA                = 0x07,    // English (South Africa)
        SUBLANG_ENGLISH_JAMAICA                     = 0x08,    // English (Jamaica)
        SUBLANG_ENGLISH_CARIBBEAN                   = 0x09,    // English (Caribbean)
        SUBLANG_ENGLISH_BELIZE                      = 0x0a,    // English (Belize)
        SUBLANG_ENGLISH_TRINIDAD                    = 0x0b,    // English (Trinidad)
        SUBLANG_ENGLISH_ZIMBABWE                    = 0x0c,    // English (Zimbabwe)
        SUBLANG_ENGLISH_PHILIPPINES                 = 0x0d,    // English (Philippines)
        SUBLANG_ENGLISH_INDIA                       = 0x10,    // English (India)
        SUBLANG_ENGLISH_MALAYSIA                    = 0x11,    // English (Malaysia)
        SUBLANG_ENGLISH_SINGAPORE                   = 0x12,    // English (Singapore)
        SUBLANG_ESTONIAN_ESTONIA                    = 0x01,    // Estonian (Estonia) = 0x0425 et-EE
        SUBLANG_FAEROESE_FAROE_ISLANDS              = 0x01,    // Faroese (Faroe Islands) = 0x0438 fo-FO
        SUBLANG_FILIPINO_PHILIPPINES                = 0x01,    // Filipino (Philippines) = 0x0464 fil-PH
        SUBLANG_FINNISH_FINLAND                     = 0x01,    // Finnish (Finland) = 0x040b
        SUBLANG_FRENCH                              = 0x01,    // French
        SUBLANG_FRENCH_BELGIAN                      = 0x02,    // French (Belgian)
        SUBLANG_FRENCH_CANADIAN                     = 0x03,    // French (Canadian)
        SUBLANG_FRENCH_SWISS                        = 0x04,    // French (Swiss)
        SUBLANG_FRENCH_LUXEMBOURG                   = 0x05,    // French (Luxembourg)
        SUBLANG_FRENCH_MONACO                       = 0x06,    // French (Monaco)
        SUBLANG_FRISIAN_NETHERLANDS                 = 0x01,    // Frisian (Netherlands) = 0x0462 fy-NL
        SUBLANG_FULAH_SENEGAL                       = 0x02,    // Fulah (Senegal) = 0x0867 ff-SN
        SUBLANG_GALICIAN_GALICIAN                   = 0x01,    // Galician (Galician) = 0x0456 gl-ES
        SUBLANG_GEORGIAN_GEORGIA                    = 0x01,    // Georgian (Georgia) = 0x0437 ka-GE
        SUBLANG_GERMAN                              = 0x01,    // German
        SUBLANG_GERMAN_SWISS                        = 0x02,    // German (Swiss)
        SUBLANG_GERMAN_AUSTRIAN                     = 0x03,    // German (Austrian)
        SUBLANG_GERMAN_LUXEMBOURG                   = 0x04,    // German (Luxembourg)
        SUBLANG_GERMAN_LIECHTENSTEIN                = 0x05,    // German (Liechtenstein)
        SUBLANG_GREEK_GREECE                        = 0x01,    // Greek (Greece)
        SUBLANG_GREENLANDIC_GREENLAND               = 0x01,    // Greenlandic (Greenland) = 0x046f kl-GL
        SUBLANG_GUJARATI_INDIA                      = 0x01,    // Gujarati (India (Gujarati Script)) = 0x0447 gu-IN
        SUBLANG_HAUSA_NIGERIA_LATIN                 = 0x01,    // Hausa (Latin, Nigeria) = 0x0468 ha-NG-Latn
        SUBLANG_HAWAIIAN_US                         = 0x01,    // Hawiian (US) = 0x0475 haw-US
        SUBLANG_HEBREW_ISRAEL                       = 0x01,    // Hebrew (Israel) = 0x040d
        SUBLANG_HINDI_INDIA                         = 0x01,    // Hindi (India) = 0x0439 hi-IN
        SUBLANG_HUNGARIAN_HUNGARY                   = 0x01,    // Hungarian (Hungary) = 0x040e
        SUBLANG_ICELANDIC_ICELAND                   = 0x01,    // Icelandic (Iceland) = 0x040f
        SUBLANG_IGBO_NIGERIA                        = 0x01,    // Igbo (Nigeria) = 0x0470 ig-NG
        SUBLANG_INDONESIAN_INDONESIA                = 0x01,    // Indonesian (Indonesia) = 0x0421 id-ID
        SUBLANG_INUKTITUT_CANADA                    = 0x01,    // Inuktitut (Syllabics) (Canada) = 0x045d iu-CA-Cans
        SUBLANG_INUKTITUT_CANADA_LATIN              = 0x02,    // Inuktitut (Canada - Latin)
        SUBLANG_IRISH_IRELAND                       = 0x02,    // Irish (Ireland)
        SUBLANG_ITALIAN                             = 0x01,    // Italian
        SUBLANG_ITALIAN_SWISS                       = 0x02,    // Italian (Swiss)
        SUBLANG_JAPANESE_JAPAN                      = 0x01,    // Japanese (Japan) = 0x0411
        SUBLANG_KANNADA_INDIA                       = 0x01,    // Kannada (India (Kannada Script)) = 0x044b kn-IN
        SUBLANG_KASHMIRI_SASIA                      = 0x02,    // Kashmiri (South Asia)
        SUBLANG_KASHMIRI_INDIA                      = 0x02,    // For app compatibility only
        SUBLANG_KAZAK_KAZAKHSTAN                    = 0x01,    // Kazakh (Kazakhstan) = 0x043f kk-KZ
        SUBLANG_KHMER_CAMBODIA                      = 0x01,    // Khmer (Cambodia) = 0x0453 kh-KH
        SUBLANG_KICHE_GUATEMALA                     = 0x01,    // K'iche (Guatemala)
        SUBLANG_KINYARWANDA_RWANDA                  = 0x01,    // Kinyarwanda (Rwanda) = 0x0487 rw-RW
        SUBLANG_KONKANI_INDIA                       = 0x01,    // Konkani (India) = 0x0457 kok-IN
        SUBLANG_KOREAN                              = 0x01,    // Korean (Extended Wansung)
        SUBLANG_KYRGYZ_KYRGYZSTAN                   = 0x01,    // Kyrgyz (Kyrgyzstan) = 0x0440 ky-KG
        SUBLANG_LAO_LAO                             = 0x01,    // Lao (Lao PDR) = 0x0454 lo-LA
        SUBLANG_LATVIAN_LATVIA                      = 0x01,    // Latvian (Latvia) = 0x0426 lv-LV
        SUBLANG_LITHUANIAN                          = 0x01,    // Lithuanian
        SUBLANG_LOWER_SORBIAN_GERMANY               = 0x02,    // Lower Sorbian (Germany) = 0x082e wee-DE
        SUBLANG_LUXEMBOURGISH_LUXEMBOURG            = 0x01,    // Luxembourgish (Luxembourg) = 0x046e lb-LU
        SUBLANG_MACEDONIAN_MACEDONIA                = 0x01,    // Macedonian (Macedonia (FYROM)) = 0x042f mk-MK
        SUBLANG_MALAY_MALAYSIA                      = 0x01,    // Malay (Malaysia)
        SUBLANG_MALAY_BRUNEI_DARUSSALAM             = 0x02,    // Malay (Brunei Darussalam)
        SUBLANG_MALAYALAM_INDIA                     = 0x01,    // Malayalam (India (Malayalam Script) ) = 0x044c ml-IN
        SUBLANG_MALTESE_MALTA                       = 0x01,    // Maltese (Malta) = 0x043a mt-MT
        SUBLANG_MAORI_NEW_ZEALAND                   = 0x01,    // Maori (New Zealand) = 0x0481 mi-NZ
        SUBLANG_MAPUDUNGUN_CHILE                    = 0x01,    // Mapudungun (Chile) = 0x047a arn-CL
        SUBLANG_MARATHI_INDIA                       = 0x01,    // Marathi (India) = 0x044e mr-IN
        SUBLANG_MOHAWK_MOHAWK                       = 0x01,    // Mohawk (Mohawk) = 0x047c moh-CA
        SUBLANG_MONGOLIAN_CYRILLIC_MONGOLIA         = 0x01,    // Mongolian (Cyrillic, Mongolia)
        SUBLANG_MONGOLIAN_PRC                       = 0x02,    // Mongolian (PRC)
        SUBLANG_NEPALI_INDIA                        = 0x02,    // Nepali (India)
        SUBLANG_NEPALI_NEPAL                        = 0x01,    // Nepali (Nepal) = 0x0461 ne-NP
        SUBLANG_NORWEGIAN_BOKMAL                    = 0x01,    // Norwegian (Bokmal)
        SUBLANG_NORWEGIAN_NYNORSK                   = 0x02,    // Norwegian (Nynorsk)
        SUBLANG_OCCITAN_FRANCE                      = 0x01,    // Occitan (France) = 0x0482 oc-FR
        SUBLANG_ODIA_INDIA                          = 0x01,    // Odia (India (Odia Script)) = 0x0448 or-IN
        SUBLANG_ORIYA_INDIA                         = 0x01,    // Deprecated: use SUBLANG_ODIA_INDIA instead
        SUBLANG_PASHTO_AFGHANISTAN                  = 0x01,    // Pashto (Afghanistan)
        SUBLANG_PERSIAN_IRAN                        = 0x01,    // Persian (Iran) = 0x0429 fa-IR
        SUBLANG_POLISH_POLAND                       = 0x01,    // Polish (Poland) = 0x0415
        SUBLANG_PORTUGUESE                          = 0x02,    // Portuguese
        SUBLANG_PORTUGUESE_BRAZILIAN                = 0x01,    // Portuguese (Brazil)
        SUBLANG_PULAR_SENEGAL                       = 0x02,    // Deprecated: Use SUBLANG_FULAH_SENEGAL instead
        SUBLANG_PUNJABI_INDIA                       = 0x01,    // Punjabi (India (Gurmukhi Script)) = 0x0446 pa-IN
        SUBLANG_PUNJABI_PAKISTAN                    = 0x02,    // Punjabi (Pakistan (Arabic Script)) = 0x0846 pa-Arab-PK
        SUBLANG_QUECHUA_BOLIVIA                     = 0x01,    // Quechua (Bolivia)
        SUBLANG_QUECHUA_ECUADOR                     = 0x02,    // Quechua (Ecuador)
        SUBLANG_QUECHUA_PERU                        = 0x03,    // Quechua (Peru)
        SUBLANG_ROMANIAN_ROMANIA                    = 0x01,    // Romanian (Romania) = 0x0418
        SUBLANG_ROMANSH_SWITZERLAND                 = 0x01,    // Romansh (Switzerland) = 0x0417 rm-CH
        SUBLANG_RUSSIAN_RUSSIA                      = 0x01,    // Russian (Russia) = 0x0419
        SUBLANG_SAKHA_RUSSIA                        = 0x01,    // Sakha (Russia) = 0x0485 sah-RU
        SUBLANG_SAMI_NORTHERN_NORWAY                = 0x01,    // Northern Sami (Norway)
        SUBLANG_SAMI_NORTHERN_SWEDEN                = 0x02,    // Northern Sami (Sweden)
        SUBLANG_SAMI_NORTHERN_FINLAND               = 0x03,    // Northern Sami (Finland)
        SUBLANG_SAMI_LULE_NORWAY                    = 0x04,    // Lule Sami (Norway)
        SUBLANG_SAMI_LULE_SWEDEN                    = 0x05,    // Lule Sami (Sweden)
        SUBLANG_SAMI_SOUTHERN_NORWAY                = 0x06,    // Southern Sami (Norway)
        SUBLANG_SAMI_SOUTHERN_SWEDEN                = 0x07,    // Southern Sami (Sweden)
        SUBLANG_SAMI_SKOLT_FINLAND                  = 0x08,    // Skolt Sami (Finland)
        SUBLANG_SAMI_INARI_FINLAND                  = 0x09,    // Inari Sami (Finland)
        SUBLANG_SANSKRIT_INDIA                      = 0x01,    // Sanskrit (India) = 0x044f sa-IN
        SUBLANG_SCOTTISH_GAELIC                     = 0x01,    // Scottish Gaelic (United Kingdom) = 0x0491 gd-GB
        SUBLANG_SERBIAN_BOSNIA_HERZEGOVINA_LATIN    = 0x06,    // Serbian (Bosnia and Herzegovina - Latin)
        SUBLANG_SERBIAN_BOSNIA_HERZEGOVINA_CYRILLIC = 0x07,    // Serbian (Bosnia and Herzegovina - Cyrillic)
        SUBLANG_SERBIAN_MONTENEGRO_LATIN            = 0x0b,    // Serbian (Montenegro - Latn)
        SUBLANG_SERBIAN_MONTENEGRO_CYRILLIC         = 0x0c,    // Serbian (Montenegro - Cyrillic)
        SUBLANG_SERBIAN_SERBIA_LATIN                = 0x09,    // Serbian (Serbia - Latin)
        SUBLANG_SERBIAN_SERBIA_CYRILLIC             = 0x0a,    // Serbian (Serbia - Cyrillic)
        SUBLANG_SERBIAN_CROATIA                     = 0x01,    // Croatian (Croatia) = 0x041a hr-HR
        SUBLANG_SERBIAN_LATIN                       = 0x02,    // Serbian (Latin)
        SUBLANG_SERBIAN_CYRILLIC                    = 0x03,    // Serbian (Cyrillic)
        SUBLANG_SINDHI_INDIA                        = 0x01,    // Sindhi (India) reserved = 0x0459
        SUBLANG_SINDHI_PAKISTAN                     = 0x02,    // Sindhi (Pakistan) = 0x0859 sd-Arab-PK
        SUBLANG_SINDHI_AFGHANISTAN                  = 0x02,    // For app compatibility only
        SUBLANG_SINHALESE_SRI_LANKA                 = 0x01,    // Sinhalese (Sri Lanka)
        SUBLANG_SOTHO_NORTHERN_SOUTH_AFRICA         = 0x01,    // Northern Sotho (South Africa)
        SUBLANG_SLOVAK_SLOVAKIA                     = 0x01,    // Slovak (Slovakia) = 0x041b sk-SK
        SUBLANG_SLOVENIAN_SLOVENIA                  = 0x01,    // Slovenian (Slovenia) = 0x0424 sl-SI
        SUBLANG_SPANISH                             = 0x01,    // Spanish (Castilian)
        SUBLANG_SPANISH_MEXICAN                     = 0x02,    // Spanish (Mexico)
        SUBLANG_SPANISH_MODERN                      = 0x03,    // Spanish (Modern)
        SUBLANG_SPANISH_GUATEMALA                   = 0x04,    // Spanish (Guatemala)
        SUBLANG_SPANISH_COSTA_RICA                  = 0x05,    // Spanish (Costa Rica)
        SUBLANG_SPANISH_PANAMA                      = 0x06,    // Spanish (Panama)
        SUBLANG_SPANISH_DOMINICAN_REPUBLIC          = 0x07,    // Spanish (Dominican Republic)
        SUBLANG_SPANISH_VENEZUELA                   = 0x08,    // Spanish (Venezuela)
        SUBLANG_SPANISH_COLOMBIA                    = 0x09,    // Spanish (Colombia)
        SUBLANG_SPANISH_PERU                        = 0x0a,    // Spanish (Peru)
        SUBLANG_SPANISH_ARGENTINA                   = 0x0b,    // Spanish (Argentina)
        SUBLANG_SPANISH_ECUADOR                     = 0x0c,    // Spanish (Ecuador)
        SUBLANG_SPANISH_CHILE                       = 0x0d,    // Spanish (Chile)
        SUBLANG_SPANISH_URUGUAY                     = 0x0e,    // Spanish (Uruguay)
        SUBLANG_SPANISH_PARAGUAY                    = 0x0f,    // Spanish (Paraguay)
        SUBLANG_SPANISH_BOLIVIA                     = 0x10,    // Spanish (Bolivia)
        SUBLANG_SPANISH_EL_SALVADOR                 = 0x11,    // Spanish (El Salvador)
        SUBLANG_SPANISH_HONDURAS                    = 0x12,    // Spanish (Honduras)
        SUBLANG_SPANISH_NICARAGUA                   = 0x13,    // Spanish (Nicaragua)
        SUBLANG_SPANISH_PUERTO_RICO                 = 0x14,    // Spanish (Puerto Rico)
        SUBLANG_SPANISH_US                          = 0x15,    // Spanish (United States)
        SUBLANG_SWAHILI_KENYA                       = 0x01,    // Swahili (Kenya) = 0x0441 sw-KE
        SUBLANG_SWEDISH                             = 0x01,    // Swedish
        SUBLANG_SWEDISH_FINLAND                     = 0x02,    // Swedish (Finland)
        SUBLANG_SYRIAC_SYRIA                        = 0x01,    // Syriac (Syria) = 0x045a syr-SY
        SUBLANG_TAJIK_TAJIKISTAN                    = 0x01,    // Tajik (Tajikistan) = 0x0428 tg-TJ-Cyrl
        SUBLANG_TAMAZIGHT_ALGERIA_LATIN             = 0x02,    // Tamazight (Latin, Algeria) = 0x085f tzm-Latn-DZ
        SUBLANG_TAMAZIGHT_MOROCCO_TIFINAGH          = 0x04,    // Tamazight (Tifinagh) = 0x105f tzm-Tfng-MA
        SUBLANG_TAMIL_INDIA                         = 0x01,    // Tamil (India)
        SUBLANG_TAMIL_SRI_LANKA                     = 0x02,    // Tamil (Sri Lanka) = 0x0849 ta-LK
        SUBLANG_TATAR_RUSSIA                        = 0x01,    // Tatar (Russia) = 0x0444 tt-RU
        SUBLANG_TELUGU_INDIA                        = 0x01,    // Telugu (India (Telugu Script)) = 0x044a te-IN
        SUBLANG_THAI_THAILAND                       = 0x01,    // Thai (Thailand) = 0x041e th-TH
        SUBLANG_TIBETAN_PRC                         = 0x01,    // Tibetan (PRC)
        SUBLANG_TIGRIGNA_ERITREA                    = 0x02,    // Tigrigna (Eritrea)
        SUBLANG_TIGRINYA_ERITREA                    = 0x02,    // Tigrinya (Eritrea) = 0x0873 ti-ER (preferred spelling)
        SUBLANG_TIGRINYA_ETHIOPIA                   = 0x01,    // Tigrinya (Ethiopia) = 0x0473 ti-ET
        SUBLANG_TSWANA_BOTSWANA                     = 0x02,    // Setswana / Tswana (Botswana) = 0x0832 tn-BW
        SUBLANG_TSWANA_SOUTH_AFRICA                 = 0x01,    // Setswana / Tswana (South Africa) = 0x0432 tn-ZA
        SUBLANG_TURKISH_TURKEY                      = 0x01,    // Turkish (Turkey) = 0x041f tr-TR
        SUBLANG_TURKMEN_TURKMENISTAN                = 0x01,    // Turkmen (Turkmenistan) = 0x0442 tk-TM
        SUBLANG_UIGHUR_PRC                          = 0x01,    // Uighur (PRC) = 0x0480 ug-CN
        SUBLANG_UKRAINIAN_UKRAINE                   = 0x01,    // Ukrainian (Ukraine) = 0x0422 uk-UA
        SUBLANG_UPPER_SORBIAN_GERMANY               = 0x01,    // Upper Sorbian (Germany) = 0x042e wen-DE
        SUBLANG_URDU_PAKISTAN                       = 0x01,    // Urdu (Pakistan)
        SUBLANG_URDU_INDIA                          = 0x02,    // Urdu (India)
        SUBLANG_UZBEK_LATIN                         = 0x01,    // Uzbek (Latin)
        SUBLANG_UZBEK_CYRILLIC                      = 0x02,    // Uzbek (Cyrillic)
        SUBLANG_VALENCIAN_VALENCIA                  = 0x02,    // Valencian (Valencia) = 0x0803 ca-ES-Valencia
        SUBLANG_VIETNAMESE_VIETNAM                  = 0x01,    // Vietnamese (Vietnam) = 0x042a vi-VN
        SUBLANG_WELSH_UNITED_KINGDOM                = 0x01,    // Welsh (United Kingdom) = 0x0452 cy-GB
        SUBLANG_WOLOF_SENEGAL                       = 0x01,    // Wolof (Senegal)
        SUBLANG_XHOSA_SOUTH_AFRICA                  = 0x01,    // isiXhosa / Xhosa (South Africa) = 0x0434 xh-ZA
        SUBLANG_YAKUT_RUSSIA                        = 0x01,    // Deprecated: use SUBLANG_SAKHA_RUSSIA instead
        SUBLANG_YI_PRC                              = 0x01,    // Yi (PRC)) = 0x0478
        SUBLANG_YORUBA_NIGERIA                      = 0x01,    // Yoruba (Nigeria) 046a yo-NG
        SUBLANG_ZULU_SOUTH_AFRICA                   = 0x01,    // isiZulu / Zulu (South Africa) = 0x0435 zu-ZA
    }
}
