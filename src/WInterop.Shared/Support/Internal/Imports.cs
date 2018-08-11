// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

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

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380073.aspx
        [DllImport(Libraries.Ole32)]
        public static extern HRESULT PropVariantClear(IntPtr pvar);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376026.aspx
        [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
        public static extern bool CertCloseStore(
            IntPtr hCertStore,
            uint dwFlags);



        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool FreeLibrary(
            IntPtr hModule);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366597.aspx
        [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
        public static extern IntPtr HeapAlloc(
            IntPtr hHeap,
            uint dwFlags,
            UIntPtr dwBytes);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366704.aspx
        [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
        public static extern IntPtr HeapReAlloc(
            IntPtr hHeap,
            uint dwFlags,
            IntPtr lpMem,
            UIntPtr dwBytes);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366701.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool HeapFree(
            IntPtr hHeap,
            uint dwFlags,
            IntPtr lpMem);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366730.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr LocalFree(
            IntPtr hMem);

        // This is safe to cache as it will never change for a process once started
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366569.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetProcessHeap();

        public static IntPtr ProcessHeap = GetProcessHeap();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679351.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern uint FormatMessageW(
            FormatMessageFlags dwFlags,
            IntPtr lpSource,
            uint dwMessageId,
            // LANGID or 0 for auto lookup
            uint dwLanguageId,
            IntPtr lpBuffer,
            // Size is in chars
            uint nSize,
            string[] Arguments);
    }
}
