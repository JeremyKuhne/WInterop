// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com.Native
{
    /// <summary>
    ///  Fast COM IUnknown method access.
    /// </summary>
    /// <remarks>
    ///  This must only be used via pointers. Cast a void* or IntPtr to IUnknown* and access via dereference.
    /// </remarks>
    /// <example>
    ///  IUnknown* unknown = (IUnknown*)CreateStorage(/* ... */);
    ///  uint refCount = unknown->AddRef();
    /// </example>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct IUnknown
    {
        public static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

        private readonly VTable* _vtable;

        public HResult QueryInterface(Guid* riid, void** ppvObject)
            => _vtable->QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

        public uint AddRef()
            => _vtable->AddRef(Unsafe.AsPointer(ref this));

        public uint Release()
            => _vtable->Release(Unsafe.AsPointer(ref this));
    }
}
