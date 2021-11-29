// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

/// <summary>
///  GDI object handle (HGDIOBJ)
/// </summary>
public readonly struct GdiObjectHandle : IDisposable
{
    public HGDIOBJ Handle { get; }
    private readonly bool _ownsHandle;

    public static GdiObjectHandle Null = new(default);

    public GdiObjectHandle(HGDIOBJ handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    public ObjectType GetObjectType()
    {
        return GdiImports.GetObjectType(Handle);
    }

    public static implicit operator GdiObjectHandle(StockFont font) => new(Gdi.GetStockFont(font).Handle, false);
    public static implicit operator GdiObjectHandle(StockBrush brush) => new(Gdi.GetStockBrush(brush), false);
    public static implicit operator GdiObjectHandle(SystemColor color) => new(Gdi.GetSystemColorBrush(color), false);
    public static implicit operator GdiObjectHandle(StockPen pen) => new(Gdi.GetStockPen(pen), false);

    public static implicit operator HGDIOBJ(GdiObjectHandle handle) => handle.Handle;

    public bool IsInvalid => Handle.IsInvalid;

    public void Dispose()
    {
        if (_ownsHandle)
            GdiImports.DeleteObject(Handle);
    }
}