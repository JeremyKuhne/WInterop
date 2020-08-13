// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the minimum DirectX support required for hardware rendering by a
    ///  render target. [D2D1_FEATURE_LEVEL]
    /// </summary>
    public enum FeatureLevel : uint
    {

        /// <summary>
        ///  The caller does not require a particular underlying D3D device level.
        ///  [D2D1_FEATURE_LEVEL_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  The D3D device level is DX9 compatible. [D2D1_FEATURE_LEVEL_9]
        /// </summary>
        Level_9 = Direct3d.FeatureLevel.Level_9_1,

        /// <summary>
        ///  The D3D device level is DX10 compatible. [D2D1_FEATURE_LEVEL_10]
        /// </summary>
        Level_10 = Direct3d.FeatureLevel.Level_10_0,
    }
}
