// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Native;

public readonly struct HTHEME
{
    public nuint Value { get; }

    public HTHEME(nuint handle) => Value = handle;
    public bool IsInvalid => Value == 0;
}