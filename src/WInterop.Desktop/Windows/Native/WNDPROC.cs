// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Native;

public unsafe readonly struct WNDPROC
{
    public readonly delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT> Value;

    public WNDPROC(delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT> value) => Value = value;
    public bool IsNull => Value is null;

    public static implicit operator WNDPROC(delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT> value)
        => new(value);
    public static implicit operator delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT>(WNDPROC value)
        => value.Value;
}