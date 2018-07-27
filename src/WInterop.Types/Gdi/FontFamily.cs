// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    /// Used to describe the desired font in a general way.
    /// </summary>
    public enum FontFamilyType : byte
    {
        // FontFamily collides with System.Drawing.FontFamily

        // https://msdn.microsoft.com/en-us/library/cc250389.aspx
        // https://msdn.microsoft.com/en-us/library/dd145132.aspx

        /// <summary>
        /// Use the default font. (FF_DONTCARE)
        /// </summary>
        DoNotCare = 0 << 4,

        /// <summary>
        /// Proportional with serifs. (FF_ROMAN)
        /// </summary>
        Roman = 1 << 4,

        /// <summary>
        /// Proportional without serifs. (FF_SWISS)
        /// </summary>
        Swiss = 2 << 4,

        /// <summary>
        /// Fixed width. (FF_MODERN)
        /// </summary>
        Modern = 3 << 4,

        /// <summary>
        /// Handwriting style. (FF_SCRIPT)
        /// </summary>
        Script = 4 << 4,

        /// <summary>
        /// Novelty (such as "Old English"). (FF_DECORATIVE)
        /// </summary>
        Decorative = 5 << 4
    }
}
