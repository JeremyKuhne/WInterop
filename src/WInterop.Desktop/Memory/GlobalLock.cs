// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Memory;

public readonly struct GlobalLock : IDisposable
{
    public GlobalHandle Handle { get; }
    public unsafe void* Pointer { get; }

    public unsafe GlobalLock(GlobalHandle handle)
    {
        Handle = handle;
        Pointer = Memory.GlobalLock(handle);
    }

    public unsafe Span<T> GetSpan<T>() where T : unmanaged
        => new(Pointer, (int)Handle.Size / sizeof(T));

    public void Dispose() => Memory.GlobalUnlock(Handle);
}