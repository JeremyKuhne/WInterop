// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native;

public readonly struct WNDPROC
{
    public readonly IntPtr Value;

    public WNDPROC(IntPtr value) => Value = value;
    public bool IsNull => Value == IntPtr.Zero;

    public static explicit operator WNDPROC(IntPtr value) => new WNDPROC(value);
    public static explicit operator IntPtr(WNDPROC value) => value.Value;
}