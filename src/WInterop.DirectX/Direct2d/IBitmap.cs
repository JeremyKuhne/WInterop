// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Root bitmap resource, linearly scaled on a draw call. [ID2D1Bitmap]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Bitmap),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBitmap : IImage
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        /// Returns the size of the bitmap in resolution independent units.
        /// </summary>
        [PreserveSig]
        void GetSize(out SizeF size); // https://github.com/dotnet/coreclr/issues/19474

        /// <summary>
        /// Returns the size of the bitmap in resolution dependent units, (pixels).
        /// </summary>
        [PreserveSig]
        void GetPixelSize(out SizeU size); // https://github.com/dotnet/coreclr/issues/19474

        /// <summary>
        /// Retrieve the format of the bitmap.
        /// </summary>
        [PreserveSig]
        PixelFormat GetPixelFormat();

        /// <summary>
        /// Return the DPI of the bitmap.
        /// </summary>
        [PreserveSig]
        void GetDpi(
            out float dpiX,
            out float dpiY);

        unsafe void CopyFromBitmap(
            PointU* destPoint,
            IBitmap bitmap,
            LtrbRectangleU* srcRect);

        unsafe void CopyFromRenderTarget(
            PointU* destPoint,
            IRenderTarget renderTarget,
            LtrbRectangleU* srcRect);

        unsafe void CopyFromMemory(
            LtrbRectangleU* dstRect,
            void* srcData,
            uint pitch);
    }
}
