// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Security;
using WInterop.Windows.Native;

namespace WInterop.GraphicsInfrastructure
{
    // IDXGIFactory2
    [ComImport,
        Guid(InterfaceIds.IID_IDXGIFactory2),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFactory2 : IObject
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

        #region IID_IDXGIFactory
        void EnumAdaptersSTUB();
        //virtual HRESULT STDMETHODCALLTYPE EnumAdapters(
        //    /* [in] */ UINT Adapter,
        //    /* [annotation][out] */
        //    _COM_Outptr_ IDXGIAdapter **ppAdapter) = 0;

        void MakeWindowAssociation(
            HWND WindowHandle,
            uint Flags);

        HWND GetWindowAssociation();

        void CreateSwapChainSTUB();
        //virtual HRESULT STDMETHODCALLTYPE CreateSwapChain(
        //    /* [annotation][in] */
        //    _In_ IUnknown *pDevice,
        //    /* [annotation][in] */
        //    _In_ DXGI_SWAP_CHAIN_DESC *pDesc,
        //    /* [annotation][out] */
        //    _COM_Outptr_ IDXGISwapChain **ppSwapChain) = 0;

        void CreateSoftwareAdapterSTUB();
        //virtual HRESULT STDMETHODCALLTYPE CreateSoftwareAdapter(
        //    /* [in] */ HMODULE Module,
        //    /* [annotation][out] */
        //    _COM_Outptr_ IDXGIAdapter **ppAdapter) = 0;
        #endregion

        #region IID_IDXGIFactory1
        void EnumAdapters1STUB();
        //virtual HRESULT STDMETHODCALLTYPE EnumAdapters1(
        //    /* [in] */ UINT Adapter,
        //    /* [annotation][out] */
        //    _COM_Outptr_ IDXGIAdapter1 **ppAdapter) = 0;

        [PreserveSig]
        BOOL IsCurrent();
        #endregion

        [PreserveSig]
        BOOL IsWindowedStereoEnabled();

        ISwapChain1 CreateSwapChainForHwnd(
            [MarshalAs(UnmanagedType.IUnknown)]
            object pDevice,
            HWND hWnd,
            in SwapChainDescriptor1 pDesc,
            IntPtr pFullscreenDesc = default,     // in DXGI_SWAP_CHAIN_FULLSCREEN_DESC
            IntPtr pRestrictToOutput = default);  // IDXGIOutput

        ISwapChain1 CreateSwapChainForCoreWindow(
            [MarshalAs(UnmanagedType.IUnknown)]
            object pDevice,
            [MarshalAs(UnmanagedType.IUnknown)]
            object pWindow,
            in SwapChainDescriptor1 pDesc,
            IntPtr pFullscreenDesc,     // in DXGI_SWAP_CHAIN_FULLSCREEN_DESC
            IntPtr pRestrictToOutput);  // IDXGIOutput

        LUID GetSharedResourceAdapterLuid(IntPtr hResource);

        uint RegisterStereoStatusWindow(
            HWND WindowHandle,
            uint wMsg);

        uint RegisterStereoStatusEvent(IntPtr hEvent);

        [PreserveSig]
        void UnregisterStereoStatus(uint dwCookie);

        uint RegisterOcclusionStatusWindow(
            HWND WindowHandle,
            uint wMsg);

        uint RegisterOcclusionStatusEvent(IntPtr hEvent);

        [PreserveSig]
        void UnregisterOcclusionStatus(uint dwCookie);

        ISwapChain1 CreateSwapChainForComposition(
            [MarshalAs(UnmanagedType.IUnknown)]
            object pDevice,
            in SwapChainDescriptor1 pDesc,
            // IDXGIOutput
            IntPtr pRestrictToOutput);
    }
}
