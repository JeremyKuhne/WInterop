// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native;

[Flags]
public enum BufferedPaintFlags : uint
{
    Erase = 0x0001,
    NoClip = 0x0002,
    NonClient = 0x0004
}