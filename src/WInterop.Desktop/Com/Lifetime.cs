// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Runtime.InteropServices;
using static WInterop.Memory.Memory;

namespace WInterop.Com;

/// <summary>
///  Lifetime management helper for a COM callable wrapper. It holds the created <typeparamref name="TObject"/>
///  wrapper with he given <typeparamref name="TVTable"/>.
/// </summary>
/// <remarks>
///  <para>
///   This should not be created directly. Instead use <see cref="Lifetime{TVTable, TObject}.Allocate"/>.
///  </para>
///  <para>
///   A COM object's memory layout is a virtual function table (vtable) pointer followed by instance data. We're
///   effectively manually creating a COM object here that contains instance data of a GCHandle to the related
///   managed object and a ref count.
///  </para>
/// </remarks>
public unsafe struct Lifetime<TVTable, TObject> where TVTable : unmanaged
{
    public TVTable* VTable;
    public IUnknown* Handle;
    public uint RefCount;

    public static unsafe uint AddRef(IUnknown* @this)
        => Interlocked.Increment(ref ((Lifetime<TVTable, TObject>*)@this)->RefCount);

    public static unsafe uint Release(IUnknown* @this)
    {
        var lifetime = (Lifetime<TVTable, TObject>*)@this;
        Debug.Assert(lifetime->RefCount > 0);
        uint count = Interlocked.Decrement(ref lifetime->RefCount);
        if (count == 0)
        {
            GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Free();
            CoTaskFree(lifetime);
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
        // Manually allocate a native instance of this struct.
        var wrapper = (Lifetime<TVTable, TObject>*)CoTaskAllocate((nuint)sizeof(Lifetime<TVTable, TObject>));

        // Assign a pointer to the vtable, allocate a GCHandle for the related object, and set the initial ref count.
        wrapper->VTable = vtable;
        wrapper->Handle = (IUnknown*)GCHandle.ToIntPtr(GCHandle.Alloc(@object));
        wrapper->RefCount = 1;

        return wrapper;
    }

    /// <summary>
    ///  Gets the object wrapped by a lifetime wrapper.
    /// </summary>
    public static TObject? GetObject(IUnknown* @this)
    {
        var lifetime = (Lifetime<TVTable, TObject>*)@this;
        return (TObject?)GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Target;
    }
}