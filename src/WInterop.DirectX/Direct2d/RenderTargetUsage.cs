// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes how a render target is remoted and whether it should be
    /// GDI-compatible. This enumeration allows a bitwise combination of its member
    /// values. [D2D1_RENDER_TARGET_USAGE]
    /// </summary>
    [Flags]
    public enum RenderTargetUsage
    {
        /// <summary>
        /// [D2D1_RENDER_TARGET_USAGE_NONE]
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Rendering will occur locally, if a terminal-services session is established, the
        /// bitmap updates will be sent to the terminal services client.
        /// [D2D1_RENDER_TARGET_USAGE_FORCE_BITMAP_REMOTING]
        /// </summary>
        ForceBitmapRemoting = 0x00000001,

        /// <summary>
        /// The render target will allow a call to GetDC on the ID2D1GdiInteropRenderTarget
        /// interface. Rendering will also occur locally. [D2D1_RENDER_TARGET_USAGE_GDI_COMPATIBLE]
        /// </summary>
        GdiCompatible = 0x00000002
    }
}
