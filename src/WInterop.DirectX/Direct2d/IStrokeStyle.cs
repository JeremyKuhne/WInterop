// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [ID2D1StrokeStyle]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1StrokeStyle),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStrokeStyle : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        [PreserveSig]
        CapStyle GetStartCap();

        [PreserveSig]
        CapStyle GetEndCap();

        [PreserveSig]
        CapStyle GetDashCap();

        [PreserveSig]
        float GetMiterLimit();

        [PreserveSig]
        LineJoin GetLineJoin();

        [PreserveSig]
        float GetDashOffset();

        [PreserveSig]
        DashStyle GetDashStyle();

        [PreserveSig]
        uint GetDashesCount();

        /// <summary>
        ///  Returns the dashes from the object into a user allocated array. The user must
        ///  call GetDashesCount to retrieve the required size.
        /// </summary>
        [PreserveSig]
        unsafe void GetDashes(
            float* dashes,
            uint dashesCount);
    }
}
