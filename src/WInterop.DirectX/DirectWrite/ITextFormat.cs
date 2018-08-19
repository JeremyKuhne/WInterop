// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    /// The format of text used for text layout. [IDWriteTextFormat]
    /// </summary>
    /// <remarks>
    /// This object may not be thread-safe and it may carry the state of text format change.
    /// </remarks>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteTextFormat),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITextFormat
    {
        /// <summary>
        /// Set alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        /// <param name="textAlignment">Text alignment option</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        void SetTextAlignment(TextAlignment textAlignment);

        /// <summary>
        /// Set alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        /// <param name="paragraphAlignment">Paragraph alignment option</param>
        void SetParagraphAlignment(ParagraphAlignment paragraphAlignment);

        /// <summary>
        /// Set word wrapping option.
        /// </summary>
        /// <param name="wordWrapping">Word wrapping option</param>
        void SetWordWrapping(WordWrapping wordWrapping);

        /// <summary>
        /// Set paragraph reading direction.
        /// </summary>
        /// <param name="readingDirection">Text reading direction</param>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        void SetReadingDirection(ReadingDirection readingDirection);

        /// <summary>
        /// Set paragraph flow direction.
        /// </summary>
        /// <param name="flowDirection">Paragraph flow direction</param>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        void SetFlowDirection(FlowDirection flowDirection);

        /// <summary>
        /// Set incremental tab stop position.
        /// </summary>
        /// <param name="incrementalTabStop">The incremental tab stop value</param>
        void SetIncrementalTabStop(float incrementalTabStop);

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
        void SetTrimming(
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
        void SetLineSpacing(
            LineSpacingMethod lineSpacingMethod,
            float lineSpacing,
            float baseline);

        /// <summary>
        /// Get alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        [PreserveSig]
        TextAlignment GetTextAlignment();

        /// <summary>
        /// Get alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        [PreserveSig]
        ParagraphAlignment GetParagraphAlignment();

        /// <summary>
        /// Get word wrapping option.
        /// </summary>
        [PreserveSig]
        WordWrapping GetWordWrapping();

        /// <summary>
        /// Get paragraph reading direction.
        /// </summary>
        [PreserveSig]
        ReadingDirection GetReadingDirection();

        /// <summary>
        /// Get paragraph flow direction.
        /// </summary>
        [PreserveSig]
        FlowDirection GetFlowDirection();

        /// <summary>
        /// Get incremental tab stop position.
        /// </summary>
        [PreserveSig]
        float GetIncrementalTabStop();

        /// <summary>
        /// Get trimming options for text overflowing the layout width.
        /// </summary>
        /// <param name="trimmingOptions">Text trimming options.</param>
        IInlineObject GetTrimming(out Trimming trimmingOptions);

        /// <summary>
        /// Get line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How line height is determined.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline.</param>
        void GetLineSpacing(
            out LineSpacingMethod lineSpacingMethod,
            out float lineSpacing,
            out float baseline);

        /// <summary>
        /// Get the font collection.
        /// </summary>
        IFontCollection GetFontCollection();

        /// <summary>
        /// Get the length of the font family name, in characters, not including the terminating NULL character.
        /// </summary>
        [PreserveSig]
        uint GetFontFamilyNameLength();

        /// <summary>
        /// Get a copy of the font family name.
        /// </summary>
        /// <param name="fontFamilyName">Character array that receives the current font family name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        unsafe void GetFontFamilyName(
            char* fontFamilyName,
            uint nameSize);

        /// <summary>
        /// Get the font weight.
        /// </summary>
        [PreserveSig]
        FontWeight GetFontWeight();

        /// <summary>
        /// Get the font style.
        /// </summary>
        [PreserveSig]
        FontStyle GetFontStyle();

        /// <summary>
        /// Get the font stretch.
        /// </summary>
        [PreserveSig]
        FontStretch GetFontStretch();

        /// <summary>
        /// Get the font em height.
        /// </summary>
        [PreserveSig]
        float GetFontSize();

        /// <summary>
        /// Get the length of the locale name, in characters, not including the terminating NULL character.
        /// </summary>
        [PreserveSig]
        uint GetLocaleNameLength();

        /// <summary>
        /// Get a copy of the locale name.
        /// </summary>
        /// <param name="localeName">Character array that receives the current locale name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        unsafe void GetLocaleName(
            char* localeName,
            uint nameSize);
    }
}
