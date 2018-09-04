// ----------------------
//    xTask Framework
// ----------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Memory
{
    public readonly struct GlobalLock : IDisposable
    {
        public GlobalHandle Handle { get; }
        public IntPtr Pointer { get; }

        public GlobalLock(GlobalHandle handle)
        {
            Handle = handle;
            Pointer = Memory.GlobalLock(handle);
        }

        public unsafe Span<T> GetSpan<T>() where T : unmanaged
        {
            return new Span<T>(Pointer.ToPointer(), (int)Handle.Size / sizeof(T));
        }

        public void Dispose()
        {
            Memory.GlobalUnlock(Handle);
        }
    }
}
