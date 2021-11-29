// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Indicates whether the figure is open or closed on its end point. [D2D1_FIGURE_END]
    /// </summary>
    public enum FigureEnd : uint
    {
        /// <summary>
        ///  [D2D1_FIGURE_END_OPEN]
        /// </summary>
        Open = D2D1_FIGURE_END.D2D1_FIGURE_END_OPEN,

        /// <summary>
        ///  [D2D1_FIGURE_END_CLOSED]
        /// </summary>
        Closed = D2D1_FIGURE_END.D2D1_FIGURE_END_CLOSED
    }
}
