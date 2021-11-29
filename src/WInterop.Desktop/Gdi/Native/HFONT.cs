// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native;

public readonly struct HFONT
{
    public IntPtr Value { get; }

    public HFONT(IntPtr handle)
    {
        Value = handle;
    }

    public bool IsInvalid => IsNull;
    public bool IsNull => Value == IntPtr.Zero;

    public static implicit operator HGDIOBJ(HFONT handle) => new HGDIOBJ(handle.Value);
    public static explicit operator HFONT(HGDIOBJ handle) => new HFONT(handle.Handle);
    public static explicit operator HFONT(IntPtr handle) => new HFONT(handle);
}