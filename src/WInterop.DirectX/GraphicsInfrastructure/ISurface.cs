// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [IDXGISurface]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDXGISurface),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISurface : ISubObject
    {
        #region IDXGIObject
        new unsafe void SetPrivateData(
            in Guid Name,
            uint DataSize,
            void* pData);

        new void SetPrivateDataInterface(
            in Guid Name,
            [MarshalAs(UnmanagedType.IUnknown)]
            object pUnknown);

        new unsafe void GetPrivateData(
            in Guid Name,
            ref uint pDataSize,
            void* pData);

        [return: MarshalAs(UnmanagedType.IUnknown)]
        new object GetParent(in Guid riid);
        #endregion

        #region IDXGIDeviceSubObject
        [return: MarshalAs(UnmanagedType.IUnknown)]
        new object GetDevice(in Guid riid);
        #endregion

        SurfaceDescriptor GetDesc();

        unsafe void Map(
            Direct2d.MappedRectangle* pLockedRect,
            uint MapFlags);

        void Unmap();
    }
}
