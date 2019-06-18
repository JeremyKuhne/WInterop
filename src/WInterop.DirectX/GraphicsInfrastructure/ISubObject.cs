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
    /// [IDXGIDeviceSubObject]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDXGIDeviceSubObject),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISubObject : IObject
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

        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetDevice(in Guid riid);
    }
}
