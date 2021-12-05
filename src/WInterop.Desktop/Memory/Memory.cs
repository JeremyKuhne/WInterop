// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Memory;

public static partial class Memory
{
    /// <summary>
    ///  The handle for the process heap.
    /// </summary>
    public static readonly HANDLE ProcessHeap = TerraFXWindows.GetProcessHeap();

    /// <summary>
    ///  Allocate memory on the process heap.
    /// </summary>
    /// <exception cref="OverflowException">
    ///  Running in 32 bit and <paramref name="bytes"/> is greater than <see cref="uint.MaxValue"/>
    /// </exception>
    public static unsafe void* HeapAllocate(ulong bytes, bool zeroMemory = true)
        => HeapAllocate(bytes, zeroMemory, HANDLE.NULL);

    /// <summary>
    ///  Allocate memory on the given heap.
    /// </summary>
    /// <param name="heap">If <see cref="HANDLE.NULL"/> will use the process heap.</param>
    /// <exception cref="OverflowException">
    ///  Running in 32 bit and <paramref name="bytes"/> is greater than <see cref="uint.MaxValue"/>
    /// </exception>
    public static unsafe void* HeapAllocate(ulong bytes, bool zeroMemory, HANDLE heap)
    {
        return TerraFXWindows.HeapAlloc(
            hHeap: heap == IntPtr.Zero ? ProcessHeap : heap,
            dwFlags: zeroMemory ? (uint)HEAP.HEAP_ZERO_MEMORY : 0,
            dwBytes: checked((nuint)bytes));
    }

    /// <summary>
    ///  Reallocate memory on the process heap.
    /// </summary>
    /// <exception cref="OverflowException">
    ///  Running in 32 bit and <paramref name="bytes"/> is greater than <see cref="uint.MaxValue"/>
    /// </exception>
    public static unsafe void* HeapReallocate(void* memory, ulong bytes, bool zeroMemory = true)
        => HeapReallocate(memory, bytes, zeroMemory, HANDLE.NULL);

    /// <summary>
    ///  Reallocate memory on the given heap.
    /// </summary>
    /// <param name="heap">If <see cref="HANDLE.NULL"/> will use the process heap.</param>
    /// <exception cref="OverflowException">
    ///  Running in 32 bit and <paramref name="bytes"/> is greater than <see cref="uint.MaxValue"/>
    /// </exception>
    public static unsafe void* HeapReallocate(void* memory, ulong bytes, bool zeroMemory, HANDLE heap)
    {
        return TerraFXWindows.HeapReAlloc(
            hHeap: heap == HANDLE.NULL ? ProcessHeap : heap,
            dwFlags: zeroMemory ? (uint)HEAP.HEAP_ZERO_MEMORY : 0,
            lpMem: memory,
            dwBytes: checked((nuint)bytes));
    }

    /// <summary>
    ///  Free the specified memory on the process heap.
    /// </summary>
    public static unsafe bool HeapFree(void* memory) => HeapFree(memory, HANDLE.NULL);

    /// <summary>
    ///  Free the specified memory on the given heap.
    /// </summary>
    /// <param name="heap">If IntPtr.Zero will use the process heap.</param>
    public static unsafe bool HeapFree(void* memory, HANDLE heap)
    {
        return TerraFXWindows.HeapFree(
            hHeap: heap == HANDLE.NULL ? ProcessHeap : heap,
            dwFlags: 0,
            lpMem: memory);
    }

    public static unsafe void LocalFree(HLOCAL memory)
    {
        if (TerraFXWindows.LocalFree(memory) != HLOCAL.NULL)
            Error.ThrowLastError();
    }

    public static GlobalHandle GlobalAlloc(ulong bytes, GlobalMemoryFlags flags)
    {
        HGLOBAL handle = TerraFXWindows.GlobalAlloc((uint)flags, (nuint)bytes);
        if (handle == HGLOBAL.NULL)
            Error.ThrowLastError();
        return new GlobalHandle(handle, bytes);
    }

    public static unsafe void* GlobalLock(GlobalHandle handle)
    {
        void* memory = TerraFXWindows.GlobalLock(handle.HGLOBAL);
        if (memory == null)
            Error.ThrowLastError();
        return memory;
    }

    public static void GlobalUnlock(GlobalHandle handle)
    {
        if (!TerraFXWindows.GlobalUnlock(handle.HGLOBAL))
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);
    }

    public static void GlobalFree(HGLOBAL handle)
    {
        if (TerraFXWindows.GlobalFree(handle) != HGLOBAL.NULL)
            Error.ThrowLastError();
    }

    public static unsafe void* CoTaskAllocate(nuint bytes)
        => TerraFXWindows.CoTaskMemAlloc(bytes);

    public static unsafe void CoTaskFree(void* handle)
        => TerraFXWindows.CoTaskMemFree(handle);
}