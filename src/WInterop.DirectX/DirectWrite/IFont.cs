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
    /// The <see cref="IFont"/> interface represents a physical font in a font collection.
    /// [IDWriteFont]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteFont),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFont
    {
        /// <summary>
        /// Gets the font family to which the specified font belongs.
        /// </summary>
        IFontFamily GetFontFamily();

        /// <summary>
        /// Gets the weight of the specified font.
        /// </summary>
        [PreserveSig]
        FontWeight GetWeight();

        /// <summary>
        /// Gets the stretch (aka. width) of the specified font.
        /// </summary>
        [PreserveSig]
        FontStretch GetStretch();

        /// <summary>
        /// Gets the style (aka. slope) of the specified font.
        /// </summary>
        [PreserveSig]
        FontStyle GetStyle();

        /// <summary>
        /// Returns TRUE if the font is a symbol font or FALSE if not.
        /// </summary>
        [PreserveSig]
        BOOL IsSymbolFont();

        /// <summary>
        /// Gets a localized strings collection containing the face names for the font (e.g., Regular or Bold), indexed by locale name.
        /// </summary>
        /// <returns>
        /// A pointer to the newly created localized strings object.
        /// </returns>
        ILocalizedStrings GetFaceNames();

        /// <summary>
        /// Gets a localized strings collection containing the specified informational strings, indexed by locale name.
        /// </summary>
        /// <param name="informationalStringID">Identifies the string to get.</param>
        /// <param name="informationalStrings">Receives a pointer to the newly created localized strings object.</param>
        /// <param name="exists">Receives the value TRUE if the font contains the specified string ID or FALSE if not.</param>
        /// <returns>
        /// Standard HRESULT error code. If the font does not contain the specified string, the return value is S_OK but 
        /// informationalStrings receives a NULL pointer and exists receives the value FALSE.
        /// </returns>
        BOOL GetInformationalStrings(
            InformationalStringId informationalStringID,
            out ILocalizedStrings informationalStrings);

        /// <summary>
        /// Gets a value that indicates what simulation are applied to the specified font.
        /// </summary>
        [PreserveSig]
        FontSimulations GetSimulations();

        /// <summary>
        /// Gets the metrics for the font.
        /// </summary>
        /// <param name="fontMetrics">Receives the font metrics.</param>
        [PreserveSig]
        void GetMetrics(out FontMetrics fontMetrics);

        /// <summary>
        /// Determines whether the font supports the specified character.
        /// </summary>
        /// <param name="unicodeValue">Unicode (UCS-4) character value.</param>
        /// <returns>Receives the value TRUE if the font supports the specified character or FALSE if not.</returns>
        BOOL HasCharacter(uint unicodeValue);

        /// <summary>
        /// Creates a font face object for the font.
        /// </summary>
        /// <returns>
        /// S pointer to the newly created font face object.
        /// </returns>
        IFontFace CreateFontFace();
    }
}
