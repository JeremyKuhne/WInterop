// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Font typography setting. [IDWriteTypography]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteTypography),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITypography
    {
        /// <summary>
        /// Add font feature.
        /// </summary>
        /// <param name="fontFeature">The font feature to add.</param>
        void AddFontFeature(FontFeature fontFeature);

        /// <summary>
        /// Get the number of font features.
        /// </summary>
        [PreserveSig]
        uint GetFontFeatureCount();

        /// <summary>
        /// Get the font feature at the specified index.
        /// </summary>
        /// <param name="fontFeatureIndex">The zero-based index of the font feature to get.</param>
        /// <param name="fontFeature">The font feature.</param>
        void GetFontFeature(
            uint fontFeatureIndex,
            out FontFeature fontFeature);
    }
}
