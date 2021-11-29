// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The format of text used for text layout. [IDWriteTextFormat]
/// </summary>
/// <remarks>
///  This object may not be thread-safe and it may carry the state of text format change.
/// </remarks>
[Guid(InterfaceIds.IID_IDWriteTextFormat)]
public readonly unsafe struct TextFormat : TextFormat.Interface, IDisposable
{
    internal IDWriteTextFormat* Handle { get; }

    internal TextFormat(IDWriteTextFormat* handle) => Handle = handle;

    internal static ref TextFormat From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<TextFormat>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public TextAlignment TextAlignment
    {
        get => (TextAlignment)Handle->GetTextAlignment();
        set => Handle->SetTextAlignment((DWRITE_TEXT_ALIGNMENT)value).ThrowIfFailed();
    }

    public ParagraphAlignment ParagraphAlignment
    {
        get => (ParagraphAlignment)Handle->GetParagraphAlignment();
        set => Handle->SetParagraphAlignment((DWRITE_PARAGRAPH_ALIGNMENT)value).ThrowIfFailed();
    }

    public WordWrapping WordWrapping
    {
        get => (WordWrapping)Handle->GetWordWrapping();
        set => Handle->SetWordWrapping((DWRITE_WORD_WRAPPING)value).ThrowIfFailed();
    }

    public ReadingDirection ReadingDirection
    {
        get => (ReadingDirection)Handle->GetReadingDirection();
        set => Handle->SetReadingDirection((DWRITE_READING_DIRECTION)value).ThrowIfFailed();
    }

    public FlowDirection FlowDirection
    {
        get => (FlowDirection)Handle->GetFlowDirection();
        set => Handle->SetFlowDirection((DWRITE_FLOW_DIRECTION)value).ThrowIfFailed();
    }

    public float IncrementalTabStop
    {
        get => Handle->GetIncrementalTabStop();
        set => Handle->SetIncrementalTabStop(value).ThrowIfFailed();
    }

    public Trimming Trimming
    {
        get
        {
            Trimming trimming;
            Handle->GetTrimming((DWRITE_TRIMMING*)&trimming, null).ThrowIfFailed();
            return trimming;
        }
        set => Handle->SetTrimming((DWRITE_TRIMMING*)&value, null).ThrowIfFailed();
    }

    public FontWeight FontWeight => (FontWeight)Handle->GetFontWeight();

    public FontStyle FontStyle => (FontStyle)Handle->GetFontStyle();

    public FontStretch FontStretch => (FontStretch)Handle->GetFontStretch();

    public float FontSize => Handle->GetFontSize();

    public void Dispose() => Handle->Release();

    public FontCollection GetFontCollection()
    {
        IDWriteFontCollection* collection;
        Handle->GetFontCollection(&collection).ThrowIfFailed();
        return new(collection);
    }

    public string GetFontFamilyName()
    {
        uint length = Handle->GetFontFamilyNameLength();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            Handle->GetFontFamilyName((ushort*)n, length + 1).ThrowIfFailed();
        }
        return name[..(int)length].ToString();
    }

    public void GetLineSpacing(out LineSpacingMethod lineSpacingMethod, out float lineSpacing, out float baseline)
    {
        fixed (void* lsm = &lineSpacingMethod)
        fixed (float* ls = &lineSpacing)
        fixed (float* bl = &baseline)
        {
            Handle->GetLineSpacing((DWRITE_LINE_SPACING_METHOD*)lsm, ls, bl).ThrowIfFailed();
        }
    }

    public string GetLocaleName()
    {
        uint length = Handle->GetLocaleNameLength();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            Handle->GetLocaleName((ushort*)n, length + 1).ThrowIfFailed();
        }
        return name[..(int)length].ToString();
    }

    public void SetLineSpacing(LineSpacingMethod lineSpacingMethod, float lineSpacing, float baseline)
        => Handle->SetLineSpacing((DWRITE_LINE_SPACING_METHOD)lineSpacingMethod, lineSpacing, baseline).ThrowIfFailed();

    /// <docs>
    ///  https://docs.microsoft.com/windows/win32/api/dwrite/nn-dwrite-idwritetextformat
    /// </docs>
    internal interface Interface
    {
        /// <summary>
        ///  Gets/sets alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        TextAlignment TextAlignment { get; set; }

        /// <summary>
        ///  Gets/sets alignment option of paragraphs relative to layout box's top and bottom edge.
        /// </summary>
        ParagraphAlignment ParagraphAlignment { get; set; }

        /// <summary>
        ///  Gets/sets the style of word wrapping.
        /// </summary>
        WordWrapping WordWrapping { get; set; }

        /// <summary>
        ///  Gets/sets paragraph reading direction.
        /// </summary>
        /// <remarks>
        ///  <see cref="FlowDirection"/> must be perpendicular to <see cref="ReadingDirection"/>.
        /// </remarks>
        ReadingDirection ReadingDirection { get; set; }

        /// <summary>
        ///  Gets/sets paragraph flow direction.
        /// </summary>
        /// <param name="flowDirection">Paragraph flow direction</param>
        /// <remarks>
        ///  <see cref="FlowDirection"/> must be perpendicular to <see cref="ReadingDirection"/>.
        /// </remarks>
        FlowDirection FlowDirection { get; set; }

        /// <summary>
        ///  Gets/sets incremental tab stop position.
        /// </summary>
        float IncrementalTabStop { get; set; }

        /// <summary>
        ///  Set trimming options for any trailing text exceeding the layout width or for any far text exceeding the
        ///  layout height.
        /// </summary>
        Trimming Trimming { get; set; }

        // (Trimming options, InlineObject sign) TrimmingWithSign { get; set; }

        /// <summary>
        ///  Set line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How to determine line height.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline. A reasonable ratio to lineSpacing is 80%.</param>
        /// <remarks>
        ///  For the default method, spacing depends solely on the content. For uniform spacing, the given line height
        ///  will override the content.
        /// </remarks>
        void SetLineSpacing(
            LineSpacingMethod lineSpacingMethod,
            float lineSpacing,
            float baseline);

        /// <summary>
        ///  Get line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How line height is determined.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline.</param>
        void GetLineSpacing(
            out LineSpacingMethod lineSpacingMethod,
            out float lineSpacing,
            out float baseline);

        /// <summary>
        ///  Get the font collection.
        /// </summary>
        FontCollection GetFontCollection();

        /// <summary>
        ///  Get a copy of the font family name.
        /// </summary>
        string GetFontFamilyName();

        /// <summary>
        ///  Get the font weight.
        /// </summary>
        FontWeight FontWeight { get; }

        /// <summary>
        ///  Get the font style.
        /// </summary>
        FontStyle FontStyle { get; }

        /// <summary>
        ///  Get the font stretch.
        /// </summary>
        FontStretch FontStretch { get; }

        /// <summary>
        ///  Get the font em height.
        /// </summary>
        float FontSize { get; }

        /// <summary>
        ///  Get the locale name.
        /// </summary>
        string GetLocaleName();
    }
}
