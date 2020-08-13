// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="GlyphRunDescription"/> structure contains additional properties
    ///  related to those in <see cref="GlyphRun"/>. [DWRITE_GLYPH_RUN_DESCRIPTION]
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe readonly struct GlyphRunDescription
    {
        /// <summary>
        ///  The locale name associated with this run.
        /// </summary>
        public readonly char* LocaleName;

        /// <summary>
        ///  The text associated with the glyphs.
        /// </summary>
        public readonly char* String;

        /// <summary>
        ///  The number of characters (UTF16 code-units).
        ///  Note that this may be different than the number of glyphs.
        /// </summary>
        public readonly uint StringLength;

        /// <summary>
        ///  An array of indices to the glyph indices array, of the first glyphs of
        ///  all the glyph clusters of the glyphs to render. 
        /// </summary>
        public readonly ushort* ClusterMap;

        /// <summary>
        ///  Corresponding text position in the original string
        ///  this glyph run came from.
        /// </summary>
        public readonly uint TextPosition;
    }
}
