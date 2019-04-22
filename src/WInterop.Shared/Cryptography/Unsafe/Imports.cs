// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Handles;

namespace WInterop.Cryptography.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376559.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern CertificateStoreHandle CertOpenStore(
            IntPtr lpszStoreProvider,
            uint dwMsgAndCertEncodingType,
            IntPtr hCryptProv,
            uint dwFlags,
            IntPtr pvPara);


        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376026.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CertCloseStore(
            IntPtr hCertStore,
            uint dwFlags);

        // Example C Program: Listing System and Physical Stores
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa382362.aspx

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376060.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CertEnumSystemStoreLocation(
            uint dwFlags,
            IntPtr pvArg,
            CertEnumSystemStoreLocationCallback pfnEnum);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376058.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CertEnumSystemStore(
            uint dwFlags,
            IntPtr pvSystemStoreLocationPara,
            IntPtr pvArg,
            CertEnumSystemStoreCallback pfnEnum);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376055.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CertEnumPhysicalStore(
            IntPtr pvSystemStore,
            uint dwFlags,
            IntPtr pvArg,
            CertEnumPhysicalStoreCallback pfnEnum);
    }
}
