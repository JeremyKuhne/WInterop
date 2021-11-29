// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  The file format of a complete font face. [DWRITE_FONT_FACE_TYPE]
/// </summary>
/// <remarks>
///  Font formats that consist of multiple files, e.g. Type 1 .PFM and .PFB, have
///  a single enum entry.
/// </remarks>
public enum FontFaceType : uint
{
    /// <summary>
    ///  OpenType font face with CFF outlines.
    /// </summary>
    Cff = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_CFF,

    /// <summary>
    ///  OpenType font face with TrueType outlines.
    /// </summary>
    TrueType = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_TRUETYPE,

    /// <summary>
    ///  OpenType font face that is a part of a TrueType or CFF collection.
    /// </summary>
    OpenTypeCollection = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_OPENTYPE_COLLECTION,

    /// <summary>
    ///  A Type 1 font face.
    /// </summary>
    Type1 = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_TYPE1,

    /// <summary>
    ///  A vector .FON format font face.
    /// </summary>
    Vector = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_VECTOR,

    /// <summary>
    ///  A bitmap .FON format font face.
    /// </summary>
    Bitmap = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_BITMAP,

    /// <summary>
    ///  Font face type is not recognized by the DirectWrite font system.
    /// </summary>
    Unknown = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_UNKNOWN,

    /// <summary>
    ///  The font data includes only the CFF table from an OpenType CFF font.
    ///  This font face type can be used only for embedded fonts (i.e., custom
    ///  font file loaders) and the resulting font face object supports only the
    ///  minimum functionality necessary to render glyphs.
    /// </summary>
    RawCff = DWRITE_FONT_FACE_TYPE.DWRITE_FONT_FACE_TYPE_RAW_CFF
}
