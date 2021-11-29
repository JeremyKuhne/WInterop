// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  [D2D1_ROUNDED_RECT]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct RoundedRectangle
{
    private readonly LtrbRectangleF _rect;
    public readonly float RadiusX;
    public readonly float RadiusY;

    public RoundedRectangle(RectangleF rectangle, float radiusX, float radiusY)
    {
        _rect = new LtrbRectangleF(rectangle);
        RadiusX = radiusX;
        RadiusY = radiusY;
    }

    public RectangleF Rectangle => _rect;
}
