// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes mapped memory from the <see cref="IBitmap1.Map"/> API. [D2D1_MAPPED_RECT] [DXGI_MAPPED_RECT]
    /// </summary>
    public unsafe readonly struct MappedRectangle
    {
        public readonly uint Pitch;
        public readonly byte* Bits;
    }
}
