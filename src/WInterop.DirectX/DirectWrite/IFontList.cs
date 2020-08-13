// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="IFontList"/> interface represents an ordered set of fonts that are part of an <see cref="IFontCollection"/>.
    ///  [IDWriteFontList]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteFontList),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFontList
    {
        /// <summary>
        ///  Gets the font collection that contains the fonts.
        /// </summary>
        IFontCollection GetFontCollection();

        /// <summary>
        ///  Gets the number of fonts in the font list.
        /// </summary>
        [PreserveSig]
        uint GetFontCount();

        /// <summary>
        ///  Gets a font given its zero-based index.
        /// </summary>
        /// <param name="index">Zero-based index of the font in the font list.</param>
        IFont GetFont(uint index);
    }
}
