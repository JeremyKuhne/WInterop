// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Memory.Native;

namespace WInterop.Memory
{
    public struct GlobalHandle : IDisposable
    {
        public HGLOBAL HGLOBAL { get; }
        public ulong Size { get; }

        public GlobalHandle(HGLOBAL handle, ulong size)
        {
            HGLOBAL = handle;
            Size = size;
        }

        public GlobalLock Lock => new GlobalLock(this);

        public void Dispose()
        {
            Memory.GlobalFree(HGLOBAL);
        }
    }
}