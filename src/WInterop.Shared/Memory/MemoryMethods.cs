// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using Internal = WInterop.Support.Internal;

namespace WInterop.Memory
{
    public static partial class Memory
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
            public static IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes) => Imports.HeapAlloc(hHeap, dwFlags, dwBytes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366704.aspx
            public static IntPtr HeapReAlloc(IntPtr hHeap, uint dwFlags, IntPtr lpMem, UIntPtr dwBytes) => Imports.HeapReAlloc(hHeap, dwFlags, lpMem, dwBytes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366701.aspx
            public static bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem) => Imports.HeapFree(hHeap, dwFlags, lpMem);

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
            public static IntPtr GetProcessHeap() => Internal.Imports.GetProcessHeap();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366730.aspx
            public static IntPtr LocalFree(IntPtr hMem) => Internal.Imports.LocalFree(hMem);
        }

        /// <summary>
        /// The handle for the process heap.
        /// </summary>
        public static IntPtr ProcessHeap = Internal.Imports.ProcessHeap;

        /// <summary>
        /// Allocate memory on the process heap.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if running in 32 bit and byteLength is greater than uint.MaxValue.</exception>
        public static IntPtr HeapAllocate(ulong byteLength, bool zeroMemory = true)
        {
            return HeapAllocate(byteLength, zeroMemory, IntPtr.Zero);
        }

        /// <summary>
        /// Allocate memory on the given heap.
        /// </summary>
        /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
        /// <exception cref="OverflowException">Thrown if running in 32 bit and byteLength is greater than uint.MaxValue.</exception>
        public static IntPtr HeapAllocate(ulong byteLength, bool zeroMemory, IntPtr heap)
        {
            return Imports.HeapAlloc(
                hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                dwFlags: zeroMemory ? MemoryDefines.HEAP_ZERO_MEMORY : 0,
                dwBytes: (UIntPtr)byteLength);
        }

        /// <summary>
        /// Reallocate memory on the process heap.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if running in 32 bit and byteLength is greater than uint.MaxValue.</exception>
        public static IntPtr HeapReallocate(IntPtr memory, ulong byteLength, bool zeroMemory = true)
        {
            return HeapReallocate(memory, byteLength, zeroMemory, IntPtr.Zero);
        }

        /// <summary>
        /// Reallocate memory on the given heap.
        /// </summary>
        /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
        /// <exception cref="OverflowException">Thrown if running in 32 bit and byteLength is greater than uint.MaxValue.</exception>
        public static IntPtr HeapReallocate(IntPtr memory, ulong byteLength, bool zeroMemory, IntPtr heap)
        {
            return Imports.HeapReAlloc(
                hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                dwFlags: zeroMemory ? MemoryDefines.HEAP_ZERO_MEMORY : 0,
                lpMem: memory,
                dwBytes: (UIntPtr)byteLength);
        }

        /// <summary>
        /// Free the specified memory on the process heap.
        /// </summary>
        public static bool HeapFree(IntPtr memory)
        {
            return HeapFree(memory, IntPtr.Zero);
        }

        /// <summary>
        /// Free the specified memory on the given heap.
        /// </summary>
        /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
        public static bool HeapFree(IntPtr memory, IntPtr heap)
        {
            return Imports.HeapFree(
                hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                dwFlags: 0,
                lpMem: memory);
        }

        public static void LocalFree(IntPtr memory)
        {
            if (Imports.LocalFree(memory) != IntPtr.Zero)
                throw Error.GetIoExceptionForLastError();
        }
    }
}
