// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly ref struct BrushHandle
{
    public HBRUSH Handle { get; }
    public bool OwnsHandle { get; }

    /// <summary>
    ///  Used to specifiy that you don't want a default brush picked in WInterop method calls.
    /// </summary>
    public static BrushHandle NoBrush => new(HBRUSH.INVALID_VALUE);

    public BrushHandle(HBRUSH handle, bool ownsHandle = true)
    {
        Debug.Assert(handle == HBRUSH.NULL
            || handle == HBRUSH.INVALID_VALUE
            || TerraFXWindows.GetObjectType(handle) == (int)ObjectType.Brush
            || TerraFXWindows.GetObjectType(handle) == 0);

        Handle = handle;
        OwnsHandle = ownsHandle;
    }

    public bool IsInvalid
        => Handle == HBRUSH.INVALID_VALUE || TerraFXWindows.GetObjectType(Handle) != (int)ObjectType.Brush;

    public void Dispose()
    {
        if (OwnsHandle)
        {
            TerraFXWindows.DeleteObject(Handle);
        }
    }

    public static implicit operator HGDIOBJ(in BrushHandle handle) => handle.Handle;
    public static implicit operator HBRUSH(in BrushHandle handle) => handle.Handle;
    public static unsafe implicit operator LResult(in BrushHandle handle) => (nint)handle.Handle.Value;
    public static implicit operator GdiObjectHandle(in BrushHandle handle) => new(handle.Handle, ownsHandle: false);
    public static implicit operator BrushHandle(in StockBrush brush) => Gdi.GetStockBrush(brush);
    public static implicit operator BrushHandle(in SystemColor color) => Gdi.GetSystemColorBrush(color);

    // You can't box a ref struct, therefore it will never be object
    public override bool Equals(object? obj) => false;

    public bool Equals(BrushHandle other) => other.Handle == Handle;
    public static bool operator ==(BrushHandle a, BrushHandle b) => a.Handle == b.Handle;
    public static bool operator !=(BrushHandle a, BrushHandle b) => a.Handle != b.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
}