// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public readonly struct HENHMETAFILE
{
    public nint Value { get; }

    public HENHMETAFILE(nint handle)
    {
        Value = handle;
    }

    public bool IsInvalid => IsNull;
    public bool IsNull => Value == 0;

    public static implicit operator HGDIOBJ(HENHMETAFILE handle) => new HGDIOBJ(handle.Value);
    public static explicit operator HENHMETAFILE(HGDIOBJ handle) => new HENHMETAFILE(handle.Handle);
    public static explicit operator HENHMETAFILE(nint handle) => new HENHMETAFILE(handle);
}