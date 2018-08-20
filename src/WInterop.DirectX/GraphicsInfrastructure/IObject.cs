// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.GraphicsInfrastructure
{
    [ComImport,
        Guid(InterfaceIds.IID_IDXGIObject),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObject
    {
        unsafe void SetPrivateData(
            in Guid Name,
            uint DataSize,
            void* pData);

        void SetPrivateDataInterface(
            in Guid Name,
            [MarshalAs(UnmanagedType.IUnknown)]
            object pUnknown);

        unsafe void GetPrivateData(
            in Guid Name,
            ref uint pDataSize,
            void* pData);

        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetParent(in Guid riid);
    }
}
