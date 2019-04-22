// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors;
using WInterop.Memory.Native;

namespace WInterop.Memory
{
    public static partial class Memory
    {
        /// <summary>
        /// The handle for the process heap.
        /// </summary>
        public static IntPtr ProcessHeap = Imports.GetProcessHeap();

        /// <summary>
        /// Allocate memory on the process heap.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if running in 32 bit and <paramref name="bytes"/> is greater than uint.MaxValue.</exception>
        public static IntPtr HeapAllocate(ulong bytes, bool zeroMemory = true)
        {
            return HeapAllocate(bytes, zeroMemory, IntPtr.Zero);
        }

        /// <summary>
        /// Allocate memory on the given heap.
        /// </summary>
        /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
        /// <exception cref="OverflowException">Thrown if running in 32 bit and <paramref name="bytes"/> is greater than uint.MaxValue.</exception>
        public static IntPtr HeapAllocate(ulong bytes, bool zeroMemory, IntPtr heap)
        {
            return Imports.HeapAlloc(
                hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                dwFlags: zeroMemory ? MemoryDefines.HEAP_ZERO_MEMORY : 0,
                dwBytes: (UIntPtr)bytes);
        }

        /// <summary>
        /// Reallocate memory on the process heap.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if running in 32 bit and <paramref name="bytes"/> is greater than uint.MaxValue.</exception>
        public static IntPtr HeapReallocate(IntPtr memory, ulong bytes, bool zeroMemory = true)
        {
            return HeapReallocate(memory, bytes, zeroMemory, IntPtr.Zero);
        }

        /// <summary>
        /// Reallocate memory on the given heap.
        /// </summary>
        /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
        /// <exception cref="OverflowException">Thrown if running in 32 bit and <paramref name="bytes"/> is greater than uint.MaxValue.</exception>
        public static IntPtr HeapReallocate(IntPtr memory, ulong bytes, bool zeroMemory, IntPtr heap)
        {
            return Imports.HeapReAlloc(
                hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                dwFlags: zeroMemory ? MemoryDefines.HEAP_ZERO_MEMORY : 0,
                lpMem: memory,
                dwBytes: (UIntPtr)bytes);
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
                Error.ThrowLastError();
        }

        public static GlobalHandle GlobalAlloc(ulong bytes, GlobalMemoryFlags flags)
        {
            HGLOBAL handle = Imports.GlobalAlloc(flags, (UIntPtr)bytes);
            if (handle.Value == IntPtr.Zero)
                Error.ThrowLastError();
            return new GlobalHandle(handle, bytes);
        }

        public static IntPtr GlobalLock(GlobalHandle handle)
        {
            IntPtr memory = Imports.GlobalLock(handle.HGLOBAL);
            if (memory == IntPtr.Zero)
                Error.ThrowLastError();
            return memory;
        }

        public static void GlobalUnlock(GlobalHandle handle)
        {
            if (!Imports.GlobalUnlock(handle.HGLOBAL))
                Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);
        }

        public static void GlobalFree(HGLOBAL handle)
        {
            if (Imports.GlobalFree(handle).Value != IntPtr.Zero)
                Error.ThrowLastError();
        }
    }
}
