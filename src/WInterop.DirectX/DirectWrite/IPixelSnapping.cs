// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="IPixelSnapping"/> interface defines the pixel snapping properties of a text renderer.
    ///  [IDWritePixelSnapping]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWritePixelSnapping),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPixelSnapping
    {
        /// <summary>
        ///  Determines whether pixel snapping is disabled. The recommended default is FALSE,
        ///  unless doing animation that requires subpixel vertical placement.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        IntBoolean IsPixelSnappingDisabled(IntPtr clientDrawingContext);

        /// <summary>
        ///  Gets the current transform that maps abstract coordinates to DIPs,
        ///  which may disable pixel snapping upon any rotation or shear.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext);

        /// <summary>
        ///  Gets the number of physical pixels per DIP. A DIP (device-independent pixel) is 1/96 inch,
        ///  so the pixelsPerDip value is the number of logical pixels per inch divided by 96 (yielding
        ///  a value of 1 for 96 DPI and 1.25 for 120).
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        /// <param name="pixelsPerDip">Receives the number of physical pixels per DIP.</param>
        float GetPixelsPerDip(IntPtr clientDrawingContext);
    }
}
