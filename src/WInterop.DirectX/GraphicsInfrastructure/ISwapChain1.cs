using System;
using System.Collections.Generic;
using System.Text;

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [IDXGISwapChain1]
    /// </summary>
    public interface ISwapChain1
    {

        MIDL_INTERFACE("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a")
    IDXGISwapChain : public IDXGIDeviceSubObject
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Present(
            /* [in] */ UINT SyncInterval,
            /* [in] */ UINT Flags) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetBuffer(
            /* [in] */ UINT Buffer,
            /* [annotation][in] */
            _In_ REFIID riid,
            /* [annotation][out][in] */
            _COM_Outptr_  void** ppSurface) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetFullscreenState(
            /* [in] */ BOOL Fullscreen,
            /* [annotation][in] */
            _In_opt_ IDXGIOutput *pTarget) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetFullscreenState(
            /* [annotation][out] */
            _Out_opt_ BOOL *pFullscreen,
            /* [annotation][out] */
            _COM_Outptr_opt_result_maybenull_ IDXGIOutput **ppTarget) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetDesc(
            /* [annotation][out] */
            _Out_ DXGI_SWAP_CHAIN_DESC *pDesc) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE ResizeBuffers(
            /* [in] */ UINT BufferCount,
            /* [in] */ UINT Width,
            /* [in] */ UINT Height,
            /* [in] */ DXGI_FORMAT NewFormat,
            /* [in] */ UINT SwapChainFlags) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE ResizeTarget(
            /* [annotation][in] */
            _In_  const DXGI_MODE_DESC* pNewTargetParameters) = 0;

        virtual HRESULT STDMETHODCALLTYPE GetContainingOutput(
            /* [annotation][out] */
            _COM_Outptr_ IDXGIOutput **ppOutput) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetFrameStatistics(
            /* [annotation][out] */
            _Out_ DXGI_FRAME_STATISTICS *pStats) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetLastPresentCount(
            /* [annotation][out] */
            _Out_ UINT *pLastPresentCount) = 0;
        
    };

    MIDL_INTERFACE("790a45f7-0d42-4876-983a-0a55cfe6f4aa")
    IDXGISwapChain1 : public IDXGISwapChain
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetDesc1(
            /* [annotation][out] */
            _Out_ DXGI_SWAP_CHAIN_DESC1 *pDesc) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetFullscreenDesc(
            /* [annotation][out] */
            _Out_ DXGI_SWAP_CHAIN_FULLSCREEN_DESC *pDesc) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetHwnd(
            /* [annotation][out] */
            _Out_ HWND *pHwnd) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCoreWindow(
            /* [annotation][in] */
            _In_ REFIID refiid,
            /* [annotation][out] */
            _COM_Outptr_  void** ppUnk) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Present1(
            /* [in] */ UINT SyncInterval,
            /* [in] */ UINT PresentFlags,
            /* [annotation][in] */
            _In_  const DXGI_PRESENT_PARAMETERS* pPresentParameters) = 0;

        virtual BOOL STDMETHODCALLTYPE IsTemporaryMonoSupported(void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetRestrictToOutput(
            /* [annotation][out] */
            _Out_ IDXGIOutput **ppRestrictToOutput) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetBackgroundColor(
            /* [annotation][in] */
            _In_  const DXGI_RGBA* pColor) = 0;

        virtual HRESULT STDMETHODCALLTYPE GetBackgroundColor(
            /* [annotation][out] */
            _Out_ DXGI_RGBA *pColor) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetRotation(
            /* [annotation][in] */
            _In_ DXGI_MODE_ROTATION Rotation) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetRotation(
            /* [annotation][out] */
            _Out_ DXGI_MODE_ROTATION *pRotation) = 0;
        
    };
}
}
