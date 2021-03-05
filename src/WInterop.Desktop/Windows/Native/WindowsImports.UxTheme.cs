// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Support;

namespace WInterop.Windows.Native
{
    public static partial class WindowsImports
    {
        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-closethemedata
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern HResult CloseThemeData(
            HTHEME hTheme);

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-getcurrentthemename
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static unsafe extern HResult GetCurrentThemeName(
            char* pszThemeFileName,
            int cchMaxNameChars,
            char* pszColorBuff,
            int cchMaxColorChars,
            char* pszSizeBuff,
            int cchMaxSizeChars);

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-isappthemed
        [SuppressGCTransition]
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern IntBoolean IsAppThemed();

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-iscompositionactive
        [SuppressGCTransition]
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern IntBoolean IsCompositionActive();

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-isthemeactive
        [SuppressGCTransition]
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern IntBoolean IsThemeActive();

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-isthemepartdefined
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern IntBoolean IsThemePartDefined(
            HTHEME hTheme,
            int iPartId,
            int iStateId);

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-openthemedataex
        [DllImport(Libraries.UxTheme, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static unsafe extern HTHEME OpenThemeDataEx(
            HWND hwnd,
            char* pszClassList,
            uint dwFlags);
    }
}
