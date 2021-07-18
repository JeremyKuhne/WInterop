// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using static WInterop.Memory.Memory;

namespace WInterop.Com.Native
{
    public unsafe struct Lifetime<TVTable> where TVTable : unmanaged
    {
        public TVTable* VTable;
        public void* Handle;
        public uint RefCount;

        public static unsafe uint AddRef(void* @this)
            => Interlocked.Increment(ref ((Lifetime<TVTable>*)@this)->RefCount);

        public static unsafe uint Release(void* @this)
        {
            var lifetime = (Lifetime<TVTable>*)@this;
            Debug.Assert(lifetime->RefCount > 0);
            uint count = Interlocked.Decrement(ref lifetime->RefCount);
            if (count == 0)
            {
                GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Free();
                CoTaskFree((IntPtr)lifetime);
            }

            return count;
        }

        public static unsafe Lifetime<TVTable>* Allocate(object @object, TVTable* vtable)
        {
            var wrapper = (Lifetime<TVTable>*)CoTaskAllocate((nuint)sizeof(Lifetime<TVTable>));

            // Create the wrapper instance.
            wrapper->VTable = vtable;
            wrapper->Handle = (void*)GCHandle.ToIntPtr(GCHandle.Alloc(@object));
            wrapper->RefCount = 1;

            return wrapper;
        }
    }
}