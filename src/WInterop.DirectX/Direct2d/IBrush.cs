// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// The root brush interface. All brushes can be used to fill or pen a geometry.
    /// [ID2D1Brush]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Brush),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBrush : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        /// Sets the opacity for when the brush is drawn over the entire fill of the brush.
        /// </summary>
        [PreserveSig]
        void SetOpacity(
            float opacity);

        /// <summary>
        /// Sets the transform that applies to everything drawn by the brush.
        /// </summary>
        [PreserveSig]
        void SetTransform(
            in Matrix3x2 transform);

        [PreserveSig]
        float GetOpacity();

        [PreserveSig]
        void GetTransform(
            out Matrix3x2 transform);
    }
}
