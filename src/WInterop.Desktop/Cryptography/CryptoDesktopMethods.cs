// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Handles.DataTypes;

namespace WInterop.Cryptography
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class CryptoDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
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
}
