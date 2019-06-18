// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Qualifies how alpha is to be treated in a bitmap or render target containing
    /// alpha. [D2D1_ALPHA_MODE] [DXGI_ALPHA_MODE]
    /// </summary>
    public enum AlphaMode : uint
    {
        /// <summary>
        /// Alpha mode should be determined implicitly. Some target surfaces do not supply
        /// or imply this information in which case alpha must be specified.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Treat the alpha as premultipled.
        /// </summary>
        Premultiplied = 1,

        /// <summary>
        /// Opacity is in the 'A' component only.
        /// </summary>
        Straight = 2,

        /// <summary>
        /// Ignore any alpha channel information.
        /// </summary>
        Ignore = 3,
    }
}
