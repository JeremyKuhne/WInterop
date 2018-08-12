// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Represents an object that can receive drawing commands. Interfaces that inherit
    /// from <see cref="IRenderTarget"/> render the drawing commands they receive in different
    /// ways. [ID2D1RenderTarget]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1RenderTarget),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRenderTarget : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        /// Create a D2D bitmap by copying from memory, or create uninitialized.
        /// </summary>
        unsafe IBitmap CreateBitmap(
            SizeU size,
            void* srcData,
            uint pitch,
            in BitmapProperties bitmapProperties);

        /// <summary>
        /// Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        unsafe IBitmap CreateBitmapFromWicBitmap(
            object wicBitmapSource, // IWICBitmapSource
            BitmapProperties* bitmapProperties);

        /// <summary>
        /// Create a D2D bitmap by sharing bits from another resource. The bitmap must be
        /// compatible with the render target for the call to succeed. For example, an
        /// IWICBitmap can be shared with a software target, or a DXGI surface can be shared
        /// with a DXGI render target.
        /// </summary>
        unsafe IBitmap CreateSharedBitmap(
            in Guid riid,
            void* data,
            BitmapProperties* bitmapProperties);

        /// <summary>
        /// Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        /// or pen a geometry.
        /// </summary>
        unsafe IBitmapBrush CreateBitmapBrush(
            IBitmap bitmap,
            BitmapBrushProperties* bitmapBrushProperties,
            BrushProperties* brushProperties);


        unsafe ISolidColorBrush CreateSolidColorBrush(
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
        void CreateGradientStopCollectionSTUB();
        //STDMETHOD(CreateGradientStopCollection)(
        //    _In_reads_(gradientStopsCount) CONST D2D1_GRADIENT_STOP* gradientStops,
        //    _In_range_(>=, 1) UINT32 gradientStopsCount,
        //     D2D1_GAMMA colorInterpolationGamma,
        //    D2D1_EXTEND_MODE extendMode,
        //    _COM_Outptr_ ID2D1GradientStopCollection** gradientStopCollection 
        //    ) PURE;

        void CreateLinearGradientBrushSTUB();
        //STDMETHOD(CreateLinearGradientBrush)(
        //    _In_ CONST D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES *linearGradientBrushProperties,
        //    _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        //    _In_ ID2D1GradientStopCollection* gradientStopCollection,
        //    _COM_Outptr_ ID2D1LinearGradientBrush** linearGradientBrush 
        //    ) PURE;

        void CreateRadialGradientBrushSTUB();
        //STDMETHOD(CreateRadialGradientBrush)(
        //    _In_ CONST D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES *radialGradientBrushProperties,
        //    _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        //    _In_ ID2D1GradientStopCollection* gradientStopCollection,
        //    _COM_Outptr_ ID2D1RadialGradientBrush** radialGradientBrush 
        //    ) PURE;

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
        STDMETHOD(CreateCompatibleRenderTarget)(
            _In_opt_ CONST D2D1_SIZE_F *desiredSize,
            _In_opt_ CONST D2D1_SIZE_U* desiredPixelSize,
            _In_opt_ CONST D2D1_PIXEL_FORMAT *desiredFormat,
            D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
            _COM_Outptr_ ID2D1BitmapRenderTarget** bitmapRenderTarget 
            ) PURE;
    
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
    STDMETHOD(CreateLayer)(
        _In_opt_ CONST D2D1_SIZE_F *size,
        _COM_Outptr_ ID2D1Layer **layer 
        ) PURE;
    
    /// <summary>
    /// Create a D2D mesh.
    /// </summary>
    STDMETHOD(CreateMesh)(
        _COM_Outptr_ ID2D1Mesh ** mesh 
        ) PURE;
    
    STDMETHOD_(void, DrawLine)(
        D2D1_POINT_2F point0,
        D2D1_POINT_2F point1,
        _In_ ID2D1Brush * brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle *strokeStyle = NULL 
        ) PURE;
    
    STDMETHOD_(void, DrawRectangle)(
        _In_ CONST D2D1_RECT_F *rect,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle* strokeStyle = NULL 
        ) PURE;
    
    STDMETHOD_(void, FillRectangle)(
        _In_ CONST D2D1_RECT_F *rect,
        _In_ ID2D1Brush *brush 
        ) PURE;
    
    STDMETHOD_(void, DrawRoundedRectangle)(
        _In_ CONST D2D1_ROUNDED_RECT *roundedRect,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle* strokeStyle = NULL 
        ) PURE;
    
    STDMETHOD_(void, FillRoundedRectangle)(
        _In_ CONST D2D1_ROUNDED_RECT *roundedRect,
        _In_ ID2D1Brush *brush 
        ) PURE;
    
    STDMETHOD_(void, DrawEllipse)(
        _In_ CONST D2D1_ELLIPSE *ellipse,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle* strokeStyle = NULL 
        ) PURE;
    
    STDMETHOD_(void, FillEllipse)(
        _In_ CONST D2D1_ELLIPSE *ellipse,
        _In_ ID2D1Brush *brush 
        ) PURE;
    
    STDMETHOD_(void, DrawGeometry)(
        _In_ ID2D1Geometry * geometry,
        _In_ ID2D1Brush* brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle *strokeStyle = NULL 
        ) PURE;
    
    /// <param name="opacityBrush">An optionally specified opacity brush. Only the alpha
    /// channel of the corresponding brush will be sampled and will be applied to the
    /// entire fill of the geometry. If this brush is specified, the fill brush must be
    /// a bitmap brush with an extend mode of D2D1_EXTEND_MODE_CLAMP.</param>
    STDMETHOD_(void, FillGeometry)(
        _In_ ID2D1Geometry * geometry,
        _In_ ID2D1Brush* brush,
        _In_opt_ ID2D1Brush* opacityBrush = NULL 
        ) PURE;
    
    /// <summary>
    /// Fill a mesh. Since meshes can only render aliased content, the render target
    /// antialiasing mode must be set to aliased.
    /// </summary>
    STDMETHOD_(void, FillMesh)(
        _In_ ID2D1Mesh * mesh,
        _In_ ID2D1Brush* brush 
        ) PURE;
    
    /// <summary>
    /// Fill using the alpha channel of the supplied opacity mask bitmap. The brush
    /// opacity will be modulated by the mask. The render target antialiasing mode must
    /// be set to aliased.
    /// </summary>
    STDMETHOD_(void, FillOpacityMask)(
        _In_ ID2D1Bitmap * opacityMask,
        _In_ ID2D1Brush* brush,
        D2D1_OPACITY_MASK_CONTENT content,
        _In_opt_ CONST D2D1_RECT_F* destinationRectangle = NULL,
        _In_opt_ CONST D2D1_RECT_F *sourceRectangle = NULL 
        ) PURE;
    
    STDMETHOD_(void, DrawBitmap)(
        _In_ ID2D1Bitmap * bitmap,
        _In_opt_ CONST D2D1_RECT_F *destinationRectangle = NULL,
        FLOAT opacity = 1.0f,
        D2D1_BITMAP_INTERPOLATION_MODE interpolationMode = D2D1_BITMAP_INTERPOLATION_MODE_LINEAR,
        _In_opt_ CONST D2D1_RECT_F* sourceRectangle = NULL 
        ) PURE;
    
    /// <summary>
    /// Draws the text within the given layout rectangle and by default also performs
    /// baseline snapping.
    /// </summary>
    STDMETHOD_(void, DrawText)(
        _In_reads_(stringLength) CONST WCHAR* string,
        UINT32 stringLength,
        _In_ IDWriteTextFormat* textFormat,
        _In_ CONST D2D1_RECT_F *layoutRect,
        _In_ ID2D1Brush *defaultFillBrush,
        D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS_NONE,
        DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE_NATURAL 
        ) PURE;
    
    /// <summary>
    /// Draw a text layout object. If the layout is not subsequently changed, this can
    /// be more efficient than DrawText when drawing the same layout repeatedly.
    /// </summary>
    /// <param name="options">The specified text options. If D2D1_DRAW_TEXT_OPTIONS_CLIP
    /// is used, the text is clipped to the layout bounds. These bounds are derived from
    /// the origin and the layout bounds of the corresponding IDWriteTextLayout object.
    /// </param>
    STDMETHOD_(void, DrawTextLayout)(
        D2D1_POINT_2F origin,
        _In_ IDWriteTextLayout * textLayout,
        _In_ ID2D1Brush* defaultFillBrush,
        D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS_NONE 
        ) PURE;
    
    STDMETHOD_(void, DrawGlyphRun)(
        D2D1_POINT_2F baselineOrigin,
        _In_ CONST DWRITE_GLYPH_RUN *glyphRun,
        _In_ ID2D1Brush *foregroundBrush,
        DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE_NATURAL 
        ) PURE;
    
    STDMETHOD_(void, SetTransform)(
        _In_ CONST D2D1_MATRIX_3X2_F *transform 
        ) PURE;
    
    STDMETHOD_(void, GetTransform)(
        _Out_ D2D1_MATRIX_3X2_F * transform 
        ) CONST PURE;

        STDMETHOD_(void, SetAntialiasMode)(
            D2D1_ANTIALIAS_MODE antialiasMode
    
            ) PURE;

        STDMETHOD_(D2D1_ANTIALIAS_MODE, GetAntialiasMode)(
    
            ) CONST PURE;
    
    STDMETHOD_(void, SetTextAntialiasMode)(
        D2D1_TEXT_ANTIALIAS_MODE textAntialiasMode
        ) PURE;

        STDMETHOD_(D2D1_TEXT_ANTIALIAS_MODE, GetTextAntialiasMode)(
    
            ) CONST PURE;
    
    STDMETHOD_(void, SetTextRenderingParams)(
        _In_opt_ IDWriteRenderingParams * textRenderingParams = NULL 
        ) PURE;
    
    /// <summary>
    /// Retrieve the text render parameters. NOTE: If NULL is specified to
    /// SetTextRenderingParameters, NULL will be returned.
    /// </summary>
    STDMETHOD_(void, GetTextRenderingParams)(
        _Outptr_result_maybenull_ IDWriteRenderingParams ** textRenderingParams 
        ) CONST PURE;

        /// <summary>
        /// Set a tag to correspond to the succeeding primitives. If an error occurs
        /// rendering a primitive, the tags can be returned from the Flush or EndDraw call.
        /// </summary>
        STDMETHOD_(void, SetTags)(
            D2D1_TAG tag1,
            D2D1_TAG tag2
            ) PURE;

        /// <summary>
        /// Retrieves the currently set tags. This does not retrieve the tags corresponding
        /// to any primitive that is in error.
        /// </summary>
        STDMETHOD_(void, GetTags)(
            _Out_opt_ D2D1_TAG * tag1 = NULL,
            _Out_opt_ D2D1_TAG* tag2 = NULL 
        ) CONST PURE;

        /// <summary>
        /// Start a layer of drawing calls. The way in which the layer must be resolved is
        /// specified first as well as the logical resource that stores the layer
        /// parameters. The supplied layer resource might grow if the specified content
        /// cannot fit inside it. The layer will grow monotonically on each axis.  If a NULL
        /// ID2D1Layer is provided, then a layer resource will be allocated automatically.
        /// </summary>
        STDMETHOD_(void, PushLayer)(
            _In_ CONST D2D1_LAYER_PARAMETERS *layerParameters,
        _In_opt_ ID2D1Layer *layer 
        ) PURE;
    
    /// <summary>
    /// Ends a layer that was defined with particular layer resources.
    /// </summary>
    STDMETHOD_(void, PopLayer)(
        ) PURE;

        STDMETHOD(Flush)(
            _Out_opt_ D2D1_TAG * tag1 = NULL,
            _Out_opt_ D2D1_TAG* tag2 = NULL 
        ) PURE;
    
    /// <summary>
    /// Gets the current drawing state and saves it into the supplied
    /// IDrawingStatckBlock.
    /// </summary>
    STDMETHOD_(void, SaveDrawingState)(
        _Inout_ ID2D1DrawingStateBlock * drawingStateBlock 
        ) CONST PURE;

        /// <summary>
        /// Copies the state stored in the block interface.
        /// </summary>
        STDMETHOD_(void, RestoreDrawingState)(
            _In_ ID2D1DrawingStateBlock * drawingStateBlock 
        ) PURE;
    
    /// <summary>
    /// Pushes a clip. The clip can be antialiased. The clip must be axis aligned. If
    /// the current world transform is not axis preserving, then the bounding box of the
    /// transformed clip rect will be used. The clip will remain in effect until a
    /// PopAxisAligned clip call is made.
    /// </summary>
    STDMETHOD_(void, PushAxisAlignedClip)(
        _In_ CONST D2D1_RECT_F *clipRect,
        D2D1_ANTIALIAS_MODE antialiasMode 
        ) PURE;
    
    STDMETHOD_(void, PopAxisAlignedClip)(
        ) PURE;

        STDMETHOD_(void, Clear)(
            _In_opt_ CONST D2D1_COLOR_F *clearColor = NULL 
        ) PURE;
    
    /// <summary>
    /// Start drawing on this render target. Draw calls can only be issued between a
    /// BeginDraw and EndDraw call.
    /// </summary>
    STDMETHOD_(void, BeginDraw)(
        ) PURE;

        /// <summary>
        /// Ends drawing on the render target, error results can be retrieved at this time,
        /// or when calling flush.
        /// </summary>
        STDMETHOD(EndDraw)(
            _Out_opt_ D2D1_TAG * tag1 = NULL,
            _Out_opt_ D2D1_TAG* tag2 = NULL 
        ) PURE;
    
    STDMETHOD_(D2D1_PIXEL_FORMAT, GetPixelFormat)(
        ) CONST PURE;
    
    /// <summary>
    /// Sets the DPI on the render target. This results in the render target being
    /// interpreted to a different scale. Neither DPI can be negative. If zero is
    /// specified for both, the system DPI is chosen. If one is zero and the other
    /// unspecified, the DPI is not changed.
    /// </summary>
    STDMETHOD_(void, SetDpi)(
        FLOAT dpiX,
        FLOAT dpiY
        ) PURE;

        /// <summary>
        /// Return the current DPI from the target.
        /// </summary>
        STDMETHOD_(void, GetDpi)(
            _Out_ FLOAT * dpiX,
            _Out_ FLOAT* dpiY 
        ) CONST PURE;

        /// <summary>
        /// Returns the size of the render target in DIPs.
        /// </summary>
        STDMETHOD_(D2D1_SIZE_F, GetSize)(
    
            ) CONST PURE;
    
    /// <summary>
    /// Returns the size of the render target in pixels.
    /// </summary>
    STDMETHOD_(D2D1_SIZE_U, GetPixelSize)(
        ) CONST PURE;
    
    /// <summary>
    /// Returns the maximum bitmap and render target size that is guaranteed to be
    /// supported by the render target.
    /// </summary>
    STDMETHOD_(UINT32, GetMaximumBitmapSize)(
        ) CONST PURE;
    
    /// <summary>
    /// Returns true if the given properties are supported by this render target. The
    /// DPI is ignored. NOTE: If the render target type is software, then neither
    /// D2D1_FEATURE_LEVEL_9 nor D2D1_FEATURE_LEVEL_10 will be considered to be
    /// supported.
    /// </summary>
    STDMETHOD_(BOOL, IsSupported)(
        _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties 
        ) CONST PURE;

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmap(
        D2D1_SIZE_U size,
        _In_opt_ CONST void* srcData,
        UINT32 pitch,
        CONST D2D1_BITMAP_PROPERTIES &bitmapProperties,
        _COM_Outptr_ ID2D1Bitmap **bitmap
        )
        {
            return CreateBitmap(size, srcData, pitch, &bitmapProperties, bitmap);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmap(
        D2D1_SIZE_U size,
        CONST D2D1_BITMAP_PROPERTIES &bitmapProperties,
        _COM_Outptr_ ID2D1Bitmap **bitmap
        )
        {
            return CreateBitmap(size, NULL, 0, &bitmapProperties, bitmap);
        }

        /// <summary>
        /// Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmapFromWicBitmap(
        _In_ IWICBitmapSource *wicBitmapSource,
        CONST D2D1_BITMAP_PROPERTIES &bitmapProperties,
        _COM_Outptr_ ID2D1Bitmap **bitmap
        )
        {
            return CreateBitmapFromWicBitmap(wicBitmapSource, &bitmapProperties, bitmap);
        }

        /// <summary>
        /// Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmapFromWicBitmap(
        _In_ IWICBitmapSource *wicBitmapSource,
        _COM_Outptr_ ID2D1Bitmap **bitmap
        )
        {
            return CreateBitmapFromWicBitmap(wicBitmapSource, NULL, bitmap);
        }

        /// <summary>
        /// Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        /// or pen a geometry.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmapBrush(
        _In_opt_ ID2D1Bitmap *bitmap,
        _COM_Outptr_ ID2D1BitmapBrush **bitmapBrush
        )
        {
            return CreateBitmapBrush(bitmap, NULL, NULL, bitmapBrush);
        }

        /// <summary>
        /// Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        /// or pen a geometry.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmapBrush(
        _In_opt_ ID2D1Bitmap *bitmap,
        CONST D2D1_BITMAP_BRUSH_PROPERTIES &bitmapBrushProperties,
        _COM_Outptr_ ID2D1BitmapBrush **bitmapBrush
        )
        {
            return CreateBitmapBrush(bitmap, &bitmapBrushProperties, NULL, bitmapBrush);
        }

        /// <summary>
        /// Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        /// or pen a geometry.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateBitmapBrush(
        _In_opt_ ID2D1Bitmap *bitmap,
        CONST D2D1_BITMAP_BRUSH_PROPERTIES &bitmapBrushProperties,
        CONST D2D1_BRUSH_PROPERTIES &brushProperties,
        _COM_Outptr_ ID2D1BitmapBrush **bitmapBrush
        )
        {
            return CreateBitmapBrush(bitmap, &bitmapBrushProperties, &brushProperties, bitmapBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateSolidColorBrush(
        CONST D2D1_COLOR_F &color,
        _COM_Outptr_ ID2D1SolidColorBrush **solidColorBrush
        )
        {
            return CreateSolidColorBrush(&color, NULL, solidColorBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateSolidColorBrush(
        CONST D2D1_COLOR_F &color,
        CONST D2D1_BRUSH_PROPERTIES &brushProperties,
        _COM_Outptr_ ID2D1SolidColorBrush **solidColorBrush
        )
        {
            return CreateSolidColorBrush(&color, &brushProperties, solidColorBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateGradientStopCollection(
        _In_reads_(gradientStopsCount) CONST D2D1_GRADIENT_STOP* gradientStops,
        UINT32 gradientStopsCount,
        _COM_Outptr_ ID2D1GradientStopCollection **gradientStopCollection
        )
        {
            return CreateGradientStopCollection(gradientStops, gradientStopsCount, D2D1_GAMMA_2_2, D2D1_EXTEND_MODE_CLAMP, gradientStopCollection);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateLinearGradientBrush(
        CONST D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES &linearGradientBrushProperties,
        _In_ ID2D1GradientStopCollection *gradientStopCollection,
        _COM_Outptr_ ID2D1LinearGradientBrush **linearGradientBrush
        )
        {
            return CreateLinearGradientBrush(&linearGradientBrushProperties, NULL, gradientStopCollection, linearGradientBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateLinearGradientBrush(
        CONST D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES &linearGradientBrushProperties,
        CONST D2D1_BRUSH_PROPERTIES &brushProperties,
        _In_ ID2D1GradientStopCollection *gradientStopCollection,
        _COM_Outptr_ ID2D1LinearGradientBrush **linearGradientBrush
        )
        {
            return CreateLinearGradientBrush(&linearGradientBrushProperties, &brushProperties, gradientStopCollection, linearGradientBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateRadialGradientBrush(
        CONST D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES &radialGradientBrushProperties,
        _In_ ID2D1GradientStopCollection *gradientStopCollection,
        _COM_Outptr_ ID2D1RadialGradientBrush **radialGradientBrush
        )
        {
            return CreateRadialGradientBrush(&radialGradientBrushProperties, NULL, gradientStopCollection, radialGradientBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateRadialGradientBrush(
        CONST D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES &radialGradientBrushProperties,
        CONST D2D1_BRUSH_PROPERTIES &brushProperties,
        _In_ ID2D1GradientStopCollection *gradientStopCollection,
        _COM_Outptr_ ID2D1RadialGradientBrush **radialGradientBrush
        )
        {
            return CreateRadialGradientBrush(&radialGradientBrushProperties, &brushProperties, gradientStopCollection, radialGradientBrush);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateCompatibleRenderTarget(
        _COM_Outptr_ ID2D1BitmapRenderTarget **bitmapRenderTarget
        )
        {
            return CreateCompatibleRenderTarget(NULL, NULL, NULL, D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE, bitmapRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateCompatibleRenderTarget(
        D2D1_SIZE_F desiredSize,
        _COM_Outptr_ ID2D1BitmapRenderTarget **bitmapRenderTarget
        )
        {
            return CreateCompatibleRenderTarget(&desiredSize, NULL, NULL, D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE, bitmapRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateCompatibleRenderTarget(
        D2D1_SIZE_F desiredSize,
        D2D1_SIZE_U desiredPixelSize,
        _COM_Outptr_ ID2D1BitmapRenderTarget **bitmapRenderTarget
        )
        {
            return CreateCompatibleRenderTarget(&desiredSize, &desiredPixelSize, NULL, D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE, bitmapRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateCompatibleRenderTarget(
        D2D1_SIZE_F desiredSize,
        D2D1_SIZE_U desiredPixelSize,
        D2D1_PIXEL_FORMAT desiredFormat,
        _COM_Outptr_ ID2D1BitmapRenderTarget **bitmapRenderTarget
        )
        {
            return CreateCompatibleRenderTarget(&desiredSize, &desiredPixelSize, &desiredFormat, D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE, bitmapRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateCompatibleRenderTarget(
        D2D1_SIZE_F desiredSize,
        D2D1_SIZE_U desiredPixelSize,
        D2D1_PIXEL_FORMAT desiredFormat,
        D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
        _COM_Outptr_ ID2D1BitmapRenderTarget **bitmapRenderTarget
        )
        {
            return CreateCompatibleRenderTarget(&desiredSize, &desiredPixelSize, &desiredFormat, options, bitmapRenderTarget);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateLayer(
        D2D1_SIZE_F size,
        _COM_Outptr_ ID2D1Layer **layer
        )
        {
            return CreateLayer(&size, layer);
        }

        COM_DECLSPEC_NOTHROW
        HRESULT
    CreateLayer(
        _COM_Outptr_ ID2D1Layer **layer
        )
        {
            return CreateLayer(NULL, layer);
        }

        COM_DECLSPEC_NOTHROW
    void
    DrawRectangle(
        CONST D2D1_RECT_F &rect,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle *strokeStyle = NULL
        )
        {
            DrawRectangle(&rect, brush, strokeWidth, strokeStyle);
        }

        COM_DECLSPEC_NOTHROW
    void
    FillRectangle(
        CONST D2D1_RECT_F &rect,
        _In_ ID2D1Brush *brush
        )
        {
            FillRectangle(&rect, brush);
        }

        COM_DECLSPEC_NOTHROW
    void
    DrawRoundedRectangle(
        CONST D2D1_ROUNDED_RECT &roundedRect,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle *strokeStyle = NULL
        )
        {
            DrawRoundedRectangle(&roundedRect, brush, strokeWidth, strokeStyle);
        }

        COM_DECLSPEC_NOTHROW
    void
    FillRoundedRectangle(
        CONST D2D1_ROUNDED_RECT &roundedRect,
        _In_ ID2D1Brush *brush
        )
        {
            FillRoundedRectangle(&roundedRect, brush);
        }

        COM_DECLSPEC_NOTHROW
    void
    DrawEllipse(
        CONST D2D1_ELLIPSE &ellipse,
        _In_ ID2D1Brush *brush,
        FLOAT strokeWidth = 1.0f,
        _In_opt_ ID2D1StrokeStyle *strokeStyle = NULL
        )
        {
            DrawEllipse(&ellipse, brush, strokeWidth, strokeStyle);
        }

        COM_DECLSPEC_NOTHROW
    void
    FillEllipse(
        CONST D2D1_ELLIPSE &ellipse,
        _In_ ID2D1Brush *brush
        )
        {
            FillEllipse(&ellipse, brush);
        }

        COM_DECLSPEC_NOTHROW
    void
    FillOpacityMask(
        _In_ ID2D1Bitmap *opacityMask,
        _In_ ID2D1Brush *brush,
        D2D1_OPACITY_MASK_CONTENT content,
        CONST D2D1_RECT_F &destinationRectangle,
        CONST D2D1_RECT_F &sourceRectangle
        )
        {
            FillOpacityMask(opacityMask, brush, content, &destinationRectangle, &sourceRectangle);
        }

        COM_DECLSPEC_NOTHROW
    void
    DrawBitmap(
        _In_ ID2D1Bitmap *bitmap,
        CONST D2D1_RECT_F &destinationRectangle,
        FLOAT opacity = 1.0f,
        D2D1_BITMAP_INTERPOLATION_MODE interpolationMode = D2D1_BITMAP_INTERPOLATION_MODE_LINEAR,
        _In_opt_ CONST D2D1_RECT_F* sourceRectangle = NULL
        )
        {
            DrawBitmap(bitmap, &destinationRectangle, opacity, interpolationMode, sourceRectangle);
        }

        COM_DECLSPEC_NOTHROW
    void
    DrawBitmap(
        _In_ ID2D1Bitmap *bitmap,
        CONST D2D1_RECT_F &destinationRectangle,
        FLOAT opacity,
        D2D1_BITMAP_INTERPOLATION_MODE interpolationMode,
        CONST D2D1_RECT_F &sourceRectangle
        )
        {
            DrawBitmap(bitmap, &destinationRectangle, opacity, interpolationMode, &sourceRectangle);
        }

        COM_DECLSPEC_NOTHROW
    void
    SetTransform(
        CONST D2D1_MATRIX_3X2_F &transform
        )
        {
            SetTransform(&transform);
        }

        COM_DECLSPEC_NOTHROW
    void
    PushLayer(
        CONST D2D1_LAYER_PARAMETERS &layerParameters,
        _In_opt_ ID2D1Layer *layer
        )
        {
            PushLayer(&layerParameters, layer);
        }

        COM_DECLSPEC_NOTHROW
    void
    PushAxisAlignedClip(
        CONST D2D1_RECT_F &clipRect,
        D2D1_ANTIALIAS_MODE antialiasMode
        )
        {
            return PushAxisAlignedClip(&clipRect, antialiasMode);
        }

        COM_DECLSPEC_NOTHROW
    void
    Clear(
        CONST D2D1_COLOR_F &clearColor
        )
        {
            return Clear(&clearColor);
        }

        /// <summary>
        /// Draws the text within the given layout rectangle and by default also performs
        /// baseline snapping.
        /// </summary>
        COM_DECLSPEC_NOTHROW
    void
    DrawText(
        _In_reads_(stringLength) CONST WCHAR*string,
        UINT32 stringLength,
        _In_ IDWriteTextFormat *textFormat,
        CONST D2D1_RECT_F &layoutRect,
        _In_ ID2D1Brush *defaultFillBrush,
        D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS_NONE,
        DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE_NATURAL
        )
        {
            return DrawText(string, stringLength, textFormat, &layoutRect, defaultFillBrush, options, measuringMode);
        }

        COM_DECLSPEC_NOTHROW
        BOOL
    IsSupported(
        CONST D2D1_RENDER_TARGET_PROPERTIES &renderTargetProperties
        ) CONST  
    {
        return IsSupported(&renderTargetProperties);
    }
}
}
