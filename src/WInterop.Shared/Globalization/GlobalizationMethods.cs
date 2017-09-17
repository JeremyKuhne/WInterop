// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Globalization
{
    public static partial class GlobalizationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern int GetLocaleInfoEx(
                string lpLocaleName,
                uint LCType,
                void* lpLCData,
                int cchData);
        }

        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;
    }
}
