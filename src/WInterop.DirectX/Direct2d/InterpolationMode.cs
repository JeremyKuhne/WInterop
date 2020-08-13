// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  This defines the superset of interpolation mode supported by D2D APIs
    ///  and built-in effects [D2D1_INTERPOLATION_MODE]
    /// </summary>
    public enum InterpolationMode : uint
    {
        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_NEAREST_NEIGHBOR]
        /// </summary>
        NearestNeighbor = 0,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_LINEAR]
        /// </summary>
        Linear = 1,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_CUBIC]
        /// </summary>
        Cubic = 2,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_MULTI_SAMPLE_LINEAR]
        /// </summary>
        MultiSampleLinear = 3,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_ANISOTROPIC]
        /// </summary>
        Anisotropic = 4,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_HIGH_QUALITY_CUBIC]
        /// </summary>
        HighQualityCubic = 5,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_FANT]
        /// </summary>
        Fant = 6,

        /// <summary>
        ///  [D2D1_INTERPOLATION_MODE_DEFINITION_MIPMAP_LINEAR]
        /// </summary>
        MipmapLinear = 7
    }
}
