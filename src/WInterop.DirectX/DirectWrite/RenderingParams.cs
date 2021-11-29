// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The interface that represents text rendering settings for glyph rasterization and filtering.
///  [IDWriteRenderingParams]
/// </summary>
[Guid(InterfaceIds.IID_IDWriteRenderingParams)]
public readonly unsafe struct RenderingParams : RenderingParams.Interface, IDisposable
{
    internal IDWriteRenderingParams* Handle { get; }

    internal RenderingParams(IDWriteRenderingParams* handle) => Handle = handle;

    public float Gamma => Handle->GetGamma();

    public float EnhancedContrast => Handle->GetEnhancedContrast();

    public float ClearTypeLevel => Handle->GetClearTypeLevel();

    public PixelGeometry PixelGeometry => (PixelGeometry)Handle->GetPixelGeometry();

    public RenderingMode RenderingMode => (RenderingMode)Handle->GetRenderingMode();

    public void Dispose() => Handle->Release();

    internal interface Interface
    {
        /// <summary>
        ///  Gets the gamma value used for gamma correction. Valid values must be
        ///  greater than zero and cannot exceed 256.
        /// </summary>
        float Gamma { get; }

        /// <summary>
        ///  Gets the amount of contrast enhancement. Valid values are greater than
        ///  or equal to zero.
        /// </summary>
        float EnhancedContrast { get; }

        /// <summary>
        ///  Gets the ClearType level. Valid values range from 0.0f (no ClearType) 
        ///  to 1.0f (full ClearType).
        /// </summary>
        float ClearTypeLevel { get; }

        /// <summary>
        ///  Gets the pixel geometry.
        /// </summary>
        PixelGeometry PixelGeometry { get; }

        /// <summary>
        ///  Gets the rendering mode.
        /// </summary>
        RenderingMode RenderingMode { get; }
    }
}
