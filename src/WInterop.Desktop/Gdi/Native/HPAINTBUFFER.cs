// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public readonly struct HPAINTBUFFER
{
    public nuint Value { get; }

    public HPAINTBUFFER(nuint handle) => Value = handle;
    public bool IsInvalid => Value == 0;
}