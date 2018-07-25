// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    public enum ObjectType : uint
    {
        // https://msdn.microsoft.com/en-us/library/dd144905.aspx

        /// <summary>
        /// [OBJ_PEN]
        /// </summary>
        Pen = 1,

        /// <summary>
        /// [OBJ_BRUSH]
        /// </summary>
        Brush = 2,

        /// <summary>
        /// [OBJ_DC]
        /// </summary>
        DeviceContext = 3,

        /// <summary>
        /// [OBJ_METADC]
        /// </summary>
        MetafileDeviceContext = 4,

        /// <summary>
        /// [OBJ_PAL]
        /// </summary>
        Palette = 5,

        /// <summary>
        /// [OBJ_FONT]
        /// </summary>
        Font = 6,

        /// <summary>
        /// [OBJ_BITMAP]
        /// </summary>
        Bitmap = 7,

        /// <summary>
        /// [OBJ_REGION]
        /// </summary>
        Region = 8,

        /// <summary>
        /// [OBJ_METAFILE]
        /// </summary>
        Metafile = 9,

        /// <summary>
        /// [OBJ_MEMDC]
        /// </summary>
        MemoryDeviceContext = 10,

        /// <summary>
        /// [OBJ_EXTPEN]
        /// </summary>
        ExtendedPen = 11,

        /// <summary>
        /// [OBJ_ENHMETADC]
        /// </summary>
        EnhancedMetafileDeviceContext = 12,

        /// <summary>
        /// [OBJ_ENHMETAFILE]
        /// </summary>
        EnhancedMetafile = 13,

        /// <summary>
        /// [OBJ_COLORSPACE]
        /// </summary>
        ColorSpace = 14
    }
}
