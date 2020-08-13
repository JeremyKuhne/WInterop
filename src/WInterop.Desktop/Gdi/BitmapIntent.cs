// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum BitmapIntent : uint
    {
        /// <summary>
        ///  Maintains saturation. [LCS_GM_BUSINESS]
        /// </summary>
        Business = 0x00000001,

        /// <summary>
        ///  Maintains colorimetric match. [LCS_GM_GRAPHICS]
        /// </summary>
        Graphics = 0x00000002,

        /// <summary>
        ///  Maintains contrast. [LCS_GM_IMAGES]
        /// </summary>
        Images = 0x00000004,

        /// <summary>
        ///  Maintains the white point. [LCS_GM_ABS_COLORIMETRIC]
        /// </summary>
        AbsoluteColorimetric = 0x00000008
    }
}
