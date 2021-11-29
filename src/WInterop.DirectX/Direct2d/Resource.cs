// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

public interface IResource
{
    Factory GetFactory();
}

internal interface IResource<T> : IHandle<T> where T : unmanaged
{
}

internal static unsafe class ResourceExtensions
{
    public static Factory GetFactory<T, H>(this T resource) where T : IResource<H> where H : unmanaged
    {
        ID2D1Factory* factory;
        ((ID2D1Resource*)resource.Handle)->GetFactory(&factory);
        return new(factory);
    }
}

[Guid(InterfaceIds.IID_ID2D1Resource)]
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct Resource : Resource.Interface
{
    private readonly ID2D1Resource* _resource;

    public Factory GetFactory()
    {
        ID2D1Factory* factory;
        _resource->GetFactory(&factory);
        return new(factory);
    }

    internal static Factory GetFactory(ID2D1Resource* resource)
    {
        ID2D1Factory* factory;
        resource->GetFactory(&factory);
        return new(factory);
    }

    internal static ref Brush From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<Brush>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public void Dispose() => _resource->Release();

    /// <summary>
    ///  [ID2D1Resource]
    /// </summary>
    internal interface Interface : IDisposable
    {
        Factory GetFactory();
    }
}
