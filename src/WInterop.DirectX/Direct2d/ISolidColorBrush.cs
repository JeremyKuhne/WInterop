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
    /// Paints an area with a solid color. [ID2D1SolidColorBrush]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1SolidColorBrush),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISolidColorBrush : IBrush
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
        void SetColor(
            in ColorF color);

        [PreserveSig]
        void GetColor(
            out ColorF color);
    }
}
