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
    /// [ID2D1Factory]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/dd371246.aspx"/>
    /// </remarks>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Factory),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFactory
    {
        /// <summary>
        /// Cause the factory to refresh any system metrics that it might have been snapped
        /// on factory creation.
        /// </summary>
        void ReloadSystemMetrics();

        /// <summary>
        /// Retrieves the current desktop DPI. To refresh this, call ReloadSystemMetrics.
        /// Note: this method is deprecated. Use DisplayProperties::LogicalDpi for Windows
        /// Store Apps and GetDpiForWindow for Win32 Apps.
        /// </summary>
        [PreserveSig]
        void GetDesktopDpi(
            out float dpiX,
            out float dpiY);

        IRectangleGeometry CreateRectangleGeometry(
            in LtrbRectangleF rectangle);

        IRoundedRectangleGeometry CreateRoundedRectangleGeometry(
            in RoundedRectangle roundedRectangle);

        IEllipseGeometry CreateEllipseGeometry(
            in Ellipse ellipse);

        /// <summary>
        /// Create a geometry which holds other geometries.
        /// </summary>
        IGeometryGroup CreateGeometryGroup(
            FillMode fillMode,
            IGeometry[] geometries,
            uint geometriesCount);

        ITransformedGeometry CreateTransformedGeometry(
            IGeometry sourceGeometry,
            in Matrix3x2 transform);

        /// <summary>
        /// Returns an initially empty path geometry interface. A geometry sink is created
        /// off the interface to populate it.
        /// </summary>
        IPathGeometry CreatePathGeometry();

        /// <summary>
        /// Allows a non-default stroke style to be specified for a given geometry at draw
        /// time.
        /// </summary>
        unsafe IStrokeStyle CreateStrokeStyle(
            in StrokeStyleProperties strokeStyleProperties,
            float* dashes,
            uint dashesCount);

        /// <summary>
        /// Creates a new drawing state block, this can be used in subsequent
        /// SaveDrawingState and RestoreDrawingState operations on the render target.
        /// </summary>
        IDrawingStateBlock CreateDrawingStateBlock(
            in DrawingStateDescription drawingStateDescription,
            IRenderingParams textRenderingParams);

        void Slot3();
        void Slot4();
        void Slot5();
        void Slot6();


/*
    

    

    
    /// <summary>
    /// Creates a render target which is a source of bitmaps.
    /// </summary>
    STDMETHOD(CreateWicBitmapRenderTarget)(
        _In_ IWICBitmap * target,
        _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        _COM_Outptr_ ID2D1RenderTarget **renderTarget 
        ) PURE;
    
    /// <summary>
    /// Creates a render target that appears on the display.
    /// </summary>
    STDMETHOD(CreateHwndRenderTarget)(
        _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        _In_ CONST D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties,
        _COM_Outptr_ ID2D1HwndRenderTarget** hwndRenderTarget 
        ) PURE;
    
    /// <summary>
    /// Creates a render target that draws to a DXGI Surface. The device that owns the
    /// surface is used for rendering.
    /// </summary>
    STDMETHOD(CreateDxgiSurfaceRenderTarget)(
        _In_ IDXGISurface * dxgiSurface,
        _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        _COM_Outptr_ ID2D1RenderTarget **renderTarget 
        ) PURE;
    
    /// <summary>
    /// Creates a render target that draws to a GDI device context.
    /// </summary>
    STDMETHOD(CreateDCRenderTarget)(
        _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        _COM_Outptr_ ID2D1DCRenderTarget **dcRenderTarget 
        ) PURE;

 */
            /*
    COM_DECLSPEC_NOTHROW
    HRESULT
    CreateRectangleGeometry(
        CONST D2D1_RECT_F &rectangle,
        _COM_Outptr_ ID2D1RectangleGeometry **rectangleGeometry
        )
        {
            return CreateRectangleGeometry(&rectangle, rectangleGeometry);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateRoundedRectangleGeometry(
        CONST D2D1_ROUNDED_RECT &roundedRectangle,
        _COM_Outptr_ ID2D1RoundedRectangleGeometry **roundedRectangleGeometry
        )
        {
            return CreateRoundedRectangleGeometry(&roundedRectangle, roundedRectangleGeometry);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateEllipseGeometry(
        CONST D2D1_ELLIPSE &ellipse,
        _COM_Outptr_ ID2D1EllipseGeometry **ellipseGeometry
        )
        {
            return CreateEllipseGeometry(&ellipse, ellipseGeometry);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateTransformedGeometry(
        _In_ ID2D1Geometry *sourceGeometry,
        CONST D2D1_MATRIX_3X2_F &transform,
        _COM_Outptr_ ID2D1TransformedGeometry **transformedGeometry
        )
        {
            return CreateTransformedGeometry(sourceGeometry, &transform, transformedGeometry);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateStrokeStyle(
        CONST D2D1_STROKE_STYLE_PROPERTIES &strokeStyleProperties,
        _In_reads_opt_(dashesCount) CONST FLOAT* dashes,
        UINT32 dashesCount,
        _COM_Outptr_ ID2D1StrokeStyle **strokeStyle
        )
        {
            return CreateStrokeStyle(&strokeStyleProperties, dashes, dashesCount, strokeStyle);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateDrawingStateBlock(
        CONST D2D1_DRAWING_STATE_DESCRIPTION &drawingStateDescription,
        _COM_Outptr_ ID2D1DrawingStateBlock **drawingStateBlock
        )
        {
            return CreateDrawingStateBlock(&drawingStateDescription, NULL, drawingStateBlock);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateDrawingStateBlock(
        _COM_Outptr_ ID2D1DrawingStateBlock **drawingStateBlock
        )
        {
            return CreateDrawingStateBlock(NULL, NULL, drawingStateBlock);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateWicBitmapRenderTarget(
        _In_ IWICBitmap *target,
        CONST D2D1_RENDER_TARGET_PROPERTIES &renderTargetProperties,
        _COM_Outptr_ ID2D1RenderTarget **renderTarget
        )
        {
            return CreateWicBitmapRenderTarget(target, &renderTargetProperties, renderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateHwndRenderTarget(
        CONST D2D1_RENDER_TARGET_PROPERTIES &renderTargetProperties,
        CONST D2D1_HWND_RENDER_TARGET_PROPERTIES &hwndRenderTargetProperties,
        _COM_Outptr_ ID2D1HwndRenderTarget **hwndRenderTarget
        )
        {
            return CreateHwndRenderTarget(&renderTargetProperties, &hwndRenderTargetProperties, hwndRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateDxgiSurfaceRenderTarget(
        _In_ IDXGISurface *dxgiSurface,
        CONST D2D1_RENDER_TARGET_PROPERTIES &renderTargetProperties,
        _COM_Outptr_ ID2D1RenderTarget **renderTarget
        )
        {
            return CreateDxgiSurfaceRenderTarget(dxgiSurface, &renderTargetProperties, renderTarget);
        }
*/
    }
}
