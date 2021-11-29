// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using static WInterop.Memory.Memory;

namespace WInterop.Com.Native;

public unsafe struct Lifetime<TVTable, TObject> where TVTable : unmanaged
{
    public TVTable* VTable;
    public void* Handle;
    public uint RefCount;

    public static unsafe uint AddRef(void* @this)
        => Interlocked.Increment(ref ((Lifetime<TVTable, TObject>*)@this)->RefCount);

    public static unsafe uint Release(void* @this)
    {
        var lifetime = (Lifetime<TVTable, TObject>*)@this;
        Debug.Assert(lifetime->RefCount > 0);
        uint count = Interlocked.Decrement(ref lifetime->RefCount);
        if (count == 0)
        {
            GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Free();
            CoTaskFree((IntPtr)lifetime);
        }

        return count;
    }

    /// <summary>
    ///  Allocate a lifetime wrapper for the given <paramref name="object"/> with the given
    ///  <paramref name="vtable"/>.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   This creates a <see cref="GCHandle"/> to root the <paramref name="object"/> until ref
    ///   counting has gone to zero.
    ///  </para>
    ///  <para>
    ///   The <paramref name="vtable"/> should be fixed, typically as a static. Com calls always
    ///   include the "this" pointer as the first argument.
    ///  </para>
    /// </remarks>
    public static unsafe Lifetime<TVTable, TObject>* Allocate(TObject @object, TVTable* vtable)
    {
        var wrapper = (Lifetime<TVTable, TObject>*)CoTaskAllocate((nuint)sizeof(Lifetime<TVTable, TObject>));

        // Create the wrapper instance.
        wrapper->VTable = vtable;
        wrapper->Handle = (void*)GCHandle.ToIntPtr(GCHandle.Alloc(@object));
        wrapper->RefCount = 1;

        return wrapper;
    }

    public static TObject? GetObject(void* @this)
    {
        var lifetime = (Lifetime<TVTable, TObject>*)@this;
        return (TObject?)GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Target;
    }
}