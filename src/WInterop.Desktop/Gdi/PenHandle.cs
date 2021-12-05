// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly struct PenHandle : IDisposable
{
    public HPEN HPEN { get; }
    private readonly bool _ownsHandle;

    public static readonly PenHandle Null = new(HPEN.NULL);

    public PenHandle(HPEN handle, bool ownsHandle = true)
    {
        Debug.Assert(handle == HPEN.NULL
            || (ObjectType)TerraFXWindows.GetObjectType(handle) == ObjectType.Pen
            || (ObjectType)TerraFXWindows.GetObjectType(handle) == ObjectType.ExtendedPen);

        HPEN = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid
    {
        get
        {
            if (HPEN == HPEN.NULL || HPEN == HPEN.INVALID_VALUE)
                return true;

            ObjectType type = (ObjectType)TerraFXWindows.GetObjectType(HPEN);
            return !(type == ObjectType.Pen || type == ObjectType.ExtendedPen);
        }
    }

    public void Dispose()
    {
        if (_ownsHandle)
            TerraFXWindows.DeleteObject(HPEN);
    }

    public static implicit operator HGDIOBJ(PenHandle handle) => handle.HPEN;
    public static implicit operator HPEN(PenHandle handle) => handle.HPEN;
    public static unsafe implicit operator LResult(PenHandle handle) => handle.HPEN.Value;
    public static implicit operator GdiObjectHandle(PenHandle handle)
        => new(handle.HPEN, ownsHandle: false);
    public static implicit operator PenHandle(StockPen pen) => Gdi.GetStockPen(pen);
}