// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        public static class Heap
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            private static class Private
            {
                // Heap Functions
                // --------------
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366711.aspx

                //internal const uint HEAP_NO_SERIALIZE = 0x00000001;
                //internal const uint HEAP_GENERATE_EXCEPTIONS = 0x00000004;
                internal const uint HEAP_ZERO_MEMORY = 0x00000008;
                //internal const uint HEAP_REALLOC_IN_PLACE_ONLY = 0x00000010;

                // HeapAlloc/Realloc take a SIZE_T for their count of bytes. This is ultimately an
                // unsigned __int3264 which is platform specific (uint on 32bit and ulong on 64bit).
                // UIntPtr can encapsulate this as it wraps void* and has unsigned constructors.
                // (IntPtr also wraps void*, but uses signed constructors.)
                // 
                // SIZE_T:
                // https://msdn.microsoft.com/en-us/library/cc441980.aspx

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366597.aspx
                [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
                internal static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366704.aspx
                [DllImport(Libraries.Kernel32, SetLastError = false, ExactSpelling = true)]
                internal static extern IntPtr HeapReAlloc(IntPtr hHeap, uint dwFlags, IntPtr lpMem, UIntPtr dwBytes);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366701.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                internal static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366706.aspx
                //[DllImport(Interop.NativeMethods.Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                //internal static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366700.aspx
                //[DllImport(Interop.NativeMethods.Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                //internal static extern bool HeapDestroy(IntPtr hHeap);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366598.aspx
                //[DllImport(Interop.NativeMethods.Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                //internal static extern UIntPtr HeapCompact(IntPtr hHeap, uint dwFlags);

                // This is safe to cache as it will never change for a process once started
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366569.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                internal static extern IntPtr GetProcessHeap();
            }

            /// <summary>
            /// The handle for the process heap.
            /// </summary>
            public static IntPtr ProcessHeap = Private.GetProcessHeap();

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
                return Private.HeapAlloc(
                    hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                    dwFlags: zeroMemory ? Private.HEAP_ZERO_MEMORY : 0,
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
                return Private.HeapReAlloc(
                    hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                    dwFlags: zeroMemory ? Private.HEAP_ZERO_MEMORY : 0,
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
                return Private.HeapFree(
                    hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
                    dwFlags: 0,
                    lpMem: memory);
            }
        }
    }
}
