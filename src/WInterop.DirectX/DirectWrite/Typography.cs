// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Font typography setting. [IDWriteTypography]
    /// </summary>
    [Guid(InterfaceIds.IID_IDWriteTypography)]
    public readonly unsafe struct Typography : Typography.Interface, IDisposable
    {
        internal IDWriteTypography* Handle { get; }

        internal Typography(IDWriteTypography* handle) => Handle = handle;

        public void AddFontFeature(FontFeature fontFeature)
        {
            Handle->AddFontFeature(Unsafe.As<FontFeature, DWRITE_FONT_FEATURE>(ref fontFeature)).ThrowIfFailed();
        }

        public void Dispose() => Handle->Release();

        public void GetFontFeature(uint fontFeatureIndex, out FontFeature fontFeature)
        {
            fixed (void* f = &fontFeature)
            {
                Handle->GetFontFeature(fontFeatureIndex, (DWRITE_FONT_FEATURE*)f);
            }
        }

        public uint FontFeatureCount => Handle->GetFontFeatureCount();

        internal interface Interface
        {
            /// <summary>
            ///  Add font feature.
            /// </summary>
            /// <param name="fontFeature">The font feature to add.</param>
            void AddFontFeature(FontFeature fontFeature);

            /// <summary>
            ///  Get the number of font features.
            /// </summary>
            uint FontFeatureCount { get; }

            /// <summary>
            ///  Get the font feature at the specified index.
            /// </summary>
            /// <param name="fontFeatureIndex">The zero-based index of the font feature to get.</param>
            /// <param name="fontFeature">The font feature.</param>
            void GetFontFeature(
                uint fontFeatureIndex,
                out FontFeature fontFeature);
        }
    }
}
