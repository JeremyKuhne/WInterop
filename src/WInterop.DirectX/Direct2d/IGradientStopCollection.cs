// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Represents an collection of gradient stops that can then be the source resource
    ///  for either a linear or radial gradient brush. [ID2D1GradientStopCollection]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1GradientStopCollection),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGradientStopCollection : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        ///  Returns the number of stops in the gradient.
        /// </summary>
        [PreserveSig]
        uint GetGradientStopCount();

        /// <summary>
        ///  Copies the gradient stops from the collection into the caller's interface.  The
        ///  returned colors have straight alpha.
        /// </summary>
        [PreserveSig]
        unsafe void GetGradientStops(
            GradientStop* gradientStops,
            uint gradientStopsCount);

        /// <summary>
        ///  Returns whether the interpolation occurs with 1.0 or 2.2 gamma.
        /// </summary>
        [PreserveSig]
        Gamma GetColorInterpolationGamma();

        [PreserveSig]
        ExtendMode GetExtendMode();
    }
}
