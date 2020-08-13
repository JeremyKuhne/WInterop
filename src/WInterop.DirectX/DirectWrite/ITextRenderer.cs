// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="ITextRenderer"/> interface represents a set of application-defined
    ///  callbacks that perform rendering of text, inline objects, and decorations
    ///  such as underlines. [IDWriteTextRenderer]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteTextRenderer),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITextRenderer : IPixelSnapping
    {
        #region IDWritePixelSnapping
        /// <summary>
        ///  Determines whether pixel snapping is disabled. The recommended default is FALSE,
        ///  unless doing animation that requires subpixel vertical placement.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        new IntBoolean IsPixelSnappingDisabled(IntPtr clientDrawingContext);

        /// <summary>
        ///  Gets the current transform that maps abstract coordinates to DIPs,
        ///  which may disable pixel snapping upon any rotation or shear.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        new Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext);

        /// <summary>
        ///  Gets the number of physical pixels per DIP. A DIP (device-independent pixel) is 1/96 inch,
        ///  so the pixelsPerDip value is the number of logical pixels per inch divided by 96 (yielding
        ///  a value of 1 for 96 DPI and 1.25 for 120).
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        /// <param name="pixelsPerDip">Receives the number of physical pixels per DIP.</param>
        new float GetPixelsPerDip(IntPtr clientDrawingContext);
        #endregion

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this function to instruct the client to
        ///  render a run of glyphs.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to 
        ///  IDWriteTextLayout::Draw.</param>
        /// <param name="baselineOriginX">X-coordinate of the baseline.</param>
        /// <param name="baselineOriginY">Y-coordinate of the baseline.</param>
        /// <param name="measuringMode">Specifies measuring mode for glyphs in the run.
        ///  Renderer implementations may choose different rendering modes for given measuring modes,
        ///  but best results are seen when the rendering mode matches the corresponding measuring mode:
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL for DWRITE_MEASURING_MODE_NATURAL
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_GDI_CLASSIC for DWRITE_MEASURING_MODE_GDI_CLASSIC
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_GDI_NATURAL for DWRITE_MEASURING_MODE_GDI_NATURAL
        /// </param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        /// <param name="glyphRunDescription">Properties of the characters associated with this run.</param>
        /// <param name="clientDrawingEffect">The drawing effect set in <see cref="ITextLayout.SetDrawingEffect(object, TextRange)"/></param>
        void DrawGlyphRun(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            MeasuringMode measuringMode,
            in GlyphRun glyphRun,
            in GlyphRunDescription glyphRunDescription,
            [MarshalAs(UnmanagedType.IUnknown)]
            object clientDrawingEffect);

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this function to instruct the client to draw
        ///  an underline.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to 
        ///  IDWriteTextLayout::Draw.</param>
        /// <param name="baselineOriginX">X-coordinate of the baseline.</param>
        /// <param name="baselineOriginY">Y-coordinate of the baseline.</param>
        /// <param name="underline">Underline logical information.</param>
        /// <param name="clientDrawingEffect">The drawing effect set in
        ///  IDWriteTextLayout::SetDrawingEffect.</param>
        /// <remarks>
        ///  A single underline can be broken into multiple calls, depending on
        ///  how the formatting changes attributes. If font sizes/styles change
        ///  within an underline, the thickness and offset will be averaged
        ///  weighted according to characters.
        ///  To get the correct top coordinate of the underline rect, add underline::offset
        ///  to the baseline's Y. Otherwise the underline will be immediately under the text.
        ///  The x coordinate will always be passed as the left side, regardless
        ///  of text directionality. This simplifies drawing and reduces the
        ///  problem of round-off that could potentially cause gaps or a double
        ///  stamped alpha blend. To avoid alpha overlap, round the end points
        ///  to the nearest device pixel.
        /// </remarks>
        void DrawUnderline(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            in Underline underline,
            [MarshalAs(UnmanagedType.IUnknown)]
            object clientDrawingEffect);

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this function to instruct the client to draw
        ///  a strikethrough.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to 
        ///  IDWriteTextLayout::Draw.</param>
        /// <param name="baselineOriginX">X-coordinate of the baseline.</param>
        /// <param name="baselineOriginY">Y-coordinate of the baseline.</param>
        /// <param name="strikethrough">Strikethrough logical information.</param>
        /// <param name="clientDrawingEffect">The drawing effect set in
        ///  IDWriteTextLayout::SetDrawingEffect.</param>
        /// <remarks>
        ///  A single strikethrough can be broken into multiple calls, depending on
        ///  how the formatting changes attributes. Strikethrough is not averaged
        ///  across font sizes/styles changes.
        ///  To get the correct top coordinate of the strikethrough rect,
        ///  add strikethrough::offset to the baseline's Y.
        ///  Like underlines, the x coordinate will always be passed as the left side,
        ///  regardless of text directionality.
        /// </remarks>
        void DrawStrikethrough(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            in Strikethrough strikethrough,
            [MarshalAs(UnmanagedType.IUnknown)]
            object clientDrawingEffect);

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this application callback when it needs to
        ///  draw an inline object.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        /// <param name="originX">X-coordinate at the top-left corner of the inline object.</param>
        /// <param name="originY">Y-coordinate at the top-left corner of the inline object.</param>
        /// <param name="inlineObject">The object set using IDWriteTextLayout::SetInlineObject.</param>
        /// <param name="isSideways">The object should be drawn on its side.</param>
        /// <param name="isRightToLeft">The object is in an right-to-left context and should be drawn flipped.</param>
        /// <param name="clientDrawingEffect">The drawing effect set in
        ///  IDWriteTextLayout::SetDrawingEffect.</param>
        /// <remarks>
        ///  The right-to-left flag is a hint for those cases where it would look
        ///  strange for the image to be shown normally (like an arrow pointing to
        ///  right to indicate a submenu).
        /// </remarks>
        void DrawInlineObject(
            IntPtr clientDrawingContext,
            float originX,
            float originY,
            IInlineObject inlineObject,
            IntBoolean isSideways,
            IntBoolean isRightToLeft,
            [MarshalAs(UnmanagedType.IUnknown)]
            object clientDrawingEffect);
    }
}
