// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using System.Runtime.CompilerServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the opacity and transformation of a brush. [D2D1_BRUSH_PROPERTIES]
    /// </summary>
    public struct BrushProperties
    {
        public readonly float Opacity;
        public readonly Matrix3x2 Transform;

        public static BrushProperties Default { get; } = new(1.0f, Matrix3x2.Identity);

        public BrushProperties(float opacity, Matrix3x2 transform)
        {
            Opacity = opacity;
            Transform = transform;
        }

        public static implicit operator BrushProperties(in D2D1_BRUSH_PROPERTIES properties)
            => Unsafe.As<D2D1_BRUSH_PROPERTIES, BrushProperties>(ref Unsafe.AsRef(properties));

        public static implicit operator D2D1_BRUSH_PROPERTIES(in BrushProperties properties)
            => Unsafe.As<BrushProperties, D2D1_BRUSH_PROPERTIES>(ref Unsafe.AsRef(properties));
    }
}
