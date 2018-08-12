// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.DirectWrite;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Represents the drawing state of a render target: the antialiasing mode,
    /// transform, tags, and text-rendering options. [ID2D1DrawingStateBlock]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1DrawingStateBlock),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDrawingStateBlock : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        /// Retrieves the state currently contained within this state block resource.
        /// </summary>
        [PreserveSig]
        void GetDescription(
            out DrawingStateDescription stateDescription);

        /// <summary>
        /// Sets the state description of this state block resource.
        /// </summary>
        [PreserveSig]
        void SetDescription(
            in DrawingStateDescription stateDescription);

        /// <summary>
        /// Sets the text rendering parameters of this state block resource.
        /// </summary>
        [PreserveSig]
        void SetTextRenderingParams(
            IRenderingParams textRenderingParams);

        /// <summary>
        /// Retrieves the text rendering parameters contained within this state block
        /// resource. If a NULL text rendering parameter was specified, NULL will be
        /// returned.
        /// </summary>
        [PreserveSig]
        void GetTextRenderingParams(
            out IRenderingParams textRenderingParams);
    }
}
