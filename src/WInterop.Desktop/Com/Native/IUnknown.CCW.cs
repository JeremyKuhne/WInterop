// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com.Native
{
    public unsafe partial struct IUnknown
    {
        public static class CCW
        {
            private static readonly VTable* CCWVTable = AllocateVTable();

            private static unsafe VTable* AllocateVTable()
            {
                // Allocate and create a singular VTable for this type projection.
                var vtable = (VTable*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(CCW), sizeof(VTable));

                // IUnknown
                vtable->QueryInterface = &QueryInterface;
                vtable->AddRef = &AddRef;
                vtable->Release = &Release;
                return vtable;
            }

            public static unsafe IntPtr CreateInstance(object @object)
                => (IntPtr)Lifetime<VTable, object>.Allocate(@object, CCWVTable);

            [UnmanagedCallersOnly]
            private static unsafe HResult QueryInterface(void* @this, Guid* iid, void* ppObject)
            {
                if (*iid == IID_IUnknown)
                {
                    ppObject = @this;
                }
                else
                {
                    ppObject = null;
                    return HResult.E_NOINTERFACE;
                }

                Lifetime<VTable, object>.AddRef(@this);
                return HResult.S_OK;
            }

            [UnmanagedCallersOnly]
            private static unsafe uint AddRef(void* @this) => Lifetime<VTable, object>.AddRef(@this);

            [UnmanagedCallersOnly]
            private static unsafe uint Release(void* @this) => Lifetime<VTable, object>.Release(@this);
        }
    }
}