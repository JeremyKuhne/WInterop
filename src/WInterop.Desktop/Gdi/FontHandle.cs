// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly struct FontHandle : IDisposable
{
    public HFONT HFONT { get; }
    private readonly bool _ownsHandle;

    public FontHandle(HFONT handle, bool ownsHandle = true)
    {
        Debug.Assert(handle == HFONT.NULL || (ObjectType)TerraFXWindows.GetObjectType(handle) == ObjectType.Font);

        HFONT = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsNull => HFONT == HFONT.NULL;

    public bool IsInvalid => HFONT == HFONT.INVALID_VALUE || (ObjectType)TerraFXWindows.GetObjectType(HFONT) != ObjectType.Font;

    public void Dispose()
    {
        if (_ownsHandle && !IsNull)
        {
            GdiImports.DeleteObject(HFONT);
        }
    }

    public static implicit operator HGDIOBJ(FontHandle handle) => handle.HFONT;
    public static implicit operator HFONT(FontHandle handle) => handle.HFONT;
    public static unsafe implicit operator LResult(FontHandle handle) => handle.HFONT.Value;
    public static implicit operator GdiObjectHandle(FontHandle handle) => new(handle.HFONT, ownsHandle: false);
    public static implicit operator FontHandle(StockFont font) => new(TerraFXWindows.GetStockFont((int)font));
}