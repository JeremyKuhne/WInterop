// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Direct2d;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The IDWriteFontFamily interface represents a set of fonts that share the same design but are differentiated
    ///  by weight, stretch, and style. [IDWriteFontFamily]
    /// </summary>
    [Guid(InterfaceIds.IID_IDWriteFontFamily)]
    public readonly unsafe struct FontFamily : FontFamily.Interface, IDisposable
    {
        private readonly IDWriteFontFamily* _handle;

        internal FontFamily(IDWriteFontFamily* handle) => _handle = handle;

        public uint FontCount => throw new NotImplementedException();

        public void Dispose() => _handle->Release();

        public LocalizedStrings GetFamilyNames()
        {
            IDWriteLocalizedStrings* names;
            _handle->GetFamilyNames(&names).ThrowIfFailed();
            return new(names);
        }

        public Font GetFont(uint index) => FontList.From(this).GetFont(index);

        public FontCollection GetFontCollection() => FontList.From(this).GetFontCollection();

        public Font GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style)
        {
            IDWriteFont* font;
            _handle->GetFirstMatchingFont(
                (DWRITE_FONT_WEIGHT)weight,
                (DWRITE_FONT_STRETCH)stretch,
                (DWRITE_FONT_STYLE)style,
                &font).ThrowIfFailed();

            return new(font);
        }

        public FontList GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style)
        {
            IDWriteFontList* fontList;
            _handle->GetMatchingFonts(
                (DWRITE_FONT_WEIGHT)weight,
                (DWRITE_FONT_STRETCH)stretch,
                (DWRITE_FONT_STYLE)style,
                &fontList).ThrowIfFailed();

            return new(fontList);
        }

        internal interface Interface : FontList.Interface
        {
            /// <summary>
            ///  Creates a localized strings object that contains the family names for the font family, indexed by locale name.
            /// </summary>
            LocalizedStrings GetFamilyNames();

            /// <summary>
            ///  Gets the font that best matches the specified properties.
            /// </summary>
            /// <param name="weight">Requested font weight.</param>
            /// <param name="stretch">Requested font stretch.</param>
            /// <param name="style">Requested font style.</param>
            Font GetFirstMatchingFont(
                FontWeight weight,
                FontStretch stretch,
                FontStyle style);

            /// <summary>
            ///  Gets a list of fonts in the font family ranked in order of how well they match the specified properties.
            /// </summary>
            /// <param name="weight">Requested font weight.</param>
            /// <param name="stretch">Requested font stretch.</param>
            /// <param name="style">Requested font style.</param>
            FontList GetMatchingFonts(
                FontWeight weight,
                FontStretch stretch,
                FontStyle style);
        }
    }
}
