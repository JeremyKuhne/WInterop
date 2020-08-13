// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Differentiates which of the two possible arcs could match the given arc
    ///  parameters. [D2D1_ARC_SIZE]
    /// </summary>
    public enum ArcSize : uint
    {
        /// <summary>
        ///  [D2D1_ARC_SIZE_SMALL]
        /// </summary>
        Small = 0,

        /// <summary>
        ///  [D2D1_ARC_SIZE_LARGE]
        /// </summary>
        Large = 1,
    }
}
