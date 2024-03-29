﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly struct BitmapHandle : IDisposable
{
    public HBITMAP Handle { get; }
    private readonly bool _ownsHandle;

    public static readonly BitmapHandle Null = new(default);

    public BitmapHandle(HBITMAP handle, bool ownsHandle = true)
    {
        Debug.Assert(handle == HBITMAP.NULL || TerraFXWindows.GetObjectType(handle) == (uint)ObjectType.Bitmap);

        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid
        => Handle == HBITMAP.INVALID_VALUE || TerraFXWindows.GetObjectType(Handle) != (int)ObjectType.Bitmap;

    public void Dispose()
    {
        if (_ownsHandle)
        {
            TerraFXWindows.DeleteObject(Handle);
        }
    }

    public static implicit operator HGDIOBJ(BitmapHandle handle) => handle.Handle;
    public static implicit operator HBITMAP(BitmapHandle handle) => handle.Handle;
    public static unsafe implicit operator LResult(BitmapHandle handle) => (nint)handle.Handle.Value;
    public static implicit operator GdiObjectHandle(BitmapHandle handle) => new(handle.Handle, ownsHandle: false);
}