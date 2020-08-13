// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the antialiasing mode used for drawing text. [D2D1_TEXT_ANTIALIAS_MODE]
    /// </summary>
    public enum TextAntialiasMode : uint
    {

        /// <summary>
        ///  Render text using the current system setting. [D2D1_TEXT_ANTIALIAS_MODE_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  Render text using ClearType. [D2D1_TEXT_ANTIALIAS_MODE_CLEARTYPE]
        /// </summary>
        ClearType = 1,

        /// <summary>
        ///  Render text using gray-scale. [D2D1_TEXT_ANTIALIAS_MODE_GRAYSCALE]
        /// </summary>
        Grayscale = 2,

        /// <summary>
        ///  Render text aliased. [D2D1_TEXT_ANTIALIAS_MODE_ALIASED]
        /// </summary>
        Aliased = 3
    }
}
