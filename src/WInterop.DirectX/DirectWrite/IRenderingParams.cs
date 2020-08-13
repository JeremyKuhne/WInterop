// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The interface that represents text rendering settings for glyph rasterization and filtering.
    ///  [IDWriteRenderingParams]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteRenderingParams),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRenderingParams
    {
        /// <summary>
        ///  Gets the gamma value used for gamma correction. Valid values must be
        ///  greater than zero and cannot exceed 256.
        /// </summary>
        [PreserveSig]
        float GetGamma();

        /// <summary>
        ///  Gets the amount of contrast enhancement. Valid values are greater than
        ///  or equal to zero.
        /// </summary>
        [PreserveSig]
        float GetEnhancedContrast();

        /// <summary>
        ///  Gets the ClearType level. Valid values range from 0.0f (no ClearType) 
        ///  to 1.0f (full ClearType).
        /// </summary>
        [PreserveSig]
        float GetClearTypeLevel();

        /// <summary>
        ///  Gets the pixel geometry.
        /// </summary>
        [PreserveSig]
        PixelGeometry GetPixelGeometry();

        /// <summary>
        ///  Gets the rendering mode.
        /// </summary>
        [PreserveSig]
        RenderingMode GetRenderingMode();
    }
}
