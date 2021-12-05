// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

public readonly struct RegionHandle : IDisposable
{
    public HRGN HRGN { get; }
    private readonly bool _ownsHandle;

    public static readonly RegionHandle Null = new(HRGN.NULL);

    public RegionHandle(HRGN handle, bool ownsHandle = true)
    {
        Debug.Assert(handle == HRGN.NULL
            || (ObjectType)TerraFXWindows.GetObjectType(handle) == ObjectType.Region);

        HRGN = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid =>
        HRGN == HRGN.INVALID_VALUE
        || (ObjectType)TerraFXWindows.GetObjectType(HRGN) != ObjectType.Region;

    public bool IsNull => HRGN == HRGN.NULL;

    public void Dispose()
    {
        if (_ownsHandle)
            TerraFXWindows.DeleteObject(HRGN);
    }

    public static implicit operator HGDIOBJ(RegionHandle handle) => handle.HRGN;
    public static implicit operator HRGN(RegionHandle handle) => handle.HRGN;
    public static unsafe implicit operator LResult(RegionHandle handle) => handle.HRGN.Value;
    public static implicit operator GdiObjectHandle(RegionHandle handle) => new(handle.HRGN, ownsHandle: false);
}