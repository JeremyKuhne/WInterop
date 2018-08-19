// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// The <see cref="FontFeature"/> structure specifies properties used to identify and execute typographic feature in the font.
    /// [DWRITE_FONT_FEATURE]
    /// </summary>
    public readonly struct FontFeature
    {
        /// <summary>
        /// The feature OpenType name identifier.
        /// </summary>
        public readonly FontFeatureTag NameTag;

        /// <summary>
        /// Execution parameter of the feature.
        /// </summary>
        /// <remarks>
        /// The parameter should be non-zero to enable the feature.  Once enabled, a feature can't be disabled again within
        /// the same range.  Features requiring a selector use this value to indicate the selector index. 
        /// </remarks>
        public readonly uint Parameter;
    }
}
