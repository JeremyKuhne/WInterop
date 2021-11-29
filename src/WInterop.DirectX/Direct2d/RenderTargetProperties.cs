// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Contains rendering options (hardware or software), pixel format, DPI
    ///  information, remoting options, and Direct3D support requirements for a render
    ///  target. [D2D1_RENDER_TARGET_PROPERTIES]
    /// </summary>
    public readonly struct RenderTargetProperties
    {
        public readonly RenderTargetType Type;
        public readonly PixelFormat PixelFormat;
        public readonly float DpiX;
        public readonly float DpiY;
        public readonly RenderTargetUsage Usage;
        public readonly FeatureLevel MinLevel;

        public static implicit operator RenderTargetProperties(in D2D1_RENDER_TARGET_PROPERTIES properties)
            => Unsafe.As<D2D1_RENDER_TARGET_PROPERTIES, RenderTargetProperties>(ref Unsafe.AsRef(properties));

        public static implicit operator D2D1_RENDER_TARGET_PROPERTIES(in RenderTargetProperties properties)
            => Unsafe.As<RenderTargetProperties, D2D1_RENDER_TARGET_PROPERTIES>(ref Unsafe.AsRef(properties));
    }
}
