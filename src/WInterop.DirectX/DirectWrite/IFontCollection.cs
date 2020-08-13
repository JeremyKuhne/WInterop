// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The IDWriteFontCollection encapsulates a collection of font families. [IDWriteFontCollection]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteFontCollection),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFontCollection
    {
        /// <summary>
        ///  Gets the number of font families in the collection.
        /// </summary>
        [PreserveSig]
        uint GetFontFamilyCount();

        /// <summary>
        ///  Creates a font family object given a zero-based font family index.
        /// </summary>
        /// <param name="index">Zero-based index of the font family.</param>
        IFontFamily GetFontFamily(uint index);

        /// <summary>
        ///  Finds the font family with the specified family name.
        /// </summary>
        /// <param name="familyName">Name of the font family. The name is not case-sensitive but must otherwise exactly match a family name in the collection.</param>
        /// <param name="index">Receives the zero-based index of the matching font family if the family name was found or UINT_MAX otherwise.</param>
        IntBoolean FindFamilyName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string familyName,
            out uint index);

        /// <summary>
        ///  Gets the font object that corresponds to the same physical font as the specified font face object. The specified physical font must belong 
        ///  to the font collection.
        /// </summary>
        /// <param name="fontFace">Font face object that specifies the physical font.</param>
        IFont GetFontFromFontFace(IFontFace fontFace);
    }
}
