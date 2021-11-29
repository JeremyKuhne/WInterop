// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The IDWriteFontCollection encapsulates a collection of font families. [IDWriteFontCollection]
/// </summary>
[Guid(InterfaceIds.IID_IDWriteFontCollection)]
public readonly unsafe struct FontCollection : FontCollection.Interface, IDisposable
{
    internal IDWriteFontCollection* Handle { get; }

    internal FontCollection(IDWriteFontCollection* handle) => Handle = handle;

    public uint FontFamilyCount => Handle->GetFontFamilyCount();

    public void Dispose() => Handle->Release();

    public bool FindFamilyName(string familyName, out uint index)
    {
        TerraFX.Interop.Windows.BOOL exists;
        fixed (void* n = familyName)
        fixed (uint* i = &index)
        {
            Handle->FindFamilyName((ushort*)n, i, &exists).ThrowIfFailed();
        }

        return exists;
    }

    public FontFamily GetFontFamily(uint index)
    {
        IDWriteFontFamily* family;
        Handle->GetFontFamily(index, &family).ThrowIfFailed();
        return new(family);
    }

    public Font GetFontFromFontFace(FontFace fontFace)
    {
        IDWriteFont* font;
        Handle->GetFontFromFontFace(fontFace._handle, &font).ThrowIfFailed();
        return new(font);
    }

    internal interface Interface
    {
        /// <summary>
        ///  Gets the number of font families in the collection.
        /// </summary>
        uint FontFamilyCount { get; }

        /// <summary>
        ///  Creates a font family object given a zero-based font family index.
        /// </summary>
        /// <param name="index">Zero-based index of the font family.</param>
        FontFamily GetFontFamily(uint index);

        /// <summary>
        ///  Finds the font family with the specified family name.
        /// </summary>
        /// <param name="familyName">Name of the font family. The name is not case-sensitive but must otherwise exactly match a family name in the collection.</param>
        /// <param name="index">Receives the zero-based index of the matching font family if the family name was found or UINT_MAX otherwise.</param>
        bool FindFamilyName(
            string familyName,
            out uint index);

        /// <summary>
        ///  Gets the font object that corresponds to the same physical font as the specified font face object. The specified physical font must belong 
        ///  to the font collection.
        /// </summary>
        /// <param name="fontFace">Font face object that specifies the physical font.</param>
        Font GetFontFromFontFace(FontFace fontFace);
    }
}
