﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public sealed class BrushHolder : IDisposable
{
    private readonly HBRUSH _brush;
    private readonly bool _shouldDispose;

    private BrushHolder(BrushHandle handle)
    {
        _brush = handle.Handle;
        _shouldDispose = handle.OwnsHandle;
        if (!_shouldDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    public static implicit operator BrushHolder(in BrushHandle handle) => new(handle);
    public static implicit operator BrushHandle(BrushHolder holder)
        => new(holder._brush, ownsHandle: holder._shouldDispose);
    public static implicit operator GdiObjectHandle(in BrushHolder holder)
        => new(holder._brush, ownsHandle: false);

    ~BrushHolder() => Dispose();

    public void Dispose()
    {
        if (_shouldDispose)
        {
            TerraFXWindows.DeleteObject(_brush);
        }

        GC.SuppressFinalize(this);
    }
}