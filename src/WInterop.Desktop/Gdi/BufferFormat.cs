// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  [BP_BUFFERFORMAT]
    /// </summary>
    public enum BufferFormat
    {
        /// <summary>
        ///  Compatible bitmap. [BPBF_COMPATIBLEBITMAP]
        /// </summary>
        CompatibleBitmap,

        /// <summary>
        ///  Device-independent bitmap. [BPBF_DIB]
        /// </summary>
        Dib,

        /// <summary>
        ///  Top-down device-independent bitmap. [BPBF_TOPDOWNDIB]
        /// </summary>
        TopDownDib,

        /// <summary>
        ///  Top-down monochrome device-independent bitmap. [BPBF_TOPDOWNMONODIB]
        /// </summary>
        TopDownMonoDib
    }
}