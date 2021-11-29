// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.EmfPlus;

public enum MetafileFrameUnit
{
    Pixel = Unit.Pixel,
    Point = Unit.Point,
    Inch = Unit.Inch,
    Document = Unit.Document,
    Millimeter = Unit.Millimeter,

    /// <summary>
    ///  GDI compatible .01 millimeter units.
    /// </summary>
    Gdi
}
