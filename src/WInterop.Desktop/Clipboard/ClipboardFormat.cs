// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi.Native;

namespace WInterop.Clipboard
{
    /// <summary>
    ///  Standard clipboard formats.
    /// </summary>
    /// <docs><see cref="https://docs.microsoft.com/windows/desktop/dataxchg/standard-clipboard-formats"/></docs>
    public enum ClipboardFormat : uint
    {
        /// <summary>
        ///  [CF_TEXT]
        /// </summary>
        Text = 1,

        /// <summary>
        ///  Bitmap handle <see cref="Gdi.BitmapHandle"/>. [CF_BITMAP]
        /// </summary>
        Bitmap = 2,

        /// <summary>
        ///  [CF_METAFILEPICT]
        /// </summary>
        MetafilePicture = 3,

        /// <summary>
        ///  [CF_SYLK]
        /// </summary>
        SymbolicLink = 4,

        /// <summary>
        ///  Software Arts Data Interchange Format [CF_DIF]
        /// </summary>
        DataInterchangeFormat = 5,

        /// <summary>
        ///  Tagged image file format [CF_TIFF]
        /// </summary>
        Tiff = 6,

        /// <summary>
        ///  [CF_OEMTEXT]
        /// </summary>
        OemText = 7,

        /// <summary>
        ///  Device independent bitmap. <see cref="BITMAPINFO"/> [CF_DIB]
        /// </summary>
        DeviceIndependentBitmap = 8,

        /// <summary>
        ///  [CF_PALETTE]
        /// </summary>
        Palette = 9,

        /// <summary>
        ///  [CF_PENDATA]
        /// </summary>
        PenData = 10,

        /// <summary>
        ///  Resource Interchange File Format [CF_RIFF]
        /// </summary>
        Riff = 11,

        /// <summary>
        ///  [CF_WAVE]
        /// </summary>
        Wave = 12,

        /// <summary>
        ///  [CF_UNICODETEXT]
        /// </summary>
        UnicodeText = 13,

        /// <summary>
        ///  [CF_ENHMETAFILE]
        /// </summary>
        EnhancedMetafile = 14,

        /// <summary>
        ///  A handle to type HDROP that identifies a list of files. Used with DragQueryFile. [CF_HDROP]
        /// </summary>
        HDrop = 15,

        /// <summary>
        ///  Locale for <see cref="Text"/> data. [CF_LOCALE]
        /// </summary>
        Locale = 16,

        /// <summary>
        ///  [CF_DIBV5]
        /// </summary>
        DeviceIndependentBitmapV5 = 17,

        /// <summary>
        ///  [CF_OWNERDISPLAY]
        /// </summary>
        OwnerDisplay = 0x0080,

        /// <summary>
        ///  Display text associated with a private format. [CF_DSPTEXT]
        /// </summary>
        DisplayText = 0x0081,

        /// <summary>
        ///  Display bitmap associated with a private format. [CF_DSPBITMAP]
        /// </summary>
        DisplayBitmap = 0x0082,

        /// <summary>
        ///  Display metafile associated with a private format. [CF_DSPMETAFILEPICT]
        /// </summary>
        DisplayMetafilePicture = 0x0083,

        /// <summary>
        ///  Display enhanced metafile associated with a private format. [CF_DSPENHMETAFILE]
        /// </summary>
        DisplayEnhancedMetafile = 0x008E
    }
}