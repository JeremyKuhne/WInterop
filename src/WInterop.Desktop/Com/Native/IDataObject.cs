// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com.Native
{
    public unsafe struct IDataObject
    {
        public static readonly Guid IID_IDataObject = new("0000010e-0000-0000-C000-000000000046");

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
#pragma warning disable IDE0060 // Remove unused parameter
            private static unsafe HResult GetData(void* @this, FORMATETC* pformatetcIn, STGMEDIUM* pmedium)
#pragma warning restore IDE0060 // Remove unused parameter
            {
                // var lifetime = (Lifetime<VTable>*)@this;
                // var dataObject = GCHandle.FromIntPtr((IntPtr)lifetime->Handle).Target as Managed.IDropSource;
                // return dropSource?.QueryContinueDrag(fEscapePressed.FromBOOL(), grfKeyState) ?? HResult.E_FAIL;

                return HResult.S_OK;
            }
        }
    }
}