// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi;

/// <summary>
///  GDI object handle (HGDIOBJ)
/// </summary>
public readonly struct GdiObjectHandle : IDisposable
{
    public HGDIOBJ HGDIOBJ { get; }
    private readonly bool _ownsHandle;

    public static readonly GdiObjectHandle Null = new(HGDIOBJ.NULL);

    public GdiObjectHandle(HGDIOBJ handle, bool ownsHandle = true)
    {
        HGDIOBJ = handle;
        _ownsHandle = ownsHandle;
    }

    public ObjectType GetObjectType() => (ObjectType)TerraFXWindows.GetObjectType(HGDIOBJ);

    public static implicit operator GdiObjectHandle(StockFont font) => new(Gdi.GetStockFont(font).HFONT, false);
    public static implicit operator GdiObjectHandle(StockBrush brush) => new(Gdi.GetStockBrush(brush), false);
    public static implicit operator GdiObjectHandle(SystemColor color) => new(Gdi.GetSystemColorBrush(color), false);
    public static implicit operator GdiObjectHandle(StockPen pen) => new(Gdi.GetStockPen(pen), false);

    public static implicit operator HGDIOBJ(GdiObjectHandle handle) => handle.HGDIOBJ;

    public bool IsNull => HGDIOBJ == HGDIOBJ.NULL;
    public bool IsInvalid => HGDIOBJ == HGDIOBJ.INVALID_VALUE;

    public void Dispose()
    {
        if (_ownsHandle)
            TerraFXWindows.DeleteObject(HGDIOBJ);
    }
}