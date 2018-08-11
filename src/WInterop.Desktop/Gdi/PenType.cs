// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum PenType : uint
    {
        /// <summary>
        /// [PS_USERSTYLE]
        /// </summary>
        UserStyle = 7,

        /// <summary>
        /// [PS_COSMETIC]
        /// </summary>
        Cosmetic = 0x00000000,

        /// <summary>
        /// [PS_GEOMETRIC]
        /// </summary>
        Geometric = 0x00010000
    }
}
