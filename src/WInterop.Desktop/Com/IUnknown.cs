// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com
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
    public unsafe struct IUnknown
    {
        private readonly void** _vtbl;

        public HResult QueryInterface(Guid* riid, void** ppvObject)
            => ((delegate* unmanaged<void*, Guid*, void**, HResult>)_vtbl[0])(
                Unsafe.AsPointer(ref this), riid, ppvObject);

        public uint AddRef()
            => ((delegate* unmanaged<void*, uint>)_vtbl[1])(
                Unsafe.AsPointer(ref this));

        public uint Release()
            => ((delegate* unmanaged<void*, uint>)_vtbl[2])(
                Unsafe.AsPointer(ref this));
    }
}
