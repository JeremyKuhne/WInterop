// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Defines the direction that an elliptical arc is drawn. [D2D1_SWEEP_DIRECTION]
    /// </summary>
    public enum SweepDirection : uint
    {
        /// <summary>
        /// [D2D1_SWEEP_DIRECTION_COUNTER_CLOCKWISE]
        /// </summary>
        CounterClockwise = 0,

        /// <summary>
        /// [D2D1_SWEEP_DIRECTION_CLOCKWISE]
        /// </summary>
        Clockwise = 1
    }
}
