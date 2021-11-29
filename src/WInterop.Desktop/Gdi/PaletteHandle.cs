// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly ref struct PaletteHandle
{
    public HPALETTE HPALETTE { get; }
    private readonly bool _ownsHandle;

    public PaletteHandle(HPALETTE handle, bool ownsHandle = true)
    {
        Debug.Assert(handle.IsInvalid || handle.Value == ((IntPtr)(-1))
            || GdiImports.GetObjectType(handle) == ObjectType.Palette || GdiImports.GetObjectType(handle) == 0);

        HPALETTE = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid => HPALETTE.IsInvalid || GdiImports.GetObjectType(HPALETTE) != ObjectType.Brush;

    public void Dispose()
    {
        if (_ownsHandle)
            GdiImports.DeleteObject(HPALETTE);
    }

    public static implicit operator HGDIOBJ(in PaletteHandle handle) => handle.HPALETTE;
    public static implicit operator HPALETTE(in PaletteHandle handle) => handle.HPALETTE;
    public static implicit operator LResult(in PaletteHandle handle) => handle.HPALETTE.Value;
    public static implicit operator GdiObjectHandle(in PaletteHandle handle) => new(handle.HPALETTE, ownsHandle: false);

    // You can't box a ref struct, therefore it will never be object
    public override bool Equals(object? obj) => false;

    public bool Equals(PaletteHandle other) => other.HPALETTE == HPALETTE;
    public static bool operator ==(PaletteHandle a, PaletteHandle b) => a.HPALETTE == b.HPALETTE;
    public static bool operator !=(PaletteHandle a, PaletteHandle b) => a.HPALETTE != b.HPALETTE;
    public override int GetHashCode() => HPALETTE.GetHashCode();
}