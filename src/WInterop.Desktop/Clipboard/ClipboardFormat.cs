// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Clipboard
{
    /// <summary>
    /// Standard clipboard formats.
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/dataxchg/standard-clipboard-formats"/></msdn>
    public enum ClipboardFormat : uint
    {
        /// <summary>
        /// [CF_TEXT]
        /// </summary>
        CF_TEXT = 1,

        /// <summary>
        /// Bitmap handle <see cref="Gdi.BitmapHandle"/>. [CF_BITMAP]
        /// </summary>
        Bitmap = 2,

        /// <summary>
        /// [CF_METAFILEPICT]
        /// </summary>
        CF_METAFILEPICT = 3,

        /// <summary>
        /// [CF_SYLK]
        /// </summary>
        CF_SYLK = 4,

        /// <summary>
        /// [CF_DIF]
        /// </summary>
        CF_DIF = 5,

        /// <summary>
        /// [CF_TIFF]
        /// </summary>
        CF_TIFF = 6,

        /// <summary>
        /// [CF_OEMTEXT]
        /// </summary>
        CF_OEMTEXT = 7,

        /// <summary>
        /// Device independent bitmap. <see cref="Gdi.BitmapInfo"/>[CF_DIB]
        /// </summary>
        CF_DIB = 8,

        /// <summary>
        /// [CF_PALETTE]
        /// </summary>
        CF_PALETTE = 9,

        /// <summary>
        /// [CF_PENDATA]
        /// </summary>
        CF_PENDATA = 10,

        /// <summary>
        /// [CF_RIFF]
        /// </summary>
        CF_RIFF = 11,

        /// <summary>
        /// [CF_WAVE]
        /// </summary>
        CF_WAVE = 12,

        /// <summary>
        /// [CF_UNICODETEXT]
        /// </summary>
        CF_UNICODETEXT = 13,

        /// <summary>
        /// [CF_ENHMETAFILE]
        /// </summary>
        CF_ENHMETAFILE = 14,

        /// <summary>
        /// [CF_HDROP]
        /// </summary>
        CF_HDROP = 15,

        /// <summary>
        /// [CF_LOCALE]
        /// </summary>
        CF_LOCALE = 16,

        /// <summary>
        /// [CF_DIBV5]
        /// </summary>
        CF_DIBV5 = 17,

        /// <summary>
        /// [CF_OWNERDISPLAY]
        /// </summary>
        CF_OWNERDISPLAY = 0x0080,

        /// <summary>
        /// [CF_DSPTEXT]
        /// </summary>
        CF_DSPTEXT = 0x0081,

        /// <summary>
        /// [CF_DSPBITMAP]
        /// </summary>
        CF_DSPBITMAP = 0x0082,

        /// <summary>
        /// [CF_DSPMETAFILEPICT]
        /// </summary>
        CF_DSPMETAFILEPICT = 0x0083,

        /// <summary>
        /// [CF_DSPENHMETAFILE]
        /// </summary>
        CF_DSPENHMETAFILE = 0x008E
    }
}
