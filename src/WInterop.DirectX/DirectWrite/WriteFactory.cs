// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.Windows.Native;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The root factory interface for all DWrite objects. [IDWriteFactory]
    /// </summary>
    [Guid(InterfaceIds.IID_IDWriteFactory)]
    public readonly unsafe struct WriteFactory : WriteFactory.Interface, IDisposable
    {
        private readonly IDWriteFactory* _handle;

        internal WriteFactory(IDWriteFactory* handle) => _handle = handle;

        public RenderingParams CreateCustomRenderingParams(
            float gamma,
            float enhancedContrast,
            float clearTypeLevel,
            PixelGeometry pixelGeometry,
            RenderingMode renderingMode)
        {
            IDWriteRenderingParams* rendering;

            _handle->CreateCustomRenderingParams(
                gamma,
                enhancedContrast,
                clearTypeLevel,
                (DWRITE_PIXEL_GEOMETRY)pixelGeometry,
                (DWRITE_RENDERING_MODE)renderingMode,
                &rendering).ThrowIfFailed();

            return new(rendering);
        }

        public InlineObject CreateEllipsisTrimmingSign(TextFormat textFormat)
        {
            IDWriteInlineObject* inlineObject;
            _handle->CreateEllipsisTrimmingSign(textFormat.Handle, &inlineObject).ThrowIfFailed();
            return new(inlineObject);
        }

        public TextLayout CreateGdiCompatibleTextLayout(
            ReadOnlySpan<char> @string,
            uint stringLength,
            TextFormat textFormat,
            float layoutWidth,
            float layoutHeight,
            float pixelsPerDip,
            Matrix3x2 transform,
            bool useGdiNatural)
        {
            IDWriteTextLayout* layout;
            fixed (void* s = @string)
            {
                _handle->CreateGdiCompatibleTextLayout(
                    (ushort*)s,
                    (uint)@string.Length,
                    textFormat.Handle,
                    layoutWidth,
                    layoutHeight,
                    pixelsPerDip,
                    (DWRITE_MATRIX*)&transform,
                    useGdiNatural,
                    &layout).ThrowIfFailed();
            }

            return new(layout);
        }

        public RenderingParams CreateMonitorRenderingParams(HMONITOR monitor)
        {
            IDWriteRenderingParams* rendering;
            _handle->CreateMonitorRenderingParams((TerraFX.Interop.Windows.HMONITOR)monitor.Value, &rendering).ThrowIfFailed();
            return new(rendering);
        }

        public RenderingParams CreateRenderingParams()
        {
            IDWriteRenderingParams* rendering;
            _handle->CreateRenderingParams(&rendering).ThrowIfFailed();
            return new(rendering);
        }

        public TextFormat CreateTextFormat(
            string fontFamilyName,
            FontCollection fontCollection = default,
            FontWeight fontWeight = FontWeight.Normal,
            FontStyle fontStyle = FontStyle.Normal,
            FontStretch fontStretch = FontStretch.Normal,
            float fontSize = 12,
            string? localeName = null)
        {
            IDWriteTextFormat* format;
            localeName ??= CultureInfo.CurrentCulture.Name;

            fixed (void* n = fontFamilyName)
            fixed (void* l = localeName)
            {
                _handle->CreateTextFormat(
                    (ushort*)n,
                    fontCollection.Handle,
                    (DWRITE_FONT_WEIGHT)fontWeight,
                    (DWRITE_FONT_STYLE)fontStyle,
                    (DWRITE_FONT_STRETCH)fontStretch,
                    fontSize,
                    (ushort*)l,
                    &format).ThrowIfFailed();
            }

            return new(format);
        }

        public TextLayout CreateTextLayout(ReadOnlySpan<char> @string, TextFormat textFormat, SizeF maxSize)
        {
            IDWriteTextLayout* layout;

            fixed (void* s = @string)
            {
                _handle->CreateTextLayout(
                    (ushort*)s,
                    (uint)@string.Length,
                    textFormat.Handle,
                    maxSize.Width,
                    maxSize.Height,
                    &layout).ThrowIfFailed();
            }

            return new(layout);
        }

        public Typography CreateTypography()
        {
            IDWriteTypography* typography;
            _handle->CreateTypography(&typography).ThrowIfFailed();
            return new(typography);
        }

        public void Dispose() => _handle->Release();

        public FontCollection GetSystemFontCollection(bool checkForUpdates)
        {
            IDWriteFontCollection* collection;
            _handle->GetSystemFontCollection(&collection, checkForUpdates).ThrowIfFailed();
            return new(collection);
        }

        internal interface Interface
        {
            /// <summary>
            ///  Gets a font collection representing the set of installed fonts.
            /// </summary>
            /// <param name="checkForUpdates">
            ///  If this parameter is true, the function performs an immediate check for changes to the set of installed fonts.
            ///  If this parameter is false, the function will still detect changes if the font cache service is running, but
            ///  there may be some latency. For example, an application might specify true if it has itself just installed a font
            ///  and wants to be sure the font collection contains that font.
            /// </param>
            FontCollection GetSystemFontCollection(bool checkForUpdates);

            /*
                        /// <summary>
                        ///  Creates a font collection using a custom font collection loader.
                        /// </summary>
                        /// <param name="collectionLoader">Application-defined font collection loader, which must have been previously
                        ///  registered using RegisterFontCollectionLoader.</param>
                        /// <param name="collectionKey">Key used by the loader to identify a collection of font files.</param>
                        /// <param name="collectionKeySize">Size in bytes of the collection key.</param>
                        /// <param name="fontCollection">Receives a pointer to the system font collection object, or NULL in case of failure.</param>
                        //void CreateCustomFontCollectionSTUB();
                        //STDMETHOD(CreateCustomFontCollection)(
                        //    _In_ IDWriteFontCollectionLoader* collectionLoader,
                        //    _In_reads_bytes_(collectionKeySize) void const* collectionKey,
                        //    UINT32 collectionKeySize,
                        //    _COM_Outptr_ IDWriteFontCollection** fontCollection
                        //    ) PURE;

                        /// <summary>
                        ///  Registers a custom font collection loader with the factory object.
                        /// </summary>
                        /// <param name="fontCollectionLoader">Application-defined font collection loader.</param>
                        void RegisterFontCollectionLoaderSTUB();
                        //STDMETHOD(RegisterFontCollectionLoader)(
                        //    _In_ IDWriteFontCollectionLoader* fontCollectionLoader
                        //    ) PURE;

                        /// <summary>
                        ///  Unregisters a custom font collection loader that was previously registered using RegisterFontCollectionLoader.
                        /// </summary>
                        /// <param name="fontCollectionLoader">Application-defined font collection loader.</param>
                        /// <returns>
                        ///  Standard HRESULT error code.
                        /// </returns>
                        void UnregisterFontCollectionLoaderSTUB();
                        //STDMETHOD(UnregisterFontCollectionLoader)(
                        //    _In_ IDWriteFontCollectionLoader* fontCollectionLoader
                        //    ) PURE;

                        /// <summary>
                        ///  CreateFontFileReference creates a font file reference object from a local font file.
                        /// </summary>
                        /// <param name="filePath">Absolute file path. Subsequent operations on the constructed object may fail
                        ///  if the user provided filePath doesn't correspond to a valid file on the disk.</param>
                        /// <param name="lastWriteTime">Last modified time of the input file path. If the parameter is omitted,
                        ///  the function will access the font file to obtain its last write time, so the clients are encouraged to specify this value
                        ///  to avoid extra disk access. Subsequent operations on the constructed object may fail
                        ///  if the user provided lastWriteTime doesn't match the file on the disk.</param>
                        /// <param name="fontFile">Contains newly created font file reference object, or NULL in case of failure.</param>
                        void CreateFontFileReferenceSTUB();
                        //STDMETHOD(CreateFontFileReference)(
                        //    _In_z_ WCHAR const* filePath,
                        //    _In_opt_ FILETIME const* lastWriteTime,
                        //    _COM_Outptr_ IDWriteFontFile** fontFile
                        //    ) PURE;

                        /// <summary>
                        ///  CreateCustomFontFileReference creates a reference to an application specific font file resource.
                        ///  This function enables an application or a document to use a font without having to install it on the system.
                        ///  The fontFileReferenceKey has to be unique only in the scope of the fontFileLoader used in this call.
                        /// </summary>
                        /// <param name="fontFileReferenceKey">Font file reference key that uniquely identifies the font file resource
                        ///  during the lifetime of fontFileLoader.</param>
                        /// <param name="fontFileReferenceKeySize">Size of font file reference key in bytes.</param>
                        /// <param name="fontFileLoader">Font file loader that will be used by the font system to load data from the file identified by
                        ///  fontFileReferenceKey.</param>
                        /// <param name="fontFile">Contains the newly created font file object, or NULL in case of failure.</param>
                        /// <returns>
                        ///  Standard HRESULT error code.
                        /// </returns>
                        /// <remarks>
                        ///  This function is provided for cases when an application or a document needs to use a font
                        ///  without having to install it on the system. fontFileReferenceKey has to be unique only in the scope
                        ///  of the fontFileLoader used in this call.
                        /// </remarks>
                        void CreateCustomFontFileReferenceSTUB();
                        //STDMETHOD(CreateCustomFontFileReference)(
                        //    _In_reads_bytes_(fontFileReferenceKeySize) void const* fontFileReferenceKey,
                        //    UINT32 fontFileReferenceKeySize,
                        //    _In_ IDWriteFontFileLoader* fontFileLoader,
                        //    _COM_Outptr_ IDWriteFontFile** fontFile
                        //    ) PURE;

                        /// <summary>
                        ///  Creates a font face object.
                        /// </summary>
                        /// <param name="fontFaceType">The file format of the font face.</param>
                        /// <param name="numberOfFiles">The number of font files required to represent the font face.</param>
                        /// <param name="fontFiles">Font files representing the font face. Since IDWriteFontFace maintains its own references
                        ///  to the input font file objects, it's OK to release them after this call.</param>
                        /// <param name="faceIndex">The zero based index of a font face in cases when the font files contain a collection of font faces.
                        ///  If the font files contain a single face, this value should be zero.</param>
                        /// <param name="fontFaceSimulationFlags">Font face simulation flags for algorithmic emboldening and italicization.</param>
                        /// <param name="fontFace">Contains the newly created font face object, or NULL in case of failure.</param>
                        void CreateFontFaceSTUB();
                        //IFontFace CreateFontFace(
                        //    FontFaceType fontFaceType,
                        //    uint numberOfFiles,
                        //    _In_reads_(numberOfFiles) IDWriteFontFile* const* fontFiles,
                        //    uint faceIndex,
                        //    DWRITE_FONT_SIMULATIONS fontFaceSimulationFlags);
            */

            /// <summary>
            ///  Creates a rendering parameters object with default settings for the primary monitor.
            /// </summary>
            RenderingParams CreateRenderingParams();

            /// <summary>
            ///  Creates a rendering parameters object with default settings for the specified monitor.
            /// </summary>
            /// <param name="monitor">The monitor to read the default values from.</param>
            RenderingParams CreateMonitorRenderingParams(HMONITOR monitor);

            /// <summary>
            ///  Creates a rendering parameters object with the specified properties.
            /// </summary>
            /// <param name="gamma">The gamma value used for gamma correction, which must be greater than zero and cannot exceed 256.</param>
            /// <param name="enhancedContrast">The amount of contrast enhancement, zero or greater.</param>
            /// <param name="clearTypeLevel">The degree of ClearType level, from 0.0f (no ClearType) to 1.0f (full ClearType).</param>
            /// <param name="pixelGeometry">The geometry of a device pixel.</param>
            /// <param name="renderingMode">Method of rendering glyphs. In most cases, this should be DWRITE_RENDERING_MODE_DEFAULT to automatically use an appropriate mode.</param>
            RenderingParams CreateCustomRenderingParams(
                float gamma,
                float enhancedContrast,
                float clearTypeLevel,
                PixelGeometry pixelGeometry,
                RenderingMode renderingMode);

/*
            /// <summary>
            ///  Registers a font file loader with DirectWrite.
            /// </summary>
            /// <param name="fontFileLoader">Pointer to the implementation of the IDWriteFontFileLoader for a particular file resource type.</param>
            /// <remarks>
            ///  This function registers a font file loader with DirectWrite.
            ///  Font file loader interface handles loading font file resources of a particular type from a key.
            ///  The font file loader interface is recommended to be implemented by a singleton object.
            ///  A given instance can only be registered once.
            ///  Succeeding attempts will return an error that it has already been registered.
            ///  IMPORTANT: font file loader implementations must not register themselves with DirectWrite
            ///  inside their constructors and must not unregister themselves in their destructors, because
            ///  registration and unregistration operations increment and decrement the object reference count respectively.
            ///  Instead, registration and unregistration of font file loaders with DirectWrite should be performed
            ///  outside of the font file loader implementation as a separate step.
            /// </remarks>
            void RegisterFontFileLoaderSTUB();
            //STDMETHOD(RegisterFontFileLoader)(
            //    _In_ IDWriteFontFileLoader* fontFileLoader
            //    ) PURE;

            /// <summary>
            ///  Unregisters a font file loader that was previously registered with the DirectWrite font system using RegisterFontFileLoader.
            /// </summary>
            /// <param name="fontFileLoader">Pointer to the file loader that was previously registered with the DirectWrite font system using RegisterFontFileLoader.</param>
            /// <returns>
            ///  This function will succeed if the user loader is requested to be removed.
            ///  It will fail if the pointer to the file loader identifies a standard DirectWrite loader,
            ///  or a loader that is never registered or has already been unregistered.
            /// </returns>
            /// <remarks>
            ///  This function unregisters font file loader callbacks with the DirectWrite font system.
            ///  The font file loader interface is recommended to be implemented by a singleton object.
            ///  IMPORTANT: font file loader implementations must not register themselves with DirectWrite
            ///  inside their constructors and must not unregister themselves in their destructors, because
            ///  registration and unregistration operations increment and decrement the object reference count respectively.
            ///  Instead, registration and unregistration of font file loaders with DirectWrite should be performed
            ///  outside of the font file loader implementation as a separate step.
            /// </remarks>
            void UnregisterFontFileLoaderSTUB();
            //STDMETHOD(UnregisterFontFileLoader)(
            //    _In_ IDWriteFontFileLoader* fontFileLoader
            //    ) PURE;
*/

            /// <summary>
            ///  Create a text format object used for text layout.
            /// </summary>
            /// <param name="fontFamilyName">Name of the font family</param>
            /// <param name="fontCollection">Font collection. 'null' indicates the system font collection.</param>
            /// <param name="fontSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
            /// <param name="localeName">Locale name</param>
            TextFormat CreateTextFormat(
                string fontFamilyName,
                FontCollection fontCollection = default,
                FontWeight fontWeight = FontWeight.Normal,
                FontStyle fontStyle = FontStyle.Normal,
                FontStretch fontStretch = FontStretch.Normal,
                float fontSize = 12.0f,
                string? localeName = null);

            /// <summary>
            ///  Create a typography object used in conjunction with text format for text layout.
            /// </summary>
            Typography CreateTypography();

/*
            /// <summary>
            ///  Create an object used for interoperability with GDI.
            /// </summary>
            /// <param name="gdiInterop">Receives the GDI interop object if successful, or NULL in case of failure.</param>
            void GetGdiInteropSTUB();
            //STDMETHOD(GetGdiInterop)(
            //    _COM_Outptr_ IDWriteGdiInterop** gdiInterop
            //    ) PURE;
*/

            /// <summary>
            ///  CreateTextLayout takes a string, format, and associated constraints
            ///  and produces an object representing the fully analyzed
            ///  and formatted result.
            /// </summary>
            /// <param name="string">The string to layout.</param>
            /// <param name="textFormat">The format to apply to the string.</param>
            /// <param name="maxSize">Size of the layout box.</param>
            TextLayout CreateTextLayout(
                ReadOnlySpan<char> @string,
                TextFormat textFormat,
                SizeF maxSize);

            /// <summary>
            ///  CreateGdiCompatibleTextLayout takes a string, format, and associated constraints
            ///  and produces and object representing the result formatted for a particular display resolution
            ///  and measuring mode. The resulting text layout should only be used for the intended resolution,
            ///  and for cases where text scalability is desired, CreateTextLayout should be used instead.
            /// </summary>
            /// <param name="string">The string to layout.</param>
            /// <param name="stringLength">The length of the string.</param>
            /// <param name="textFormat">The format to apply to the string.</param>
            /// <param name="layoutWidth">Width of the layout box.</param>
            /// <param name="layoutHeight">Height of the layout box.</param>
            /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if rendering onto a 96 DPI device then pixelsPerDip
            ///  is 1. If rendering onto a 120 DPI device then pixelsPerDip is 120/96.</param>
            /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
            ///  scaling specified the font size and pixelsPerDip.</param>
            /// <param name="useGdiNatural">
            ///  When set to FALSE, instructs the text layout to use the same metrics as GDI aliased text.
            ///  When set to TRUE, instructs the text layout to use the same metrics as text measured by GDI using a font
            ///  created with CLEARTYPE_NATURAL_QUALITY.
            /// </param>
            TextLayout CreateGdiCompatibleTextLayout(
                ReadOnlySpan<char> @string,
                uint stringLength,
                TextFormat textFormat,
                float layoutWidth,
                float layoutHeight,
                float pixelsPerDip,
                Matrix3x2 transform,
                bool useGdiNatural);

            /// <summary>
            ///  The application may call this function to create an inline object for trimming, using an ellipsis as the
            ///  omission sign. The ellipsis will be created using the current settings of the format, including base font,
            ///  style, and any effects. Alternate omission signs can be created by the application by implementing
            ///  IDWriteInlineObject.
            /// </summary>
            /// <param name="textFormat">Text format used as a template for the omission sign.</param>
            /// <returns>Created omission sign.</returns>
            InlineObject CreateEllipsisTrimmingSign(TextFormat textFormat);

/*
            ///// <param name="textAnalyzer">The resultant object.</param>
            /// <summary>
            ///  Return an interface to perform text analysis with.
            /// </summary>
            void CreateTextAnalyzerSTUB();
            //STDMETHOD(CreateTextAnalyzer)(
            //    _COM_Outptr_ IDWriteTextAnalyzer** textAnalyzer
            //    ) PURE;


            ///// <param name="substitutionMethod">Method of number substitution to use.</param>
            ///// <param name="localeName">Which locale to obtain the digits from.</param>
            ///// <param name="ignoreUserOverride">Ignore the user's settings and use the locale defaults</param>
            ///// <param name="numberSubstitution">Receives a pointer to the newly created object.</param>
            /// <summary>
            ///  Creates a number substitution object using a locale name,
            ///  substitution method, and whether to ignore user overrides (uses NLS
            ///  defaults for the given culture instead).
            /// </summary>
            void CreateNumberSubstitutionSTUB();
            //STDMETHOD(CreateNumberSubstitution)(
            //    _In_ DWRITE_NUMBER_SUBSTITUTION_METHOD substitutionMethod,
            //    _In_z_ WCHAR const* localeName,
            //    _In_ BOOL ignoreUserOverride,
            //    _COM_Outptr_ IDWriteNumberSubstitution** numberSubstitution
            //    ) PURE;

            /// <summary>
            ///  Creates a glyph run analysis object, which encapsulates information
            ///  used to render a glyph run.
            /// </summary>
            /// <param name="glyphRun">Structure specifying the properties of the glyph run.</param>
            /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if rendering onto a 96 DPI bitmap then pixelsPerDip
            ///  is 1. If rendering onto a 120 DPI bitmap then pixelsPerDip is 120/96.</param>
            /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
            ///  scaling specified by the emSize and pixelsPerDip.</param>
            /// <param name="renderingMode">Specifies the rendering mode, which must be one of the raster rendering modes (i.e., not default
            ///  and not outline).</param>
            /// <param name="measuringMode">Specifies the method to measure glyphs.</param>
            /// <param name="baselineOriginX">Horizontal position of the baseline origin, in DIPs.</param>
            /// <param name="baselineOriginY">Vertical position of the baseline origin, in DIPs.</param>
            /// <param name="glyphRunAnalysis">Receives a pointer to the newly created object.</param>
            void CreateGlyphRunAnalysisSTUB();
            //STDMETHOD(CreateGlyphRunAnalysis)(
            //    _In_ DWRITE_GLYPH_RUN const* glyphRun,
            //    FLOAT pixelsPerDip,
            //    _In_opt_ DWRITE_MATRIX const* transform,
            //    DWRITE_RENDERING_MODE renderingMode,
            //    DWRITE_MEASURING_MODE measuringMode,
            //    FLOAT baselineOriginX,
            //    FLOAT baselineOriginY,
            //    _COM_Outptr_ IDWriteGlyphRunAnalysis** glyphRunAnalysis
            //    ) PURE;
*/
        }
    }
}
