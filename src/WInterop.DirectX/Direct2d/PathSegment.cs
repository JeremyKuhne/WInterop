// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Indicates whether the given segment should be stroked, or, if the join between
    ///  this segment and the previous one should be smooth. [D2D1_PATH_SEGMENT]
    /// </summary>
    [Flags]
    public enum PathSegment : uint
    {
        /// <summary>
        ///  [D2D1_PATH_SEGMENT_NONE]
        /// </summary>
        None = 0x00000000,

        /// <summary>
        ///  [D2D1_PATH_SEGMENT_FORCE_UNSTROKED]
        /// </summary>
        ForceUnstroked = 0x00000001,

        /// <summary>
        ///  [D2D1_PATH_SEGMENT_FORCE_ROUND_LINE_JOIN]
        /// </summary>
        ForceRoundLineJoin = 0x00000002
    }
}
