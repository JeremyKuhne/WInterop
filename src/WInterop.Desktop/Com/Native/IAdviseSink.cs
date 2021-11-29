// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using WInterop.Errors;

namespace WInterop.Com.Native;

public unsafe struct IAdviseSink
{
    public static readonly Guid IID_IEnumFORMATETC = new("0000010f-0000-0000-C000-000000000046");

    private readonly VTable* _vtable;

    public HResult QueryInterface(Guid* riid, void** ppvObject)
        => _vtable->UnknownVTable.QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

    public uint AddRef()
        => _vtable->UnknownVTable.AddRef(Unsafe.AsPointer(ref this));

    public uint Release()
        => _vtable->UnknownVTable.Release(Unsafe.AsPointer(ref this));

    public void OnDataChange(FORMATETC* pFormatetc, STGMEDIUM* pStgmed)
        => _vtable->OnDataChange(Unsafe.AsPointer(ref this), pFormatetc, pStgmed);

    public void OnViewChange(uint dwAspect, int lindex)
        => _vtable->OnViewChange(Unsafe.AsPointer(ref this), dwAspect, lindex);

    /// <param name="pmk">An IMoniker reference.</param>
    public void OnRename(void* pmk)
        => _vtable->OnRename(Unsafe.AsPointer(ref this), pmk);

    public void OnSave()
        => _vtable->OnSave(Unsafe.AsPointer(ref this));

    public void OnClose()
        => _vtable->OnSave(Unsafe.AsPointer(ref this));

    public unsafe struct VTable
    {
        public IUnknown.VTable UnknownVTable;

        public delegate* unmanaged<void*, FORMATETC*, STGMEDIUM*, void> OnDataChange;
        public delegate* unmanaged<void*, uint, int, void> OnViewChange;
        public delegate* unmanaged<void*, void*, void> OnRename;
        public delegate* unmanaged<void*, void> OnSave;
        public delegate* unmanaged<void*, void> OnClose;
    }
}