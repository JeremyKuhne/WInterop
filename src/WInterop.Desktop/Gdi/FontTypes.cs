// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd162618.aspx
[Flags]
public enum FontTypes
{
    Raster = 0x0001,    // RASTER_FONTTYPE
    Device = 0x0002,    // DEVICE_FONTTYPE
    TrueType = 0x0004     // TRUETYPE_FONTTYPE
}