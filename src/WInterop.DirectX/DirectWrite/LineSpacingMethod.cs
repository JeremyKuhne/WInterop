// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The method used for line spacing in layout. [DWRITE_LINE_SPACING_METHOD]
    /// </summary>
    public enum LineSpacingMethod : uint
    {
        /// <summary>
        ///  Line spacing depends solely on the content, growing to accommodate the size of fonts and inline objects.
        /// </summary>
        Default = DWRITE_LINE_SPACING_METHOD.DWRITE_LINE_SPACING_METHOD_DEFAULT,

        /// <summary>
        ///  Lines are explicitly set to uniform spacing, regardless of contained font sizes.
        ///  This can be useful to avoid the uneven appearance that can occur from font fallback.
        /// </summary>
        Uniform = DWRITE_LINE_SPACING_METHOD.DWRITE_LINE_SPACING_METHOD_UNIFORM,

        /// <summary>
        ///  Line spacing and baseline distances are proportional to the computed values based on the content, the size of the fonts and inline objects.
        /// </summary>
        Proportional = DWRITE_LINE_SPACING_METHOD.DWRITE_LINE_SPACING_METHOD_PROPORTIONAL
    }
}
