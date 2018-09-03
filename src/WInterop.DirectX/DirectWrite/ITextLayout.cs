// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    /// The IDWriteTextLayout interface represents a block of text after it has
    /// been fully analyzed and formatted. [IDWriteTextLayout]
    ///
    /// All coordinates are in device independent pixels (DIPs).
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteTextLayout),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITextLayout : ITextFormat
    {
        #region IID_IDWriteTextFormat
        /// <summary>
        /// Set alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        /// <param name="textAlignment">Text alignment option</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        new void SetTextAlignment(TextAlignment textAlignment);

        /// <summary>
        /// Set alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        /// <param name="paragraphAlignment">Paragraph alignment option</param>
        new void SetParagraphAlignment(ParagraphAlignment paragraphAlignment);

        /// <summary>
        /// Set word wrapping option.
        /// </summary>
        /// <param name="wordWrapping">Word wrapping option</param>
        new void SetWordWrapping(WordWrapping wordWrapping);

        /// <summary>
        /// Set paragraph reading direction.
        /// </summary>
        /// <param name="readingDirection">Text reading direction</param>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        new void SetReadingDirection(ReadingDirection readingDirection);

        /// <summary>
        /// Set paragraph flow direction.
        /// </summary>
        /// <param name="flowDirection">Paragraph flow direction</param>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        new void SetFlowDirection(FlowDirection flowDirection);

        /// <summary>
        /// Set incremental tab stop position.
        /// </summary>
        /// <param name="incrementalTabStop">The incremental tab stop value</param>
        new void SetIncrementalTabStop(float incrementalTabStop);

        /// <summary>
        /// Set trimming options for any trailing text exceeding the layout width
        /// or for any far text exceeding the layout height.
        /// </summary>
        /// <param name="trimmingOptions">Text trimming options.</param>
        /// <param name="trimmingSign">Application-defined omission sign. This parameter may be NULL if no trimming sign is desired.</param>
        /// <remarks>
        /// Any inline object can be used for the trimming sign, but CreateEllipsisTrimmingSign
        /// provides a typical ellipsis symbol. Trimming is also useful vertically for hiding
        /// partial lines.
        /// </remarks>
        new void SetTrimming(
            in Trimming trimmingOptions,
            IInlineObject trimmingSign);

        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How to determine line height.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline. A reasonable ratio to lineSpacing is 80%.</param>
        /// <remarks>
        /// For the default method, spacing depends solely on the content.
        /// For uniform spacing, the given line height will override the content.
        /// </remarks>
        new void SetLineSpacing(
            LineSpacingMethod lineSpacingMethod,
            float lineSpacing,
            float baseline);

        /// <summary>
        /// Get alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        [PreserveSig]
        new TextAlignment GetTextAlignment();

        /// <summary>
        /// Get alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        [PreserveSig]
        new ParagraphAlignment GetParagraphAlignment();

        /// <summary>
        /// Get word wrapping option.
        /// </summary>
        [PreserveSig]
        new WordWrapping GetWordWrapping();

        /// <summary>
        /// Get paragraph reading direction.
        /// </summary>
        [PreserveSig]
        new ReadingDirection GetReadingDirection();

        /// <summary>
        /// Get paragraph flow direction.
        /// </summary>
        [PreserveSig]
        new FlowDirection GetFlowDirection();

        /// <summary>
        /// Get incremental tab stop position.
        /// </summary>
        [PreserveSig]
        new float GetIncrementalTabStop();

        /// <summary>
        /// Get trimming options for text overflowing the layout width.
        /// </summary>
        /// <param name="trimmingOptions">Text trimming options.</param>
        new IInlineObject GetTrimming(out Trimming trimmingOptions);

        /// <summary>
        /// Get line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How line height is determined.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline.</param>
        new void GetLineSpacing(
            out LineSpacingMethod lineSpacingMethod,
            out float lineSpacing,
            out float baseline);

        /// <summary>
        /// Get the font collection.
        /// </summary>
        new IFontCollection GetFontCollection();

        /// <summary>
        /// Get the length of the font family name, in characters, not including the terminating NULL character.
        /// </summary>
        [PreserveSig]
        new uint GetFontFamilyNameLength();

        /// <summary>
        /// Get a copy of the font family name.
        /// </summary>
        /// <param name="fontFamilyName">Character array that receives the current font family name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        new unsafe void GetFontFamilyName(
            char* fontFamilyName,
            uint nameSize);

        /// <summary>
        /// Get the font weight.
        /// </summary>
        [PreserveSig]
        new FontWeight GetFontWeight();

        /// <summary>
        /// Get the font style.
        /// </summary>
        [PreserveSig]
        new FontStyle GetFontStyle();

        /// <summary>
        /// Get the font stretch.
        /// </summary>
        [PreserveSig]
        new FontStretch GetFontStretch();

        /// <summary>
        /// Get the font em height.
        /// </summary>
        [PreserveSig]
        new float GetFontSize();

        /// <summary>
        /// Get the length of the locale name, in characters, not including the terminating NULL character.
        /// </summary>
        [PreserveSig]
        new uint GetLocaleNameLength();

        /// <summary>
        /// Get a copy of the locale name.
        /// </summary>
        /// <param name="localeName">Character array that receives the current locale name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        new unsafe void GetLocaleName(
            char* localeName,
            uint nameSize);
        #endregion

        /// <summary>
        /// Set layout maximum width
        /// </summary>
        /// <param name="maxWidth">Layout maximum width</param>
        void SetMaxWidth(float maxWidth);

        /// <summary>
        /// Set layout maximum height
        /// </summary>
        /// <param name="maxHeight">Layout maximum height</param>
        void SetMaxHeight(float maxHeight);

        /// <summary>
        /// Set the font collection.
        /// </summary>
        /// <param name="fontCollection">The font collection to set</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontCollection(
            IFontCollection fontCollection,
            TextRange textRange);

        /// <summary>
        /// Set null-terminated font family name.
        /// </summary>
        /// <param name="fontFamilyName">Font family name</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontFamilyName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string fontFamilyName,
            TextRange textRange);

        /// <summary>
        /// Set font weight.
        /// </summary>
        /// <param name="fontWeight">Font weight</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontWeight(
            FontWeight fontWeight,
            TextRange textRange);

        /// <summary>
        /// Set font style.
        /// </summary>
        /// <param name="fontStyle">Font style</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontStyle(
            FontStyle fontStyle,
            TextRange textRange);

        /// <summary>
        /// Set font stretch.
        /// </summary>
        /// <param name="fontStretch">font stretch</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontStretch(
            FontStretch fontStretch,
            TextRange textRange);

        /// <summary>
        /// Set font em height.
        /// </summary>
        /// <param name="fontSize">Font em height</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetFontSize(
            float fontSize,
            TextRange textRange);

        /// <summary>
        /// Set underline.
        /// </summary>
        /// <param name="hasUnderline">The Boolean flag indicates whether underline takes place</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetUnderline(
            Boolean32 hasUnderline,
            TextRange textRange);

        /// <summary>
        /// Set strikethrough.
        /// </summary>
        /// <param name="hasStrikethrough">The Boolean flag indicates whether strikethrough takes place</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetStrikethrough(
            Boolean32 hasStrikethrough,
            TextRange textRange);

        /// <summary>
        /// Set application-defined drawing effect.
        /// </summary>
        /// <param name="drawingEffect">Pointer to an application-defined drawing effect.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        /// <remarks>
        /// This drawing effect is associated with the specified range and will be passed back
        /// to the application via the callback when the range is drawn at drawing time.
        /// </remarks>
        void SetDrawingEffect(
            [MarshalAs(UnmanagedType.IUnknown)]
            object drawingEffect,
            TextRange textRange);

        /// <summary>
        /// Set inline object.
        /// </summary>
        /// <param name="inlineObject">Pointer to an application-implemented inline object.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        /// <remarks>
        /// This inline object applies to the specified range and will be passed back
        /// to the application via the DrawInlineObject callback when the range is drawn.
        /// Any text in that range will be suppressed.
        /// </remarks>
        void SetInlineObject(
            IInlineObject inlineObject,
            TextRange textRange);

        /// <summary>
        /// Set font typography features.
        /// </summary>
        /// <param name="typography">Pointer to font typography setting.</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetTypography(
            ITypography typography,
            TextRange textRange);

        /// <summary>
        /// Set locale name.
        /// </summary>
        /// <param name="localeName">Locale name</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        void SetLocaleName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string localeName,
            TextRange textRange);

        /// <summary>
        /// Get layout maximum width
        /// </summary>
        [PreserveSig]
        float GetMaxWidth();

        /// <summary>
        /// Get layout maximum height
        /// </summary>
        [PreserveSig]
        float GetMaxHeight();

        /// <summary>
        /// Get the font collection where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontCollection">The current font collection</param>
        /// <param name="textRange">Text range to which this change applies.</param>
        unsafe void GetFontCollection(
            uint currentPosition,
            out IFontCollection fontCollection,
            TextRange* textRange);

        /// <summary>
        /// Get the length of the font family name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="nameLength">Size of the character array in character count not including the terminated NULL character.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetFontFamilyNameLength(
            uint currentPosition,
            out uint nameLength,
            TextRange* textRange);

        /// <summary>
        /// Copy the font family name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontFamilyName">Character array that receives the current font family name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetFontFamilyName(
            uint currentPosition,
            char* fontFamilyName,
            uint nameSize,
            TextRange* textRange);

        /// <summary>
        /// Get the font weight where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontWeight">The current font weight</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetFontWeight(
            uint currentPosition,
            out FontWeight fontWeight,
            TextRange* textRange);

        /// <summary>
        /// Get the font style where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontStyle">The current font style</param>
        /// <param name="textRange">The position range of the current format.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        unsafe void GetFontStyle(
            uint currentPosition,
            out FontStyle fontStyle,
            TextRange* textRange);

        /// <summary>
        /// Get the font stretch where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontStretch">The current font stretch</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetFontStretch(
            uint currentPosition,
            out FontStretch fontStretch,
            TextRange* textRange);

        /// <summary>
        /// Get the font em height where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="fontSize">The current font em height</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetFontSize(
            uint currentPosition,
            out float fontSize,
            TextRange* textRange);

        /// <summary>
        /// Get the underline presence where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="hasUnderline">The Boolean flag indicates whether text is underlined.</param>
        /// <param name="textRange">The position range of the current format.</param>

        unsafe void GetUnderline(
            uint currentPosition,
            out Boolean32 hasUnderline,
            TextRange* textRange);

        /// <summary>
        /// Get the strikethrough presence where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="hasStrikethrough">The Boolean flag indicates whether text has strikethrough.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetStrikethrough(
            uint currentPosition,
            out Boolean32 hasStrikethrough,
            TextRange* textRange);

        /// <summary>
        /// Get the application-defined drawing effect where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="drawingEffect">The current application-defined drawing effect.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetDrawingEffect(
            uint currentPosition,
            [MarshalAs(UnmanagedType.IUnknown)]
            out object drawingEffect,
            TextRange* textRange);

        /// <summary>
        /// Get the inline object at the given position.
        /// </summary>
        /// <param name="currentPosition">The given text position.</param>
        /// <param name="inlineObject">The inline object.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetInlineObject(
            uint currentPosition,
            out IInlineObject inlineObject,
            TextRange* textRange);

        /// <summary>
        /// Get the typography setting where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="typography">The current typography setting.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetTypography(
            uint currentPosition,
            out ITypography typography,
            TextRange* textRange);

        /// <summary>
        /// Get the length of the locale name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="nameLength">Size of the character array in character count not including the terminated NULL character.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetLocaleNameLength(
            uint currentPosition,
            out uint nameLength,
            TextRange* textRange);

        /// <summary>
        /// Get the locale name where the current position is at.
        /// </summary>
        /// <param name="currentPosition">The current text position.</param>
        /// <param name="localeName">Character array that receives the current locale name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        /// <param name="textRange">The position range of the current format.</param>
        unsafe void GetLocaleName(
            uint currentPosition,
            char* localeName,
            uint nameSize,
            TextRange* textRange);

        /// <summary>
        /// Initiate drawing of the text.
        /// </summary>
        /// <param name="clientDrawingContext">An application defined value
        /// included in rendering callbacks.</param>
        /// <param name="renderer">The set of application-defined callbacks that do
        /// the actual rendering.</param>
        /// <param name="originX">X-coordinate of the layout's left side.</param>
        /// <param name="originY">Y-coordinate of the layout's top side.</param>
        void Draw(
            IntPtr clientDrawingContext,
            ITextRenderer renderer,
            float originX,
            float originY);

        /// <summary>
        /// GetLineMetrics returns properties of each line.
        /// </summary>
        /// <param name="lineMetrics">The array to fill with line information.</param>
        /// <param name="maxLineCount">The maximum size of the lineMetrics array.</param>
        /// <param name="actualLineCount">The actual size of the lineMetrics
        /// array that is needed.</param>
        /// <remarks>
        /// If maxLineCount is not large enough E_NOT_SUFFICIENT_BUFFER, 
        /// which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER),
        /// is returned and *actualLineCount is set to the number of lines
        /// needed.
        /// </remarks>
        unsafe void GetLineMetrics(
            LineMetrics* lineMetrics,
            uint maxLineCount,
            out uint actualLineCount);

        /// <summary>
        /// GetMetrics retrieves overall metrics for the formatted string.
        /// </summary>
        /// <param name="textMetrics">The returned metrics.</param>
        /// <remarks>
        /// Drawing effects like underline and strikethrough do not contribute
        /// to the text size, which is essentially the sum of advance widths and
        /// line heights. Additionally, visible swashes and other graphic
        /// adornments may extend outside the returned width and height.
        /// </remarks>
        TextMetrics GetMetrics();

        /// <summary>
        /// GetOverhangMetrics returns the overhangs (in DIPs) of the layout and all
        /// objects contained in it, including text glyphs and inline objects.
        /// </summary>
        /// <param name="overhangs">Overshoots of visible extents (in DIPs) outside the layout.</param>
        /// <remarks>
        /// Any underline and strikethrough do not contribute to the black box
        /// determination, since these are actually drawn by the renderer, which
        /// is allowed to draw them in any variety of styles.
        /// </remarks>
        OverhangMetrics GetOverhangMetrics();

        /// <summary>
        /// Retrieve logical properties and measurement of each cluster.
        /// </summary>
        /// <param name="clusterMetrics">The array to fill with cluster information.</param>
        /// <param name="maxClusterCount">The maximum size of the clusterMetrics array.</param>
        /// <param name="actualClusterCount">The actual size of the clusterMetrics array that is needed.</param>
        /// <remarks>
        /// If maxClusterCount is not large enough E_NOT_SUFFICIENT_BUFFER, 
        /// which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER), 
        /// is returned and *actualClusterCount is set to the number of clusters
        /// needed.
        /// </remarks>
        unsafe void GetClusterMetrics(
            ClusterMetrics* clusterMetrics,
            uint maxClusterCount,
            out uint actualClusterCount);

        /// <summary>
        /// Determines the minimum possible width the layout can be set to without
        /// emergency breaking between the characters of whole words.
        /// </summary>
        /// <param name="minWidth">Minimum width.</param>
        float DetermineMinWidth();

        /// <summary>
        /// Given a coordinate (in DIPs) relative to the top-left of the layout box,
        /// this returns the corresponding hit-test metrics of the text string where
        /// the hit-test has occurred. This is useful for mapping mouse clicks to caret
        /// positions. When the given coordinate is outside the text string, the function
        /// sets the output value *isInside to false but returns the nearest character
        /// position.
        /// </summary>
        /// <param name="pointX">X coordinate to hit-test, relative to the top-left location of the layout box.</param>
        /// <param name="pointY">Y coordinate to hit-test, relative to the top-left location of the layout box.</param>
        /// <param name="isTrailingHit">Output flag indicating whether the hit-test location is at the leading or the trailing
        ///     side of the character. When the output *isInside value is set to false, this value is set according to the output
        ///     *position value to represent the edge closest to the hit-test location. </param>
        /// <param name="isInside">Output flag indicating whether the hit-test location is inside the text string.
        ///     When false, the position nearest the text's edge is returned.</param>
        /// <param name="hitTestMetrics">Output geometry fully enclosing the hit-test location. When the output *isInside value
        ///     is set to false, this structure represents the geometry enclosing the edge closest to the hit-test location.</param>
        HitTestMetrics HitTestPoint(
            float pointX,
            float pointY,
            out Boolean32 isTrailingHit,
            out Boolean32 isInside);

        /// <summary>
        /// Given a text position and whether the caret is on the leading or trailing
        /// edge of that position, this returns the corresponding coordinate (in DIPs)
        /// relative to the top-left of the layout box. This is most useful for drawing
        /// the caret's current position, but it could also be used to anchor an IME to the
        /// typed text or attach a floating menu near the point of interest. It may also be
        /// used to programmatically obtain the geometry of a particular text position
        /// for UI automation.
        /// </summary>
        /// <param name="textPosition">Text position to get the coordinate of.</param>
        /// <param name="isTrailingHit">Flag indicating whether the location is of the leading or the trailing side of the specified text position. </param>
        /// <param name="pointX">Output caret X, relative to the top-left of the layout box.</param>
        /// <param name="pointY">Output caret Y, relative to the top-left of the layout box.</param>
        /// <param name="hitTestMetrics">Output geometry fully enclosing the specified text position.</param>
        /// <remarks>
        /// When drawing a caret at the returned X,Y, it should be centered on X
        /// and drawn from the Y coordinate down. The height will be the size of the
        /// hit-tested text (which can vary in size within a line).
        /// Reading direction also affects which side of the character the caret is drawn.
        /// However, the returned X coordinate will be correct for either case.
        /// You can get a text length back that is larger than a single character.
        /// This happens for complex scripts when multiple characters form a single cluster,
        /// when diacritics join their base character, or when you test a surrogate pair.
        /// </remarks>
        HitTestMetrics HitTestTextPosition(
            uint textPosition,
            Boolean32 isTrailingHit,
            out float pointX,
            out float pointY);

        /// <summary>
        /// The application calls this function to get a set of hit-test metrics
        /// corresponding to a range of text positions. The main usage for this
        /// is to draw highlighted selection of the text string.
        ///
        /// The function returns E_NOT_SUFFICIENT_BUFFER, which is equivalent to 
        /// HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER), when the buffer size of
        /// hitTestMetrics is too small to hold all the regions calculated by the
        /// function. In such situation, the function sets the output value
        /// *actualHitTestMetricsCount to the number of geometries calculated.
        /// The application is responsible to allocate a new buffer of greater
        /// size and call the function again.
        ///
        /// A good value to use as an initial value for maxHitTestMetricsCount may
        /// be calculated from the following equation:
        ///     maxHitTestMetricsCount = lineCount * maxBidiReorderingDepth
        ///
        /// where lineCount is obtained from the value of the output argument
        /// *actualLineCount from the function IDWriteTextLayout::GetLineMetrics,
        /// and the maxBidiReorderingDepth value from the DWRITE_TEXT_METRICS
        /// structure of the output argument *textMetrics from the function
        /// <see cref="IFactory.CreateTextLayout"/>.
        /// </summary>
        /// <param name="textPosition">First text position of the specified range.</param>
        /// <param name="textLength">Number of positions of the specified range.</param>
        /// <param name="originX">Offset of the X origin (left of the layout box) which is added to each of the hit-test metrics returned.</param>
        /// <param name="originY">Offset of the Y origin (top of the layout box) which is added to each of the hit-test metrics returned.</param>
        /// <param name="hitTestMetrics">Pointer to a buffer of the output geometry fully enclosing the specified position range.</param>
        /// <param name="maxHitTestMetricsCount">Maximum number of distinct metrics it could hold in its buffer memory.</param>
        /// <param name="actualHitTestMetricsCount">Actual number of metrics returned or needed.</param>
        /// <remarks>
        /// There are no gaps in the returned metrics. While there could be visual gaps,
        /// depending on bidi ordering, each range is contiguous and reports all the text,
        /// including any hidden characters and trimmed text.
        /// The height of each returned range will be the same within each line, regardless
        /// of how the font sizes vary.
        /// </remarks>
        unsafe void HitTestTextRange(
            uint textPosition,
            uint textLength,
            float originX,
            float originY,
            HitTestMetrics* hitTestMetrics,
            uint maxHitTestMetricsCount,
            out uint actualHitTestMetricsCount);
    }

    public static class TextLayoutExtensions
    {
        public static void SetMaxSize(this ITextLayout textLayout, SizeF maxSize)
        {
            textLayout.SetMaxHeight(maxSize.Height);
            textLayout.SetMaxWidth(maxSize.Width);
        }
    }
}
