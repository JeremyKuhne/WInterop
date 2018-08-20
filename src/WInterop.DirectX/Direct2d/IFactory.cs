// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.DirectWrite;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [ID2D1Factory] {ID2D1Factory1]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/dd371246.aspx"/>
    /// </remarks>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Factory1),
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
            ref Matrix3x2 transform);

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

        /// <summary>
        /// Creates a render target which is a source of bitmaps.
        /// </summary>
        void CreateWicBitmapRenderTargetSTUB();
        //STDMETHOD(CreateWicBitmapRenderTarget)(
        //    _In_ IWICBitmap * target,
        //    _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        //_COM_Outptr_ ID2D1RenderTarget **renderTarget 
        //) PURE;

        /// <summary>
        /// Creates a render target that appears on the display. [CreateHwndRenderTarget]
        /// </summary>
        IWindowRenderTarget CreateWindowRenderTarget(
            in RenderTargetProperties renderTargetProperties,
            in WindowRenderTargetProperties hwndRenderTargetProperties);

        /// <summary>
        /// Creates a render target that draws to a DXGI Surface. The device that owns the
        /// surface is used for rendering.
        /// </summary>
        void CreateDxgiSurfaceRenderTargetSTUB();
        //STDMETHOD(CreateDxgiSurfaceRenderTarget)(
        //    _In_ IDXGISurface * dxgiSurface,
        //    _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        //    _COM_Outptr_ ID2D1RenderTarget **renderTarget 
        //    ) PURE;

        // TODO: really ID2D1DCRenderTarget
        /// <summary>
        /// Creates a render target that draws to a GDI device context.
        /// </summary>
        IRenderTarget CreateDCRenderTarget(
            in RenderTargetProperties renderTargetProperties);

        /// <summary>
        /// This creates a new Direct2D device from the given IDXGIDevice.
        /// </summary>
        IDevice CreateDevice(
            IntPtr dxgiDevice);
    
    ///// <summary>
    ///// This creates a stroke style with the ability to preserve stroke width in various
    ///// ways.
    ///// </summary>
    //STDMETHOD(CreateStrokeStyle)(
    //    _In_ CONST D2D1_STROKE_STYLE_PROPERTIES1 *strokeStyleProperties,
    //    _In_reads_opt_(dashesCount) CONST FLOAT *dashes,
    //    UINT32 dashesCount,
    //    _COM_Outptr_ ID2D1StrokeStyle1** strokeStyle 
    //    ) PURE;
    
    //using ID2D1Factory::CreateStrokeStyle;
    
    ///// <summary>
    ///// Creates a path geometry with new operational methods.
    ///// </summary>
    //STDMETHOD(CreatePathGeometry)(
    //    _COM_Outptr_ ID2D1PathGeometry1 ** pathGeometry 
    //    ) PURE;
    
    //using ID2D1Factory::CreatePathGeometry;
    
    ///// <summary>
    ///// Creates a new drawing state block, this can be used in subsequent
    ///// SaveDrawingState and RestoreDrawingState operations on the render target.
    ///// </summary>
    //STDMETHOD(CreateDrawingStateBlock)(
    //    _In_opt_ CONST D2D1_DRAWING_STATE_DESCRIPTION1 *drawingStateDescription,
    //    _In_opt_ IDWriteRenderingParams *textRenderingParams,
    //    _COM_Outptr_ ID2D1DrawingStateBlock1 **drawingStateBlock 
    //    ) PURE;
    
    //using ID2D1Factory::CreateDrawingStateBlock;
    
    ///// <summary>
    ///// Creates a new GDI metafile.
    ///// </summary>
    //STDMETHOD(CreateGdiMetafile)(
    //    _In_ IStream * metafileStream,
    //    _COM_Outptr_ ID2D1GdiMetafile** metafile 
    //    ) PURE;
    
    ///// <summary>
    ///// This globally registers the given effect. The effect can later be instantiated
    ///// by using the registered class id. The effect registration is reference counted.
    ///// </summary>
    //STDMETHOD(RegisterEffectFromStream)(
    //    _In_ REFCLSID classId,
    //    _In_ IStream* propertyXml,
    //    _In_reads_opt_(bindingsCount) CONST D2D1_PROPERTY_BINDING *bindings,
    //    UINT32 bindingsCount,
    //    _In_ CONST PD2D1_EFFECT_FACTORY effectFactory 
    //    ) PURE;
    
    ///// <summary>
    ///// This globally registers the given effect. The effect can later be instantiated
    ///// by using the registered class id. The effect registration is reference counted.
    ///// </summary>
    //STDMETHOD(RegisterEffectFromString)(
    //    _In_ REFCLSID classId,
    //    _In_ PCWSTR propertyXml,
    //    _In_reads_opt_(bindingsCount) CONST D2D1_PROPERTY_BINDING *bindings,
    //    UINT32 bindingsCount,
    //    _In_ CONST PD2D1_EFFECT_FACTORY effectFactory 
    //    ) PURE;
    
    ///// <summary>
    ///// This unregisters the given effect by its class id, you need to call
    ///// UnregisterEffect for every call to ID2D1Factory1::RegisterEffectFromStream and
    ///// ID2D1Factory1::RegisterEffectFromString to completely unregister it.
    ///// </summary>
    //STDMETHOD(UnregisterEffect)(
    //    _In_ REFCLSID classId 
    //    ) PURE;
    
    ///// <summary>
    ///// This returns all of the registered effects in the process, including any
    ///// built-in effects.
    ///// </summary>
    ///// <param name="effectsReturned">The number of effects returned into the passed in
    ///// effects array.</param>
    ///// <param name="effectsRegistered">The number of effects currently registered in
    ///// the system.</param>
    //STDMETHOD(GetRegisteredEffects)(
    //    _Out_writes_to_opt_(effectsCount, * effectsReturned) CLSID *effects,
    //    UINT32 effectsCount,
    //    _Out_opt_ UINT32* effectsReturned,
    //    _Out_opt_ UINT32* effectsRegistered 
    //    ) CONST PURE;

    //    /// <summary>
    //    /// This retrieves the effect properties for the given effect, all of the effect
    //    /// properties will be set to a default value since an effect is not instantiated to
    //    /// implement the returned property interface.
    //    /// </summary>
    //    STDMETHOD(GetEffectProperties)(
    //        _In_ REFCLSID effectId,
    //        _COM_Outptr_ ID2D1Properties** properties 
    //    ) CONST PURE;
    }

    public static class FactoryExtensions
    {
        public unsafe static IStrokeStyle CreateStrokeStyle(
            this IFactory factory,
            in StrokeStyleProperties strokeStyleProperties)
            => factory.CreateStrokeStyle(strokeStyleProperties, null, 0);
    }
}
