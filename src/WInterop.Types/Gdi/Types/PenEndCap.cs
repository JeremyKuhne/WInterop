// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    public enum PenEndCap : uint
    {
        /// <summary>
        /// [PS_ENDCAP_ROUND]
        /// </summary>
        Round = 0x00000000,

        /// <summary>
        /// [PS_ENDCAP_SQUARE]
        /// </summary>
        Square = 0x00000100,

        /// <summary>
        /// [PS_ENDCAP_FLAT]
        /// </summary>
        Flat = 0x00000200
    }
}
