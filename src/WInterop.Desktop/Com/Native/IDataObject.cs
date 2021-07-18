// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com.Native
{
    public unsafe struct IAdviseSink
    {
        public static readonly Guid IID_IEnumFORMATETC = new Guid("0000010f-0000-0000-C000-000000000046");

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

    public unsafe struct IEnumFORMATETC
    {
        public static readonly Guid IID_IEnumFORMATETC = new Guid("00000103-0000-0000-C000-000000000046");

        private readonly VTable* _vtable;

        public HResult QueryInterface(Guid* riid, void** ppvObject)
            => _vtable->UnknownVTable.QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

        public uint AddRef()
            => _vtable->UnknownVTable.AddRef(Unsafe.AsPointer(ref this));

        public uint Release()
            => _vtable->UnknownVTable.Release(Unsafe.AsPointer(ref this));

        public HResult Next(uint celt, FORMATETC* rgelt, uint* pceltFetched)
            => _vtable->Next(Unsafe.AsPointer(ref this), celt, rgelt, pceltFetched);

        public HResult Skip(uint celt)
            => _vtable->Skip(Unsafe.AsPointer(ref this), celt);

        public HResult Reset()
            => _vtable->Reset(Unsafe.AsPointer(ref this));

        public HResult Clone(IEnumFORMATETC** ppenum)
            => _vtable->Clone(Unsafe.AsPointer(ref this), ppenum);

        public unsafe struct VTable
        {
            public IUnknown.VTable UnknownVTable;

            public delegate* unmanaged<void*, uint, FORMATETC*, uint*, HResult> Next;
            public delegate* unmanaged<void*, uint, HResult> Skip;
            public delegate* unmanaged<void*, HResult> Reset;
            public delegate* unmanaged<void*, IEnumFORMATETC**, HResult> Clone;
        }
    }

    public enum ADVF : uint
    {
        ADVF_NODATA = 1,
        ADVF_PRIMEFIRST = 2,
        ADVF_ONLYONCE = 4,
        ADVF_DATAONSTOP = 64,
        ADVFCACHE_NOHANDLER = 8,
        ADVFCACHE_FORCEBUILTIN = 16,
        ADVFCACHE_ONSAVE = 32
    }

    public unsafe struct IEnumSTATDATA
    {
        public static readonly Guid IID_IEnumSTATDATA = new Guid("00000105-0000-0000-C000-000000000046");

        private readonly VTable* _vtable;

        public HResult QueryInterface(Guid* riid, void** ppvObject)
            => _vtable->UnknownVTable.QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

        public uint AddRef()
            => _vtable->UnknownVTable.AddRef(Unsafe.AsPointer(ref this));

        public uint Release()
            => _vtable->UnknownVTable.Release(Unsafe.AsPointer(ref this));

        public HResult Next(uint celt, STATDATA* rgelt, uint* pceltFetched)
            => _vtable->Next(Unsafe.AsPointer(ref this), celt, rgelt, pceltFetched);

        public HResult Skip(uint celt)
            => _vtable->Skip(Unsafe.AsPointer(ref this), celt);

        public HResult Reset()
            => _vtable->Reset(Unsafe.AsPointer(ref this));

        public HResult Clone(IEnumSTATDATA** ppenum)
            => _vtable->Clone(Unsafe.AsPointer(ref this), ppenum);

        public unsafe struct VTable
        {
            public IUnknown.VTable UnknownVTable;

            public delegate* unmanaged<void*, uint, STATDATA*, uint*, HResult> Next;
            public delegate* unmanaged<void*, uint, HResult> Skip;
            public delegate* unmanaged<void*, HResult> Reset;
            public delegate* unmanaged<void*, IEnumSTATDATA**, HResult> Clone;
        }
    }

    public unsafe struct IDataObject
    {
        public static readonly Guid IID_IDataObject = new Guid("0000010e-0000-0000-C000-000000000046");

        private readonly VTable* _vtable;

        public HResult QueryInterface(Guid* riid, void** ppvObject)
            => _vtable->UnknownVTable.QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

        public uint AddRef()
            => _vtable->UnknownVTable.AddRef(Unsafe.AsPointer(ref this));

        public uint Release()
            => _vtable->UnknownVTable.Release(Unsafe.AsPointer(ref this));

        public HResult GetData(FORMATETC* pformatetcIn, STGMEDIUM* pmedium)
            => _vtable->GetData(Unsafe.AsPointer(ref this), pformatetcIn, pmedium);

        public HResult GetDataHere(FORMATETC* pformatetc, STGMEDIUM* pmedium)
            => _vtable->GetDataHere(Unsafe.AsPointer(ref this), pformatetc, pmedium);

        public HResult QueryGetData(FORMATETC* pformatetc)
            => _vtable->QueryGetData(Unsafe.AsPointer(ref this), pformatetc);

        public HResult GetCanonicalFormatEtc(FORMATETC* pformatetcIn, FORMATETC* pformatetcOut)
            => _vtable->GetCanonicalFormatEtc(Unsafe.AsPointer(ref this), pformatetcIn, pformatetcOut);

        public HResult SetData(FORMATETC* pformatetc, STGMEDIUM* pmedium, BOOL fRelease)
            => _vtable->SetData(Unsafe.AsPointer(ref this), pformatetc, pmedium, fRelease);

        public HResult EnumFormatEtc(DataDirection dwDirection, IEnumFORMATETC** ppenumFormatEtc)
            => _vtable->EnumFormatEtc(Unsafe.AsPointer(ref this), dwDirection, ppenumFormatEtc);

        public HResult DAdvise(FORMATETC* pformatetc, uint advf, IAdviseSink* pAdvSink, uint* pdwConnection)
            => _vtable->DAdvise(Unsafe.AsPointer(ref this), pformatetc, advf, pAdvSink, pdwConnection);

        public HResult DUnadvise(uint dwConnection)
            => _vtable->DUnadvise(Unsafe.AsPointer(ref this), dwConnection);

        public HResult EnumDAdvise(void** ppenumAdvise)
            => _vtable->EnumDAdvise(Unsafe.AsPointer(ref this), ppenumAdvise);

        public unsafe struct VTable
        {
            public IUnknown.VTable UnknownVTable;

            public delegate* unmanaged<void*, FORMATETC*, STGMEDIUM*, HResult> GetData;
            public delegate* unmanaged<void*, FORMATETC*, STGMEDIUM*, HResult> GetDataHere;
            public delegate* unmanaged<void*, FORMATETC*, HResult> QueryGetData;
            public delegate* unmanaged<void*, FORMATETC*, FORMATETC*, HResult> GetCanonicalFormatEtc;
            public delegate* unmanaged<void*, FORMATETC*, STGMEDIUM*, BOOL, HResult> SetData;
            public delegate* unmanaged<void*, DataDirection, IEnumFORMATETC**, HResult> EnumFormatEtc;
            public delegate* unmanaged<void*, FORMATETC*, uint, IAdviseSink*, uint*, HResult> DAdvise;
            public delegate* unmanaged<void*, uint, HResult> DUnadvise;

            // IEnumSTATDATA
            public delegate* unmanaged<void*, void**, HResult> EnumDAdvise;
        }

        public static class CCW
        {
            private static readonly VTable* CCWVTable = AllocateVTable();

            private static unsafe VTable* AllocateVTable()
            {
                // Allocate and create a singular VTable for this type projection.
                var vtable = (VTable*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(CCW), sizeof(VTable));

                // IUnknown
                vtable->UnknownVTable.QueryInterface = &QueryInterface;
                vtable->UnknownVTable.AddRef = &AddRef;
                vtable->UnknownVTable.Release = &Release;
                return vtable;
            }

            [UnmanagedCallersOnly]
            private static unsafe HResult QueryInterface(void* @this, Guid* iid, void* ppObject)
            {
                if (*iid == IUnknown.IID_IUnknown || *iid == IID_IDataObject)
                {
                    ppObject = @this;
                }
                else
                {
                    ppObject = null;
                    return HResult.E_NOINTERFACE;
                }

                Lifetime<VTable>.AddRef(@this);
                return HResult.S_OK;
            }

            [UnmanagedCallersOnly]
            private static unsafe uint AddRef(void* @this) => Lifetime<VTable>.AddRef(@this);

            [UnmanagedCallersOnly]
            private static unsafe uint Release(void* @this) => Lifetime<VTable>.Release(@this);

            [UnmanagedCallersOnly]
            private static unsafe HResult GetData(void* @this, FORMATETC* pformatetcIn, STGMEDIUM* pmedium)
            {
                var lifetime = (Lifetime<VTable>*)@this;
                //var dataObject = GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Target as Managed.IDropSource;
                //return dropSource?.QueryContinueDrag(fEscapePressed.FromBOOL(), grfKeyState) ?? HResult.E_FAIL;

                return HResult.S_OK;
            }
        }
    }
}
