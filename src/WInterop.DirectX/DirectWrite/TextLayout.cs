// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace WInterop.DirectWrite;

/// <summary>
///  The IDWriteTextLayout interface represents a block of text after it has
///  been fully analyzed and formatted. [IDWriteTextLayout]
///
///  All coordinates are in device independent pixels (DIPs).
/// </summary>
[Guid(InterfaceIds.IID_IDWriteTextLayout)]
public readonly unsafe struct TextLayout : TextLayout.Interface, IDisposable
{
    internal readonly IDWriteTextLayout* Handle { get; }

    internal TextLayout(IDWriteTextLayout* handle) => Handle = handle;

    public float MaxWidth
    {
        get => Handle->GetMaxWidth();
        set => Handle->SetMaxWidth(value).ThrowIfFailed();
    }

    public float MaxHeight
    {
        get => Handle->GetMaxHeight();
        set => Handle->SetMaxHeight(value).ThrowIfFailed();
    }

    public TextAlignment TextAlignment
    {
        get => TextFormat.From(this).TextAlignment;
        set => TextFormat.From(this).TextAlignment = value;
    }

    public ParagraphAlignment ParagraphAlignment
    {
        get => TextFormat.From(this).ParagraphAlignment;
        set => TextFormat.From(this).ParagraphAlignment = value;
    }

    public WordWrapping WordWrapping
    {
        get => TextFormat.From(this).WordWrapping;
        set => TextFormat.From(this).WordWrapping = value;
    }

    public ReadingDirection ReadingDirection
    {
        get => TextFormat.From(this).ReadingDirection;
        set => TextFormat.From(this).ReadingDirection = value;
    }

    public FlowDirection FlowDirection
    {
        get => TextFormat.From(this).FlowDirection;
        set => TextFormat.From(this).FlowDirection = value;
    }

    public float IncrementalTabStop
    {
        get => TextFormat.From(this).IncrementalTabStop;
        set => TextFormat.From(this).IncrementalTabStop = value;
    }

    public Trimming Trimming
    {
        get => TextFormat.From(this).Trimming;
        set => TextFormat.From(this).Trimming = value;
    }

    public FontWeight FontWeight => TextFormat.From(this).FontWeight;

    public FontStyle FontStyle => TextFormat.From(this).FontStyle;

    public FontStretch FontStretch => TextFormat.From(this).FontStretch;

    public float FontSize => TextFormat.From(this).FontSize;

    public float DetermineMinWidth()
    {
        float minWidth;
        Handle->DetermineMinWidth(&minWidth).ThrowIfFailed();
        return minWidth;
    }

    public void Dispose() => Handle->Release();

    public void Draw(IntPtr clientDrawingContext, TextRenderer renderer, PointF origin)
    {
        Handle->Draw(
            (void*)clientDrawingContext,
            renderer.Handle,
            origin.X,
            origin.Y).ThrowIfFailed();
    }

    public ClusterMetrics[] GetClusterMetrics()
    {
        uint count;
        var result = Handle->GetClusterMetrics(null, 0, &count);

        if (count == 0)
        {
            return Array.Empty<ClusterMetrics>();
        }

        ClusterMetrics[] metrics = new ClusterMetrics[(int)count];
        fixed (void* m = metrics)
        {
            Handle->GetClusterMetrics((DWRITE_CLUSTER_METRICS*)m, count, &count).ThrowIfFailed();
        }
        return metrics;
    }

    public IntPtr GetDrawingEffect(uint currentPosition)
    {
        IUnknown* effect;
        Handle->GetDrawingEffect(currentPosition, &effect).ThrowIfFailed();
        return (IntPtr)effect;
    }

    public FontCollection GetFontCollection(uint currentPosition)
    {
        IDWriteFontCollection* collection;
        Handle->GetFontCollection(currentPosition, &collection).ThrowIfFailed();
        return new(collection);
    }

    public FontCollection GetFontCollection() => TextFormat.From(this).GetFontCollection();

    public string GetFontFamilyName(uint currentPosition)
    {
        uint length;
        Handle->GetFontFamilyNameLength(currentPosition, &length).ThrowIfFailed();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            Handle->GetFontFamilyName(currentPosition, (ushort*)n, length + 1).ThrowIfFailed();
        }
        return name[..(int)length].ToString();
    }

    public string GetFontFamilyName() => TextFormat.From(this).GetFontFamilyName();

    public float GetFontSize(uint currentPosition)
    {
        float size;
        Handle->GetFontSize(currentPosition, &size).ThrowIfFailed();
        return size;
    }

    public FontStretch GetFontStretch(uint currentPosition)
    {
        FontStretch stretch;
        Handle->GetFontStretch(currentPosition, (DWRITE_FONT_STRETCH*)&stretch).ThrowIfFailed();
        return stretch;
    }

    public FontStyle GetFontStyle(uint currentPosition)
    {
        FontStyle style;
        Handle->GetFontStyle(currentPosition, (DWRITE_FONT_STYLE*)&style).ThrowIfFailed();
        return style;
    }

    public FontWeight GetFontWeight(uint currentPosition)
    {
        FontWeight weight;
        Handle->GetFontWeight(currentPosition, (DWRITE_FONT_WEIGHT*)&weight).ThrowIfFailed();
        return weight;
    }

    public InlineObject GetInlineObject(uint currentPosition)
    {
        IDWriteInlineObject* inlineObject;
        Handle->GetInlineObject(currentPosition, &inlineObject).ThrowIfFailed();
        return new(inlineObject);
    }

    public LineMetrics[] GetLineMetrics()
    {
        uint count;
        var result = Handle->GetLineMetrics(null, 0, &count);
        if (count == 0)
        {
            return Array.Empty<LineMetrics>();
        }

        LineMetrics[] metrics = new LineMetrics[count];
        fixed (void* m = metrics)
        {
            Handle->GetLineMetrics((DWRITE_LINE_METRICS*)m, count, &count).ThrowIfFailed();
        }

        return metrics;
    }

    public void GetLineSpacing(out LineSpacingMethod lineSpacingMethod, out float lineSpacing, out float baseline)
        => TextFormat.From(this).GetLineSpacing(out lineSpacingMethod, out lineSpacing, out baseline);

    public string GetLocaleName(uint currentPosition)
    {
        uint length;
        Handle->GetLocaleNameLength(currentPosition, &length).ThrowIfFailed();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            Handle->GetLocaleName(currentPosition, (ushort*)n, length + 1).ThrowIfFailed();
        }
        return name[..(int)length].ToString();
    }

    public string GetLocaleName() => TextFormat.From(this).GetLocaleName();

    public TextMetrics GetMetrics()
    {
        TextMetrics metrics;
        Handle->GetMetrics((DWRITE_TEXT_METRICS*)&metrics).ThrowIfFailed();
        return metrics;
    }

    public OverhangMetrics GetOverhangMetrics()
    {
        OverhangMetrics metrics;
        Handle->GetOverhangMetrics((DWRITE_OVERHANG_METRICS*)&metrics).ThrowIfFailed();
        return metrics;
    }

    public bool GetStrikethrough(uint currentPosition)
    {
        TerraFX.Interop.Windows.BOOL strikeThrough;
        Handle->GetStrikethrough(currentPosition, &strikeThrough).ThrowIfFailed();
        return strikeThrough;
    }

    public Typography GetTypography(uint currentPosition)
    {
        IDWriteTypography* typography;
        Handle->GetTypography(currentPosition, &typography).ThrowIfFailed();
        return new(typography);
    }

    public bool GetUnderline(uint currentPosition)
    {
        TerraFX.Interop.Windows.BOOL underline;
        Handle->GetUnderline(currentPosition, &underline).ThrowIfFailed();
        return underline;
    }

    public void HitTestPoint(PointF point, out bool isTrailingHit, out bool isInside, out HitTestMetrics hitTestMetrics)
    {
        TerraFX.Interop.Windows.BOOL ith;
        TerraFX.Interop.Windows.BOOL ii;
        fixed (void* htm = &hitTestMetrics)
        {
            Handle->HitTestPoint(point.X, point.Y, &ith, &ii, (DWRITE_HIT_TEST_METRICS*)htm).ThrowIfFailed();
        }

        isTrailingHit = ith;
        isInside = ii;
    }

    public void HitTestTextPosition(uint textPosition, bool isTrailingHit, out PointF point, out HitTestMetrics hitTestMetrics)
    {
        float x;
        float y;
        fixed (void* htm = &hitTestMetrics)
        {
            Handle->HitTestTextPosition(textPosition, isTrailingHit, &x, &y, (DWRITE_HIT_TEST_METRICS*)htm).ThrowIfFailed();
        }

        point = new(x, y);
    }

    public void SetDrawingEffect(IntPtr drawingEffect, TextRange textRange)
        => Handle->SetDrawingEffect((IUnknown*)(void*)drawingEffect, textRange.ToD2D()).ThrowIfFailed();

    public void SetFontCollection(FontCollection fontCollection, TextRange textRange)
        => Handle->SetFontCollection(fontCollection.Handle, textRange.ToD2D()).ThrowIfFailed();

    public void SetFontFamilyName(string fontFamilyName, TextRange textRange)
    {
        fixed (void* n = fontFamilyName)
        {
            Handle->SetFontFamilyName((ushort*)n, textRange.ToD2D()).ThrowIfFailed(); ;
        }
    }

    public void SetFontSize(float fontSize, TextRange textRange)
        => Handle->SetFontSize(fontSize, textRange.ToD2D()).ThrowIfFailed();

    public void SetFontStretch(FontStretch fontStretch, TextRange textRange)
        => Handle->SetFontStretch((DWRITE_FONT_STRETCH)fontStretch, textRange.ToD2D()).ThrowIfFailed();

    public void SetFontStyle(FontStyle fontStyle, TextRange textRange)
        => Handle->SetFontStyle((DWRITE_FONT_STYLE)fontStyle, textRange.ToD2D()).ThrowIfFailed();

    public void SetFontWeight(FontWeight fontWeight, TextRange textRange)
        => Handle->SetFontWeight((DWRITE_FONT_WEIGHT)fontWeight, textRange.ToD2D()).ThrowIfFailed();

    public void SetInlineObject(InlineObject inlineObject, TextRange textRange)
        => Handle->SetInlineObject(inlineObject.Handle, textRange.ToD2D()).ThrowIfFailed();

    public void SetLineSpacing(LineSpacingMethod lineSpacingMethod, float lineSpacing, float baseline)
        => Handle->SetLineSpacing((DWRITE_LINE_SPACING_METHOD)lineSpacingMethod, lineSpacing, baseline).ThrowIfFailed();

    public void SetLocaleName(string localeName, TextRange textRange)
    {
        fixed (void* n = localeName)
        {
            Handle->SetLocaleName((ushort*)n, textRange.ToD2D()).ThrowIfFailed();
        }
    }

    public void SetStrikethrough(bool hasStrikethrough, TextRange textRange)
        => Handle->SetStrikethrough(hasStrikethrough, textRange.ToD2D()).ThrowIfFailed();

    public void SetTypography(Typography typography, TextRange textRange)
        => Handle->SetTypography(typography.Handle, textRange.ToD2D()).ThrowIfFailed();

    public void SetUnderline(bool hasUnderline, TextRange textRange)
        => Handle->SetUnderline(hasUnderline, textRange.ToD2D()).ThrowIfFailed();

    /// <docs>
    ///  https://docs.microsoft.com/windows/win32/api/dwrite/nn-dwrite-idwritetextlayout
    /// </docs>
    internal interface Interface : TextFormat.Interface
    {
        /// <summary>
        ///  Get/set layout maximum width.
        /// </summary>
        float MaxWidth { get; set; }

        /// <summary>
        ///  Get/set layout maximum height
        /// </summary>
        float MaxHeight { get; set; }

        /// <summary>
        ///  Set the font collection.
        /// </summary>
        /// <param name="fontCollection">The font collection to set</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontCollection(
            FontCollection fontCollection,
            TextRange textRange);

        /// <summary>
        ///  Set null-terminated font family name.
        /// </summary>
        /// <param name="fontFamilyName">Font family name</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontFamilyName(
            string fontFamilyName,
            TextRange textRange);

        /// <summary>
        ///  Set font weight.
        /// </summary>
        /// <param name="fontWeight">Font weight</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontWeight(
            FontWeight fontWeight,
            TextRange textRange);

        /// <summary>
        ///  Set font style.
        /// </summary>
        /// <param name="fontStyle">Font style</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontStyle(
            FontStyle fontStyle,
            TextRange textRange);

        /// <summary>
        ///  Set font stretch.
        /// </summary>
        /// <param name="fontStretch">font stretch</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontStretch(
            FontStretch fontStretch,
            TextRange textRange);

        /// <summary>
        ///  Set font em height.
        /// </summary>
        /// <param name="fontSize">Font em height</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontSize(
            float fontSize,
            TextRange textRange);

        /// <summary>
        ///  Set underline.
        /// </summary>
        /// <param name="hasUnderline">The Boolean flag indicates whether underline takes place</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetUnderline(
            bool hasUnderline,
            TextRange textRange);

        /// <summary>
        ///  Set strikethrough.
        /// </summary>
        /// <param name="hasStrikethrough">The Boolean flag indicates whether strikethrough takes place</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetStrikethrough(
            bool hasStrikethrough,
            TextRange textRange);

        /// <summary>
        ///  Set application-defined drawing effect.
        /// </summary>
        /// <param name="drawingEffect">Pointer to an application-defined drawing effect.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        /// <remarks>
        ///  This drawing effect is associated with the specified range and will be passed back
        ///  to the application via the callback when the range is drawn at drawing time.
        /// </remarks>
        void SetDrawingEffect(
            IntPtr drawingEffect,
            TextRange textRange);

        /// <summary>
        ///  Set inline object.
        /// </summary>
        /// <param name="inlineObject">Pointer to an application-implemented inline object.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        /// <remarks>
        ///  This inline object applies to the specified range and will be passed back
        ///  to the application via the DrawInlineObject callback when the range is drawn.
        ///  Any text in that range will be suppressed.
        /// </remarks>
        void SetInlineObject(
            InlineObject inlineObject,
            TextRange textRange);

        /// <summary>
        ///  Set font typography features.
        /// </summary>
        /// <param name="typography">Pointer to font typography setting.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetTypography(
            Typography typography,
            TextRange textRange);

        /// <summary>
        ///  Set locale name.
        /// </summary>
        /// <param name="localeName">Locale name</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetLocaleName(
            string localeName,
            TextRange textRange);

        // TODO: Add overloads that out the TextRange for getters. Presumption is
        // there is extra overhead in calculating the TextRange.

        /// <summary>
        ///  Get the font collection where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        FontCollection GetFontCollection(uint currentPosition);

        /// <summary>
        ///  Copy the font family name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        string GetFontFamilyName(uint currentPosition);

        /// <summary>
        ///  Get the font weight where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        FontWeight GetFontWeight(uint currentPosition);

        /// <summary>
        ///  Get the font style where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        FontStyle GetFontStyle(uint currentPosition);

        /// <summary>
        ///  Get the font stretch where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        FontStretch GetFontStretch(uint currentPosition);

        /// <summary>
        ///  Get the font em height where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        float GetFontSize(uint currentPosition);

        /// <summary>
        ///  Get the underline presence where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        bool GetUnderline(uint currentPosition);

        /// <summary>
        ///  Get the strikethrough presence where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        bool GetStrikethrough(uint currentPosition);

        /// <summary>
        ///  Get the application-defined drawing effect where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        IntPtr GetDrawingEffect(uint currentPosition);

        /// <summary>
        ///  Get the inline object at the given position.
        /// </summary>
        /// <param name="currentPosition">The given text position.</param>
        InlineObject GetInlineObject(uint currentPosition);

        /// <summary>
        ///  Get the typography setting where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        Typography GetTypography(uint currentPosition);

        /// <summary>
        ///  Get the locale name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        string GetLocaleName(uint currentPosition);

        /// <summary>
        ///  Initiate drawing of the text.
        /// </summary>
        /// <param name="clientDrawingContext">An application defined value
        ///  included in rendering callbacks.</param>
        /// <param name="renderer">The set of application-defined callbacks that do
        ///  the actual rendering.</param>
        /// <param name="origin">The layout's top-left side.</param>
        void Draw(
            IntPtr clientDrawingContext,
            TextRenderer renderer,
            PointF origin);

        /// <summary>
        ///  GetLineMetrics returns properties of each line.
        /// </summary>
        /// <param name="lineMetrics">The array to fill with line information.</param>
        /// <param name="maxLineCount">The maximum size of the lineMetrics array.</param>
        /// <param name="actualLineCount">The actual size of the lineMetrics
        ///  array that is needed.</param>
        /// <remarks>
        ///  If maxLineCount is not large enough E_NOT_SUFFICIENT_BUFFER, 
        ///  which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER),
        ///  is returned and *actualLineCount is set to the number of lines
        ///  needed.
        /// </remarks>
        LineMetrics[] GetLineMetrics();

        /// <summary>
        ///  GetMetrics retrieves overall metrics for the formatted string.
        /// </summary>
        /// <remarks>
        ///  Drawing effects like underline and strikethrough do not contribute to the text size, which is essentially
        ///  the sum of advance widths and line heights. Additionally, visible swashes and other graphic adornments
        ///  may extend outside the returned width and height.
        /// </remarks>
        TextMetrics GetMetrics();

        /// <summary>
        ///  GetOverhangMetrics returns the overhangs (in DIPs) of the layout and all objects contained in it,
        ///  including text glyphs and inline objects.
        /// </summary>
        /// <returns>Overshoots of visible extents (in DIPs) outside the layout.</returns>
        /// <remarks>
        ///  Any underline and strikethrough do not contribute to the black box determination, since these are
        ///  actually drawn by the renderer, which is allowed to draw them in any variety of styles.
        /// </remarks>
        OverhangMetrics GetOverhangMetrics();

        /// <summary>
        ///  Retrieve logical properties and measurement of each cluster.
        /// </summary>
        /// <param name="clusterMetrics">The array to fill with cluster information.</param>
        /// <param name="maxClusterCount">The maximum size of the clusterMetrics array.</param>
        /// <param name="actualClusterCount">The actual size of the clusterMetrics array that is needed.</param>
        /// <remarks>
        ///  If maxClusterCount is not large enough E_NOT_SUFFICIENT_BUFFER, 
        ///  which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER), 
        ///  is returned and *actualClusterCount is set to the number of clusters
        ///  needed.
        /// </remarks>
        ClusterMetrics[] GetClusterMetrics();

        /// <summary>
        ///  Determines the minimum possible width the layout can be set to without emergency breaking between
        ///  the characters of whole words.
        /// </summary>
        float DetermineMinWidth();

        /// <summary>
        ///  Given a coordinate (in DIPs) relative to the top-left of the layout box, this returns the corresponding
        ///  hit-test metrics of the text string where the hit-test has occurred. This is useful for mapping mouse
        ///  clicks to caret positions. When the given coordinate is outside the text string, the function sets the
        ///  output value *isInside to false but returns the nearest character position.
        /// </summary>
        /// <param name="point">Point to hit-test, relative to the top-left location of the layout box.</param>
        /// <param name="isTrailingHit">
        ///  Output flag indicating whether the hit-test location is at the leading or the trailing side of the
        ///  character. When <paramref name="isInside"/> is set to false, this value is set according to the output
        ///  *position value to represent the edge closest to the hit-test location.
        /// </param>
        /// <param name="isInside">
        ///  Output flag indicating whether the hit-test location is inside the text string. When false, the position
        ///  nearest the text's edge is returned.</param>
        /// <returns>
        ///  Output geometry fully enclosing the hit-test location. When the output <paramref name="isInside"/> value
        ///  is set to false, this structure represents the geometry enclosing the edge closest to the hit-test
        ///  location.
        /// </returns>
        void HitTestPoint(
            PointF point,
            out bool isTrailingHit,
            out bool isInside,
            out HitTestMetrics hitTestMetrics);

        /// <summary>
        ///  Given a text position and whether the caret is on the leading or trailing
        ///  edge of that position, this returns the corresponding coordinate (in DIPs)
        ///  relative to the top-left of the layout box. This is most useful for drawing
        ///  the caret's current position, but it could also be used to anchor an IME to the
        ///  typed text or attach a floating menu near the point of interest. It may also be
        ///  used to programmatically obtain the geometry of a particular text position
        ///  for UI automation.
        /// </summary>
        /// <param name="textPosition">Text position to get the coordinate of.</param>
        /// <param name="isTrailingHit">Flag indicating whether the location is of the leading or the trailing side of the specified text position. </param>
        /// <param name="point">Output caret point, relative to the top-left of the layout box.</param>
        /// <param name="hitTestMetrics">Output geometry fully enclosing the specified text position.</param>
        /// <remarks>
        ///  When drawing a caret at the returned X,Y, it should be centered on X
        ///  and drawn from the Y coordinate down. The height will be the size of the
        ///  hit-tested text (which can vary in size within a line).
        ///  Reading direction also affects which side of the character the caret is drawn.
        ///  However, the returned X coordinate will be correct for either case.
        ///  You can get a text length back that is larger than a single character.
        ///  This happens for complex scripts when multiple characters form a single cluster,
        ///  when diacritics join their base character, or when you test a surrogate pair.
        /// </remarks>
        void HitTestTextPosition(
            uint textPosition,
            bool isTrailingHit,
            out PointF point,
            out HitTestMetrics hitTestMetrics);

        /*
                    /// <summary>
                    ///  The application calls this function to get a set of hit-test metrics
                    ///  corresponding to a range of text positions. The main usage for this
                    ///  is to draw highlighted selection of the text string.
                    ///
                    ///  The function returns E_NOT_SUFFICIENT_BUFFER, which is equivalent to 
                    ///  HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER), when the buffer size of
                    ///  hitTestMetrics is too small to hold all the regions calculated by the
                    ///  function. In such situation, the function sets the output value
                    ///  *actualHitTestMetricsCount to the number of geometries calculated.
                    ///  The application is responsible to allocate a new buffer of greater
                    ///  size and call the function again.
                    ///
                    ///  A good value to use as an initial value for maxHitTestMetricsCount may
                    ///  be calculated from the following equation:
                    ///     maxHitTestMetricsCount = lineCount * maxBidiReorderingDepth
                    ///
                    ///  where lineCount is obtained from the value of the output argument
                    ///  *actualLineCount from the function IDWriteTextLayout::GetLineMetrics,
                    ///  and the maxBidiReorderingDepth value from the DWRITE_TEXT_METRICS
                    ///  structure of the output argument *textMetrics from the function
                    /// <see cref="IFactory.CreateTextLayout"/>.
                    /// </summary>
                    /// <param name="textPosition">First text position of the specified range.</param>
                    /// <param name="textLength">Number of positions of the specified range.</param>
                    /// <param name="origin">Offset of the origin (left-top of the layout box) which is added to each of the hit-test metrics returned.</param>
                    /// <param name="hitTestMetrics">Pointer to a buffer of the output geometry fully enclosing the specified position range.</param>
                    /// <param name="maxHitTestMetricsCount">Maximum number of distinct metrics it could hold in its buffer memory.</param>
                    /// <param name="actualHitTestMetricsCount">Actual number of metrics returned or needed.</param>
                    /// <remarks>
                    ///  There are no gaps in the returned metrics. While there could be visual gaps,
                    ///  depending on bidi ordering, each range is contiguous and reports all the text,
                    ///  including any hidden characters and trimmed text.
                    ///  The height of each returned range will be the same within each line, regardless
                    ///  of how the font sizes vary.
                    /// </remarks>
                    unsafe void HitTestTextRange(
                        uint textPosition,
                        uint textLength,
                        PointF origin,
                        out HitTestMetrics hitTestMetrics,
                        uint maxHitTestMetricsCount,
                        out uint actualHitTestMetricsCount);
                        */
    }
}

public static class TextLayoutExtensions
{
    public static void SetMaxSize(this TextLayout textLayout, SizeF maxSize)
    {
        textLayout.MaxHeight = maxSize.Height;
        textLayout.MaxWidth = maxSize.Width;
    }
}
