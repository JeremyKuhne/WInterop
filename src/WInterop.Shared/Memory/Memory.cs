// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors;
using WInterop.Memory.Unsafe;

namespace WInterop.Memory
{
    public static partial class Memory
    {
        /// <summary>
        /// The handle for the process heap.
        /// </summary>
        public static IntPtr ProcessHeap = Imports.ProcessHeap;

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
                throw Error.GetExceptionForLastError();
        }
    }
}
