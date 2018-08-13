// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Contains rendering options (hardware or software), pixel format, DPI
    /// information, remoting options, and Direct3D support requirements for a render
    /// target. [D2D1_RENDER_TARGET_PROPERTIES]
    /// </summary>
    public readonly struct RenderTargetProperties
    {
        public readonly RenderTargetType Type;
        public readonly PixelFormat PixelFormat;
        public readonly float DpiX;
        public readonly float DpiY;
        public readonly RenderTargetUsage Usage;
        public readonly FeatureLevel MinLevel;
    }
}
