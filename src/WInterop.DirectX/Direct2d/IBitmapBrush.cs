// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  A bitmap brush allows a bitmap to be used to fill a geometry. [ID2D1BitmapBrush]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1BitmapBrush),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBitmapBrush : IBrush
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        #region ID2D1Brush
        /// <summary>
        ///  Sets the opacity for when the brush is drawn over the entire fill of the brush.
        /// </summary>
        [PreserveSig]
        new void SetOpacity(
            float opacity);

        /// <summary>
        ///  Sets the transform that applies to everything drawn by the brush.
        /// </summary>
        [PreserveSig]
        new void SetTransform(
            ref Matrix3x2 transform);

        [PreserveSig]
        new float GetOpacity();

        [PreserveSig]
        new void GetTransform(
            out Matrix3x2 transform);
        #endregion

        /// <summary>
        ///  Sets how the bitmap is to be treated outside of its natural extent on the X
        ///  axis.
        /// </summary>
        [PreserveSig]
        void SetExtendModeX(
            ExtendMode extendModeX);

        /// <summary>
        ///  Sets how the bitmap is to be treated outside of its natural extent on the X
        ///  axis.
        /// </summary>
        [PreserveSig]
        void SetExtendModeY(
            ExtendMode extendModeY);

        /// <summary>
        ///  Sets the interpolation mode used when this brush is used.
        /// </summary>
        [PreserveSig]
        void SetInterpolationMode(
            BitmapInterpolationMode interpolationMode);

        /// <summary>
        ///  Sets the bitmap associated as the source of this brush.
        /// </summary>
        [PreserveSig]
        void SetBitmap(
            IBitmap bitmap);

        [PreserveSig]
        ExtendMode GetExtendModeX();

        [PreserveSig]
        ExtendMode GetExtendModeY();

        [PreserveSig]
        BitmapInterpolationMode GetInterpolationMode();

        [PreserveSig]
        void GetBitmap(
            out IBitmap bitmap);
    }
}
