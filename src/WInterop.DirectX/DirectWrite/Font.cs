// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Direct2d;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="Font"/> interface represents a physical font in a font collection.
    ///  [IDWriteFont]
    /// </summary>
    [Guid(InterfaceIds.IID_IDWriteFont)]
    public readonly unsafe struct Font : Font.Interface, IDisposable
    {
        private readonly IDWriteFont* _handle;

        internal Font(IDWriteFont* handle) => _handle = handle;

        public FontWeight Weight => (FontWeight)_handle->GetWeight();

        public FontStretch Stretch => (FontStretch)_handle->GetStretch();

        public FontStyle Style => (FontStyle)_handle->GetStyle();

        public bool IsSymbolFont => _handle->IsSymbolFont();

        public FontSimulations Simulations => (FontSimulations)_handle->GetSimulations();

        public FontMetrics Metrics
        {
            get
            {
                FontMetrics* metrics;
                _handle->GetMetrics((DWRITE_FONT_METRICS*)&metrics);
                return Metrics;
            }
        }

        public FontFace CreateFontFace()
        {
            IDWriteFontFace* fontFace;
            _handle->CreateFontFace(&fontFace).ThrowIfFailed();
            return new(fontFace);
        }

        public void Dispose() => _handle->Release();

        public LocalizedStrings GetFaceNames()
        {
            IDWriteLocalizedStrings* strings;
            _handle->GetFaceNames(&strings).ThrowIfFailed();
            return new(strings);
        }

        public FontFamily GetFontFamily()
        {
            IDWriteFontFamily* family;
            _handle->GetFontFamily(&family).ThrowIfFailed();
            return new(family);
        }

        public bool GetInformationalStrings(InformationalStringId informationalStringID, out LocalizedStrings informationalStrings)
        {
            IDWriteLocalizedStrings* strings;
            TerraFX.Interop.Windows.BOOL exists;
            _handle->GetInformationalStrings((DWRITE_INFORMATIONAL_STRING_ID)informationalStringID, &strings, &exists).ThrowIfFailed();
            informationalStrings = new(strings);
            return exists;
        }

        public bool HasCharacter(uint unicodeValue)
        {
            TerraFX.Interop.Windows.BOOL exists;
            _handle->HasCharacter(unicodeValue, &exists).ThrowIfFailed();
            return exists;
        }

        internal interface Interface
        {
            /// <summary>
            ///  Gets the font family to which the specified font belongs.
            /// </summary>
            FontFamily GetFontFamily();

            /// <summary>
            ///  Gets the weight of the specified font.
            /// </summary>
            FontWeight Weight { get; }

            /// <summary>
            ///  Gets the stretch (aka. width) of the specified font.
            /// </summary>
            FontStretch Stretch { get;  }

            /// <summary>
            ///  Gets the style (aka. slope) of the specified font.
            /// </summary>
            FontStyle Style { get; }

            /// <summary>
            ///  Returns TRUE if the font is a symbol font or FALSE if not.
            /// </summary>
            bool IsSymbolFont { get; }

            /// <summary>
            ///  Gets a localized strings collection containing the face names for the font (e.g., Regular or Bold), indexed by locale name.
            /// </summary>
            /// <returns>
            ///  A pointer to the newly created localized strings object.
            /// </returns>
            LocalizedStrings GetFaceNames();

            /// <summary>
            ///  Gets a localized strings collection containing the specified informational strings, indexed by locale name.
            /// </summary>
            /// <param name="informationalStringID">Identifies the string to get.</param>
            /// <param name="informationalStrings">Receives a pointer to the newly created localized strings object.</param>
            bool GetInformationalStrings(
                InformationalStringId informationalStringID,
                out LocalizedStrings informationalStrings);

            /// <summary>
            ///  Gets a value that indicates what simulation are applied to the specified font.
            /// </summary>
            FontSimulations Simulations { get; }

            /// <summary>
            ///  Gets the metrics for the font.
            /// </summary>
            /// <param name="fontMetrics">Receives the font metrics.</param>
            FontMetrics Metrics { get; }

            /// <summary>
            ///  Determines whether the font supports the specified character.
            /// </summary>
            /// <param name="unicodeValue">Unicode (UCS-4) character value.</param>
            /// <returns>Receives the value TRUE if the font supports the specified character or FALSE if not.</returns>
            bool HasCharacter(uint unicodeValue);

            /// <summary>
            ///  Creates a font face object for the font.
            /// </summary>
            /// <returns>
            ///  S pointer to the newly created font face object.
            /// </returns>
            FontFace CreateFontFace();
        }
    }
}
