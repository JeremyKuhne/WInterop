// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Security;
using WInterop.Errors;
using WInterop.Registry;
using WInterop.Shell;
using WInterop.Shell.Native;

namespace WInterop.Shell
{
    public static partial class ShellMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762188.aspx
            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult SHGetKnownFolderPath(
                ref Guid rfid,
                KnownFolderFlags dwFlags,
                SafeHandle hToken,
                out string ppszPath);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762187.aspx
            [DllImport(Libraries.Shell32, ExactSpelling = true)]
            public static extern HResult SHGetKnownFolderIDList(
                ref Guid rfid,
                KnownFolderFlags dwFlags,
                SafeHandle hToken,
                out ItemIdList ppidl);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762191.aspx
            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult SHGetNameFromIDList(
                ItemIdList pidl,
                ShellItemDisplayNames sigdnName,
                out string ppszName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773760.aspx
            [DllImport(Libraries.Shlwapi, CharSet = CharSet.Unicode, SetLastError = false, ExactSpelling = true)]
            public static extern bool PathUnExpandEnvStringsW(
                string pszPath,
                SafeHandle pszBuf,
                uint cchBuf);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762275.aspx
            [DllImport(Libraries.Userenv, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool ExpandEnvironmentStringsForUserW(
                AccessToken hToken,
                string lpSrc,
                SafeHandle lpDst,
                uint dwSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773468.aspx
            [DllImport(Libraries.Shlwapi, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult AssocQueryKeyW(
                AssociationFlags flags,
                AssociationKey key,
                string pszAssoc,
                string pszExtra,
                out RegistryKeyHandle phkeyOut);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773471.aspx
            [DllImport(Libraries.Shlwapi, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult AssocQueryStringW(
                AssociationFlags flags,
                AssociationString str,
                string pszAssoc,
                string pszExtra,
                SafeHandle pszOut,
                ref uint pcchOut);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773473.aspx
            [DllImport(Libraries.Shlwapi, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult AssocQueryStringByKeyW(
                AssociationFlags flags,
                AssociationString str,
                RegistryKeyHandle hkAssoc,
                string pszExtra,
                SafeHandle pszOut,
                ref uint pcchOut);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762079.aspx
            [DllImport(Libraries.PropSys, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HResult PSGetPropertyDescriptionListFromString(
                string pszPropList,
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                [MarshalAs(UnmanagedType.Interface)] out IPropertyDescriptionList ppv);

            // https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shbrowseforfolderw
            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern ItemIdList SHBrowseForFolderW(
                ref BROWSEINFO lpbi);

            // https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shgetpathfromidlistex
            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern bool SHGetPathFromIDListEx(
                ItemIdList pidl,
                char* pszPath,
                uint cchPath,
                uint uOpts);
        }
    }
}
