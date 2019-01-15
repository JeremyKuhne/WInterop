// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Globalization;
using WInterop.Windows;
using WInterop.Windows.Unsafe;

namespace WInterop.Globalization.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd318103.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern int GetLocaleInfoEx(
            string lpLocaleName,
            uint LCType,
            void* lpLCData,
            int cchData);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd317762.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern int CompareStringOrdinal(
            ref char lpString1,
            int cchCount1,
            ref char lpString2,
            int cchCount2,
            bool bIgnoreCase);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd318702.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern int LCMapStringEx(
            char* lpLocaleName,
            LocaleMapFlags dwMapFlags,
            char* lpSrcStr,
            int cchSrc,
            char* lpDestStr,
            int cchDest,
            NlsVersionInfoExtended* lpVersionInformation,
            void* lpReserved,
            LParam sortHandle);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winnls/nf-winnls-getacp
        [DllImport(Libraries.Winnls, ExactSpelling = true)]
        public static extern uint GetACP();
    }
}
