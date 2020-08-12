// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Memory.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // Heap Functions
        // --------------
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366711.aspx

        // HeapAlloc/Realloc take a SIZE_T for their count of bytes. This is ultimately an
        // unsigned __int3264 which is platform specific (uint on 32bit and ulong on 64bit).
        // UIntPtr can encapsulate this as it wraps void* and has unsigned constructors.
        // (IntPtr also wraps void*, but uses signed constructors.)
        //
        // SIZE_T:
        // https://msdn.microsoft.com/en-us/library/cc441980.aspx

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

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366706.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366700.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool HeapDestroy(IntPtr hHeap);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366598.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern UIntPtr HeapCompact(IntPtr hHeap, uint dwFlags);

        // This is safe to cache as it will never change for a process once started
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366569.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetProcessHeap();

        // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-globalalloc
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern HGLOBAL GlobalAlloc(
            GlobalMemoryFlags uFlags,
            UIntPtr dwBytes);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-globalfree
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern HGLOBAL GlobalFree(
            HGLOBAL hMem);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-globallock
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GlobalLock(
            HGLOBAL hMem);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-globalunlock
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern Boolean32 GlobalUnlock(
            HGLOBAL hMem);
    }
}
