// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public readonly struct RegionHandle : IDisposable
    {
        public HRGN Handle { get; }
        private readonly bool _ownsHandle;

        public static RegionHandle Null = new RegionHandle(default);

        public RegionHandle(HGDIOBJ handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || GdiMethods.Imports.GetObjectType(handle) == ObjectType.Region);

            Handle = new HRGN(handle.Handle);
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => Handle.IsInvalid || GdiMethods.Imports.GetObjectType(Handle) != ObjectType.Region;

        public void Dispose()
        {
            if (_ownsHandle)
                GdiMethods.Imports.DeleteObject(Handle);
        }

        public static implicit operator HGDIOBJ(RegionHandle handle) => handle.Handle;
        public static implicit operator HRGN(RegionHandle handle) => handle.Handle;
        public static implicit operator RegionHandle(HRGN handle) => new RegionHandle(handle, ownsHandle: true);
        public static implicit operator LRESULT(RegionHandle handle) => handle.Handle.Handle;
        public static implicit operator GdiObjectHandle(RegionHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
    }
}
