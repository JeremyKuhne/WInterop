// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly struct BitmapHandle : IDisposable
{
    public HBITMAP Handle { get; }
    private readonly bool _ownsHandle;

    public static BitmapHandle Null = new(default);

    public BitmapHandle(HBITMAP handle, bool ownsHandle = true)
    {
        Debug.Assert(handle.IsInvalid || GdiImports.GetObjectType(handle) == ObjectType.Bitmap);

        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid => Handle.IsInvalid || GdiImports.GetObjectType(Handle) != ObjectType.Bitmap;

    public void Dispose()
    {
        if (_ownsHandle)
            GdiImports.DeleteObject(Handle);
    }

    public static implicit operator HGDIOBJ(BitmapHandle handle) => handle.Handle;
    public static implicit operator HBITMAP(BitmapHandle handle) => handle.Handle;
    public static implicit operator LResult(BitmapHandle handle) => handle.Handle.Handle;
    public static implicit operator GdiObjectHandle(BitmapHandle handle) => new(handle.Handle, ownsHandle: false);
}