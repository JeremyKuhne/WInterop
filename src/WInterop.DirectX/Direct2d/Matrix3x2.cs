// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Represents a 3-by-2 matrix. [D2D_MATRIX_3X2_F]
    /// </summary>
    public struct Matrix3x2
    {
        /// <summary>
        /// Horizontal scaling / cosine of rotation
        /// </summary>
        public float m11;

        /// <summary>
        /// Vertical shear / sine of rotation
        /// </summary>
        public float m12;

        /// <summary>
        /// Horizontal shear / negative sine of rotation
        /// </summary>
        public float m21;

        /// <summary>
        /// Vertical scaling / cosine of rotation
        /// </summary>
        public float m22;

        /// <summary>
        /// Horizontal shift (always orthogonal regardless of rotation)
        /// </summary>
        public float dx;

        /// <summary>
        /// Vertical shift (always orthogonal regardless of rotation)
        /// </summary>
        public float dy;
    }
}
