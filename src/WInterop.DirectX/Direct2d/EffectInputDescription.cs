// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    /// This identifies a certain input connection of a certain effect. [D2D1_EFFECT_INPUT_DESCRIPTION]
    /// </summary>
    public readonly struct EffectInputDescription
    {
        /// <summary>
        /// The effect whose input connection is being specified.
        /// </summary>
        /// <remarks>
        /// This is an ID2D1Effect*.
        /// </remarks>
        public readonly IntPtr Effect;

        /// <summary>
        /// The index of the input connection into the specified effect.
        /// </summary>
        public readonly uint InputIndex;

        /// <summary>
        /// The rectangle which would be available on the specified input connection during
        /// render operations.
        /// </summary>
        public readonly LtrbRectangleF InputRectangle;
    }
}
