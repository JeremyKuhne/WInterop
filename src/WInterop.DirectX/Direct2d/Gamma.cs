// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  This determines what gamma is used for interpolation/blending.
    /// </summary>
    public enum Gamma : uint
    {
        /// <summary>
        ///  Colors are manipulated in 2.2 gamma color space. [D2D1_GAMMA_2_2]
        /// </summary>
        ColorSpace_2_2 = D2D1_GAMMA.D2D1_GAMMA_2_2,

        /// <summary>
        ///  Colors are manipulated in 1.0 gamma color space. [D2D1_GAMMA_1_0]
        /// </summary>
        ColorSpace_1_0 = D2D1_GAMMA.D2D1_GAMMA_1_0,
    }
}
