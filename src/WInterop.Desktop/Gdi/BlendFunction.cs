// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  [BLENDFUNCTION]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-blendfunction</docs>
public struct BlendFunction
{
    /// <summary>
    ///  The only valid value is 0x00 (AC_SRC_OVER).
    /// </summary>
    public byte BlendOp;

    /// <summary>
    ///  Must be zero.
    /// </summary>
    public byte BlendFlags;

    public byte SourceConstantAlpha;

    /// <summary>
    ///  Set to 0x01 (AC_SRC_ALPHA) when there is an alpha channel.
    /// </summary>
    public byte AlphaFormat;
}