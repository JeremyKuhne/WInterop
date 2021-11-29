// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Specifies the algorithm that is used when images are scaled or rotated. Note
///  Starting in Windows 8, more interpolations modes are available. See
///  D2D1_INTERPOLATION_MODE for more info. [D2D1_BITMAP_INTERPOLATION_MODE]
/// </summary>
public enum BitmapInterpolationMode : uint
{

    /// <summary>
    ///  Nearest Neighbor filtering. Also known as nearest pixel or nearest point
    ///  sampling. [D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR]
    /// </summary>
    NearestNeighbor = InterpolationMode.NearestNeighbor,

    /// <summary>
    ///  Linear filtering. [D2D1_BITMAP_INTERPOLATION_MODE_LINEAR]
    /// </summary>
    Linear = InterpolationMode.Linear,
}
