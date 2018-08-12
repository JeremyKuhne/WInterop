// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes the opacity and transformation of a brush. [D2D1_BRUSH_PROPERTIES]
    /// </summary>
    public readonly struct BrushProperties
    {
        public readonly float Opacity;
        public readonly Matrix3x2 Transform;
    }
}
