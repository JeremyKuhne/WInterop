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
    /// [IDXGIDevice]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDXGIDevice),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDevice : IObject
    {
        #region IID_IDXGIObject
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
        #endregion

        // Return is IDXGIAdapter
        IObject GetAdapter();

        void CreateSurfaceSTUB();
        //virtual HRESULT STDMETHODCALLTYPE CreateSurface(
        //    /* [annotation][in] */
        //    _In_  const DXGI_SURFACE_DESC* pDesc,
        //    /* [in] */ UINT NumSurfaces,
        //    /* [in] */ DXGI_USAGE Usage,
        //    /* [annotation][in] */
        //    _In_opt_  const DXGI_SHARED_RESOURCE* pSharedResource,
        //    /* [annotation][out] */
        //    _COM_Outptr_  IDXGISurface** ppSurface) = 0;

        void QueryResourceResidencySTUB();
        //virtual HRESULT STDMETHODCALLTYPE QueryResourceResidency(
        //    /* [annotation][size_is][in] */
        //    _In_reads_(NumResources) IUnknown *const * ppResources,
        //   /* [annotation][size_is][out] */
        //   _Out_writes_(NumResources)  DXGI_RESIDENCY* pResidencyStatus,
        //   /* [in] */ UINT NumResources) = 0;

        void SetGPUThreadPriority(int Priority);

        int GetGPUThreadPriority();
    }
}
