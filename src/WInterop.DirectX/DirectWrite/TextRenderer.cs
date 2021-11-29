// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="TextRenderer"/> interface represents a set of application-defined
///  callbacks that perform rendering of text, inline objects, and decorations
///  such as underlines. [IDWriteTextRenderer]
/// </summary>
[Guid(InterfaceIds.IID_IDWriteTextRenderer)]
public unsafe readonly struct TextRenderer : TextRenderer.Interface, IDisposable
{
    internal IDWriteTextRenderer* Handle { get; }

    internal TextRenderer(IDWriteTextRenderer* handle) => Handle = handle;

    public void DrawGlyphRun(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        MeasuringMode measuringMode,
        GlyphRun glyphRun,
        GlyphRunDescription glyphRunDescription,
        IntPtr clientDrawingEffect)
    {
        Handle->DrawGlyphRun(
            (void*)clientDrawingContext,
            baselineOrigin.X,
            baselineOrigin.Y,
            (DWRITE_MEASURING_MODE)measuringMode,
            (DWRITE_GLYPH_RUN*)&glyphRun,
            (DWRITE_GLYPH_RUN_DESCRIPTION*)&glyphRunDescription,
            (IUnknown*)(void*)clientDrawingEffect).ThrowIfFailed();
    }

    public void DrawInlineObject(
        IntPtr clientDrawingContext,
        PointF origin,
        InlineObject inlineObject,
        bool isSideways,
        bool isRightToLeft,
        IntPtr clientDrawingEffect)
    {
        Handle->DrawInlineObject(
            (void*)clientDrawingContext,
            origin.X,
            origin.Y,
            inlineObject.Handle,
            isSideways,
            isRightToLeft,
            (IUnknown*)(void*)clientDrawingEffect).ThrowIfFailed();
    }

    public void DrawStrikethrough(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Strikethrough strikethrough,
        IntPtr clientDrawingEffect)
    {
        Handle->DrawStrikethrough(
            (void*)clientDrawingContext,
            baselineOrigin.X,
            baselineOrigin.Y,
            (DWRITE_STRIKETHROUGH*)&strikethrough,
            (IUnknown*)(void*)clientDrawingEffect).ThrowIfFailed();
    }

    public void DrawUnderline(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Underline underline,
        IntPtr clientDrawingEffect)
    {
        Handle->DrawUnderline(
            (void*)clientDrawingContext,
            baselineOrigin.X,
            baselineOrigin.Y,
            (DWRITE_UNDERLINE*)&underline,
            (IUnknown*)(void*)clientDrawingEffect).ThrowIfFailed();
    }

    public Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext)
        => PixelSnapping.From(this).GetCurrentTransform(clientDrawingContext);

    public float GetPixelsPerDip(IntPtr clientDrawingContext)
        => PixelSnapping.From(this).GetPixelsPerDip(clientDrawingContext);

    public bool IsPixelSnappingDisabled(IntPtr clientDrawingContext)
        => PixelSnapping.From(this).IsPixelSnappingDisabled(clientDrawingContext);

    public void Dispose() => Handle->Release();

    internal interface Interface : PixelSnapping.Interface
    {
        /// <summary>
        ///  IDWriteTextLayout::Draw calls this function to instruct the client to
        ///  render a run of glyphs.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to 
        ///  IDWriteTextLayout::Draw.</param>
        /// <param name="baselineOrigin">Origin of the baseline.</param>
        /// <param name="measuringMode">Specifies measuring mode for glyphs in the run.
        ///  Renderer implementations may choose different rendering modes for given measuring modes,
        ///  but best results are seen when the rendering mode matches the corresponding measuring mode:
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL for DWRITE_MEASURING_MODE_NATURAL
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_GDI_CLASSIC for DWRITE_MEASURING_MODE_GDI_CLASSIC
        ///  DWRITE_RENDERING_MODE_CLEARTYPE_GDI_NATURAL for DWRITE_MEASURING_MODE_GDI_NATURAL
        /// </param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        /// <param name="glyphRunDescription">Properties of the characters associated with this run.</param>
        /// <param name="clientDrawingEffect">The drawing effect set in SetDrawingEffect.</param>
        void DrawGlyphRun(
            IntPtr clientDrawingContext,
            PointF baselineOrigin,
            MeasuringMode measuringMode,
            GlyphRun glyphRun,
            GlyphRunDescription glyphRunDescription,
            IntPtr clientDrawingEffect);

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this function to instruct the client to draw
        ///  an underline.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to 
        ///  IDWriteTextLayout::Draw.</param>
        /// <param name="baselineOrigin">Origin of the baseline.</param>
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
            PointF baselineOrigin,
            Underline underline,
            IntPtr clientDrawingEffect);

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
            PointF baselineOrigin,
            Strikethrough strikethrough,
            IntPtr clientDrawingEffect);

        /// <summary>
        ///  IDWriteTextLayout::Draw calls this application callback when it needs to
        ///  draw an inline object.
        /// </summary>
        /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
        /// <param name="origin">The top-left corner of the inline object.</param>
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
            PointF origin,
            InlineObject inlineObject,
            bool isSideways,
            bool isRightToLeft,
            IntPtr clientDrawingEffect);
    }
}
