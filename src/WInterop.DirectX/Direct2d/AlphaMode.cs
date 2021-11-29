// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Qualifies how alpha is to be treated in a bitmap or render target containing
    ///  alpha. [D2D1_ALPHA_MODE]
    /// </summary>
    public enum AlphaMode : uint
    {
        /// <summary>
        ///  Alpha mode should be determined implicitly. Some target surfaces do not supply
        ///  or imply this information in which case alpha must be specified.
        ///  [D2D1_ALPHA_MODE_UNKNOWN]
        /// </summary>
        Unknown = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_UNKNOWN,

        /// <summary>
        ///  Treat the alpha as premultipled. [D2D1_ALPHA_MODE_PREMULTIPLIED]
        /// </summary>
        Premultiplied = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED,

        /// <summary>
        ///  Opacity is in the 'A' component only. [D2D1_ALPHA_MODE_STRAIGHT]
        /// </summary>
        Straight = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_STRAIGHT,

        /// <summary>
        ///  Ignore any alpha channel information. [D2D1_ALPHA_MODE_IGNORE]
        /// </summary>
        Ignore = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_IGNORE,
    }
}
