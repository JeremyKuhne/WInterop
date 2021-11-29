// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.Native;

public readonly struct GpImage
{
    public nuint Handle { get; }

    private GpImage(nuint handle) => Handle = handle;

    public static explicit operator GpImage(GpMetafile metafile) => new(metafile.Handle);
}
