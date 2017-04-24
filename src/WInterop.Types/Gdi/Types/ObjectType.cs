// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd144905.aspx
    public enum ObjectType : uint
    {
        OBJ_PEN            = 1,
        OBJ_BRUSH          = 2,
        OBJ_DC             = 3,
        OBJ_METADC         = 4,
        OBJ_PAL            = 5,
        OBJ_FONT           = 6,
        OBJ_BITMAP         = 7,
        OBJ_REGION         = 8,
        OBJ_METAFILE       = 9,
        OBJ_MEMDC          = 10,
        OBJ_EXTPEN         = 11,
        OBJ_ENHMETADC      = 12,
        OBJ_ENHMETAFILE    = 13,
        OBJ_COLORSPACE     = 14
    }
}
