// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.DirectWrite;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [ID2D1Factory]
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
        ///  Cause the factory to refresh any system metrics that it might have been snapped
        ///  on factory creation.
        /// </summary>
        void ReloadSystemMetrics();

        /// <summary>
        ///  Retrieves the current desktop DPI. To refresh this, call ReloadSystemMetrics.
        ///  Note: this method is deprecated. Use DisplayProperties::LogicalDpi for Windows
        ///  Store Apps and GetDpiForWindow for Win32 Apps.
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
        ///  Create a geometry which holds other geometries.
        /// </summary>
        IGeometryGroup CreateGeometryGroup(
            FillMode fillMode,
            IGeometry[] geometries,
            uint geometriesCount);

        ITransformedGeometry CreateTransformedGeometry(
            IGeometry sourceGeometry,
            ref Matrix3x2 transform);

        /// <summary>
        ///  Returns an initially empty path geometry interface. A geometry sink is created
        ///  off the interface to populate it.
        /// </summary>
        IPathGeometry CreatePathGeometry();

        /// <summary>
        ///  Allows a non-default stroke style to be specified for a given geometry at draw
        ///  time.
        /// </summary>
        unsafe IStrokeStyle CreateStrokeStyle(
            in StrokeStyleProperties strokeStyleProperties,
            float* dashes,
            uint dashesCount);

        /// <summary>
        ///  Creates a new drawing state block, this can be used in subsequent
        ///  SaveDrawingState and RestoreDrawingState operations on the render target.
        /// </summary>
        IDrawingStateBlock CreateDrawingStateBlock(
            in DrawingStateDescription drawingStateDescription,
            IRenderingParams textRenderingParams);

        /// <summary>
        ///  Creates a render target which is a source of bitmaps.
        /// </summary>
        void CreateWicBitmapRenderTargetSTUB();
        //STDMETHOD(CreateWicBitmapRenderTarget)(
        //    _In_ IWICBitmap * target,
        //    _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        //_COM_Outptr_ ID2D1RenderTarget **renderTarget 
        //) PURE;

        /// <summary>
        ///  Creates a render target that appears on the display. [CreateHwndRenderTarget]
        /// </summary>
        IWindowRenderTarget CreateWindowRenderTarget(
            in RenderTargetProperties renderTargetProperties,
            in WindowRenderTargetProperties hwndRenderTargetProperties);

        /// <summary>
        ///  Creates a render target that draws to a DXGI Surface. The device that owns the
        ///  surface is used for rendering.
        /// </summary>
        void CreateDxgiSurfaceRenderTargetSTUB();
        //STDMETHOD(CreateDxgiSurfaceRenderTarget)(
        //    _In_ IDXGISurface * dxgiSurface,
        //    _In_ CONST D2D1_RENDER_TARGET_PROPERTIES *renderTargetProperties,
        //    _COM_Outptr_ ID2D1RenderTarget **renderTarget 
        //    ) PURE;

        // TODO: really ID2D1DCRenderTarget
        /// <summary>
        ///  Creates a render target that draws to a GDI device context.
        /// </summary>
        IRenderTarget CreateDCRenderTarget(
            in RenderTargetProperties renderTargetProperties);
    }

    public static class FactoryExtensions
    {
        public unsafe static IStrokeStyle CreateStrokeStyle(
            this IFactory factory,
            in StrokeStyleProperties strokeStyleProperties)
            => factory.CreateStrokeStyle(strokeStyleProperties, null, 0);
    }
}
