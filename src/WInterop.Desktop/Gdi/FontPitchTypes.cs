// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi;

[Flags]
public enum FontPitchTypes : byte
{
    // https://msdn.microsoft.com/en-us/library/dd145132.aspx
    // https://msdn.microsoft.com/en-us/library/cc250403.aspx

    /// <summary>
    ///  The font is NOT fixed. (TMPF_FIXED_PITCH)
    /// </summary>
    /// <remarks>Yes, this doesn't match the define.</remarks>
    VariablePitch = 0x01,

    /// <summary>
    ///  The font is vector based. (TMPF_VECTOR)
    /// </summary>
    Vector = 0x02,

    /// <summary>
    ///  The font is a Device font. (TMPF_DEVICE)
    /// </summary>
    Device = 0x08,

    /// <summary>
    ///  The font is a TrueType font. (TMPF_TRUETYPE)
    /// </summary>
    TrueType = 0x04,
}