// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public readonly struct HDC
{
    public nuint Handle { get; }

    public HDC(nuint handle)
    {
        Handle = handle;
    }

    public bool IsInvalid => Handle == 0 || Handle == unchecked((nuint)(-1));
}