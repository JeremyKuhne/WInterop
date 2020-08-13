// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.Direct2d;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  This interface exposes various font data such as metrics, names, and glyph outlines.
    ///  It contains font face type, appropriate file references and face identification data.
    ///  [IDWriteFontFace]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteFontFace),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFontFace
    {
        /// <summary>
        ///  Obtains the file format type of a font face.
        /// </summary>
        [PreserveSig]
        FontFaceType GetType();

        /// <summary>
        ///  Obtains the font files representing a font face.
        /// </summary>
        /// <param name="numberOfFiles">The number of files representing the font face.</param>
        /// <param name="fontFiles">User provided array that stores pointers to font files representing the font face.
        ///  This parameter can be NULL if the user is only interested in the number of files representing the font face.
        ///  This API increments reference count of the font file pointers returned according to COM conventions, and the client
        ///  should release them when finished.</param>
        /// <returns>
        ///  Standard HRESULT error code.
        /// </returns>
        void GetFilesSTUB();
        //STDMETHOD(GetFiles)(
        //    _Inout_ UINT32* numberOfFiles,
        //    _Out_writes_opt_(*numberOfFiles) IDWriteFontFile** fontFiles
        //) PURE;

        /// <summary>
        ///  Obtains the zero-based index of the font face in its font file or files. If the font files contain a single face,
        ///  the return value is zero.
        /// </summary>
        [PreserveSig]
        uint GetIndex();

        /// <summary>
        ///  Obtains the algorithmic style simulation flags of a font face.
        /// </summary>
        [PreserveSig]
        FontSimulations GetSimulations();

        /// <summary>
        ///  Determines whether the font is a symbol font.
        /// </summary>
        [PreserveSig]
        IntBoolean IsSymbolFont();

        /// <summary>
        ///  Obtains design units and common metrics for the font face.
        ///  These metrics are applicable to all the glyphs within a fontface and are used by applications for layout calculations.
        /// </summary>
        /// <param name="fontFaceMetrics">Points to a DWRITE_FONT_METRICS structure to fill in.
        ///  The metrics returned by this function are in font design units.</param>
        [PreserveSig]
        void GetMetrics(out FontMetrics fontFaceMetrics);

        /// <summary>
        ///  Obtains the number of glyphs in the font face.
        /// </summary>
        [PreserveSig]
        ushort GetGlyphCount();

        ///// <param name="glyphIndices">An array of glyph indices to compute the metrics for.</param>
        ///// <param name="glyphCount">The number of elements in the glyphIndices array.</param>
        ///// <param name="glyphMetrics">Array of DWRITE_GLYPH_METRICS structures filled by this function.
        /////  The metrics returned by this function are in font design units.</param>
        ///// <param name="isSideways">Indicates whether the font is being used in a sideways run.
        /////  This can affect the glyph metrics if the font has oblique simulation
        /////  because sideways oblique simulation differs from non-sideways oblique simulation.</param>
        /// <summary>
        ///  Obtains ideal glyph metrics in font design units. Design glyphs metrics are used for glyph positioning.
        /// </summary>
        /// <returns>
        ///  Standard HRESULT error code. If any of the input glyph indices are outside of the valid glyph index range
        ///  for the current font face, E_INVALIDARG will be returned.
        /// </returns>
        void GetDesignGlyphMetricsSTUB();
        //STDMETHOD(GetDesignGlyphMetrics)(
        //    _In_reads_(glyphCount) UINT16 const* glyphIndices,
        //    UINT32 glyphCount,
        //_Out_writes_(glyphCount) DWRITE_GLYPH_METRICS* glyphMetrics,
        //BOOL isSideways = FALSE
        //) PURE;

        /// <summary>
        ///  Returns the nominal mapping of UTF-32 Unicode code points to glyph indices as defined by the font 'cmap'
        ///  table. Note that this mapping is primarily provided for line layout engines built on top of the physical
        ///  font API. Because of OpenType glyph substitution and line layout character substitution, the nominal
        ///  conversion does not always correspond to how a Unicode string will map to glyph indices when rendering
        ///  using a particular font face. Also, note that Unicode Variation Selectors provide for alternate mappings
        ///  for character to glyph. This call will always return the default variant.
        /// </summary>
        /// <param name="codePoints">An array of UTF-32 code points to obtain nominal glyph indices from.</param>
        /// <param name="codePointCount">The number of elements in the codePoints array.</param>
        /// <param name="glyphIndices">Array of nominal glyph indices filled by this function.</param>
        /// <returns>
        ///  Standard HRESULT error code.
        /// </returns>
        unsafe void GetGlyphIndices(
            uint* codePoints,
            uint codePointCount,
            ushort* glyphIndices);

        /// <summary>
        ///  Finds the specified OpenType font table if it exists and returns a pointer to it.
        ///  The function accesses the underlying font data via the IDWriteFontFileStream interface
        ///  implemented by the font file loader.
        /// </summary>
        /// <param name="openTypeTableTag">Four character tag of table to find.
        ///     Use the DWRITE_MAKE_OPENTYPE_TAG() macro to create it.
        ///     Unlike GDI, it does not support the special TTCF and null tags to access the whole font.</param>
        /// <param name="tableData">
        ///     Pointer to base of table in memory.
        ///     The pointer is only valid so long as the FontFace used to get the font table still exists
        ///     (not any other FontFace, even if it actually refers to the same physical font).
        /// </param>
        /// <param name="tableSize">Byte size of table.</param>
        /// <param name="tableContext">
        ///     Opaque context which must be freed by calling ReleaseFontTable.
        ///     The context actually comes from the lower level IDWriteFontFileStream,
        ///     which may be implemented by the application or DWrite itself.
        ///     It is possible for a NULL tableContext to be returned, especially if
        ///     the implementation directly memory maps the whole file.
        ///     Nevertheless, always release it later, and do not use it as a test for function success.
        ///     The same table can be queried multiple times,
        ///     but each returned context can be different, so release each separately.
        /// </param>
        /// <param name="exists">True if table exists.</param>
        /// <returns>
        ///  Standard HRESULT error code.
        ///  If a table can not be found, the function will not return an error, but the size will be 0, table NULL, and exists = FALSE.
        ///  The context does not need to be freed if the table was not found.
        /// </returns>
        /// <remarks>
        ///  The context for the same tag may be different for each call,
        ///  so each one must be held and released separately.
        /// </remarks>
        unsafe IntBoolean TryGetFontTable(
            uint openTypeTableTag,
            void** tableData,
            out uint tableSize,
            out IntPtr tableContext,
            out IntBoolean exists);

        /// <summary>
        ///  Releases the table obtained earlier from TryGetFontTable.
        /// </summary>
        /// <param name="tableContext">Opaque context from TryGetFontTable.</param>
        [PreserveSig]
        void ReleaseFontTable(IntPtr tableContext);

        /// <summary>
        ///  Computes the outline of a run of glyphs by calling back to the outline sink interface.
        /// </summary>
        /// <param name="emSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="glyphIndices">Array of glyph indices.</param>
        /// <param name="glyphAdvances">Optional array of glyph advances in DIPs.</param>
        /// <param name="glyphOffsets">Optional array of glyph offsets.</param>
        /// <param name="glyphCount">Number of glyphs.</param>
        /// <param name="isSideways">If true, specifies that glyphs are rotated 90 degrees to the left and vertical metrics are used.
        ///  A client can render a vertical run by specifying isSideways = true and rotating the resulting geometry 90 degrees to the
        ///  right using a transform.</param>
        /// <param name="isRightToLeft">If true, specifies that the advance direction is right to left. By default, the advance direction
        ///  is left to right.</param>
        /// <param name="geometrySink">Interface the function calls back to draw each element of the geometry.</param>
        unsafe void GetGlyphRunOutline(
            float emSize,
            ushort* glyphIndices,
            float* glyphAdvances,
            GlyphOffset* glyphOffsets,
            uint glyphCount,
            IntBoolean isSideways,
            IntBoolean isRightToLeft,
            IGeometrySink geometrySink);

        /// <summary>
        ///  Determines the recommended rendering mode for the font given the specified size and rendering parameters.
        /// </summary>
        /// <param name="emSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if the DPI of the rendering surface is 96 this 
        ///  value is 1.0f. If the DPI is 120, this value is 120.0f/96.</param>
        /// <param name="measuringMode">Specifies measuring mode that will be used for glyphs in the font.
        ///  Renderer implementations may choose different rendering modes for given measuring modes, but
        ///  best results are seen when the corresponding modes match:
        /// <see cref="RenderingMode.Natural"/> for <see cref="MeasuringMode.Natural"/>
        /// <see cref="RenderingMode.GdiClassic"/> for <see cref="MeasuringMode.GdiClassic"/>
        /// <see cref="RenderingMode.GdiNatural"/> for <see cref="MeasuringMode.GdiNatural"/>
        /// </param>
        /// <param name="renderingParams">Rendering parameters object. This parameter is necessary in case the rendering parameters 
        ///  object overrides the rendering mode.</param>
        /// <returns>
        ///  The recommended rendering mode to use.
        /// </returns>
        RenderingMode GetRecommendedRenderingMode(
            float emSize,
            float pixelsPerDip,
            MeasuringMode measuringMode,
            IRenderingParams renderingParams);

        /// <summary>
        ///  Obtains design units and common metrics for the font face.
        ///  These metrics are applicable to all the glyphs within a fontface and are used by applications for layout calculations.
        /// </summary>
        /// <param name="emSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if the DPI of the rendering surface is 96 this 
        ///  value is 1.0f. If the DPI is 120, this value is 120.0f/96.</param>
        /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
        ///  scaling specified by the font size and pixelsPerDip.</param>
        unsafe FontMetrics GetGdiCompatibleMetrics(
            float emSize,
            float pixelsPerDip,
            Matrix3x2* transform);

        /// <summary>
        ///  Obtains glyph metrics in font design units with the return values compatible with what GDI would produce.
        ///  Glyphs metrics are used for positioning of individual glyphs.
        /// </summary>
        /// <param name="emSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if the DPI of the rendering surface is 96 this 
        ///  value is 1.0f. If the DPI is 120, this value is 120.0f/96.</param>
        /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
        ///  scaling specified by the font size and pixelsPerDip.</param>
        /// <param name="useGdiNatural">
        ///  When set to FALSE, the metrics are the same as the metrics of GDI aliased text.
        ///  When set to TRUE, the metrics are the same as the metrics of text measured by GDI using a font
        ///  created with CLEARTYPE_NATURAL_QUALITY.
        /// </param>
        /// <param name="glyphIndices">An array of glyph indices to compute the metrics for.</param>
        /// <param name="glyphCount">The number of elements in the glyphIndices array.</param>
        /// <param name="glyphMetrics">Array of DWRITE_GLYPH_METRICS structures filled by this function.
        ///  The metrics returned by this function are in font design units.</param>
        /// <param name="isSideways">Indicates whether the font is being used in a sideways run.
        ///  This can affect the glyph metrics if the font has oblique simulation
        ///  because sideways oblique simulation differs from non-sideways oblique simulation.</param>
        unsafe void GetGdiCompatibleGlyphMetrics(
            float emSize,
            float pixelsPerDip,
            Matrix3x2* transform,
            IntBoolean useGdiNatural,
            in ushort glyphIndices,
            uint glyphCount,
            out GlyphMetrics glyphMetrics,
            IntBoolean isSideways);
    }
}
