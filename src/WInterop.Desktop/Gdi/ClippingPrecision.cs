// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/cc669448.aspx
// https://msdn.microsoft.com/en-us/library/dd183499.aspx
[Flags]
public enum ClippingPrecision : byte
{
    /// <summary>
    ///  Use default clipping. (CLIP_DEFAULT_PRECIS)
    /// </summary>
    Default = 0,

    /// <summary>
    ///  Not used. (CLIP_CHARACTER_PRECIS)
    /// </summary>
    Character = 1,

    /// <summary>
    ///  Returned when enumerating fonts. (CLIP_STROKE_PRECIS)
    /// </summary>
    Stroke = 2,

    /// <summary>
    ///  Controls font rotation. Set to rotate according to the orientation of the coordinate system.
    ///  If not set device fonts rotate counterclockwise, otherwise follows coordinate system. (CLIP_LH_ANGLES)
    /// </summary>
    Angles = 1 << 4,

    /// <summary>
    ///  Not used. (CLIP_TT_ALWAYS)
    /// </summary>
    TrueTypeAlways = 2 << 4,

    /// <summary>
    ///  Disable font association. (CLIP_DFA_DISABLE)
    /// </summary>
    FontAssociationDisable = 4 << 4,

    /// <summary>
    ///  Use font embedding to render document content. (CLIP_EMBEDDED)
    /// </summary>
    Embedded = 8 << 4
}