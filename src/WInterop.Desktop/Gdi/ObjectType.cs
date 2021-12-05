// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public enum ObjectType : uint
{
    // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getobjecttype

    /// <summary>
    ///  [OBJ_PEN]
    /// </summary>
    Pen = OBJ.OBJ_PEN,

    /// <summary>
    ///  [OBJ_BRUSH]
    /// </summary>
    Brush = OBJ.OBJ_BRUSH,

    /// <summary>
    ///  [OBJ_DC]
    /// </summary>
    DeviceContext = OBJ.OBJ_DC,

    /// <summary>
    ///  [OBJ_METADC]
    /// </summary>
    MetafileDeviceContext = OBJ.OBJ_METADC,

    /// <summary>
    ///  [OBJ_PAL]
    /// </summary>
    Palette = OBJ.OBJ_PAL,

    /// <summary>
    ///  [OBJ_FONT]
    /// </summary>
    Font = OBJ.OBJ_FONT,

    /// <summary>
    ///  [OBJ_BITMAP]
    /// </summary>
    Bitmap = OBJ.OBJ_BITMAP,

    /// <summary>
    ///  [OBJ_REGION]
    /// </summary>
    Region = OBJ.OBJ_REGION,

    /// <summary>
    ///  [OBJ_METAFILE]
    /// </summary>
    Metafile = OBJ.OBJ_METAFILE,

    /// <summary>
    ///  [OBJ_MEMDC]
    /// </summary>
    MemoryDeviceContext = OBJ.OBJ_MEMDC,

    /// <summary>
    ///  [OBJ_EXTPEN]
    /// </summary>
    ExtendedPen = OBJ.OBJ_EXTPEN,

    /// <summary>
    ///  [OBJ_ENHMETADC]
    /// </summary>
    EnhancedMetafileDeviceContext = OBJ.OBJ_ENHMETADC,

    /// <summary>
    ///  [OBJ_ENHMETAFILE]
    /// </summary>
    EnhancedMetafile = OBJ.OBJ_ENHMETAFILE,

    /// <summary>
    ///  [OBJ_COLORSPACE]
    /// </summary>
    ColorSpace = OBJ.OBJ_COLORSPACE
}