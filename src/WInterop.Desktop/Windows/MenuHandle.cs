﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows.Native;

namespace WInterop.Windows;

public readonly struct MenuHandle : IDisposable
{
    public HMENU HMENU { get; }
    private readonly bool _ownsHandle;

    public MenuHandle(HMENU handle, bool ownsHandle = true)
    {
        HMENU = handle;
        _ownsHandle = ownsHandle;
    }

    public bool IsInvalid => HMENU == HMENU.INVALID_VALUE;

    public void Dispose()
    {
        if (_ownsHandle)
            WindowsImports.DestroyMenu(HMENU);
    }

    public static implicit operator HMENU(MenuHandle handle) => handle.HMENU;
    public static unsafe explicit operator MenuHandle(int id) => new(new HMENU((void*)id), ownsHandle: true);

    public override bool Equals(object? obj) => obj is MenuHandle other && other.HMENU == HMENU;
    public bool Equals(MenuHandle other) => other.HMENU == HMENU;
    public static bool operator ==(MenuHandle a, MenuHandle b) => a.HMENU == b.HMENU;
    public static bool operator !=(MenuHandle a, MenuHandle b) => a.HMENU != b.HMENU;
    public override int GetHashCode() => HMENU.GetHashCode();
}