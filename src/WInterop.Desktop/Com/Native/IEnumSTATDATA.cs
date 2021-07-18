// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using WInterop.Errors;

namespace WInterop.Com.Native
{
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
}