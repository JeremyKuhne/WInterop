// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.DirectWrite;
using WInterop.Errors;

namespace WInterop.Direct2d
{
    /// <summary>
    /// The device context represents a set of state and a command buffer that is used
    /// to render to a target bitmap. [ID2D1DeviceContext]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1DeviceContext),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDeviceContext : IRenderTarget
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        #region ID2D1RenderTarget
        /// <summary>
        /// Create a D2D bitmap by copying from memory, or create uninitialized.
        /// </summary>
        new unsafe IBitmap CreateBitmap(
            SizeU size,
            void* srcData,
            uint pitch,
            in BitmapProperties bitmapProperties);

        /// <summary>
        /// Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        new unsafe IBitmap CreateBitmapFromWicBitmap(
            object wicBitmapSource, // IWICBitmapSource
            BitmapProperties* bitmapProperties);

        /// <summary>
        /// Create a D2D bitmap by sharing bits from another resource. The bitmap must be
        /// compatible with the render target for the call to succeed. For example, an
        /// IWICBitmap can be shared with a software target, or a DXGI surface can be shared
        /// with a DXGI render target.
        /// </summary>
        new unsafe IBitmap CreateSharedBitmap(
            in Guid riid,
            void* data,
            BitmapProperties* bitmapProperties);

        /// <summary>
        /// Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        /// or pen a geometry.
        /// </summary>
        new unsafe IBitmapBrush CreateBitmapBrush(
            IBitmap bitmap,
            BitmapBrushProperties* bitmapBrushProperties,
            BrushProperties* brushProperties);

        new unsafe ISolidColorBrush CreateSolidColorBrush(
            in ColorF color,
            BrushProperties* brushProperties);

        /// <summary>
        /// A gradient stop collection represents a set of stops in an ideal unit length.
        /// This is the source resource for a linear gradient and radial gradient brush.
        /// </summary>
        /// <param name="colorInterpolationGamma">Specifies which space the color
        /// interpolation occurs in.</param>
        /// <param name="extendMode">Specifies how the gradient will be extended outside of
        /// the unit length.</param>
        new unsafe IGradientStopCollection CreateGradientStopCollection(
            GradientStop* gradientStops,
            uint gradientStopsCount,
            Gamma colorInterpolationGamma,
            ExtendMode extendMode);

        new void CreateLinearGradientBrushSTUB();
        //STDMETHOD(CreateLinearGradientBrush)(
        //    _In_ CONST D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES *linearGradientBrushProperties,
        //    _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        //    _In_ ID2D1GradientStopCollection* gradientStopCollection,
        //    _COM_Outptr_ ID2D1LinearGradientBrush** linearGradientBrush 
        //    ) PURE;

        new unsafe IRadialGradientBrush CreateRadialGradientBrush(
            in RadialGradientBrushProperties radialGradientBrushProperties,
            BrushProperties* brushProperties,
            IGradientStopCollection gradientStopCollection);

        /// <summary>
        /// Creates a bitmap render target whose bitmap can be used as a source for
        /// rendering in the API.
        /// </summary>
        /// <param name="desiredSize">The requested size of the target in DIPs. If the pixel
        /// size is not specified, the DPI is inherited from the parent target. However, the
        /// render target will never contain a fractional number of pixels.</param>
        /// <param name="desiredPixelSize">The requested size of the render target in
        /// pixels. If the DIP size is also specified, the DPI is calculated from these two
        /// values. If the desired size is not specified, the DPI is inherited from the
        /// parent render target. If neither value is specified, the compatible render
        /// target will be the same size and have the same DPI as the parent target.</param>
        /// <param name="desiredFormat">The desired pixel format. The format must be
        /// compatible with the parent render target type. If the format is not specified,
        /// it will be inherited from the parent render target.</param>
        /// <param name="options">Allows the caller to retrieve a GDI compatible render
        /// target.</param>
        /// <param name="bitmapRenderTarget">The returned bitmap render target.</param>
        new unsafe IBitmapRenderTarget CreateCompatibleRenderTarget(
            SizeF* desiredSize,
            SizeU* desiredPixelSize,
            PixelFormat* desiredFormat,
            CompatibleRenderTargetOptions options);

        /// <summary>
        /// Creates a layer resource that can be used on any target and which will resize
        /// under the covers if necessary.
        /// </summary>
        /// <param name="size">The resolution independent minimum size hint for the layer
        /// resource. Specify this to prevent unwanted reallocation of the layer backing
        /// store. The size is in DIPs, but, it is unaffected by the current world
        /// transform. If the size is unspecified, the returned resource is a placeholder
        /// and the backing store will be allocated to be the minimum size that can hold the
        /// content when the layer is pushed.</param>
        new void CreateLayerStub();
        //STDMETHOD(CreateLayer)(
        //    _In_opt_ CONST D2D1_SIZE_F *size,
        //    _COM_Outptr_ ID2D1Layer **layer 
        //    ) PURE;

        /// <summary>
        /// Create a D2D mesh.
        /// </summary>
        new void CreateMeshSTUB();
        //STDMETHOD(CreateMesh)(
        //    _COM_Outptr_ ID2D1Mesh ** mesh 
        //    ) PURE;

        [PreserveSig]
        new void DrawLine(
            PointF point0,
            PointF point1,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        new void DrawRectangle(
            in LtrbRectangleF rect,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        new void FillRectangle(
            in LtrbRectangleF rect,
            IBrush brush);

        [PreserveSig]
        new void DrawRoundedRectangle(
            in RoundedRectangle roundedRect,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        new void FillRoundedRectangle(
            in RoundedRectangle roundedRect,
            IBrush brush);

        [PreserveSig]
        new void DrawEllipse(
            in Ellipse ellipse,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        new void FillEllipse(
            in Ellipse ellipse,
            IBrush brush);

        [PreserveSig]
        new void DrawGeometry(
            IGeometry geometry,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        /// <param name="opacityBrush">An optionally specified opacity brush. Only the alpha
        /// channel of the corresponding brush will be sampled and will be applied to the
        /// entire fill of the geometry. If this brush is specified, the fill brush must be
        /// a bitmap brush with an extend mode of D2D1_EXTEND_MODE_CLAMP.</param>
        [PreserveSig]
        new void FillGeometry(
            IGeometry geometry,
            IBrush brush,
            IBrush opacityBrush = null);

        /// <summary>
        /// Fill a mesh. Since meshes can only render aliased content, the render target
        /// antialiasing mode must be set to aliased.
        /// </summary>
        new void FillMeshSTUB();
        //STDMETHOD_(void, FillMesh)(
        //    _In_ ID2D1Mesh * mesh,
        //    _In_ ID2D1Brush* brush 
        //    ) PURE;

        /// <summary>
        /// Fill using the alpha channel of the supplied opacity mask bitmap. The brush
        /// opacity will be modulated by the mask. The render target antialiasing mode must
        /// be set to aliased.
        /// </summary>
        new void FillOpacityMaskSTUB();
        //STDMETHOD_(void, FillOpacityMask)(
        //    _In_ ID2D1Bitmap * opacityMask,
        //    _In_ ID2D1Brush* brush,
        //    D2D1_OPACITY_MASK_CONTENT content,
        //    _In_opt_ CONST D2D1_RECT_F* destinationRectangle = NULL,
        //    _In_opt_ CONST D2D1_RECT_F *sourceRectangle = NULL 
        //    ) PURE;

        [PreserveSig]
        new unsafe void DrawBitmap(
            IBitmap bitmap,
            LtrbRectangleF* destinationRectangle = null,
            float opacity = 1.0f,
            BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.Linear,
            LtrbRectangleF* sourceRectangle = null);

        /// <summary>
        /// Draws the text within the given layout rectangle and by default also performs
        /// baseline snapping.
        /// </summary>
        [PreserveSig]
        new unsafe void DrawText(
            char* @string,
            uint stringLength,
            ITextFormat textFormat,
            in LtrbRectangleF layoutRect,
            IBrush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None,
            MeasuringMode measuringMode = MeasuringMode.Natural);

        /// <summary>
        /// Draw a text layout object. If the layout is not subsequently changed, this can
        /// be more efficient than DrawText when drawing the same layout repeatedly.
        /// </summary>
        /// <param name="options">
        /// The specified text options. If <see cref="DrawTextOptions.Clip"/> is used, the text
        /// is clipped to the layout bounds. These bounds are derived from the origin and the
        /// layout bounds of the corresponding <see cref="ITextLayout"/> object.
        /// </param>
        [PreserveSig]
        new void DrawTextLayout(
            PointF origin,
            ITextLayout textLayout,
            IBrush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None);

        [PreserveSig]
        new void DrawGlyphRun(
            PointF baselineOrigin,
            in GlyphRun glyphRun,
            IBrush foregroundBrush,
            MeasuringMode measuringMode = MeasuringMode.Natural);

        [PreserveSig]
        new void SetTransform(
            ref Matrix3x2 transform);

        [PreserveSig]
        new void GetTransform(
            out Matrix3x2 transform);

        [PreserveSig]
        new void SetAntialiasMode(
            AntialiasMode antialiasMode);

        [PreserveSig]
        new AntialiasMode GetAntialiasMode();

        [PreserveSig]
        new void SetTextAntialiasMode(
            TextAntialiasMode textAntialiasMode);

        [PreserveSig]
        new TextAntialiasMode GetTextAntialiasMode();

        [PreserveSig]
        new void SetTextRenderingParams(
            DirectWrite.IRenderingParams textRenderingParams = null);

        /// <summary>
        /// Retrieve the text render parameters. NOTE: If NULL is specified to
        /// SetTextRenderingParameters, NULL will be returned.
        /// </summary>
        [PreserveSig]
        new void GetTextRenderingParams(
            out DirectWrite.IRenderingParams textRenderingParams);

        /// <summary>
        /// Set a tag to correspond to the succeeding primitives. If an error occurs
        /// rendering a primitive, the tags can be returned from the Flush or EndDraw call.
        /// </summary>
        [PreserveSig]
        new void SetTags(
            ulong tag1,
            ulong tag2);

        /// <summary>
        /// Retrieves the currently set tags. This does not retrieve the tags corresponding
        /// to any primitive that is in error.
        /// </summary>
        [PreserveSig]
        new void GetTags(
            out ulong tag1,
            out ulong tag2);

        /// <summary>
        /// Start a layer of drawing calls. The way in which the layer must be resolved is
        /// specified first as well as the logical resource that stores the layer
        /// parameters. The supplied layer resource might grow if the specified content
        /// cannot fit inside it. The layer will grow monotonically on each axis.  If a NULL
        /// ID2D1Layer is provided, then a layer resource will be allocated automatically.
        /// </summary>
        new void PushLayerSTUB();
        //STDMETHOD_(void, PushLayer)(
        //    _In_ CONST D2D1_LAYER_PARAMETERS *layerParameters,
        //_In_opt_ ID2D1Layer *layer 
        //) PURE;

        /// <summary>
        /// Ends a layer that was defined with particular layer resources.
        /// </summary>
        [PreserveSig]
        new void PopLayer();

        new void Flush(
            out ulong tag1,
            out ulong tag2);

        /// <summary>
        /// Gets the current drawing state and saves it into the supplied
        /// IDrawingStatckBlock.
        /// </summary>
        [PreserveSig]
        new void SaveDrawingState(
            IDrawingStateBlock drawingStateBlock);

        /// <summary>
        /// Copies the state stored in the block interface.
        /// </summary>
        [PreserveSig]
        new void RestoreDrawingState(
            IDrawingStateBlock drawingStateBlock);

        /// <summary>
        /// Pushes a clip. The clip can be antialiased. The clip must be axis aligned. If
        /// the current world transform is not axis preserving, then the bounding box of the
        /// transformed clip rect will be used. The clip will remain in effect until a
        /// PopAxisAligned clip call is made.
        /// </summary>
        [PreserveSig]
        new void PushAxisAlignedClip(
            in LtrbRectangleF clipRect,
            AntialiasMode antialiasMode);

        [PreserveSig]
        new void PopAxisAlignedClip();

        [PreserveSig]
        new unsafe void Clear(
            ColorF* clearColor = null);

        /// <summary>
        /// Start drawing on this render target. Draw calls can only be issued between a
        /// BeginDraw and EndDraw call.
        /// </summary>
        [PreserveSig]
        new void BeginDraw();

        /// <summary>
        /// Ends drawing on the render target, error results can be retrieved at this time,
        /// or when calling flush.
        /// </summary>
        new void EndDraw(
            out ulong tag1,
            out ulong tag2);

        [PreserveSig]
        new void GetPixelFormat(out PixelFormat pixelFormat);

        /// <summary>
        /// Sets the DPI on the render target. This results in the render target being
        /// interpreted to a different scale. Neither DPI can be negative. If zero is
        /// specified for both, the system DPI is chosen. If one is zero and the other
        /// unspecified, the DPI is not changed.
        /// </summary>
        [PreserveSig]
        new void SetDpi(
            float dpiX,
            float dpiY);

        /// <summary>
        /// Return the current DPI from the target.
        /// </summary>
        [PreserveSig]
        new void GetDpi(
            out float dpiX,
            out float dpiY);

        /// <summary>
        /// Returns the size of the render target in DIPs.
        /// </summary>
        [PreserveSig]
        new void GetSize(out SizeF size);

        /// <summary>
        /// Returns the size of the render target in pixels.
        /// </summary>
        [PreserveSig]
        new SizeU GetPixelSize(out SizeU pixelSize);

        /// <summary>
        /// Returns the maximum bitmap and render target size that is guaranteed to be
        /// supported by the render target.
        /// </summary>
        [PreserveSig]
        new uint GetMaximumBitmapSize();

        /// <summary>
        /// Returns true if the given properties are supported by this render target. The
        /// DPI is ignored. NOTE: If the render target type is software, then neither
        /// D2D1_FEATURE_LEVEL_9 nor D2D1_FEATURE_LEVEL_10 will be considered to be
        /// supported.
        /// </summary>
        [PreserveSig]
        new BOOL IsSupported(
            in RenderTargetProperties renderTargetProperties);
        #endregion

        /// <summary>
        /// Creates a bitmap with extended bitmap properties, potentially from a block of
        /// memory.
        /// </summary>
        STDMETHOD(CreateBitmap)(
            D2D1_SIZE_U size,
            _In_opt_ CONST void* sourceData,
            UINT32 pitch,
        _In_ CONST D2D1_BITMAP_PROPERTIES1* bitmapProperties,
        _COM_Outptr_ ID2D1Bitmap1** bitmap 
        ) PURE;
    
        /// <summary>
        /// Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        void CreateBitmapFromWicBitmapSTUB();
        //STDMETHOD(CreateBitmapFromWicBitmap)(
        //    _In_ IWICBitmapSource * wicBitmapSource,
        //    _In_opt_ CONST D2D1_BITMAP_PROPERTIES1 *bitmapProperties,
        //    _COM_Outptr_ ID2D1Bitmap1 **bitmap 
        //    ) PURE;

        /// <summary>
        /// Creates a color context from a color space.  If the space is Custom, the context
        /// is initialized from the profile/profileSize arguments.  Otherwise the context is
        /// initialized with the profile bytes associated with the space and
        /// profile/profileSize are ignored.
        /// </summary>
        void CreateColorContextSTUB();
        //STDMETHOD(CreateColorContext)(
        //    D2D1_COLOR_SPACE space,
        //    _In_reads_opt_(profileSize) CONST BYTE* profile,
        //    UINT32 profileSize,
        //    _COM_Outptr_ ID2D1ColorContext **colorContext 
        //    ) PURE;

        void CreateColorContextFromFilenameSTUB();
        //STDMETHOD(CreateColorContextFromFilename)(
        //    _In_ PCWSTR filename,
        //    _COM_Outptr_ ID2D1ColorContext** colorContext 
        //    ) PURE;

        void CreateColorContextFromWicColorContextSTUB();
        //STDMETHOD(CreateColorContextFromWicColorContext)(
        //    _In_ IWICColorContext * wicColorContext,
        //    _COM_Outptr_ ID2D1ColorContext** colorContext 
        //    ) PURE;

        /// <summary>
        /// Creates a bitmap from a DXGI surface with a set of extended properties.
        /// </summary>
        STDMETHOD(CreateBitmapFromDxgiSurface)(
            _In_ IDXGISurface * surface,
            _In_opt_ CONST D2D1_BITMAP_PROPERTIES1 *bitmapProperties,
            _COM_Outptr_ ID2D1Bitmap1 **bitmap 
            ) PURE;

    /// <summary>
    /// Create a new effect, the effect must either be built in or previously registered
    /// through ID2D1Factory1::RegisterEffectFromStream or
    /// ID2D1Factory1::RegisterEffectFromString.
    /// </summary>
    STDMETHOD(CreateEffect)(
        _In_ REFCLSID effectId,
        _COM_Outptr_ ID2D1Effect** effect 
        ) PURE;
    
    /// <summary>
    /// A gradient stop collection represents a set of stops in an ideal unit length.
    /// This is the source resource for a linear gradient and radial gradient brush.
    /// </summary>
    /// <param name="preInterpolationSpace">Specifies both the input color space and the
    /// space in which the color interpolation occurs.</param>
    /// <param name="postInterpolationSpace">Specifies the color space colors will be
    /// converted to after interpolation occurs.</param>
    /// <param name="bufferPrecision">Specifies the precision in which the gradient
    /// buffer will be held.</param>
    /// <param name="extendMode">Specifies how the gradient will be extended outside of
    /// the unit length.</param>
    /// <param name="colorInterpolationMode">Determines if colors will be interpolated
    /// in straight alpha or premultiplied alpha space.</param>
    STDMETHOD(CreateGradientStopCollection)(
        _In_reads_(straightAlphaGradientStopsCount) CONST D2D1_GRADIENT_STOP* straightAlphaGradientStops,
        _In_range_(>=, 1) UINT32 straightAlphaGradientStopsCount,
         D2D1_COLOR_SPACE preInterpolationSpace,
        D2D1_COLOR_SPACE postInterpolationSpace,
        D2D1_BUFFER_PRECISION bufferPrecision,
        D2D1_EXTEND_MODE extendMode,
        D2D1_COLOR_INTERPOLATION_MODE colorInterpolationMode,
        _COM_Outptr_ ID2D1GradientStopCollection1 **gradientStopCollection1 
        ) PURE;
    
    using ID2D1RenderTarget::CreateGradientStopCollection;
    
    /// <summary>
    /// Creates an image brush, the input image can be any type of image, including a
    /// bitmap, effect and a command list.
    /// </summary>
    STDMETHOD(CreateImageBrush)(
        _In_opt_ ID2D1Image * image,
        _In_ CONST D2D1_IMAGE_BRUSH_PROPERTIES *imageBrushProperties,
        _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        _COM_Outptr_ ID2D1ImageBrush** imageBrush 
        ) PURE;
    
    STDMETHOD(CreateBitmapBrush)(
        _In_opt_ ID2D1Bitmap * bitmap,
        _In_opt_ CONST D2D1_BITMAP_BRUSH_PROPERTIES1 *bitmapBrushProperties,
        _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        _COM_Outptr_ ID2D1BitmapBrush1** bitmapBrush 
        ) PURE;
    
    using ID2D1RenderTarget::CreateBitmapBrush;
    
    /// <summary>
    /// Creates a new command list.
    /// </summary>
    STDMETHOD(CreateCommandList)(
        _COM_Outptr_ ID2D1CommandList ** commandList 
        ) PURE;
    
    /// <summary>
    /// Indicates whether the format is supported by D2D.
    /// </summary>
    STDMETHOD_(BOOL, IsDxgiFormatSupported)(
        DXGI_FORMAT format
        ) CONST PURE;
    
    /// <summary>
    /// Indicates whether the buffer precision is supported by D2D.
    /// </summary>
    STDMETHOD_(BOOL, IsBufferPrecisionSupported)(
        D2D1_BUFFER_PRECISION bufferPrecision
        ) CONST PURE;
    
    /// <summary>
    /// This retrieves the local-space bounds in DIPs of the current image using the
    /// device context DPI.
    /// </summary>
    STDMETHOD(GetImageLocalBounds)(
        _In_ ID2D1Image * image,
        _Out_ D2D1_RECT_F* localBounds 
        ) CONST PURE;

        /// <summary>
        /// This retrieves the world-space bounds in DIPs of the current image using the
        /// device context DPI.
        /// </summary>
        STDMETHOD(GetImageWorldBounds)(
            _In_ ID2D1Image * image,
            _Out_ D2D1_RECT_F* worldBounds 
        ) CONST PURE;

        /// <summary>
        /// Retrieves the world-space bounds in DIPs of the glyph run using the device
        /// context DPI.
        /// </summary>
        STDMETHOD(GetGlyphRunWorldBounds)(
            D2D1_POINT_2F baselineOrigin,
            _In_ CONST DWRITE_GLYPH_RUN *glyphRun,
        DWRITE_MEASURING_MODE measuringMode,
        _Out_ D2D1_RECT_F* bounds 
        ) CONST PURE;

        /// <summary>
        /// Retrieves the device associated with this device context.
        /// </summary>
        STDMETHOD_(void, GetDevice)(
            _Outptr_ ID2D1Device ** device 
        ) CONST PURE;

        /// <summary>
        /// Sets the target for this device context to point to the given image. The image
        /// can be a command list or a bitmap created with the D2D1_BITMAP_OPTIONS_TARGET
        /// flag.
        /// </summary>
        STDMETHOD_(void, SetTarget)(
            _In_opt_ ID2D1Image * image 
        ) PURE;
    
    /// <summary>
    /// Gets the target that this device context is currently pointing to.
    /// </summary>
    STDMETHOD_(void, GetTarget)(
        _Outptr_result_maybenull_ ID2D1Image ** image 
        ) CONST PURE;

        /// <summary>
        /// Sets tuning parameters for internal rendering inside the device context.
        /// </summary>
        STDMETHOD_(void, SetRenderingControls)(
            _In_ CONST D2D1_RENDERING_CONTROLS *renderingControls 
        ) PURE;
    
    /// <summary>
    /// This retrieves the rendering controls currently selected into the device
    /// context.
    /// </summary>
    STDMETHOD_(void, GetRenderingControls)(
        _Out_ D2D1_RENDERING_CONTROLS * renderingControls 
        ) CONST PURE;

        /// <summary>
        /// Changes the primitive blending mode for all of the rendering operations.
        /// </summary>
        STDMETHOD_(void, SetPrimitiveBlend)(
            D2D1_PRIMITIVE_BLEND primitiveBlend
    
            ) PURE;

        /// <summary>
        /// Returns the primitive blend currently selected into the device context.
        /// </summary>
        STDMETHOD_(D2D1_PRIMITIVE_BLEND, GetPrimitiveBlend)(
    
            ) CONST PURE;
    
    /// <summary>
    /// Changes the units used for all of the rendering operations.
    /// </summary>
    STDMETHOD_(void, SetUnitMode)(
        D2D1_UNIT_MODE unitMode
        ) PURE;

        /// <summary>
        /// Returns the unit mode currently set on the device context.
        /// </summary>
        STDMETHOD_(D2D1_UNIT_MODE, GetUnitMode)(
    
            ) CONST PURE;
    
    /// <summary>
    /// Draws the glyph run with an extended description to describe the glyphs.
    /// </summary>
    STDMETHOD_(void, DrawGlyphRun)(
        D2D1_POINT_2F baselineOrigin,
        _In_ CONST DWRITE_GLYPH_RUN *glyphRun,
        _In_opt_ CONST DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
        _In_ ID2D1Brush* foregroundBrush,
        DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE_NATURAL 
        ) PURE;
    
    using ID2D1RenderTarget::DrawGlyphRun;
    
    /// <summary>
    /// Draw an image to the device context. The image represents either a concrete
    /// bitmap or the output of an effect graph.
    /// </summary>
    STDMETHOD_(void, DrawImage)(
        _In_ ID2D1Image * image,
        _In_opt_ CONST D2D1_POINT_2F *targetOffset = NULL,
        _In_opt_ CONST D2D1_RECT_F* imageRectangle = NULL,
        D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE_LINEAR,
        D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE_SOURCE_OVER 
        ) PURE;
    
    /// <summary>
    /// Draw a metafile to the device context.
    /// </summary>
    STDMETHOD_(void, DrawGdiMetafile)(
        _In_ ID2D1GdiMetafile * gdiMetafile,
        _In_opt_ CONST D2D1_POINT_2F *targetOffset = NULL 
        ) PURE;
    
    STDMETHOD_(void, DrawBitmap)(
        _In_ ID2D1Bitmap * bitmap,
        _In_opt_ CONST D2D1_RECT_F *destinationRectangle,
        FLOAT opacity,
        D2D1_INTERPOLATION_MODE interpolationMode,
        _In_opt_ CONST D2D1_RECT_F* sourceRectangle = NULL,
        _In_opt_ CONST D2D1_MATRIX_4X4_F *perspectiveTransform = NULL 
        ) PURE;
    
    using ID2D1RenderTarget::DrawBitmap;
    
    /// <summary>
    /// Push a layer on the device context.
    /// </summary>
    STDMETHOD_(void, PushLayer)(
        _In_ CONST D2D1_LAYER_PARAMETERS1 *layerParameters,
        _In_opt_ ID2D1Layer *layer 
        ) PURE;
    
    using ID2D1RenderTarget::PushLayer;
    
    /// <summary>
    /// This indicates that a portion of an effect's input is invalid. This method can
    /// be called many times.
    /// </summary>
    STDMETHOD(InvalidateEffectInputRectangle)(
        _In_ ID2D1Effect * effect,
        UINT32 input,
        _In_ CONST D2D1_RECT_F* inputRectangle 
        ) PURE;
    
    /// <summary>
    /// Gets the number of invalid ouptut rectangles that have accumulated at the
    /// effect.
    /// </summary>
    STDMETHOD(GetEffectInvalidRectangleCount)(
        _In_ ID2D1Effect * effect,
        _Out_ UINT32* rectangleCount 
        ) PURE;
    
    /// <summary>
    /// Gets the invalid rectangles that are at the output of the effect.
    /// </summary>
    STDMETHOD(GetEffectInvalidRectangles)(
        _In_ ID2D1Effect * effect,
        _Out_writes_(rectanglesCount) D2D1_RECT_F* rectangles,
        UINT32 rectanglesCount 
        ) PURE;
    
    /// <summary>
    /// Gets the maximum region of each specified input which would be used during a
    /// subsequent rendering operation
    /// </summary>
    STDMETHOD(GetEffectRequiredInputRectangles)(
        _In_ ID2D1Effect * renderEffect,
        _In_opt_ CONST D2D1_RECT_F *renderImageRectangle,
        _In_reads_(inputCount) CONST D2D1_EFFECT_INPUT_DESCRIPTION *inputDescriptions,
        _Out_writes_(inputCount) D2D1_RECT_F* requiredInputRects,
        UINT32 inputCount 
        ) PURE;
    
    /// <summary>
    /// Fill using the alpha channel of the supplied opacity mask bitmap. The brush
    /// opacity will be modulated by the mask. The render target antialiasing mode must
    /// be set to aliased.
    /// </summary>
    STDMETHOD_(void, FillOpacityMask)(
        _In_ ID2D1Bitmap * opacityMask,
        _In_ ID2D1Brush* brush,
        _In_opt_ CONST D2D1_RECT_F *destinationRectangle = NULL,
        _In_opt_ CONST D2D1_RECT_F* sourceRectangle = NULL 
        ) PURE;
    }
}
