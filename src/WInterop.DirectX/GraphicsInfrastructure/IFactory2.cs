// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Windows.Native;

namespace WInterop.GraphicsInfrastructure
{
    // IDXGIFactory2
    [ComImport,
        Guid(InterfaceIds.IID_IDXGIFactory2),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFactory2
    {
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
            in SwapChainDescription1 pDesc,
            IntPtr pFullscreenDesc,     // in DXGI_SWAP_CHAIN_FULLSCREEN_DESC
            IntPtr pRestrictToOutput);  // IDXGIOutput

        void CreateSwapChainForCoreWindowSTUB();
        //virtual HRESULT STDMETHODCALLTYPE CreateSwapChainForCoreWindow(
        //    /* [annotation][in] */
        //    _In_ IUnknown *pDevice,
        //    /* [annotation][in] */
        //    _In_ IUnknown *pWindow,
        //    /* [annotation][in] */
        //    _In_  const DXGI_SWAP_CHAIN_DESC1* pDesc,
        //    /* [annotation][in] */
        //    _In_opt_  IDXGIOutput* pRestrictToOutput,
        //    /* [annotation][out] */
        //    _COM_Outptr_  IDXGISwapChain1** ppSwapChain) = 0;

        void GetSharedResourceAdapterLuidSTUB();
        //virtual HRESULT STDMETHODCALLTYPE GetSharedResourceAdapterLuid(
        //    /* [annotation] */
        //    _In_ HANDLE hResource,
        //    /* [annotation] */
        //    _Out_ LUID *pLuid) = 0;

        void RegisterStereoStatusWindowSTUB();
        //virtual HRESULT STDMETHODCALLTYPE RegisterStereoStatusWindow(
        //    /* [annotation][in] */
        //    _In_ HWND WindowHandle,
        //    /* [annotation][in] */
        //    _In_ UINT wMsg,
        //    /* [annotation][out] */
        //    _Out_ DWORD *pdwCookie) = 0;

        void RegisterStereoStatusEventSTUB();
        //virtual HRESULT STDMETHODCALLTYPE RegisterStereoStatusEvent(
        //    /* [annotation][in] */
        //    _In_ HANDLE hEvent,
        //    /* [annotation][out] */
        //    _Out_ DWORD *pdwCookie) = 0;

        void UnregisterStereoStatusSTUB();
        //virtual void STDMETHODCALLTYPE UnregisterStereoStatus(
        //    /* [annotation][in] */
        //    _In_ DWORD dwCookie) = 0;

        void RegisterOcclusionStatusWindowSTUB();
        //virtual HRESULT STDMETHODCALLTYPE RegisterOcclusionStatusWindow(
        //    /* [annotation][in] */
        //    _In_ HWND WindowHandle,
        //    /* [annotation][in] */
        //    _In_ UINT wMsg,
        //    /* [annotation][out] */
        //    _Out_ DWORD *pdwCookie) = 0;

        void RegisterOcclusionStatusEventSTUB();
        //virtual HRESULT STDMETHODCALLTYPE RegisterOcclusionStatusEvent(
        //    /* [annotation][in] */
        //    _In_ HANDLE hEvent,
        //    /* [annotation][out] */
        //    _Out_ DWORD *pdwCookie) = 0;

        void UnregisterOcclusionStatusSTUB();
        //virtual void STDMETHODCALLTYPE UnregisterOcclusionStatus(
        //    /* [annotation][in] */
        //    _In_ DWORD dwCookie) = 0;

        void CreateSwapChainForCompositionSTUB();
        //virtual HRESULT STDMETHODCALLTYPE CreateSwapChainForComposition(
        //    /* [annotation][in] */
        //    _In_ IUnknown *pDevice,
        //    /* [annotation][in] */
        //    _In_  const DXGI_SWAP_CHAIN_DESC1* pDesc,
        //    /* [annotation][in] */
        //    _In_opt_  IDXGIOutput* pRestrictToOutput,
        //    /* [annotation][out] */
        //    _COM_Outptr_  IDXGISwapChain1** ppSwapChain) = 0;
    }
}
