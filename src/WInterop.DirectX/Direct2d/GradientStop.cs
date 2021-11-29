// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Contains the position and color of a gradient stop. [D2D1_GRADIENT_STOP]
/// </summary>
public readonly struct GradientStop
{
    public readonly float Position;
    public readonly ColorF Color;

    public GradientStop(float position, ColorF color)
    {
        Position = position;
        Color = color;
    }
}
