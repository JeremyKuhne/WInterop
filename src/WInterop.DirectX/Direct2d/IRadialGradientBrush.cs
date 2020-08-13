// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Paints an area with a radial gradient. [ID2D1RadialGradientBrush]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1RadialGradientBrush),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRadialGradientBrush : IBrush
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
        ///  Sets the center of the radial gradient. This will be in local coordinates and
        ///  will not depend on the geometry being filled.
        /// </summary>
        [PreserveSig]
        void SetCenter(PointF center);

        /// <summary>
        ///  Sets offset of the origin relative to the radial gradient center.
        /// </summary>
        [PreserveSig]
        void SetGradientOriginOffset(PointF gradientOriginOffset);

        [PreserveSig]
        void SetRadiusX(float radiusX);

        [PreserveSig]
        void SetRadiusY(float radiusY);

        // TODO: Bug in COM interop, should return PointF
        [PreserveSig]
        void GetCenter(out PointF center);

        // TODO: Bug in COM interop, should return PointF
        [PreserveSig]
        void GetGradientOriginOffset(out PointF offset);

        [PreserveSig]
        float GetRadiusX();

        [PreserveSig]
        float GetRadiusY();

        [PreserveSig]
        void GetGradientStopCollection(out IGradientStopCollection gradientStopCollection);
    }
}
