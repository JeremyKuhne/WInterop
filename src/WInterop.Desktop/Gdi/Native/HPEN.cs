// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public readonly struct HPEN
{
    public IntPtr Handle { get; }

    public HPEN(IntPtr handle)
    {
        Handle = handle;
    }

    public bool IsInvalid => Handle == IntPtr.Zero;

    public static implicit operator HGDIOBJ(HPEN handle) => new(handle.Handle);
    public static explicit operator HPEN(HGDIOBJ handle) => new(handle.Handle);
}