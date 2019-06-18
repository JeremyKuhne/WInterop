// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// A blend mode that applies to all primitives drawn on the context. [D2D1_PRIMITIVE_BLEND]
    /// </summary>
    public enum PrimitiveBlend : uint
    {
        SourceOver = 0,
        Copy = 1,
        Min = 2,
        Add = 3,
        Max = 4,
    }
}
