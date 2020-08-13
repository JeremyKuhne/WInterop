// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Enum which describes the drawing of the corners on the line. [D2D1_LINE_JOIN]
    /// </summary>
    public enum LineJoin : uint
    {
        /// <summary>
        ///  Miter join. [D2D1_LINE_JOIN_MITER]
        /// </summary>
        Miter = 0,

        /// <summary>
        ///  Bevel join. [D2D1_LINE_JOIN_BEVEL]
        /// </summary>
        Bevel = 1,

        /// <summary>
        ///  Round join. [D2D1_LINE_JOIN_ROUND]
        /// </summary>
        Round = 2,

        /// <summary>
        ///  Miter/Bevel join. [D2D1_LINE_JOIN_MITER_OR_BEVEL]
        /// </summary>
        MiterOrBevel = 3
    }
}
