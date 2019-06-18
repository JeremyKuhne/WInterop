// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Provides a brush that can take any effect, command list or bitmap and use it to
    /// fill a 2D shape. [ID2D1ImageBrush]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1ImageBrush),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IImageBrush : IBrush
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        #region ID2D1Brush
        /// <summary>
        /// Sets the opacity for when the brush is drawn over the entire fill of the brush.
        /// </summary>
        [PreserveSig]
        new void SetOpacity(
            float opacity);

        /// <summary>
        /// Sets the transform that applies to everything drawn by the brush.
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

        [PreserveSig]
        void SetImage(IImage image);

        [PreserveSig]
        void SetExtendModeX(ExtendMode extendModeX);

        [PreserveSig]
        void SetExtendModeY(ExtendMode extendModeY);

        [PreserveSig]
        void SetInterpolationMode(InterpolationMode interpolationMode);

        [PreserveSig]
        void SetSourceRectangle(in LtrbRectangleF sourceRectangle);

        [PreserveSig]
        void GetImage(out IImage image);

        [PreserveSig]
        ExtendMode GetExtendModeX();

        [PreserveSig]
        ExtendMode GetExtendModeY();

        [PreserveSig]
        InterpolationMode GetInterpolationMode();

        [PreserveSig]
        void GetSourceRectangle(out LtrbRectangleF sourceRectangle);
    }
}
