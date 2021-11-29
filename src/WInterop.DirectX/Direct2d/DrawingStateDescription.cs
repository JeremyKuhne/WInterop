// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace WInterop.Direct2d;

/// <summary>
///  Allows the drawing state to be atomically created. This also specifies the
///  drawing state that is saved into an IDrawingStateBlock object.
///  [D2D1_DRAWING_STATE_DESCRIPTION]
/// </summary>
public readonly struct DrawingStateDescription
{
    public readonly AntialiasMode AntialiasMode;
    public readonly TextAntialiasMode TextAntialiasMode;
    public readonly ulong Tag1;
    public readonly ulong Tag2;
    public readonly Matrix3x2 Transform;
}
