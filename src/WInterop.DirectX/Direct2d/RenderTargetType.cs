// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes whether a render target uses hardware or software rendering, or if
    ///  Direct2D should select the rendering mode. [D2D1_RENDER_TARGET_TYPE]
    /// </summary>
    public enum RenderTargetType : uint
    {
        /// <summary>
        ///  D2D is free to choose the render target type for the caller. [D2D1_RENDER_TARGET_TYPE_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  The render target will render using the CPU. [D2D1_RENDER_TARGET_TYPE_SOFTWARE]
        /// </summary>
        Software = 1,

        /// <summary>
        ///  The render target will render using the GPU. [D2D1_RENDER_TARGET_TYPE_HARDWARE]
        /// </summary>
        Hardware = 2
    }
}
