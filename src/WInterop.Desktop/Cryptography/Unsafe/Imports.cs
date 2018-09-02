﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Handles;

namespace WInterop.Cryptography.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // System Store Locations
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa388136.aspx

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376560.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern CertificateStoreHandle CertOpenSystemStoreW(
            IntPtr hprov,
            string szSubsystemProtocol);

    }
}
