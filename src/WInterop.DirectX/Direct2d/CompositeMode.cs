// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Specifies the composite mode that will be applied. [D2D1_COMPOSITE_MODE]
    /// </summary>
    public enum CompositeMode : uint
    {
        SourceOver = 0,
        DestinationOver = 1,
        SourceIn = 2,
        DestinationIn = 3,
        SourceOut = 4,
        DestinationOut = 5,
        SourceAtop = 6,
        DestinationAtop = 7,
        Xor = 8,
        Plus = 9,
        SourceCopy = 10,
        BoundedSourceCopy = 11,
        MaskInvert = 12,
    }
}
