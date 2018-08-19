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
    /// The IDWriteFontFamily interface represents a set of fonts that share the same design but are differentiated
    /// by weight, stretch, and style. [IDWriteFontFamily]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteFontFamily),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFontFamily : IFontList
    {
        #region IDWriteFontList
        /// <summary>
        /// Gets the font collection that contains the fonts.
        /// </summary>
        new IFontCollection GetFontCollection();

        /// <summary>
        /// Gets the number of fonts in the font list.
        /// </summary>
        [PreserveSig]
        new uint GetFontCount();

        /// <summary>
        /// Gets a font given its zero-based index.
        /// </summary>
        /// <param name="index">Zero-based index of the font in the font list.</param>
        new IFont GetFont(uint index);
        #endregion

        /// <summary>
        /// Creates a localized strings object that contains the family names for the font family, indexed by locale name.
        /// </summary>
        ILocalizedStrings GetFamilyNames();

        /// <summary>
        /// Gets the font that best matches the specified properties.
        /// </summary>
        /// <param name="weight">Requested font weight.</param>
        /// <param name="stretch">Requested font stretch.</param>
        /// <param name="style">Requested font style.</param>
        IFont GetFirstMatchingFont(
            FontWeight weight,
            FontStretch stretch,
            FontStyle style);

        /// <summary>
        /// Gets a list of fonts in the font family ranked in order of how well they match the specified properties.
        /// </summary>
        /// <param name="weight">Requested font weight.</param>
        /// <param name="stretch">Requested font stretch.</param>
        /// <param name="style">Requested font style.</param>
        IFontList GetMatchingFonts(
            FontWeight weight,
            FontStretch stretch,
            FontStyle style);
    }
}
