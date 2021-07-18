// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ProcessAndThreads;

namespace WInterop.Memory.Native
{
    /// <summary>
    ///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class MemoryImports
    {
        // Heap Functions
        // --------------
        // https://docs.microsoft.com/windows/win32/memory/heap-functions

        // HeapAlloc/Realloc take a SIZE_T for their count of bytes. This is ultimately an
        // unsigned __int3264 which is platform specific (uint on 32bit and ulong on 64bit).
        // UIntPtr can encapsulate this as it wraps void* and has unsigned constructors.
        // (IntPtr also wraps void*, but uses signed constructors.)
        //
        // SIZE_T:
        // https://docs.microsoft.com/openspecs/windows_protocols/ms-dtyp/1dc2ff19-6fef-4c5f-b4fd-afbc2557fd81

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heapalloc
        [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
        public static extern IntPtr HeapAlloc(
            IntPtr hHeap,
            uint dwFlags,
            UIntPtr dwBytes);

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heaprealloc
        [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
        public static extern IntPtr HeapReAlloc(
            IntPtr hHeap,
            uint dwFlags,
            IntPtr lpMem,
            UIntPtr dwBytes);

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heapfree
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool HeapFree(
            IntPtr hHeap,
            uint dwFlags,
            IntPtr lpMem);

        // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-localfree
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr LocalFree(
            IntPtr hMem);

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heapsize
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heapdestroy
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool HeapDestroy(IntPtr hHeap);

        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-heapcompact
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern UIntPtr HeapCompact(IntPtr hHeap, uint dwFlags);

        // This is safe to cache as it will never change for a process once started
        // https://docs.microsoft.com/windows/win32/api/heapapi/nf-heapapi-getprocessheap
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetProcessHeap();

        // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-globalalloc
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern HGLOBAL GlobalAlloc(
            GlobalMemoryFlags uFlags,
            UIntPtr dwBytes);

        // https://docs.microsoft.com/windows/desktop/api/winbase/nf-winbase-globalfree
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern HGLOBAL GlobalFree(
            HGLOBAL hMem);

        // https://docs.microsoft.com/windows/desktop/api/winbase/nf-winbase-globallock
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GlobalLock(
            HGLOBAL hMem);

        // https://docs.microsoft.com/windows/desktop/api/winbase/nf-winbase-globalunlock
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern IntBoolean GlobalUnlock(
            HGLOBAL hMem);

        // https://docs.microsoft.com/windows/win32/api/psapi/nf-psapi-getprocessmemoryinfo
        [DllImport(Libraries.Psapi, SetLastError = true, ExactSpelling = true)]
        public static extern IntBoolean GetProcessMemoryInfo(
            ProcessHandle Process,
            ref ProcessMemoryCounters ppsmemCounters,
            uint cb);

        // https://docs.microsoft.com/windows/win32/api/combaseapi/nf-combaseapi-cotaskmemalloc
        [DllImport(Libraries.Ole32, ExactSpelling = true)]
        public static extern IntPtr CoTaskMemAlloc(
            nuint cb);

        // https://docs.microsoft.com/windows/win32/api/combaseapi/nf-combaseapi-cotaskmemrealloc
        [DllImport(Libraries.Ole32, ExactSpelling = true)]
        public static extern IntPtr CoTaskMemRealloc(
            IntPtr pv,
            nuint cb);

        // https://docs.microsoft.com/windows/win32/api/combaseapi/nf-combaseapi-cotaskmemfree
        [DllImport(Libraries.Ole32, ExactSpelling = true)]
        public static extern void CoTaskMemFree(
            IntPtr pv);
    }
}
