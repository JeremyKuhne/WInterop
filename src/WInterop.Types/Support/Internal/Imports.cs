// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.DataTypes;

namespace WInterop.Support.Internal
{
    /// <summary>
    /// The subset of APIs necessary to support data types, notably handle closing methods.
    /// All must be available in Windows Store Applications (WinRT/UWP). Not intended for
    /// use outside of WInterop, prefer using the wraps in the Methods classes if necessary.
    /// </summary>
    public static class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(
            IntPtr handle);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms221165.aspx
        [DllImport(Libraries.OleAut32)]
        public static extern HRESULT VariantClear(
            IntPtr pvarg);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376026.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertCloseStore(
            IntPtr hCertStore,
            uint dwFlags);
    }
}
