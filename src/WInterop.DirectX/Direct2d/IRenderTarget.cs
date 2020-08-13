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
    ///  Represents an object that can receive drawing commands. Interfaces that inherit
    ///  from <see cref="IRenderTarget"/> render the drawing commands they receive in different
    ///  ways. [ID2D1RenderTarget]
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
        ///  Create a D2D bitmap by copying from memory, or create uninitialized.
        /// </summary>
        /// <param name="pitch">Byte count of each scanline, ignored if <paramref name="srcData"/> is null.</param>
        unsafe IBitmap CreateBitmap(
            SizeU size,
            void* srcData,
            uint pitch,
            in BitmapProperties bitmapProperties);

        /// <summary>
        ///  Create a D2D bitmap by copying a WIC bitmap.
        /// </summary>
        unsafe IBitmap CreateBitmapFromWicBitmap(
            object wicBitmapSource, // IWICBitmapSource
            BitmapProperties* bitmapProperties);

        /// <summary>
        ///  Create a D2D bitmap by sharing bits from another resource. The bitmap must be
        ///  compatible with the render target for the call to succeed. For example, an
        ///  IWICBitmap can be shared with a software target, or a DXGI surface can be shared
        ///  with a DXGI render target.
        /// </summary>
        unsafe IBitmap CreateSharedBitmap(
            in Guid riid,
            void* data,
            BitmapProperties* bitmapProperties);

        /// <summary>
        ///  Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill
        ///  or pen a geometry.
        /// </summary>
        unsafe IBitmapBrush CreateBitmapBrush(
            IBitmap bitmap,
            BitmapBrushProperties* bitmapBrushProperties,
            BrushProperties* brushProperties);

        unsafe ISolidColorBrush CreateSolidColorBrush(
            in ColorF color,
            BrushProperties* brushProperties);

        /// <summary>
        ///  A gradient stop collection represents a set of stops in an ideal unit length.
        ///  This is the source resource for a linear gradient and radial gradient brush.
        /// </summary>
        /// <param name="colorInterpolationGamma">Specifies which space the color
        ///  interpolation occurs in.</param>
        /// <param name="extendMode">Specifies how the gradient will be extended outside of
        ///  the unit length.</param>
        unsafe IGradientStopCollection CreateGradientStopCollection(
            GradientStop* gradientStops,
            uint gradientStopsCount,
            Gamma colorInterpolationGamma,
            ExtendMode extendMode);

        void CreateLinearGradientBrushSTUB();
        //STDMETHOD(CreateLinearGradientBrush)(
        //    _In_ CONST D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES *linearGradientBrushProperties,
        //    _In_opt_ CONST D2D1_BRUSH_PROPERTIES* brushProperties,
        //    _In_ ID2D1GradientStopCollection* gradientStopCollection,
        //    _COM_Outptr_ ID2D1LinearGradientBrush** linearGradientBrush 
        //    ) PURE;

        unsafe IRadialGradientBrush CreateRadialGradientBrush(
            in RadialGradientBrushProperties radialGradientBrushProperties,
            BrushProperties* brushProperties,
            IGradientStopCollection gradientStopCollection);

        /// <summary>
        ///  Creates a bitmap render target whose bitmap can be used as a source for
        ///  rendering in the API.
        /// </summary>
        /// <param name="desiredSize">The requested size of the target in DIPs. If the pixel
        ///  size is not specified, the DPI is inherited from the parent target. However, the
        ///  render target will never contain a fractional number of pixels.</param>
        /// <param name="desiredPixelSize">The requested size of the render target in
        ///  pixels. If the DIP size is also specified, the DPI is calculated from these two
        ///  values. If the desired size is not specified, the DPI is inherited from the
        ///  parent render target. If neither value is specified, the compatible render
        ///  target will be the same size and have the same DPI as the parent target.</param>
        /// <param name="desiredFormat">The desired pixel format. The format must be
        ///  compatible with the parent render target type. If the format is not specified,
        ///  it will be inherited from the parent render target.</param>
        /// <param name="options">Allows the caller to retrieve a GDI compatible render
        ///  target.</param>
        /// <param name="bitmapRenderTarget">The returned bitmap render target.</param>
        unsafe IBitmapRenderTarget CreateCompatibleRenderTarget(
            SizeF* desiredSize,
            SizeU* desiredPixelSize,
            PixelFormat* desiredFormat,
            CompatibleRenderTargetOptions options);

        /// <summary>
        ///  Creates a layer resource that can be used on any target and which will resize
        ///  under the covers if necessary.
        /// </summary>
        /// <param name="size">The resolution independent minimum size hint for the layer
        ///  resource. Specify this to prevent unwanted reallocation of the layer backing
        ///  store. The size is in DIPs, but, it is unaffected by the current world
        ///  transform. If the size is unspecified, the returned resource is a placeholder
        ///  and the backing store will be allocated to be the minimum size that can hold the
        ///  content when the layer is pushed.</param>
        void CreateLayerStub();
        //STDMETHOD(CreateLayer)(
        //    _In_opt_ CONST D2D1_SIZE_F *size,
        //    _COM_Outptr_ ID2D1Layer **layer 
        //    ) PURE;

        /// <summary>
        ///  Create a D2D mesh.
        /// </summary>
        void CreateMeshSTUB();
        //STDMETHOD(CreateMesh)(
        //    _COM_Outptr_ ID2D1Mesh ** mesh 
        //    ) PURE;

        [PreserveSig]
        void DrawLine(
            PointF point0,
            PointF point1,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        void DrawRectangle(
            in LtrbRectangleF rect,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        void FillRectangle(
            in LtrbRectangleF rect,
            IBrush brush);

        [PreserveSig]
        void DrawRoundedRectangle(
            in RoundedRectangle roundedRect,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        void FillRoundedRectangle(
            in RoundedRectangle roundedRect,
            IBrush brush);

        [PreserveSig]
        void DrawEllipse(
            in Ellipse ellipse,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        [PreserveSig]
        void FillEllipse(
            in Ellipse ellipse,
            IBrush brush);

        [PreserveSig]
        void DrawGeometry(
            IGeometry geometry,
            IBrush brush,
            float strokeWidth = 1.0f,
            IStrokeStyle strokeStyle = null);

        /// <param name="opacityBrush">An optionally specified opacity brush. Only the alpha
        ///  channel of the corresponding brush will be sampled and will be applied to the
        ///  entire fill of the geometry. If this brush is specified, the fill brush must be
        ///  a bitmap brush with an extend mode of D2D1_EXTEND_MODE_CLAMP.</param>
        [PreserveSig]
        void FillGeometry(
            IGeometry geometry,
            IBrush brush,
            IBrush opacityBrush = null);

        /// <summary>
        ///  Fill a mesh. Since meshes can only render aliased content, the render target
        ///  antialiasing mode must be set to aliased.
        /// </summary>
        void FillMeshSTUB();
        //STDMETHOD_(void, FillMesh)(
        //    _In_ ID2D1Mesh * mesh,
        //    _In_ ID2D1Brush* brush 
        //    ) PURE;

        /// <summary>
        ///  Fill using the alpha channel of the supplied opacity mask bitmap. The brush
        ///  opacity will be modulated by the mask. The render target antialiasing mode must
        ///  be set to aliased.
        /// </summary>
        void FillOpacityMaskSTUB();
        //STDMETHOD_(void, FillOpacityMask)(
        //    _In_ ID2D1Bitmap * opacityMask,
        //    _In_ ID2D1Brush* brush,
        //    D2D1_OPACITY_MASK_CONTENT content,
        //    _In_opt_ CONST D2D1_RECT_F* destinationRectangle = NULL,
        //    _In_opt_ CONST D2D1_RECT_F *sourceRectangle = NULL 
        //    ) PURE;

        [PreserveSig]
        unsafe void DrawBitmap(
            IBitmap bitmap,
            LtrbRectangleF* destinationRectangle = null,
            float opacity = 1.0f,
            BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.Linear,
            LtrbRectangleF* sourceRectangle = null);

        /// <summary>
        ///  Draws the text within the given layout rectangle and by default also performs
        ///  baseline snapping.
        /// </summary>
        [PreserveSig]
        unsafe void DrawText(
            char* @string,
            uint stringLength,
            ITextFormat textFormat,
            in LtrbRectangleF layoutRect,
            IBrush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None,
            MeasuringMode measuringMode = MeasuringMode.Natural);

        /// <summary>
        ///  Draw a text layout object. If the layout is not subsequently changed, this can
        ///  be more efficient than DrawText when drawing the same layout repeatedly.
        /// </summary>
        /// <param name="options">
        ///  The specified text options. If <see cref="DrawTextOptions.Clip"/> is used, the text
        ///  is clipped to the layout bounds. These bounds are derived from the origin and the
        ///  layout bounds of the corresponding <see cref="ITextLayout"/> object.
        /// </param>
        [PreserveSig]
        void DrawTextLayout(
            PointF origin,
            ITextLayout textLayout,
            IBrush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None);

        [PreserveSig]
        void DrawGlyphRun(
            PointF baselineOrigin,
            in GlyphRun glyphRun,
            IBrush foregroundBrush,
            MeasuringMode measuringMode = MeasuringMode.Natural);

        [PreserveSig]
        void SetTransform(
            ref Matrix3x2 transform);

        [PreserveSig]
        void GetTransform(
            out Matrix3x2 transform);

        [PreserveSig]
        void SetAntialiasMode(
            AntialiasMode antialiasMode);

        [PreserveSig]
        AntialiasMode GetAntialiasMode();

        [PreserveSig]
        void SetTextAntialiasMode(
            TextAntialiasMode textAntialiasMode);

        [PreserveSig]
        TextAntialiasMode GetTextAntialiasMode();

        [PreserveSig]
        void SetTextRenderingParams(
            DirectWrite.IRenderingParams textRenderingParams = null);

        /// <summary>
        ///  Retrieve the text render parameters. NOTE: If NULL is specified to
        ///  SetTextRenderingParameters, NULL will be returned.
        /// </summary>
        [PreserveSig]
        void GetTextRenderingParams(
            out DirectWrite.IRenderingParams textRenderingParams);

        /// <summary>
        ///  Set a tag to correspond to the succeeding primitives. If an error occurs
        ///  rendering a primitive, the tags can be returned from the Flush or EndDraw call.
        /// </summary>
        [PreserveSig]
        void SetTags(
            ulong tag1,
            ulong tag2);

        /// <summary>
        ///  Retrieves the currently set tags. This does not retrieve the tags corresponding
        ///  to any primitive that is in error.
        /// </summary>
        [PreserveSig]
        void GetTags(
            out ulong tag1,
            out ulong tag2);

        /// <summary>
        ///  Start a layer of drawing calls. The way in which the layer must be resolved is
        ///  specified first as well as the logical resource that stores the layer
        ///  parameters. The supplied layer resource might grow if the specified content
        ///  cannot fit inside it. The layer will grow monotonically on each axis.  If a NULL
        ///  ID2D1Layer is provided, then a layer resource will be allocated automatically.
        /// </summary>
        void PushLayerSTUB();
        //STDMETHOD_(void, PushLayer)(
        //    _In_ CONST D2D1_LAYER_PARAMETERS *layerParameters,
        //_In_opt_ ID2D1Layer *layer 
        //) PURE;

        /// <summary>
        ///  Ends a layer that was defined with particular layer resources.
        /// </summary>
        [PreserveSig]
        void PopLayer();

        void Flush(
            out ulong tag1,
            out ulong tag2);

        /// <summary>
        ///  Gets the current drawing state and saves it into the supplied
        ///  IDrawingStatckBlock.
        /// </summary>
        [PreserveSig]
        void SaveDrawingState(
            IDrawingStateBlock drawingStateBlock);

        /// <summary>
        ///  Copies the state stored in the block interface.
        /// </summary>
        [PreserveSig]
        void RestoreDrawingState(
            IDrawingStateBlock drawingStateBlock);

        /// <summary>
        ///  Pushes a clip. The clip can be antialiased. The clip must be axis aligned. If
        ///  the current world transform is not axis preserving, then the bounding box of the
        ///  transformed clip rect will be used. The clip will remain in effect until a
        ///  PopAxisAligned clip call is made.
        /// </summary>
        [PreserveSig]
        void PushAxisAlignedClip(
            in LtrbRectangleF clipRect,
            AntialiasMode antialiasMode);

        [PreserveSig]
        void PopAxisAlignedClip();

        [PreserveSig]
        unsafe void Clear(
            ColorF* clearColor = null);

        /// <summary>
        ///  Start drawing on this render target. Draw calls can only be issued between a
        ///  BeginDraw and EndDraw call.
        /// </summary>
        [PreserveSig]
        void BeginDraw();

        /// <summary>
        ///  Ends drawing on the render target, error results can be retrieved at this time,
        ///  or when calling flush.
        /// </summary>
        [PreserveSig]
        HResult EndDraw(
            out ulong tag1,
            out ulong tag2);

        [PreserveSig]
        void GetPixelFormat(out PixelFormat pixelFormat);

        /// <summary>
        ///  Sets the DPI on the render target. This results in the render target being
        ///  interpreted to a different scale. Neither DPI can be negative. If zero is
        ///  specified for both, the system DPI is chosen. If one is zero and the other
        ///  unspecified, the DPI is not changed.
        /// </summary>
        [PreserveSig]
        void SetDpi(
            float dpiX,
            float dpiY);

        /// <summary>
        ///  Return the current DPI from the target.
        /// </summary>
        [PreserveSig]
        void GetDpi(
            out float dpiX,
            out float dpiY);

        /// <summary>
        ///  Returns the size of the render target in DIPs.
        /// </summary>
        [PreserveSig]
        void GetSize(out SizeF size);

        /// <summary>
        ///  Returns the size of the render target in pixels.
        /// </summary>
        [PreserveSig]
        void GetPixelSize(out SizeU pixelSize);

        /// <summary>
        ///  Returns the maximum bitmap and render target size that is guaranteed to be
        ///  supported by the render target.
        /// </summary>
        [PreserveSig]
        uint GetMaximumBitmapSize();

        /// <summary>
        ///  Returns true if the given properties are supported by this render target. The
        ///  DPI is ignored. NOTE: If the render target type is software, then neither
        ///  D2D1_FEATURE_LEVEL_9 nor D2D1_FEATURE_LEVEL_10 will be considered to be
        ///  supported.
        /// </summary>
        [PreserveSig]
        IntBoolean IsSupported(
            in RenderTargetProperties renderTargetProperties);
    }

    public static class RenderTargetExtensions
    {
        public unsafe static IBitmap CreateBitmap(this IRenderTarget renderTarget, Size size, in BitmapProperties properties)
            => renderTarget.CreateBitmap(size, null, 0, properties);

        public unsafe static ISolidColorBrush CreateSolidColorBrush(this IRenderTarget renderTarget, in ColorF color)
            => renderTarget.CreateSolidColorBrush(color, null);

        public unsafe static ISolidColorBrush CreateSolidColorBrush(
            this IRenderTarget renderTarget, in ColorF color, in BrushProperties properties)
        {
            fixed (BrushProperties* p = &properties)
                return renderTarget.CreateSolidColorBrush(color, p);
        }

        public unsafe static IBitmapRenderTarget CreateCompatibleRenderTarget(this IRenderTarget renderTarget, SizeF desiredSize)
            => renderTarget.CreateCompatibleRenderTarget(&desiredSize, null, null, CompatibleRenderTargetOptions.None);

        public unsafe static IBitmapBrush CreateBitmapBrush(this IRenderTarget renderTarget, IBitmap bitmap, BitmapBrushProperties bitmapBrushProperties)
            => renderTarget.CreateBitmapBrush(bitmap, &bitmapBrushProperties, null);

        public unsafe static IRadialGradientBrush CreateRadialGradientBrush(
            this IRenderTarget renderTarget,
            in RadialGradientBrushProperties properties,
            IGradientStopCollection gradientStopCollection)
        {
            return renderTarget.CreateRadialGradientBrush(properties, null, gradientStopCollection);
        }

        public unsafe static IGradientStopCollection CreateGradienStopCollection(
            this IRenderTarget renderTarget,
            ReadOnlySpan<GradientStop> gradientStops,
            Gamma gamma = Gamma.ColorSpace_2_2,
            ExtendMode extendMode = ExtendMode.Clamp)
        {
            fixed (GradientStop* gs = &MemoryMarshal.GetReference(gradientStops))
            {
                return renderTarget.CreateGradientStopCollection(gs, (uint)gradientStops.Length, gamma, extendMode);
            }
        }

        public unsafe static void Clear(this IRenderTarget renderTarget)
            => renderTarget.Clear(null);

        public unsafe static void Clear(this IRenderTarget renderTarget, in ColorF color)
        {
            fixed (ColorF* c = &color)
                renderTarget.Clear(c);
        }

        public static SizeF GetSize(this IRenderTarget renderTarget)
        {
            renderTarget.GetSize(out SizeF size);
            return size;
        }

        /// <summary>
        ///  Set the transform to identity.
        /// </summary>
        public static void SetTransform(this IRenderTarget renderTarget)
        {
            Matrix3x2 identity = Matrix3x2.Identity;
            renderTarget.SetTransform(ref identity);
        }

        public static HResult EndDraw(this IRenderTarget renderTarget) => renderTarget.EndDraw(out _, out _);
    }
}
