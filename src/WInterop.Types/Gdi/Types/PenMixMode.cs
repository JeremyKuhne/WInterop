// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    /// <summary>
    /// Pen pixel mix modes (ROP2)
    /// </summary>
    public enum PenMixMode : int
    {
        // https://msdn.microsoft.com/en-us/library/cc234985.aspx
        // https://msdn.microsoft.com/en-us/library/dd183369.aspx

        /// <summary>
        /// Pixel is always drawn black. [R2_BLACK]
        /// </summary>
        Black = 1,

        /// <summary>
        /// Inverse of result of MergePen. [R2_NOTMERGEPEN]
        /// </summary>
        NotMergePen = 2,

        /// <summary>
        /// The pixel is a combination of the colors that are common to both the screen and the inverse of the pen. [R2_MASKNOTPEN]
        /// </summary>
        MaskNotPen = 3,

        /// <summary>
        /// The pixel is the inverse of the pen color. [R2_NOTCOPYPEN]
        /// </summary>
        NotCopyPen = 4,

        /// <summary>
        /// The pixel is a combination of the colors that are common to both the pen and the inverse of the screen. [R2_MASKPENNOT]
        /// </summary>
        MaskPenNot = 5,

        /// <summary>
        /// The pixel is the inverse of the screen color. [R2_NOT]
        /// </summary>
        Not = 6,

        /// <summary>
        /// The pixel is a combination of the colors in the pen and in the screen, but not in both. [R2_XORPEN]
        /// </summary>
        XOrPen = 7,

        /// <summary>
        /// Inverse of the result of MaskPen. [R2_NOTMASKPEN]
        /// </summary>
        NotMaskPen = 8,

        /// <summary>
        /// The pixel is a combination of the colors that are common to both the pen and the screen. [R2_MASKPEN]
        /// </summary>
        MaskPen = 9,

        /// <summary>
        /// Inverse of the result of XOrPen. [R2_NOTXORPEN]
        /// </summary>
        NotXOrPen = 10,

        /// <summary>
        /// The pixel remains unchanged. [R2_NOP]
        /// </summary>
        Nop = 11,

        /// <summary>
        /// The pixel is a combination of the screen color and the inverse of the pen color. [R2_MERGENOTPEN]
        /// </summary>
        MergeNotPen = 12,

        /// <summary>
        /// The pixel always has the color of the pen. [R2_COPYPEN]
        /// </summary>
        CopyPen = 13,

        /// <summary>
        /// The pixel is a combination of the pen color and the inverse of the screen color. [R2_MERGEPENNOT]
        /// </summary>
        MergePenNot = 14,

        /// <summary>
        /// The pixel is a combination of the pen color and the screen color. [R2_MERGEPEN]
        /// </summary>
        MergePen = 15,

        /// <summary>
        /// The pixel is always drawn as white. [R2_WHITE]
        /// </summary>
        White = 16
    }
}
