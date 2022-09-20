// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com.Native;

public static class Unknown
{
    public unsafe class CCW
    {
        private static readonly IUnknown.Vtbl<IUnknown>* CCWVTable = AllocateVTable();

        private static unsafe IUnknown.Vtbl<IUnknown>* AllocateVTable()
        {
            // Allocate and create a static VTable for this type projection.
            var vtable = (IUnknown.Vtbl<IUnknown>*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(CCW), sizeof(IUnknown.Vtbl<IUnknown>));

            // IUnknown
            vtable->QueryInterface = &QueryInterface;
            vtable->AddRef = &AddRef;
            vtable->Release = &Release;
            return vtable;
        }

        public static unsafe IUnknown* CreateInstance(object @object)
            => (IUnknown*)Lifetime<IUnknown.Vtbl<IUnknown>, object>.Allocate(@object, CCWVTable);

        [UnmanagedCallersOnly]
        private static unsafe int QueryInterface(IUnknown* @this, Guid* iid, void** ppObject)
        {
            if (ppObject is null)
            {
                return (int)HResult.E_POINTER;
            }

            if (*iid == typeof(IUnknown).GUID)
            {
                *ppObject = @this;
            }
            else
            {
                *ppObject = null;
                return (int)HResult.E_NOINTERFACE;
            }

            Lifetime<IUnknown.Vtbl<IUnknown>, object>.AddRef(@this);
            return (int)HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe uint AddRef(IUnknown* @this) => Lifetime<IUnknown.Vtbl<IUnknown>, object>.AddRef(@this);

        [UnmanagedCallersOnly]
        private static unsafe uint Release(IUnknown* @this) => Lifetime<IUnknown.Vtbl<IUnknown>, object>.Release(@this);
    }
}