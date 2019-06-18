// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Specifies how the bitmap can be used. [D2D1_BITMAP_OPTIONS]
    /// </summary>
    [Flags]
    public enum BitmapOptions : uint
    {
        /// <summary>
        /// The bitmap is created with default properties.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// The bitmap can be specified as a target in <see cref="IDeviceContext.SetTarget"/>.
        /// </summary>
        Target = 0x00000001,

        /// <summary>
        /// The bitmap cannot be used as an input to DrawBitmap, DrawImage, in a bitmap
        /// brush or as an input to an effect.
        /// </summary>
        CannotDraw = 0x00000002,

        /// <summary>
        /// The bitmap can be read from the CPU.
        /// </summary>
        CpuRead = 0x00000004,

        /// <summary>
        /// The bitmap works with the ID2D1GdiInteropRenderTarget::GetDC API.
        /// </summary>
        GdiCompatible = 0x00000008,
    }
}
