// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.GraphicsInfrastructure;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Root bitmap resource, linearly scaled on a draw call. [ID2D1Bitmap1]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Bitmap1),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBitmap1 : IBitmap
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        #region ID2D1Bitmap
        /// <summary>
        /// Returns the size of the bitmap in resolution independent units.
        /// </summary>
        [PreserveSig]
        new void GetSize(out SizeF size);

        /// <summary>
        /// Returns the size of the bitmap in resolution dependent units, (pixels).
        /// </summary>
        [PreserveSig]
        new void GetPixelSize(out SizeU size);

        /// <summary>
        /// Retrieve the format of the bitmap.
        /// </summary>
        [PreserveSig]
        new PixelFormat GetPixelFormat();

        /// <summary>
        /// Return the DPI of the bitmap.
        /// </summary>
        [PreserveSig]
        new void GetDpi(
            out float dpiX,
            out float dpiY);

        new unsafe void CopyFromBitmap(
            PointU* destPoint,
            IBitmap bitmap,
            LtrbRectangleU* srcRect);

        new unsafe void CopyFromRenderTarget(
            PointU* destPoint,
            IRenderTarget renderTarget,
            LtrbRectangleU* srcRect);

        new unsafe void CopyFromMemory(
            LtrbRectangleU* dstRect,
            void* srcData,
            uint pitch);
        #endregion

        /// <summary>
        /// Retrieves the color context information associated with the bitmap.
        /// </summary>
        /// <param name="colorContext">ID2D1ColorContext, can be null.</param>
        [PreserveSig]
        void GetColorContext(out IntPtr colorContext);

        /// <summary>
        /// Retrieves the bitmap options used when creating the API.
        /// </summary>
        BitmapOptions GetOptions();

        /// <summary>
        /// Retrieves the DXGI surface from the corresponding bitmap, if the bitmap was
        /// created from a device derived from a D3D device.
        /// </summary>
        ISurface GetSurface();

        /// <summary>
        /// Maps the given bitmap into memory. The bitmap must have been created with the
        /// <see cref="BitmapOptions.CpuRead"/> flag.
        /// </summary>
        MappedRectangle Map(MapOptions options);

        /// <summary>
        /// Unmaps the given bitmap from memory.
        /// </summary>
        void Unmap();
    }
}
