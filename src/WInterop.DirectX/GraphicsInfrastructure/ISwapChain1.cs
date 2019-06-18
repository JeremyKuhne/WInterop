// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Direct2d;
using WInterop.Windows.Native;

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [IDXGISwapChain1]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDXGISwapChain1),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISwapChain1 : ISubObject // Actually derives from ISwapChain
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

        #region IDXGISwapChain // "310d36a0-d2e7-4c0a-aa04-6a9d23b8886a"
        void Present(
            uint SyncInterval,
            uint Flags);

        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetBuffer(
            uint Buffer,
            in Guid riid);

        void SetFullscreenState(
            BOOL Fullscreen,
            // IDXGIOutput
            IntPtr pTarget);

        // Return is IDXGIOutput
        IntPtr GetFullscreenState(BOOL pFullscreen);

        void GetDescSTUB();
        //void GetDesc(
        //    /* [annotation][out] */
        //    _Out_ DXGI_SWAP_CHAIN_DESC *pDesc) = 0;

        void ResizeBuffers(
            uint BufferCount,
            uint Width,
            uint Height,
            Format NewFormat,
            uint SwapChainFlags);

        void ResizeTargetSTUB();
        //virtual HRESULT STDMETHODCALLTYPE ResizeTarget(
        //    /* [annotation][in] */
        //    _In_  const DXGI_MODE_DESC* pNewTargetParameters) = 0;

        // Return is IDXGIOutput
        IntPtr GetContainingOutput();

        void GetFrameStatisticsSTUB();
        //virtual HRESULT STDMETHODCALLTYPE GetFrameStatistics(
        //    /* [annotation][out] */
        //    _Out_ DXGI_FRAME_STATISTICS *pStats) = 0;

        uint GetLastPresentCount();
        #endregion


        void GetDesc1STUB();
        //virtual HRESULT STDMETHODCALLTYPE GetDesc1(
        //    /* [annotation][out] */
        //    _Out_ DXGI_SWAP_CHAIN_DESC1 *pDesc) = 0;

        void GetFullscreenDescSTUB();
        //virtual HRESULT STDMETHODCALLTYPE GetFullscreenDesc(
        //    /* [annotation][out] */
        //    _Out_ DXGI_SWAP_CHAIN_FULLSCREEN_DESC *pDesc) = 0;

        HWND GetHwnd();

        // Return is IUnknown
        IntPtr GetCoreWindow(in Guid refiid);

        void Present1STUB();
        //virtual HRESULT STDMETHODCALLTYPE Present1(
        //    /* [in] */ UINT SyncInterval,
        //    /* [in] */ UINT PresentFlags,
        //    /* [annotation][in] */
        //    _In_  const DXGI_PRESENT_PARAMETERS* pPresentParameters) = 0;

        [PreserveSig]
        BOOL IsTemporaryMonoSupported();

        // Return is IDXGIOutput
        IntPtr GetRestrictToOutput();

        void SetBackgroundColor(in ColorF pColor);

        ColorF GetBackgroundColor();

        void SetRotation(ModeRotation Rotation);

        ModeRotation GetRotation();
    }
}
