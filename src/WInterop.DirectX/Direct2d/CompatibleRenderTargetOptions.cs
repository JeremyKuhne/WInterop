// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Specifies additional features supportable by a compatible render target when it
///  is created. This enumeration allows a bitwise combination of its member values.
/// </summary>
[Flags]
public enum CompatibleRenderTargetOptions : uint
{
    /// <summary>
    ///  [D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE]
    /// </summary>
    None = 0x00000000,

    /// <summary>
    ///  The compatible render target will allow a call to GetDC on the ID2D1GdiInteropRenderTarget interface.
    ///  This can be specified even if the parent render target is not GDI compatible.
    ///  [D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_GDI_COMPATIBLE]
    /// </summary>
    GdiCompatible = 0x00000001,
}
